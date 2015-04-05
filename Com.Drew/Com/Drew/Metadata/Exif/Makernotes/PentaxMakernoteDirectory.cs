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
using System.Collections.Generic;
using Com.Drew.Metadata.Exif.Makernotes;
using JetBrains.Annotations;
using Sharpen;

namespace Com.Drew.Metadata.Exif.Makernotes
{
	/// <summary>Describes tags specific to Pentax and Asahi cameras.</summary>
	/// <author>Drew Noakes http://drewnoakes.com</author>
	public class PentaxMakernoteDirectory : Com.Drew.Metadata.Directory
	{
		/// <summary>
		/// 0 = Auto
		/// 1 = Night-scene
		/// 2 = Manual
		/// 4 = Multiple
		/// </summary>
		public const int TagCaptureMode = unchecked((int)(0x0001));

		/// <summary>
		/// 0 = Good
		/// 1 = Better
		/// 2 = Best
		/// </summary>
		public const int TagQualityLevel = unchecked((int)(0x0002));

		/// <summary>
		/// 2 = Custom
		/// 3 = Auto
		/// </summary>
		public const int TagFocusMode = unchecked((int)(0x0003));

		/// <summary>
		/// 1 = Auto
		/// 2 = Flash on
		/// 4 = Flash off
		/// 6 = Red-eye Reduction
		/// </summary>
		public const int TagFlashMode = unchecked((int)(0x0004));

		/// <summary>
		/// 0 = Auto
		/// 1 = Daylight
		/// 2 = Shade
		/// 3 = Tungsten
		/// 4 = Fluorescent
		/// 5 = Manual
		/// </summary>
		public const int TagWhiteBalance = unchecked((int)(0x0007));

		/// <summary>(0 = Off)</summary>
		public const int TagDigitalZoom = unchecked((int)(0x000A));

		/// <summary>
		/// 0 = Normal
		/// 1 = Soft
		/// 2 = Hard
		/// </summary>
		public const int TagSharpness = unchecked((int)(0x000B));

		/// <summary>
		/// 0 = Normal
		/// 1 = Low
		/// 2 = High
		/// </summary>
		public const int TagContrast = unchecked((int)(0x000C));

		/// <summary>
		/// 0 = Normal
		/// 1 = Low
		/// 2 = High
		/// </summary>
		public const int TagSaturation = unchecked((int)(0x000D));

		/// <summary>
		/// 10 = ISO 100
		/// 16 = ISO 200
		/// 100 = ISO 100
		/// 200 = ISO 200
		/// </summary>
		public const int TagIsoSpeed = unchecked((int)(0x0014));

		/// <summary>
		/// 1 = Normal
		/// 2 = Black & White
		/// 3 = Sepia
		/// </summary>
		public const int TagColour = unchecked((int)(0x0017));

		/// <summary>See Print Image Matching for specification.</summary>
		/// <remarks>
		/// See Print Image Matching for specification.
		/// http://www.ozhiker.com/electronics/pjmt/jpeg_info/pim.html
		/// </remarks>
		public const int TagPrintImageMatchingInfo = unchecked((int)(0x0E00));

		/// <summary>(String).</summary>
		public const int TagTimeZone = unchecked((int)(0x1000));

		/// <summary>(String).</summary>
		public const int TagDaylightSavings = unchecked((int)(0x1001));

		[NotNull]
		protected internal static readonly Dictionary<int, string> _tagNameMap = new Dictionary<int, string>();

		static PentaxMakernoteDirectory()
		{
			_tagNameMap.Put(TagCaptureMode, "Capture Mode");
			_tagNameMap.Put(TagQualityLevel, "Quality Level");
			_tagNameMap.Put(TagFocusMode, "Focus Mode");
			_tagNameMap.Put(TagFlashMode, "Flash Mode");
			_tagNameMap.Put(TagWhiteBalance, "White Balance");
			_tagNameMap.Put(TagDigitalZoom, "Digital Zoom");
			_tagNameMap.Put(TagSharpness, "Sharpness");
			_tagNameMap.Put(TagContrast, "Contrast");
			_tagNameMap.Put(TagSaturation, "Saturation");
			_tagNameMap.Put(TagIsoSpeed, "ISO Speed");
			_tagNameMap.Put(TagColour, "Colour");
			_tagNameMap.Put(TagPrintImageMatchingInfo, "Print Image Matching (PIM) Info");
			_tagNameMap.Put(TagTimeZone, "Time Zone");
			_tagNameMap.Put(TagDaylightSavings, "Daylight Savings");
		}

		public PentaxMakernoteDirectory()
		{
			this.SetDescriptor(new PentaxMakernoteDescriptor(this));
		}

		[NotNull]
		public override string GetName()
		{
			return "Pentax Makernote";
		}

		[NotNull]
		protected internal override Dictionary<int, string> GetTagNameMap()
		{
			return _tagNameMap;
		}
	}
}
