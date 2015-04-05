/*
 * Modified by Yakov Danilov <yakodani@gmail.com> for Imazen LLC (Ported from Java to C#) 
 * Copyright 2002-2013 Drew Noakes
 *
 *    Licensed under the Apache License, Version 2.0 (the "License");
 *    you may not use this file except in compliance with the License.
 *    You may obtain a copy of the License at
 *
 *        http://www.apache.org/licenses/LICENSE-2.0
 *
 *    Unless required by applicable law or agreed to in writing, software
 *    distributed under the License is distributed on an "AS IS" BASIS,
 *    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *    See the License for the specific language governing permissions and
 *    limitations under the License.
 *
 * More information about this project is available at:
 *
 *    http://drewnoakes.com/code/exif/
 *    http://code.google.com/p/metadata-extractor/
 */
using System;
using Com.Drew.Lang;
using JetBrains.Annotations;
using Sharpen;

namespace Com.Drew.Lang
{
	/// <summary>Represents a latitude and longitude pair, giving a position on earth in spherical coordinates.</summary>
	/// <remarks>
	/// Represents a latitude and longitude pair, giving a position on earth in spherical coordinates.
	/// <p/>
	/// Values of latitude and longitude are given in degrees.
	/// <p/>
	/// This type is immutable.
	/// </remarks>
	public sealed class GeoLocation
	{
		private readonly double _latitude;

		private readonly double _longitude;

		/// <summary>
		/// Instantiates a new instance of
		/// <see cref="GeoLocation"/>
		/// .
		/// </summary>
		/// <param name="latitude">the latitude, in degrees</param>
		/// <param name="longitude">the longitude, in degrees</param>
		public GeoLocation(double latitude, double longitude)
		{
			_latitude = latitude;
			_longitude = longitude;
		}

		/// <returns>the latitudinal angle of this location, in degrees.</returns>
		public double GetLatitude()
		{
			return _latitude;
		}

		/// <returns>the longitudinal angle of this location, in degrees.</returns>
		public double GetLongitude()
		{
			return _longitude;
		}

		/// <returns>true, if both latitude and longitude are equal to zero</returns>
		public bool IsZero()
		{
			return _latitude == 0 && _longitude == 0;
		}

		/// <summary>
		/// Converts a decimal degree angle into its corresponding DMS (degrees-minutes-seconds) representation as a string,
		/// of format:
		/// <c>-1В° 23' 4.56"</c>
		/// </summary>
		[NotNull]
		public static string DecimalToDegreesMinutesSecondsString(double @decimal)
		{
			double[] dms = DecimalToDegreesMinutesSeconds(@decimal);
			DecimalFormat format = new DecimalFormat("0.##");
			return Sharpen.Extensions.StringFormat("%sВ° %s' %s\"", format.Format(dms[0]), format.Format(dms[1]), format.Format(dms[2]));
		}

		/// <summary>
		/// Converts a decimal degree angle into its corresponding DMS (degrees-minutes-seconds) component values, as
		/// a double array.
		/// </summary>
		[NotNull]
		public static double[] DecimalToDegreesMinutesSeconds(double @decimal)
		{
			int d = (int)@decimal;
			double m = Math.Abs((@decimal % 1) * 60);
			double s = (m % 1) * 60;
			return new double[] { d, (int)m, s };
		}

		/// <summary>
		/// Converts DMS (degrees-minutes-seconds) rational values, as given in
		/// <see cref="Com.Drew.Metadata.Exif.GpsDirectory"/>
		/// ,
		/// into a single value in degrees, as a double.
		/// </summary>
		[CanBeNull]
		public static double? DegreesMinutesSecondsToDecimal(Rational degs, Rational mins, Rational secs, bool isNegative)
		{
			double @decimal = Math.Abs(degs.DoubleValue()) + mins.DoubleValue() / 60.0d + secs.DoubleValue() / 3600.0d;
			if (double.IsNaN(@decimal))
			{
				return null;
			}
			if (isNegative)
			{
				@decimal *= -1;
			}
			return @decimal;
		}

		public override bool Equals(object o)
		{
			if (this == o)
			{
				return true;
			}
			if (o == null || GetType() != o.GetType())
			{
				return false;
			}
			Com.Drew.Lang.GeoLocation that = (Com.Drew.Lang.GeoLocation)o;
			if (Sharpen.Extensions.Compare(that._latitude, _latitude) != 0)
			{
				return false;
			}
			if (Sharpen.Extensions.Compare(that._longitude, _longitude) != 0)
			{
				return false;
			}
			return true;
		}

		public override int GetHashCode()
		{
			int result;
			long temp;
			temp = _latitude != +0.0d ? Sharpen.Extensions.DoubleToLongBits(_latitude) : 0L;
			result = (int)(temp ^ ((long)(((ulong)temp) >> 32)));
			temp = _longitude != +0.0d ? Sharpen.Extensions.DoubleToLongBits(_longitude) : 0L;
			result = 31 * result + (int)(temp ^ ((long)(((ulong)temp) >> 32)));
			return result;
		}

		/// <returns>
		/// a string representation of this location, of format:
		/// <c>1.23, 4.56</c>
		/// </returns>
		[NotNull]
		public override string ToString()
		{
			return _latitude + ", " + _longitude;
		}

		/// <returns>
		/// a string representation of this location, of format:
		/// <c>-1В° 23' 4.56", 54В° 32' 1.92"</c>
		/// </returns>
		[NotNull]
		public string ToDMSString()
		{
			return DecimalToDegreesMinutesSecondsString(_latitude) + ", " + DecimalToDegreesMinutesSecondsString(_longitude);
		}
	}
}
