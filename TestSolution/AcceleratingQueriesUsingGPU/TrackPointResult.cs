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

namespace TestSolution.AcceleratingQueriesUsingGPU
{
    /// <summary>
    /// We could choose to return track points and the nearest target. This is easily achieved by modifying the Track
    /// class's SelectPoints method.
    /// </summary>
    public struct TrackPointResult
    {
        public TrackPointResult(TrackPoint point, TrackPoint target)
        {
            Point = point;
            Target = target;
        }
        
        public TrackPoint Point;
        public TrackPoint Target;
    }
}
