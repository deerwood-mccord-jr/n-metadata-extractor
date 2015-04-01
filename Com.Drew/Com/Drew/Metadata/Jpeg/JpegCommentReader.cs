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
using Com.Drew.Imaging.Jpeg;
using JetBrains.Annotations;
using Sharpen;

namespace Com.Drew.Metadata.Jpeg
{
	/// <summary>
	/// Decodes the comment stored within JPEG files, populating a
	/// <see cref="Com.Drew.Metadata.Metadata"/>
	/// object with tag values in a
	/// <see cref="JpegCommentDirectory"/>
	/// .
	/// </summary>
	/// <author>Drew Noakes https://drewnoakes.com</author>
	public class JpegCommentReader : JpegSegmentMetadataReader
	{
		[NotNull]
		public virtual Iterable<JpegSegmentType> GetSegmentTypes()
		{
			return Arrays.AsList(JpegSegmentType.Com).AsIterable();
		}

		public virtual bool CanProcess([NotNull] sbyte[] segmentBytes, [NotNull] JpegSegmentType segmentType)
		{
			// The entire contents of the byte[] is the comment. There's nothing here to discriminate upon.
			return true;
		}

		public virtual void ReadJpegSegments([NotNull] Iterable<sbyte[]> segments, [NotNull] Com.Drew.Metadata.Metadata metadata, [NotNull] JpegSegmentType segmentType)
		{
			foreach (sbyte[] segmentBytes in segments)
			{
				JpegCommentDirectory directory = new JpegCommentDirectory();
				metadata.AddDirectory(directory);
				// The entire contents of the directory are the comment
				directory.SetString(JpegCommentDirectory.TagComment, Sharpen.Runtime.GetStringForBytes(segmentBytes));
			}
		}
	}
}
