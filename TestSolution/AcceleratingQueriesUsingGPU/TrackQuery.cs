/*
CUDAfy.NET - LGPL 2.1 License
Please consider purchasing a commerical license - it helps development, frees you from LGPL restrictions
and provides you with support.  Thank you!
Copyright (C) 2013 Hybrid DSP Systems
http://www.hybriddsp.com

This library is free software; you can redistribute it and/or
modify it under the terms of the GNU Lesser General Public
License as published by the Free Software Foundation; either
version 2.1 of the License, or (at your option) any later version.

This library is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
Lesser General Public License for more details.

You should have received a copy of the GNU Lesser General Public
License along with this library; if not, write to the Free Software
Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301  USA
*/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Cudafy;
using Cudafy.Host;
using Cudafy.Translator;

namespace TestSolution.AcceleratingQueriesUsingGPU
{
    /// <summary>
    /// Implements means of querying GPS points on a GPU.
    /// </summary>
    public class TrackQuery
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TrackQuery"/> class.
        /// </summary>
        public TrackQuery(GPGPU gpu)
        {
            var props = gpu.GetDeviceProperties();
            var name = props.PlatformName + props.Name;
            name = CleanFileName(name);
            // Does an already serialized valid cudafy module xml file exist. If so
            // no need to re-generate it.
            var mod = CudafyModule.TryDeserialize(name);
            // The gpu can be either a CudaGPU or an OpenCLDevice. Realize that an NVIDIA GPU can be both!
            // And an Intel CPU can show up as both an AMD and an Intel OpenCL device if both OpenCL SDKs are 
            // installed.
            bool fromFile = true;
            CudafyTranslator.Language = (gpu is CudaGPU) ? eLanguage.Cuda : eLanguage.OpenCL;            
            if (mod == null || !mod.TryVerifyChecksums())
            {
                // Convert the .NET code into either CUDA C or OpenCL C.
                mod = CudafyTranslator.Cudafy(typeof(TrackPoint), typeof(TrackQuery));
                // Store to file for re-use
                mod.Serialize(name);
                fromFile = false;
            }
            try
            {
                // Load the module on to the device
                gpu.LoadModule(mod);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("LoadModule failed");
                Debug.WriteLine(mod.CompilerOutput);
                // Try again...
                if (fromFile)
                {
                    Debug.WriteLine("Try Cudafying again");
                    mod = CudafyTranslator.Cudafy(typeof(TrackPoint), typeof(TrackQuery));
                    gpu.LoadModule(mod);
                }
                else
                    throw;
            }
            _gpu = gpu;
        }

        private string CleanFileName(string fileName)
        {
            return InvalidNameChars().Aggregate(fileName, (current, c) => current.Replace(c.ToString(), string.Empty));
        }

        private IEnumerable<char> InvalidNameChars()
        {
            var chars = new List<char>(Path.GetInvalidFileNameChars());
            chars.AddRange(new char[] { ' ', ')', '(', '.', '@' });
            return chars;
        }
        
        /// <summary>
        /// Pointer to an array of GPS points.
        /// </summary>
        private TrackPoint[] _trackPoints = new TrackPoint[0];

        /// <summary>
        /// Pointer to an array of GPS points on the device.
        /// </summary>
        private TrackPoint[] _trackPoints_dev;

        /// <summary>
        /// Current number of points.
        /// </summary>
        private int _currentLength = 0;

        /// <summary>
        /// An array that will contain the indexes of the selected targets.
        /// </summary>
        private byte[] _indexes = new byte[0];

        /// <summary>
        /// An array that will contain the indexes of the selected targets on the device.
        /// </summary>
        private byte[] _indexes_dev;


        /// <summary>
        /// Handle to the GPU device.
        /// </summary>
        private GPGPU _gpu;

        public GPGPU GPU
        {
            get { return _gpu; }
        }

        public const int ciMAXTARGETPOINTS = 16;

        private const int ciTHREADSPERBLOCK = 256;

        /// <summary>
        /// We will store the target points in the GPU's constant memory.
        /// This is a small but very fast, read-only memory.
        /// </summary>
        [Cudafy]
        public static TrackPoint[] _targets = new TrackPoint[ciMAXTARGETPOINTS];

