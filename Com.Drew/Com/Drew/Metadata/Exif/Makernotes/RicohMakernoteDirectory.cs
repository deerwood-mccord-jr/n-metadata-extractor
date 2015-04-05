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
	/// <summary>Describes tags specific to Ricoh cameras.</summary>
	/// <author>Drew Noakes http://drewnoakes.com</author>
	public class RicohMakernoteDirectory : Com.Drew.Metadata.Directory
	{
		public const int TagMakernoteDataType = unchecked((int)(0x0001));

		public const int TagVersion = unchecked((int)(0x0002));

		public const int TagPrintImageMatchingInfo = unchecked((int)(0x0E00));

		public const int TagRicohCameraInfoMakernoteSubIfdPointer = unchecked((int)(0x2001));

		[NotNull]
		protected internal static readonly Dictionary<int, string> _tagNameMap = new Dictionary<int, string>();

		static RicohMakernoteDirectory()
		{
			_tagNameMap.Put(TagMakernoteDataType, "Makernote Data Type");
			_tagNameMap.Put(TagVersion, "Version");
			_tagNameMap.Put(TagPrintImageMatchingInfo, "Print Image Matching (PIM) Info");
			_tagNameMap.Put(TagRicohCameraInfoMakernoteSubIfdPointer, "Ricoh Camera Info Makernote Sub-IFD");
		}

		public RicohMakernoteDirectory()
		{
			this.SetDescriptor(new RicohMakernoteDescriptor(this));
		}

		[NotNull]
		public override string GetName()
		{
			return "Ricoh Makernote";
		}

		[NotNull]
		protected internal override Dictionary<int, string> GetTagNameMap()
		{
			return _tagNameMap;
		}
	}
}
