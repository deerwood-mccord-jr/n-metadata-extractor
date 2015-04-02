/*
 * Copyright 2002-2015 Drew Noakes
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
 *    https://drewnoakes.com/code/exif/
 *    https://github.com/drewnoakes/metadata-extractor
 */
using Com.Drew.Lang;
using Com.Drew.Metadata;
using JetBrains.Annotations;
using Sharpen;

namespace Com.Drew.Metadata.Exif
{
	/// <summary>
	/// Provides human-readable string representations of tag values stored in a
	/// <see cref="GpsDirectory"/>
	/// .
	/// </summary>
	/// <author>Drew Noakes https://drewnoakes.com</author>
	public class GpsDescriptor : TagDescriptor<GpsDirectory>
	{
		public GpsDescriptor([NotNull] GpsDirectory directory)
			: base(directory)
		{
		}

		[CanBeNull]
		public override string GetDescription(int tagType)
		{
			switch (tagType)
			{
				case GpsDirectory.TagVersionId:
				{
					return GetGpsVersionIdDescription();
				}

				case GpsDirectory.TagAltitude:
				{
					return GetGpsAltitudeDescription();
				}

				case GpsDirectory.TagAltitudeRef:
				{
					return GetGpsAltitudeRefDescription();
				}

				case GpsDirectory.TagStatus:
				{
					return GetGpsStatusDescription();
				}

				case GpsDirectory.TagMeasureMode:
				{
					return GetGpsMeasureModeDescription();
				}

				case GpsDirectory.TagSpeedRef:
				{
					return GetGpsSpeedRefDescription();
				}

				case GpsDirectory.TagTrackRef:
				case GpsDirectory.TagImgDirectionRef:
				case GpsDirectory.TagDestBearingRef:
				{
					return GetGpsDirectionReferenceDescription(tagType);
				}

				case GpsDirectory.TagTrack:
				case GpsDirectory.TagImgDirection:
				case GpsDirectory.TagDestBearing:
				{
					return GetGpsDirectionDescription(tagType);
				}

				case GpsDirectory.TagDestDistanceRef:
				{
					return GetGpsDestinationReferenceDescription();
				}

				case GpsDirectory.TagTimeStamp:
				{
					return GetGpsTimeStampDescription();
				}

				case GpsDirectory.TagLongitude:
				{
					// three rational numbers -- displayed in HH"MM"SS.ss
					return GetGpsLongitudeDescription();
				}

				case GpsDirectory.TagLatitude:
				{
					// three rational numbers -- displayed in HH"MM"SS.ss
					return GetGpsLatitudeDescription();
				}

				case GpsDirectory.TagDifferential:
				{
					return GetGpsDifferentialDescription();
				}

				default:
				{
					return base.GetDescription(tagType);
				}
			}
		}

		[CanBeNull]
		private string GetGpsVersionIdDescription()
		{
			return GetVersionBytesDescription(GpsDirectory.TagVersionId, 1);
		}

		[CanBeNull]
		public virtual string GetGpsLatitudeDescription()
		{
			GeoLocation location = _directory.GetGeoLocation();
			return location == null ? null : GeoLocation.DecimalToDegreesMinutesSecondsString(location.GetLatitude());
		}

		[CanBeNull]
		public virtual string GetGpsLongitudeDescription()
		{
			GeoLocation location = _directory.GetGeoLocation();
			return location == null ? null : GeoLocation.DecimalToDegreesMinutesSecondsString(location.GetLongitude());
		}

		[CanBeNull]
		public virtual string GetGpsTimeStampDescription()
		{
			// time in hour, min, sec
			Rational[] timeComponents = _directory.GetRationalArray(GpsDirectory.TagTimeStamp);
			DecimalFormat df = new DecimalFormat("00.00");
			return timeComponents == null ? null : Sharpen.Extensions.StringFormat("%02d:%02d:%s UTC", timeComponents[0].IntValue(), timeComponents[1].IntValue(), df.Format(timeComponents[2].DoubleValue()));
		}

		[CanBeNull]
		public virtual string GetGpsDestinationReferenceDescription()
		{
			string value = _directory.GetString(GpsDirectory.TagDestDistanceRef);
			if (value == null)
			{
				return null;
			}
			string distanceRef = Sharpen.Extensions.Trim(value);
			if (Sharpen.Runtime.EqualsIgnoreCase("K", distanceRef))
			{
				return "kilometers";
			}
			else
			{
				if (Sharpen.Runtime.EqualsIgnoreCase("M", distanceRef))
				{
					return "miles";
				}
				else
				{
					if (Sharpen.Runtime.EqualsIgnoreCase("N", distanceRef))
					{
						return "knots";
					}
					else
					{
						return "Unknown (" + distanceRef + ")";
					}
				}
			}
		}

