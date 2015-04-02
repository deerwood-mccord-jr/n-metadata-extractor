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
using System.Collections.Generic;
using JetBrains.Annotations;
using Sharpen;

namespace Com.Drew.Metadata.Jpeg
{
	/// <summary>Describes tags used by a JPEG file comment.</summary>
	/// <author>Drew Noakes https://drewnoakes.com</author>
	public class JpegCommentDirectory : Com.Drew.Metadata.Directory
	{
		/// <summary>This value does not apply to a particular standard.</summary>
		/// <remarks>
		/// This value does not apply to a particular standard. Rather, this value has been fabricated to maintain
		/// consistency with other directory types.
		/// </remarks>
		public const int TagComment = 0;

		[NotNull]
		protected internal static readonly Dictionary<int?, string> _tagNameMap = new Dictionary<int?, string>();

		static JpegCommentDirectory()
		{
			_tagNameMap.Put(TagComment, "JPEG Comment");
		}

		public JpegCommentDirectory()
		{
			this.SetDescriptor(new JpegCommentDescriptor(this));
		}

		[NotNull]
		public override string GetName()
		{
			return "JpegComment";
		}

		[NotNull]
		protected internal override Dictionary<int?, string> GetTagNameMap()
		{
			return _tagNameMap;
		}
	}
}
