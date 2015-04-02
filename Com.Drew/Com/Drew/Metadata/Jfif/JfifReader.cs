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
using System.IO;
using Com.Drew.Imaging.Jpeg;
using Com.Drew.Lang;
using Com.Drew.Metadata;
using JetBrains.Annotations;
using Sharpen;

namespace Com.Drew.Metadata.Jfif
{
	/// <summary>Reader for JFIF data, found in the APP0 JPEG segment.</summary>
	/// <remarks>
	/// Reader for JFIF data, found in the APP0 JPEG segment.
	/// <p>
	/// More info at: http://en.wikipedia.org/wiki/JPEG_File_Interchange_Format
	/// </remarks>
	/// <author>Yuri Binev, Drew Noakes, Markus Meyer</author>
	public class JfifReader : JpegSegmentMetadataReader, MetadataReader
	{
		public const string Preamble = "JFIF";

		[NotNull]
		public virtual Iterable<JpegSegmentType> GetSegmentTypes()
		{
			return Arrays.AsList(JpegSegmentType.App0).AsIterable();
		}

		public virtual void ReadJpegSegments([NotNull] Iterable<sbyte[]> segments, [NotNull] Com.Drew.Metadata.Metadata metadata, [NotNull] JpegSegmentType segmentType)
		{
			foreach (sbyte[] segmentBytes in segments)
			{
				// Skip segments not starting with the required header
				if (segmentBytes.Length >= 4 && Preamble.Equals(Sharpen.Runtime.GetStringForBytes(segmentBytes, 0, Preamble.Length)))
				{
					Extract(new ByteArrayReader(segmentBytes), metadata);
				}
			}
		}

		/// <summary>
		/// Performs the Jfif data extraction, adding found values to the specified
		/// instance of
		/// <see cref="Com.Drew.Metadata.Metadata"/>
		/// .
		/// </summary>
		public virtual void Extract([NotNull] RandomAccessReader reader, [NotNull] Com.Drew.Metadata.Metadata metadata)
		{
			JfifDirectory directory = new JfifDirectory();
			metadata.AddDirectory(directory);
			try
			{
				// For JFIF, the tag number is also the offset into the segment
				int ver = reader.GetUInt16(JfifDirectory.TagVersion);
				directory.SetInt(JfifDirectory.TagVersion, ver);
				int units = reader.GetUInt8(JfifDirectory.TagUnits);
				directory.SetInt(JfifDirectory.TagUnits, units);
				int height = reader.GetUInt16(JfifDirectory.TagResx);
				directory.SetInt(JfifDirectory.TagResx, height);
				int width = reader.GetUInt16(JfifDirectory.TagResy);
				directory.SetInt(JfifDirectory.TagResy, width);
			}
			catch (IOException me)
			{
				directory.AddError(me.Message);
			}
		}
	}
}
