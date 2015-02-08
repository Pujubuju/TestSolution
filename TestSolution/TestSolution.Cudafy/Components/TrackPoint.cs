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
using System.Runtime.InteropServices;
using Cudafy;

namespace TestSolution.Cudafy.Components
{
    /// <summary>
    /// A GPS Point structure that can be used on CPU and GPU. Use the Cudafy attribute.
    /// </summary>
    [Cudafy]
    [StructLayout(LayoutKind.Sequential)]
    public struct TrackPoint
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TrackPoint"/> struct.
        /// </summary>
        /// <param name="lon">The longitude.</param>
        /// <param name="lat">The latitude.</param>
        /// <param name="timeStamp">The time stamp.</param>
        [CudafyIgnore]
        public TrackPoint(float lon, float lat, long timeStamp)
        {
            Longitude = lon;
            Latitude = lat;
            TimeStamp = timeStamp;
        }

        /// <summary>
        /// Longitude in degrees.
        /// </summary>
        public float Longitude;

        /// <summary>
        /// Latitude in degrees.
        /// </summary>
        public float Latitude;

        /// <summary>
        /// Timestamp in ticks
        /// </summary>
        public long TimeStamp;

        /// <summary>
        /// Gets or sets the time. Since the GPU does not understand DateTime or properties
        /// we flag this to be ignored by using CudafyIgnore attribute.
        /// </summary>
        /// <value>
        /// The time.
        /// </value>
        [CudafyIgnore]
        public DateTime Time
        {
            get { return new DateTime(TimeStamp);  }
            set { TimeStamp = value.Ticks; }
        }

        #region Moved to TrackQuery because OpenCL does not support methods in structs.
        ///// <summary>
        ///// All members of a struct are translated to CUDA C by default.
        ///// This method calculates the distance between two GPS points.
        ///// </summary>
        ///// <param name="B">The second point.</param>
        ///// <returns>Distance in metres.</returns>
        //public float DistanceTo(TrackPoint B)
        //{
        //    float dDistance = Single.MinValue;
        //    float dLat1InRad = Latitude * CONSTS.PI2;
        //    float dLong1InRad = Longitude * CONSTS.PI2;
        //    float dLat2InRad = B.Latitude * CONSTS.PI2;
        //    float dLong2InRad = B.Longitude * CONSTS.PI2;
            
        //    float dLongitude = dLong2InRad - dLong1InRad;
        //    float dLatitude = dLat2InRad - dLat1InRad;

        //    // Intermediate result a.
        //    float a = GMath.Pow(GMath.Sin(dLatitude / 2.0F), 2.0F) +
        //               GMath.Cos(dLat1InRad) * GMath.Cos(dLat2InRad) *
        //               GMath.Pow(GMath.Sin(dLongitude / 2.0F), 2.0F);

        //    // Intermediate result c (great circle distance in Radians).
        //    float c = 2.0F * GMath.Atan2(GMath.Sqrt(a), GMath.Sqrt(1.0F - a));

        //    // Distance
        //    dDistance = CONSTS.EARTHRADIUS * c;
        //    return dDistance * 1000;
        //}

        ///// <summary>
        ///// Gets the bearing in degrees between two points.
        ///// </summary>
        ///// <param name="B">The second point.</param>
        ///// <returns>Bearing in degrees.</returns>
        //public float BearingTo(TrackPoint B)
        //{
        //    float lat1 = Latitude * CONSTS.PI2;
        //    float lat2 = B.Latitude * CONSTS.PI2;
        //    float dLon = (B.Longitude - Longitude) * CONSTS.PI2;

        //    float y = GMath.Sin(dLon) * GMath.Cos(lat2);
        //    float x = GMath.Cos(lat1) * GMath.Sin(lat2) -
        //            GMath.Sin(lat1) * GMath.Cos(lat2) * GMath.Cos(dLon);
        //    return (180 * GMath.Atan2(y, x)) / GMath.PI;
        //}
        #endregion

        /// <summary>
        /// Gets the great circle coordinate at the specified bearing and at a distance calculated from 
        /// the given speed and time span.
        /// </summary>
        /// <param name="bearing">The bearing in degrees.</param>
        /// <param name="speed">The speed in metres/second.</param>
        /// <param name="dTime">The time in ticks.</param>
        /// <returns></returns>
        [CudafyIgnore]
        public TrackPoint GetGreatCircleCoordinateAt(float bearing, float speed, long dTime)
        {
            float R = CONSTS.EARTHRADIUS;
            float distance = speed * (dTime / 10000000);
            float d = (distance / 1000);
            float brng = ToRad(bearing);
            float lon1 = ToRad(Longitude);
            float lat1 = ToRad(Latitude);

            float lat2 = GMath.Asin(GMath.Sin(lat1) * GMath.Cos(d / R) +
                      GMath.Cos(lat1) * GMath.Sin(d / R) * GMath.Cos(brng));
            float lon2 = lon1 + GMath.Atan2(GMath.Sin(brng) * GMath.Sin(d / R) * GMath.Cos(lat1),
                                         GMath.Cos(d / R) - GMath.Sin(lat1) * GMath.Sin(lat2));
            if (lon2 > GMath.PI)
                lon2 -= GMath.PI * 2;
            else if (lon2 <= -GMath.PI)
                lon2 += GMath.PI * 2;

            lon2 = FromRad(lon2);
            lat2 = FromRad(lat2);

            long newTime = TimeStamp + dTime;
            return new TrackPoint(lon2, lat2, newTime);
        }

        /// <summary>
        /// Converts to radians.
        /// </summary>
        /// <param name="x">Angle in degrees.</param>
        /// <returns>Angle in radians.</returns>
        [CudafyIgnore]
        private float ToRad(float x)
        {
            return x / 180 * GMath.PI;
        }
        /// <summary>
        /// Converts from radians.
        /// </summary>
        /// <param name="x">Angle in radians.</param>
        /// <returns>Angle in radians.</returns>
        [CudafyIgnore]
        private float FromRad(float x)
        {
            return x / GMath.PI * 180;
        }
    }

    /// <summary>
    /// Some useful constants we can freely use on CPU and GPU.
    /// </summary>
    public static class CONSTS
    {
        /// <summary>
        /// Radius of the earth in kilometres.
        /// </summary>
        public const float EARTHRADIUS = 6376.5F;

        /// <summary>
        /// PI / 180.
        /// </summary>
        public const float PI2 = (float)Math.PI / 180.0F;
    }
}
