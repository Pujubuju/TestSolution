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
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Cudafy;
using Cudafy.Host;

namespace TestSolution.AcceleratingQueriesUsingGPU
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            nudNoTargets.Maximum = TrackQuery.ciMAXTARGETPOINTS;
            string sAdding = "Adding ";
            Splasher.Status = sAdding + csLINQ;
            cobTarget.Items.Add(csLINQ);
            chartPerformance.Series.Add(csLINQ);
            chartPerformance.Series[csLINQ].ChartType = SeriesChartType.Bar;

            Splasher.Status = sAdding + csPLINQ;            
            cobTarget.Items.Add(csPLINQ);
            chartPerformance.Series.Add(csPLINQ);
            chartPerformance.Series[csPLINQ].ChartType = SeriesChartType.Bar;

            List<GPGPU> gpuList = new List<GPGPU>();
            try
            {
                for (int j = 0; j < 2; j++)
                {
                    eGPUType gpuType = j == 0 ? eGPUType.Cuda : eGPUType.OpenCL;
                    int cnt = 0;
                    Splasher.Status = string.Format("Enumerating {0} devices", gpuType);
                    try
                    {
                        cnt = CudafyHost.GetDeviceCount(gpuType); 
                        if (cnt == 0)
                        {
                            MessageBox.Show(string.Format("Failed to find any devices for {0}", gpuType));
                            continue;
                        }
                    }
                    catch (Exception deviceCntEx)
                    {
                        MessageBox.Show(string.Format("Failed to get device count for {0}\r\nError:\r\n{1}", gpuType, deviceCntEx.Message));
                        continue;
                    }
                    for (int i = 0; i < cnt; i++)
                    {
                        try
                        {
                            var gpu = CudafyHost.GetDevice(gpuType, i);
                            string name = GetFullName(gpu);

                            Splasher.Status = sAdding + name;
                            TrackQuery track = new TrackQuery(gpu);
                            _trackQueries.Add(name, track);
                            cobTarget.Items.Add(name);
                            chartPerformance.Series.Add(name);
                            chartPerformance.Series[name].ChartType = SeriesChartType.Bar;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                    }
                }
            }
            finally
            {
                cobTarget.SelectedIndex = 0;
                Splasher.Close();
            }
        }

        private string GetFullName(GPGPU gpu)
        {
            GPGPUProperties prop = gpu.GetDeviceProperties();
            return ((gpu is OpenCLDevice) ? "OpenCL-" : "CUDA-") + prop.PlatformName + "-" + prop.Name;
        }

        private const string csLINQ = "LINQ";

        private const string csPLINQ = "PLINQ";

        private const string csNAME = "AcceleratingQueriesUsingGPU";

        private TrackPoint[] _track = new TrackPoint[0];
       
        private List<TrackPoint> _targets = new List<TrackPoint>();

        #region GPU

        private Dictionary<string, TrackQuery> _trackQueries = new Dictionary<string, TrackQuery>();

        /// <summary>
        /// Loads the track on to the GPU.
        /// </summary>
        private void LoadTrack()
        {
            Stopwatch sw = Stopwatch.StartNew();
            foreach (TrackQuery trackQuery in _trackQueries.Values)
            {
                trackQuery.LoadTrack(_track);
            }
            long time = sw.ElapsedMilliseconds;
            SetStatusStrip(string.Format("Track uploaded to all GPUs in {0}ms", time));
        }

        private void SetStatusStrip(string text)
        {
            Invoke(new Action(() =>
            {
                statusStrip.Items[0].Text = text; 
                statusStrip.Invalidate();
                statusStrip.Update();
                statusStrip.Refresh();
                Application.DoEvents();
            }));
        }

        private void TestGPU(bool naive)
        {
            var startTime = dtpStartTime.Value;
            var endTime = dtpEndTime.Value;
            // Get the radius in metres around each target.
            float radius = Convert.ToSingle(tbRadius.Text);

            // Get selected GPU
            string target = cobTarget.SelectedItem.ToString();
            TrackQuery trackQuery = _trackQueries.Where(kvp => kvp.Key == target).FirstOrDefault().Value;
            string ts = string.Format("Running using {0}", target);
            SetStatusStrip(ts);
            // Time the selecting and counting of the points that lie with radius of the targets.
            Stopwatch sw = Stopwatch.StartNew();
            var pnts = trackQuery.SelectPoints(_targets.ToArray(), radius, startTime, endTime, naive);
            int cnt = pnts.Count();
            long time = sw.ElapsedMilliseconds;
            chartPerformance.Series[target].Points.Clear();
            chartPerformance.Series[target].Points.AddY(time);
            ts = string.Format("Found {0} points in {1}ms using {2}", cnt, time, target);
            SetStatusStrip(ts);
        }

        #endregion

        #region Generate test route

        public void GenerateRoute()
        {
            int noPoints = (int)Invoke(new Func<int>(() => { return Convert.ToInt32(tbNoPoints.Text); }));
            if(noPoints < 1 || noPoints > 10000000)
                throw new ArgumentOutOfRangeException("Number of points must be between 1 and 10000000.");
            _track = new TrackPoint[noPoints];

            // Create a starting point at 5 degrees east and 45 degrees north. It will be a random route, so 
            // we set some values for generating this: max speed, current speed, current bearing.
            var tp = new TrackPoint(5, 45, DateTime.Now.Ticks);// longitute, latitude, timestamp in ticks
            float maxSpeed = _randSpeed.Next(30) + 50;
            float currentSpeed = GetNewSpeed(_randSpeed.Next((int)maxSpeed), maxSpeed); // m/s
            float bearing = (float)_randSpeed.NextDouble() * 360;
            float w = 0, e=0, n=0, s=0;
            for (int i = 0; i < noPoints; i++)
            {
                int clockwiseCtr = _randSpeed.Next(30) + 5;
                // Get the next point at current bearing, speed and one second later (time is in ticks).
                tp = tp.GetGreatCircleCoordinateAt(bearing, currentSpeed, 10000000);
                // For the record let's keep track of the bounds of the track (max west, east, north, south).
                if (i == 0)
                {
                    n = tp.Latitude;
                    w = tp.Longitude;
                    e = tp.Longitude;
                    s = tp.Latitude;
                }
                else
                {
                    if (tp.Longitude < w)
                        w = tp.Longitude;
                    else if (tp.Longitude > e)
                        e = tp.Longitude;
                    if (tp.Latitude < s)
                        s = tp.Latitude;
                    else if (tp.Latitude > n)
                        n = tp.Latitude;
                }
                // Add point
                _track[i] = tp;
                // Calculate new bearing and speed based on current settings.
                bearing = GetNewBearing(bearing, currentSpeed, maxSpeed, i % clockwiseCtr == 0);
                currentSpeed = GetNewSpeed(currentSpeed, maxSpeed);
            }
            // Update the target generation settings in the UI.
            var startTime = _track[0].Time;
            var endTime = _track[_track.Length - 1].Time;

            Invoke(new Action(() => { dtpStartTime.Value = startTime; }));

            Invoke(new Action(() => { dtpEndTime.Value = endTime; }));
            
            Invoke(new Action(() => { lblTimeRange.Text = string.Format("Time range: {0} -> {1}", startTime, endTime); }));

            string areaStr = string.Format("{0},{1} -> {2},{3}", w.ToString("0.0"), n.ToString("0.0"), e.ToString("0.0"), s.ToString("0.0"));
            Invoke(new Action(() => { lblAreaRange.Text = string.Format("Area range: {0}", areaStr); }));
        }


        private Random _randSpeed = new Random();

        private bool accelerating = true;

        private float GetNewSpeed(float currentSpeed, float maxSpeed)
        {
            float ds = (float)_randSpeed.NextDouble() * 5.0F;
            if (!accelerating)
                ds *= -1;
            float newSpeed = currentSpeed + ds;
            if (newSpeed > maxSpeed)
                accelerating = false;
            if (newSpeed < 0)
            {
                accelerating = true;
                newSpeed = 0;
            }
            return newSpeed;
        }

        private float GetNewBearing(float currentBearing, float currentSpeed, float maxSpeed, bool clockwise)
        {
            float ds = currentSpeed > 0 ? (float)_randSpeed.NextDouble() * (maxSpeed / currentSpeed) : 0;
            if (!clockwise)
                ds *= -1;
            float newBearing = currentBearing + ds;
            if (newBearing > 360)
                newBearing -= 360;
            else if(newBearing < 0)
                newBearing += 360;

            return newBearing;
        }

        private Action _action;

        private void btnGenerateRoute_Click(object sender, EventArgs e)
        {
            if (_action != null)
                return;
            //_targetChanged = true;
            btnGenerateRoute.Enabled = false;
            btnRun.Enabled = false;
            btnGenerateTargets.Enabled = false;
            _action = new Action(GenerateRoute);
            _action.BeginInvoke(new AsyncCallback((res) =>
            {
                try
                {
                    _action.EndInvoke(res);

                    Invoke(new Action(() =>
                    {
                        // You need to initialize the GPU on the UI thread, unless we enable multithreading.
                        // Doing this on another thread will cause an invalid context error later.
                        // Let's keep things simple!
                        LoadTrack();
                        foreach (var series in chartPerformance.Series)
                            series.Points.Clear();
                        btnGenerateTargets.Enabled = true;
                    }));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
                finally
                {
                    Invoke(new Action(() =>
                    {
                        btnGenerateRoute.Enabled = true;
                    }));
                    _action = null;
                }
                
            }), null);
        }

        private void btnGenerateTargets_Click(object sender, EventArgs e)
        {
            if (_track.Length == 0)
            {
                MessageBox.Show("Generate route first!");
                return;
            }
            _targets.Clear();
            int noTargets = (int)nudNoTargets.Value;
            Random rand = new Random();

            for (int i = 0; i < noTargets; i++)
                _targets.Add(_track[rand.Next(_track.Length)]);

            foreach (var series in chartPerformance.Series)
                series.Points.Clear();

            SetStatusStrip(string.Format("{0} targets made. Select device and run.", noTargets));
            btnRun.Enabled = true;

        }

        private const string csTARGETSCHANGED = "Target settings have been changed. Generate targets again.";

        private void dtpStartTime_ValueChanged(object sender, EventArgs e)
        {
            var dtp = sender as DateTimePicker;
            lblDate.Text = dtp.Value.ToShortDateString();
            SetStatusStrip(csTARGETSCHANGED);
        }

        private void dtpEndTime_ValueChanged(object sender, EventArgs e)
        {
            var dtp = sender as DateTimePicker;
            lblEndTime.Text = dtp.Value.ToShortDateString();
            SetStatusStrip(csTARGETSCHANGED);
        }

        private void tbRadius_TextChanged(object sender, EventArgs e)
        {
            SetStatusStrip(csTARGETSCHANGED);
        }

        private void nudNoTargets_ValueChanged(object sender, EventArgs e)
        {
            SetStatusStrip(csTARGETSCHANGED);
        }

        #endregion

        #region Test Linq and PLinq

        /// <summary>
        /// Runs the query on the CPU.
        /// </summary>
        /// <param name="parallel">if set to <c>true</c> then use PLinq, else use Linq.</param>
        private void TestCPU(bool parallel)
        {
            var startTime = dtpStartTime.Value;
            var endTime = dtpEndTime.Value;
            string target = parallel ? csPLINQ : csLINQ;
            float radius = Convert.ToSingle(tbRadius.Text);
            string ts = string.Format("Running using {0}", target);
            SetStatusStrip(ts);
            Stopwatch sw = Stopwatch.StartNew();
            var pnts = GetPoints(_targets.ToArray(), radius, startTime, endTime, parallel);
            int cnt = pnts.Count();
            long time = sw.ElapsedMilliseconds;
            chartPerformance.Series[target].Points.Clear();
            chartPerformance.Series[target].Points.AddY(time);
            ts = string.Format("Found {0} points in {1}ms using {2}", cnt, time, target);
            SetStatusStrip(ts);
        }

        private IEnumerable<TrackPoint> GetPoints(TrackPoint[] targets, float radius, DateTime startTime, DateTime endTime, bool parallel)
        {
            long targetStartTime = startTime.Ticks;
            long targetEndTime = endTime.Ticks;
            // Running the query in parallel is as easy as adding AsParallel(). We're not concerned with the ordering, so it's ideal.
            if(parallel)
                return _track.AsParallel().Where(tp => GetNearestTargetIndex(tp, targets, radius, targetStartTime, targetEndTime) < 255);
            else
                return _track.Where(tp => GetNearestTargetIndex(tp, targets, radius, targetStartTime, targetEndTime) < 255);
        }

        /// <summary>
        /// Gets the index of the nearest target.
        /// </summary>
        /// <param name="tp">The point in the track to test against.</param>
        /// <param name="targets">Array of targets.</param>
        /// <param name="radius">The radius.</param>
        /// <param name="targetStartTime">The target start time.</param>
        /// <param name="targetEndTime">The target end time.</param>
        /// <returns>Returns index of nearest target or 255 if none found.</returns>
        private byte GetNearestTargetIndex(TrackPoint tp, TrackPoint[] targets, float radius, long targetStartTime, long targetEndTime)
        {
            float minDist = Single.MaxValue;
            byte index = 255; 
            // If we're not within the time range then no need to look further.
            if (tp.TimeStamp >= targetStartTime && tp.TimeStamp <= targetEndTime)
            {
                int noTargets = targets.Length;
                // Go through the targets and get the index of the closest one.
                for (int i = 0; i < noTargets; i++)
                {
                    TrackPoint target = targets[i];
                    float d = TrackQuery.DistanceTo(tp, target);
                    if (d <= radius && d < minDist)
                    {
                        minDist = d;
                        index = (byte)i;
                    }
                }
                }
            return index;
        }

        #endregion

        private void btnRun_Click(object sender, EventArgs e)
        {
            string device = cobTarget.SelectedItem as String;
            if (device == csLINQ)
            {
                TestCPU(false);
            }
            else if (device == csPLINQ)
            {
                TestCPU(true);
            }
            else 
            {
                TestGPU(true);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Visit www.hybriddsp.com to get the latest CUDAfy SDK?", "Question", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                SafeSurf("http://www.hybriddsp.com");
            }
        }

        private void SafeSurf(string url)
        {
            try
            {
                Process.Start(url);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }
}