        /// <summary>
        /// Loads the specified GPS points to the GPU.
        /// </summary>
        /// <param name="trackPoints">The track points.</param>
        public void LoadTrack(TrackPoint[] trackPoints)
        {
            _currentLength = trackPoints.Length;
            // If the current number of points on the GPU are less than the number of points
            // we want to load then we resize the allocated memory on the GPU. We simply free
            // the existing memory and allocate new memory. We need arrays on the GPU to hold
            // the track points and the selected indexes. We make an array on the host to hold
            // the returned indexes.
            if (_trackPoints.Length < trackPoints.Length)
            {
                if (_trackPoints_dev != null)
                {
                    _gpu.Free(_trackPoints_dev);
                    _gpu.Free(_indexes_dev);
                }
                _trackPoints_dev = _gpu.Allocate(trackPoints);
                _indexes_dev = _gpu.Allocate<byte>(trackPoints.Length);
                _indexes = new byte[trackPoints.Length];
            }
            _trackPoints = trackPoints;
            // Copy the GPS points to the GPU.
            _gpu.CopyToDevice(trackPoints, 0, _trackPoints_dev, 0, trackPoints.Length);            
        }

        /// <summary>
        /// Selects the points that are within radius of the given targets between the specified times.
        /// </summary>
        /// <param name="targets">The targets.</param>
        /// <param name="radius">The radius in metres.</param>
        /// <param name="startTime">The start time.</param>
        /// <param name="endTime">The end time.</param>
        /// <returns>Points meeting the query criteria.</returns>
        public IEnumerable<TrackPoint> SelectPoints(TrackPoint[] targets, float radius, DateTime startTime, DateTime endTime, bool naive = false)
        {
            int blocksPerGrid;
            // Validate the parameters and calculate how many blocks of threads we will need on the GPU.
            // Each block of threads will execute ciTHREADSPERBLOCK threads.
            Initialize(radius, startTime, endTime, out blocksPerGrid);
            // Copy the targets to constant memory.
            _gpu.CopyToConstantMemory(targets, 0, _targets, 0, targets.Length);
            // Launch blocksPerGrid*ciTHREADSPERBLOCK threads in parallel. Each thread will test one GPS point against all targets.
            string functionToLaunch = "SelectPointsKernel" + (naive ? "Naive" : "");
            _gpu.Launch(blocksPerGrid, ciTHREADSPERBLOCK, functionToLaunch, _trackPoints_dev, _currentLength, 
                radius, startTime.Ticks, endTime.Ticks, _indexes_dev, targets.Length);
            // Copy the indexes array back from the GPU to the host and search for all points that have an index < 255.
            // These correspond to GPS points lying within the search criteria.
            _gpu.CopyFromDevice(_indexes_dev, 0, _indexes, 0, _currentLength);
            for (int i = 0; i < _currentLength; i++)
            {
                byte index = _indexes[i];
                if (index < 255)
                    yield return _trackPoints[i];
            }
        }

        /// <summary>
        /// This is the code that runs on the GPU.
        /// </summary>
        /// <param name="thread">An automatically generated passed in variable that identifies the thread.</param>
        /// <param name="track">The GPS points.</param>
        /// <param name="noPoints">The number of points (can be less than the size of the array on the device).</param>
        /// <param name="radius">The radius.</param>
        /// <param name="startTime">The start time.</param>
        /// <param name="endTime">The end time.</param>
        /// <param name="indexes">The indexes.</param>
        /// <param name="noTargets">The number of targets (can be less than the size of the array on the device).</param>
        [Cudafy]
        public static void SelectPointsKernel(GThread thread, TrackPoint[] track, int noPoints, 
            float radius, long startTime, long endTime, 
            byte[] indexes, int noTargets)
        {
            // Here we use another form of GPU memory called shared memory. This is shared between threads of a single block.
            // The size of the shared memory must be constant and here we set it to the number of threads per block.
            // This can be more efficient than the naive implementation below, however on the latest fermi architecture there is little
            // or no difference.
            byte[] cache = thread.AllocateShared<byte>("cache", ciTHREADSPERBLOCK);
            // Get the unique index of the thread: size of the block * block index + thread index.
            int tid = thread.blockDim.x * thread.blockIdx.x + thread.threadIdx.x;
            // Check we are not beyond the end of the points.
            if (tid < noPoints)
            {
                TrackPoint tp = track[tid];
                float minDist = Single.MaxValue;
                byte index = 255;
                if (tp.TimeStamp >= startTime && tp.TimeStamp <= endTime)
                {
                    for (int i = 0; i < noTargets; i++)
                    {
                        // get the target from constant memory
                        TrackPoint target = _targets[i];
                        // Calculate distance to target and if less than radius and current nearest target
                        // set minDist and index.
                        float d = DistanceTo(tp, target);
                        if (d <= radius && d < minDist)
                        {
                            minDist = d;
                            index = (byte)i;
                        }
                    }
                }
                // set the index.
                cache[thread.threadIdx.x] = index;
                // Synchronize all threads in block
                thread.SyncThreads();
                // Write the results into global memory array
                indexes[tid] = (byte)cache[thread.threadIdx.x];
            }
        }

