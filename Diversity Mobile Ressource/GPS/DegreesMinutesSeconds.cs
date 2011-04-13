//#######################################################################
//Diversity Mobile Synchronization
//Project Homepage:  http://www.diversitymobile.net
//Copyright (C) 2011  Tobias Schneider, Lehrstuhl Angewndte Informatik IV, Universität Bayreuth
//
//This program is free software; you can redistribute it and/or modify
//it under the terms of the GNU General Public License as published by
//the Free Software Foundation; either version 2 of the License, or
//(at your option) any later version.
//
//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.
//
//You should have received a copy of the GNU General Public License along
//with this program; if not, write to the Free Software Foundation, Inc.,
//51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA.
//#######################################################################


using System;

namespace UBT.AI4.Bio.DiversityCollection.Ressource.GPS
{
    /// <summary>
    /// class that represents a gps coordinate in degrees, minutes, and seconds.  
    /// </summary>
    public class DegreesMinutesSeconds
    {

        bool isPositive;
        /// <summary>
        /// Returns true if the degrees, minutes and seconds refer to a positive value,
        /// false otherwise.
        /// </summary>
        public bool IsPositive
        {
            get { return isPositive; }
        }

        uint degrees;
        /// <summary>
        /// The degrees unit of the coordinate
        /// </summary>
        public uint Degrees
        {
            get { return degrees; }
        }

        uint minutes;
        /// <summary>
        /// The minutes unit of the coordinate
        /// </summary>
        public uint Minutes
        {
            get { return minutes; }
        }

        double seconds;
        /// <summary>
        /// The seconds unit of the coordinate
        /// </summary>
        public double Seconds
        {
            get { return seconds; }
        }

        /// <summary>
        /// Constructs a new instance of DegreesMinutesSeconds converting 
        /// from decimal degrees
        /// </summary>
        /// <param name="decimalDegrees">Initial value as decimal degrees</param>
        public DegreesMinutesSeconds(double decimalDegrees)
        {
            isPositive = (decimalDegrees > 0);
            
            degrees = (uint) Math.Abs(decimalDegrees);
            
            double doubleMinutes = (Math.Abs(decimalDegrees) - Math.Abs((double)degrees)) * 60.0;
            minutes = (uint) doubleMinutes;

            seconds = (doubleMinutes - (double)minutes) * 60.0;
        }

        /// <summary>
        /// Constructs a new instance of DegreesMinutesSeconds
        /// </summary>
        /// <param name="isPositive">True if the coordinates are positive coordinate, false if they
        /// are negative coordinates.</param>
        /// <param name="degrees">Degrees unit of the coordinate</param>
        /// <param name="minutes">Minutes unit of the coordinate</param>
        /// <param name="seconds">Seconds unit of the coordinate. This should be a positive value.</param>
        public DegreesMinutesSeconds(bool isPositive, uint degrees, uint minutes, double seconds)
        {
            this.isPositive = isPositive;
            this.degrees = degrees;
            this.minutes = minutes;
            this.seconds = seconds;
        }

        /// <summary>
        /// Converts the decimal, minutes, seconds coordinate to 
        /// decimal degrees
        /// </summary>
        /// <returns></returns>
        public double ToDecimalDegrees()
        {
            double val = (double)degrees + ((double)minutes / 60.0) + ((double)seconds / 3600.0);
            val = isPositive ? val : val * -1;
            return val;
        }

        /// <summary>
        /// Converts the instance to a string in format: D M' S"
        /// </summary>
        /// <returns>string representation of degrees, minutes, seconds</returns>
        public override string ToString()
        {
            return degrees + "d " + minutes + "' " + seconds + "\"";
        }
    }
}
