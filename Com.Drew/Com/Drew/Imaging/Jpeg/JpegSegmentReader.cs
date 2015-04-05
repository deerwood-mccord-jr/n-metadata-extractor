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
using System.Collections.Generic;
using System.IO;
using Com.Drew.Imaging.Jpeg;
using Com.Drew.Lang;
using JetBrains.Annotations;
using Sharpen;

namespace Com.Drew.Imaging.Jpeg
{
	/// <summary>Performs read functions of JPEG files, returning specific file segments.</summary>
	/// <remarks>
	/// Performs read functions of JPEG files, returning specific file segments.
	/// <p/>
	/// JPEG files are composed of a sequence of consecutive JPEG 'segments'. Each is identified by one of a set of byte
	/// values, modelled in the
	/// <see cref="JpegSegmentType"/>
	/// enumeration. Use <code>readSegments</code> to read out the some
	/// or all segments into a
	/// <see cref="JpegSegmentData"/>
	/// object, from which the raw JPEG segment byte arrays may be accessed.
	/// </remarks>
	/// <author>Drew Noakes http://drewnoakes.com</author>
	public class JpegSegmentReader
	{
		/// <summary>Private, because this segment crashes my algorithm, and searching for it doesn't work (yet).</summary>
		private const sbyte SegmentSos = unchecked((sbyte)unchecked((int)(0xDA)));

		/// <summary>Private, because one wouldn't search for it.</summary>
		private const sbyte MarkerEoi = unchecked((sbyte)unchecked((int)(0xD9)));

		/// <summary>
		/// Processes the provided JPEG data, and extracts the specified JPEG segments into a
		/// <see cref="JpegSegmentData"/>
		/// object.
		/// <p/>
		/// Will not return SOS (start of scan) or EOI (end of image) segments.
		/// </summary>
		/// <param name="file">
		/// a
		/// <see cref="Sharpen.FilePath"/>
		/// from which the JPEG data will be read.
		/// </param>
		/// <param name="segmentTypes">
		/// the set of JPEG segments types that are to be returned. If this argument is <code>null</code>
		/// then all found segment types are returned.
		/// </param>
		/// <exception cref="Com.Drew.Imaging.Jpeg.JpegProcessingException"/>
		/// <exception cref="System.IO.IOException"/>
		[NotNull]
		public static JpegSegmentData ReadSegments(FilePath file, Iterable<JpegSegmentType> segmentTypes)
		{
			FileInputStream stream = null;
			try
			{
				stream = new FileInputStream(file);
				return ReadSegments(new Com.Drew.Lang.StreamReader(stream), segmentTypes);
			}
			finally
			{
				if (stream != null)
				{
					stream.Close();
				}
			}
		}

		/// <summary>
		/// Processes the provided JPEG data, and extracts the specified JPEG segments into a
		/// <see cref="JpegSegmentData"/>
		/// object.
		/// <p/>
		/// Will not return SOS (start of scan) or EOI (end of image) segments.
		/// </summary>
		/// <param name="reader">
		/// a
		/// <see cref="Com.Drew.Lang.SequentialReader"/>
		/// from which the JPEG data will be read. It must be positioned at the
		/// beginning of the JPEG data stream.
		/// </param>
		/// <param name="segmentTypes">
		/// the set of JPEG segments types that are to be returned. If this argument is <code>null</code>
		/// then all found segment types are returned.
		/// </param>
		/// <exception cref="Com.Drew.Imaging.Jpeg.JpegProcessingException"/>
		/// <exception cref="System.IO.IOException"/>
		[NotNull]
		public static JpegSegmentData ReadSegments(SequentialReader reader, Iterable<JpegSegmentType> segmentTypes)
		{
			// Must be big-endian
			System.Diagnostics.Debug.Assert((reader.IsMotorolaByteOrder()));
			// first two bytes should be JPEG magic number
			int magicNumber = reader.GetUInt16();
			if (magicNumber != unchecked((int)(0xFFD8)))
			{
				throw new JpegProcessingException("JPEG data is expected to begin with 0xFFD8 (ГїГ�) not 0x" + Sharpen.Extensions.ToHexString(magicNumber));
			}
			ICollection<sbyte> segmentTypeBytes = null;
			if (segmentTypes != null)
			{
				segmentTypeBytes = new HashSet<sbyte>();
				foreach (JpegSegmentType segmentType in segmentTypes)
				{
					segmentTypeBytes.Add(segmentType.byteValue);
				}
			}
			JpegSegmentData segmentData = new JpegSegmentData();
			do
			{
				// next byte is the segment identifier: 0xFF
				short segmentIdentifier = reader.GetUInt8();
				if (segmentIdentifier != unchecked((int)(0xFF)))
				{
					throw new JpegProcessingException("Expected JPEG segment start identifier 0xFF, not 0x" + Sharpen.Extensions.ToHexString(segmentIdentifier));
				}
				// next byte is the segment type
				sbyte segmentType = reader.GetInt8();
				if (segmentType == SegmentSos)
				{
					// The 'Start-Of-Scan' segment's length doesn't include the image data, instead would
					// have to search for the two bytes: 0xFF 0xD9 (EOI).
					// It comes last so simply return at this point
					return segmentData;
				}
				if (segmentType == MarkerEoi)
				{
					// the 'End-Of-Image' segment -- this should never be found in this fashion
					return segmentData;
				}
				// next 2-bytes are <segment-size>: [high-byte] [low-byte]
				int segmentLength = reader.GetUInt16();
				// segment length includes size bytes, so subtract two
				segmentLength -= 2;
				if (segmentLength < 0)
				{
					throw new JpegProcessingException("JPEG segment size would be less than zero");
				}
				// Check whether we are interested in this segment
				if (segmentTypeBytes == null || segmentTypeBytes.Contains(segmentType))
				{
					sbyte[] segmentBytes = reader.GetBytes(segmentLength);
					System.Diagnostics.Debug.Assert((segmentLength == segmentBytes.Length));
					segmentData.AddSegment(segmentType, segmentBytes);
				}
				else
				{
					// Some if the JPEG is truncated, just return what data we've already gathered
					if (!reader.TrySkip(segmentLength))
					{
						return segmentData;
					}
				}
			}
			while (true);
		}

		/// <exception cref="System.Exception"/>
		private JpegSegmentReader()
		{
			throw new Exception("Not intended for instantiation.");
		}
	}
}