        /// <summary>
        /// This implementation without shared memory can be less efficient on pre-Fermi architecture GPUs.
        /// </summary>
        /// <param name="thread">An automatically generated passed in variable that identifies the thread.</param>
        /// <param name="track">The GPS points.</param>
        /// <param name="noPoints">The number of points (can be less than the size of the array on the device).</param>
        /// <param name="radius">The radius.</param>
        /// <param name="startTime">The start time.</param>
        /// <param name="endTime">The end time.</param>
        /// <param name="indexes">The indexes.</param>
        /// <param name="noTargets">The number of targets (can be less than the size of the array on the device).</param>
        [Cudafy]
        public static void SelectPointsKernelNaive(GThread thread, TrackPoint[] track, int noPoints,
            float radius, long startTime, long endTime,
            byte[] indexes, int noTargets)
        {
            int tid = thread.blockDim.x * thread.blockIdx.x + thread.threadIdx.x;
            if (tid < noPoints)
            {
                TrackPoint tp = track[tid];
                float minDist = Single.MaxValue;
                byte index = 255;
                if (tp.TimeStamp >= startTime && tp.TimeStamp <= endTime)
                {
                    for (int i = 0; i < noTargets; i++)
                    {
                        TrackPoint target = _targets[i];
                        float d = DistanceTo(tp, target);
                        if (d <= radius && d < minDist)
                        {
                            minDist = d;
                            index = (byte)i;
                        }
                    }
                }

                indexes[tid] = index;
            }
        }

        /// <summary>
        /// All members of a struct are translated to CUDA C by default.
        /// This method calculates the distance between two GPS points.
        /// </summary>
        /// <param name="B">The second point.</param>
        /// <returns>Distance in metres.</returns>
        [Cudafy]
        public static float DistanceTo(TrackPoint A, TrackPoint B)
        {
            float dDistance = Single.MinValue;
            float dLat1InRad = A.Latitude * CONSTS.PI2;
            float dLong1InRad = A.Longitude * CONSTS.PI2;
            float dLat2InRad = B.Latitude * CONSTS.PI2;
            float dLong2InRad = B.Longitude * CONSTS.PI2;

            float dLongitude = dLong2InRad - dLong1InRad;
            float dLatitude = dLat2InRad - dLat1InRad;

            // Intermediate result a.
            float a = GMath.Pow(GMath.Sin(dLatitude / 2.0F), 2.0F) +
                       GMath.Cos(dLat1InRad) * GMath.Cos(dLat2InRad) *
                       GMath.Pow(GMath.Sin(dLongitude / 2.0F), 2.0F);

            // Intermediate result c (great circle distance in Radians).
            float c = 2.0F * GMath.Atan2(GMath.Sqrt(a), GMath.Sqrt(1.0F - a));

            // Distance
            dDistance = CONSTS.EARTHRADIUS * c;
            return dDistance * 1000;
        }

        /// <summary>
        /// Gets the bearing in degrees between two points.
        /// </summary>
        /// <param name="B">The second point.</param>
        /// <returns>Bearing in degrees.</returns>
        [Cudafy]
        public static float BearingTo(TrackPoint A, TrackPoint B)
        {
            float lat1 = A.Latitude * CONSTS.PI2;
            float lat2 = B.Latitude * CONSTS.PI2;
            float dLon = (B.Longitude - A.Longitude) * CONSTS.PI2;

            float y = GMath.Sin(dLon) * GMath.Cos(lat2);
            float x = GMath.Cos(lat1) * GMath.Sin(lat2) -
                    GMath.Sin(lat1) * GMath.Cos(lat2) * GMath.Cos(dLon);
            return (180 * GMath.Atan2(y, x)) / GMath.PI;
        }

        private void Initialize(float radius, DateTime startTime, DateTime endTime, out int blocksPerGrid)
        {
            blocksPerGrid = (_trackPoints.Length + (ciTHREADSPERBLOCK) - 1) / (ciTHREADSPERBLOCK);
            if (radius < 0)
                throw new ArgumentOutOfRangeException("radius");
            if (startTime > endTime)
                throw new ArgumentException("startTime must be less than endTime");
        }
    }
}
