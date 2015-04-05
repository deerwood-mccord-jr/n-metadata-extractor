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
using Com.Drew.Metadata;
using Com.Drew.Metadata.Exif.Makernotes;
using JetBrains.Annotations;
using Sharpen;

namespace Com.Drew.Metadata.Exif.Makernotes
{
	/// <summary>
	/// Provides human-readable string representations of tag values stored in a
	/// <see cref="KyoceraMakernoteDirectory"/>
	/// .
	/// <p/>
	/// Some information about this makernote taken from here:
	/// http://www.ozhiker.com/electronics/pjmt/jpeg_info/kyocera_mn.html
	/// <p/>
	/// Most manufacturer's Makernote counts the "offset to data" from the first byte
	/// of TIFF header (same as the other IFD), but Kyocera (along with Fujifilm) counts
	/// it from the first byte of Makernote itself.
	/// </summary>
	/// <author>Drew Noakes http://drewnoakes.com</author>
	public class KyoceraMakernoteDescriptor : TagDescriptor<KyoceraMakernoteDirectory>
	{
		public KyoceraMakernoteDescriptor(KyoceraMakernoteDirectory directory)
			: base(directory)
		{
		}

		[CanBeNull]
		public override string GetDescription(int tagType)
		{
			switch (tagType)
			{
                case KyoceraMakernoteDirectory.TagPrintImageMatchingInfo:
				{
					return GetPrintImageMatchingInfoDescription();
				}

                case KyoceraMakernoteDirectory.TagProprietaryThumbnail:
				{
					return GetProprietaryThumbnailDataDescription();
				}

				default:
				{
					return base.GetDescription(tagType);
				}
			}
		}

		[CanBeNull]
		public virtual string GetPrintImageMatchingInfoDescription()
		{
            return GetByteLengthDescription(KyoceraMakernoteDirectory.TagPrintImageMatchingInfo);
		}

		[CanBeNull]
		public virtual string GetProprietaryThumbnailDataDescription()
		{
            return GetByteLengthDescription(KyoceraMakernoteDirectory.TagProprietaryThumbnail);
		}
	}
}
