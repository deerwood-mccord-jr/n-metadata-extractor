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
using System;
using System.IO;
using Com.Drew.Imaging.Jpeg;
using Com.Drew.Lang;
using Com.Drew.Metadata;
using JetBrains.Annotations;
using Sharpen;

namespace Com.Drew.Metadata.Icc
{
	/// <summary>Reads an ICC profile.</summary>
	/// <remarks>
	/// Reads an ICC profile.
	/// <ul>
	/// <li>http://en.wikipedia.org/wiki/ICC_profile</li>
	/// <li>http://www.sno.phy.queensu.ca/~phil/exiftool/TagNames/ICC_Profile.html</li>
	/// </ul>
	/// </remarks>
	/// <author>Yuri Binev</author>
	/// <author>Drew Noakes https://drewnoakes.com</author>
	public class IccReader : JpegSegmentMetadataReader, MetadataReader
	{
		public const string JpegSegmentPreamble = "ICC_PROFILE";

		[NotNull]
		public virtual Iterable<JpegSegmentType> GetSegmentTypes()
		{
			return Arrays.AsList(JpegSegmentType.App2).AsIterable();
		}

		public virtual void ReadJpegSegments([NotNull] Iterable<sbyte[]> segments, [NotNull] Com.Drew.Metadata.Metadata metadata, [NotNull] JpegSegmentType segmentType)
		{
			int preambleLength = JpegSegmentPreamble.Length;
			// ICC data can be spread across multiple JPEG segments.
			// We concat them together in this buffer for later processing.
			sbyte[] buffer = null;
			foreach (sbyte[] segmentBytes in segments)
			{
				// Skip any segments that do not contain the required preamble
				if (segmentBytes.Length < preambleLength || !Sharpen.Runtime.EqualsIgnoreCase(JpegSegmentPreamble, Sharpen.Runtime.GetStringForBytes(segmentBytes, 0, preambleLength)))
				{
					continue;
				}
				// NOTE we ignore three bytes here -- are they useful for anything?
				// Grow the buffer
				if (buffer == null)
				{
					buffer = new sbyte[segmentBytes.Length - 14];
					// skip the first 14 bytes
					System.Array.Copy(segmentBytes, 14, buffer, 0, segmentBytes.Length - 14);
				}
				else
				{
					sbyte[] newBuffer = new sbyte[buffer.Length + segmentBytes.Length - 14];
					System.Array.Copy(buffer, 0, newBuffer, 0, buffer.Length);
					System.Array.Copy(segmentBytes, 14, newBuffer, buffer.Length, segmentBytes.Length - 14);
					buffer = newBuffer;
				}
			}
			if (buffer != null)
			{
				Extract(new ByteArrayReader(buffer), metadata);
			}
		}

