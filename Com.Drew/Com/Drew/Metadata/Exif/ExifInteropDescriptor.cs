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
using Com.Drew.Metadata.Exif;
using JetBrains.Annotations;
using Sharpen;

namespace Com.Drew.Metadata.Exif
{
	/// <summary>
	/// Provides human-readable string representations of tag values stored in a
	/// <see cref="ExifInteropDirectory"/>
	/// .
	/// </summary>
	/// <author>Drew Noakes http://drewnoakes.com</author>
	public class ExifInteropDescriptor : TagDescriptor<ExifInteropDirectory>
	{
		public ExifInteropDescriptor(ExifInteropDirectory directory)
			: base(directory)
		{
		}

		[CanBeNull]
		public override string GetDescription(int tagType)
		{
			switch (tagType)
			{
                case ExifInteropDirectory.TagInteropIndex:
				{
					return GetInteropIndexDescription();
				}

                case ExifInteropDirectory.TagInteropVersion:
				{
					return GetInteropVersionDescription();
				}

				default:
				{
					return base.GetDescription(tagType);
				}
			}
		}

		[CanBeNull]
		public virtual string GetInteropVersionDescription()
		{
            return GetVersionBytesDescription(ExifInteropDirectory.TagInteropVersion, 2);
		}

		[CanBeNull]
		public virtual string GetInteropIndexDescription()
		{
            string value = _directory.GetString(ExifInteropDirectory.TagInteropIndex);
			if (value == null)
			{
				return null;
			}
			return Sharpen.Runtime.EqualsIgnoreCase("R98", Sharpen.Extensions.Trim(value)) ? "Recommended Exif Interoperability Rules (ExifR98)" : "Unknown (" + value + ")";
		}
	}
}