		[CanBeNull]
		public virtual string GetGpsDirectionDescription(int tagType)
		{
			Rational angle = _directory.GetRational(tagType);
			// provide a decimal version of rational numbers in the description, to avoid strings like "35334/199 degrees"
			string value = angle != null ? new DecimalFormat("0.##").Format(angle.DoubleValue()) : _directory.GetString(tagType);
			return value == null || Sharpen.Extensions.Trim(value).Length == 0 ? null : Sharpen.Extensions.Trim(value) + " degrees";
		}

		[CanBeNull]
		public virtual string GetGpsDirectionReferenceDescription(int tagType)
		{
			string value = _directory.GetString(tagType);
			if (value == null)
			{
				return null;
			}
			string gpsDistRef = Sharpen.Extensions.Trim(value);
			if (Sharpen.Runtime.EqualsIgnoreCase("T", gpsDistRef))
			{
				return "True direction";
			}
			else
			{
				if (Sharpen.Runtime.EqualsIgnoreCase("M", gpsDistRef))
				{
					return "Magnetic direction";
				}
				else
				{
					return "Unknown (" + gpsDistRef + ")";
				}
			}
		}

		[CanBeNull]
		public virtual string GetGpsSpeedRefDescription()
		{
			string value = _directory.GetString(GpsDirectory.TagSpeedRef);
			if (value == null)
			{
				return null;
			}
			string gpsSpeedRef = Sharpen.Extensions.Trim(value);
			if (Sharpen.Runtime.EqualsIgnoreCase("K", gpsSpeedRef))
			{
				return "kph";
			}
			else
			{
				if (Sharpen.Runtime.EqualsIgnoreCase("M", gpsSpeedRef))
				{
					return "mph";
				}
				else
				{
					if (Sharpen.Runtime.EqualsIgnoreCase("N", gpsSpeedRef))
					{
						return "knots";
					}
					else
					{
						return "Unknown (" + gpsSpeedRef + ")";
					}
				}
			}
		}

		[CanBeNull]
		public virtual string GetGpsMeasureModeDescription()
		{
			string value = _directory.GetString(GpsDirectory.TagMeasureMode);
			if (value == null)
			{
				return null;
			}
			string gpsSpeedMeasureMode = Sharpen.Extensions.Trim(value);
			if (Sharpen.Runtime.EqualsIgnoreCase("2", gpsSpeedMeasureMode))
			{
				return "2-dimensional measurement";
			}
			else
			{
				if (Sharpen.Runtime.EqualsIgnoreCase("3", gpsSpeedMeasureMode))
				{
					return "3-dimensional measurement";
				}
				else
				{
					return "Unknown (" + gpsSpeedMeasureMode + ")";
				}
			}
		}

		[CanBeNull]
		public virtual string GetGpsStatusDescription()
		{
			string value = _directory.GetString(GpsDirectory.TagStatus);
			if (value == null)
			{
				return null;
			}
			string gpsStatus = Sharpen.Extensions.Trim(value);
			if (Sharpen.Runtime.EqualsIgnoreCase("A", gpsStatus))
			{
				return "Active (Measurement in progress)";
			}
			else
			{
				if (Sharpen.Runtime.EqualsIgnoreCase("V", gpsStatus))
				{
					return "Void (Measurement Interoperability)";
				}
				else
				{
					return "Unknown (" + gpsStatus + ")";
				}
			}
		}

		[CanBeNull]
		public virtual string GetGpsAltitudeRefDescription()
		{
			return GetIndexedDescription(GpsDirectory.TagAltitudeRef, "Sea level", "Below sea level");
		}

		[CanBeNull]
		public virtual string GetGpsAltitudeDescription()
		{
			Rational value = _directory.GetRational(GpsDirectory.TagAltitude);
			return value == null ? null : value.IntValue() + " metres";
		}

		[CanBeNull]
		public virtual string GetGpsDifferentialDescription()
		{
			return GetIndexedDescription(GpsDirectory.TagDifferential, "No Correction", "Differential Corrected");
		}

		[CanBeNull]
		public virtual string GetDegreesMinutesSecondsDescription()
		{
			GeoLocation location = _directory.GetGeoLocation();
			return location == null ? null : location.ToDMSString();
		}
	}
}