		public virtual void Extract([NotNull] RandomAccessReader reader, [NotNull] Com.Drew.Metadata.Metadata metadata)
		{
			// TODO review whether the 'tagPtr' values below really do require RandomAccessReader or whether SequentialReader may be used instead
			IccDirectory directory = new IccDirectory();
			try
			{
				int profileByteCount = reader.GetInt32(IccDirectory.TagProfileByteCount);
				directory.SetInt(IccDirectory.TagProfileByteCount, profileByteCount);
				// For these tags, the int value of the tag is in fact it's offset within the buffer.
				Set4ByteString(directory, IccDirectory.TagCmmType, reader);
				SetInt32(directory, IccDirectory.TagProfileVersion, reader);
				Set4ByteString(directory, IccDirectory.TagProfileClass, reader);
				Set4ByteString(directory, IccDirectory.TagColorSpace, reader);
				Set4ByteString(directory, IccDirectory.TagProfileConnectionSpace, reader);
				SetDate(directory, IccDirectory.TagProfileDatetime, reader);
				Set4ByteString(directory, IccDirectory.TagSignature, reader);
				Set4ByteString(directory, IccDirectory.TagPlatform, reader);
				SetInt32(directory, IccDirectory.TagCmmFlags, reader);
				Set4ByteString(directory, IccDirectory.TagDeviceMake, reader);
				int temp = reader.GetInt32(IccDirectory.TagDeviceModel);
				if (temp != 0)
				{
					if (temp <= unchecked((int)(0x20202020)))
					{
						directory.SetInt(IccDirectory.TagDeviceModel, temp);
					}
					else
					{
						directory.SetString(IccDirectory.TagDeviceModel, GetStringFromInt32(temp));
					}
				}
				SetInt32(directory, IccDirectory.TagRenderingIntent, reader);
				SetInt64(directory, IccDirectory.TagDeviceAttr, reader);
				float[] xyz = new float[] { reader.GetS15Fixed16(IccDirectory.TagXyzValues), reader.GetS15Fixed16(IccDirectory.TagXyzValues + 4), reader.GetS15Fixed16(IccDirectory.TagXyzValues + 8) };
				directory.SetObject(IccDirectory.TagXyzValues, xyz);
				// Process 'ICC tags'
				int tagCount = reader.GetInt32(IccDirectory.TagTagCount);
				directory.SetInt(IccDirectory.TagTagCount, tagCount);
				for (int i = 0; i < tagCount; i++)
				{
					int pos = IccDirectory.TagTagCount + 4 + i * 12;
					int tagType = reader.GetInt32(pos);
					int tagPtr = reader.GetInt32(pos + 4);
					int tagLen = reader.GetInt32(pos + 8);
					sbyte[] b = reader.GetBytes(tagPtr, tagLen);
					directory.SetByteArray(tagType, b);
				}
			}
			catch (IOException ex)
			{
				directory.AddError("Exception reading ICC profile: " + ex.Message);
			}
			metadata.AddDirectory(directory);
		}

		/// <exception cref="System.IO.IOException"/>
		private void Set4ByteString([NotNull] Com.Drew.Metadata.Directory directory, int tagType, [NotNull] RandomAccessReader reader)
		{
			int i = reader.GetInt32(tagType);
			if (i != 0)
			{
				directory.SetString(tagType, GetStringFromInt32(i));
			}
		}

		/// <exception cref="System.IO.IOException"/>
		private void SetInt32([NotNull] Com.Drew.Metadata.Directory directory, int tagType, [NotNull] RandomAccessReader reader)
		{
			int i = reader.GetInt32(tagType);
			if (i != 0)
			{
				directory.SetInt(tagType, i);
			}
		}

		/// <exception cref="System.IO.IOException"/>
		private void SetInt64([NotNull] Com.Drew.Metadata.Directory directory, int tagType, [NotNull] RandomAccessReader reader)
		{
			long l = reader.GetInt64(tagType);
			if (l != 0)
			{
				directory.SetLong(tagType, l);
			}
		}

		/// <exception cref="System.IO.IOException"/>
		private void SetDate([NotNull] IccDirectory directory, int tagType, [NotNull] RandomAccessReader reader)
		{
			int y = reader.GetUInt16(tagType);
			int m = reader.GetUInt16(tagType + 2);
			int d = reader.GetUInt16(tagType + 4);
			int h = reader.GetUInt16(tagType + 6);
			int M = reader.GetUInt16(tagType + 8);
			int s = reader.GetUInt16(tagType + 10);
			//        final Date value = new Date(Date.UTC(y - 1900, m - 1, d, h, M, s));
			Sharpen.Calendar calendar = Sharpen.Calendar.GetInstance(Sharpen.Extensions.GetTimeZone("UTC"));
			calendar.Set(y, m, d, h, M, s);
			DateTime value = calendar.GetTime();
			directory.SetDate(tagType, value);
		}

		[NotNull]
		public static string GetStringFromInt32(int d)
		{
			// MSB
			sbyte[] b = new sbyte[] { unchecked((sbyte)((d & unchecked((int)(0xFF000000))) >> 24)), unchecked((sbyte)((d & unchecked((int)(0x00FF0000))) >> 16)), unchecked((sbyte)((d & unchecked((int)(0x0000FF00))) >> 8)), unchecked((sbyte)((d & unchecked(
				(int)(0x000000FF))))) };
			return Sharpen.Runtime.GetStringForBytes(b);
		}
	}
}
