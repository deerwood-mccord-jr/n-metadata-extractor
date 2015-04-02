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
using Com.Drew.Imaging;
using JetBrains.Annotations;
using Sharpen;

namespace Com.Drew.Imaging.Jpeg
{
	/// <summary>An exception class thrown upon unexpected and fatal conditions while processing a JPEG file.</summary>
	/// <author>Drew Noakes https://drewnoakes.com</author>
	[System.Serializable]
	public class JpegProcessingException : ImageProcessingException
	{
		private const long serialVersionUID = -7870179776125450158L;

		public JpegProcessingException([CanBeNull] string message)
			: base(message)
		{
		}

		public JpegProcessingException([CanBeNull] string message, [CanBeNull] Exception cause)
			: base(message, cause)
		{
		}

		public JpegProcessingException([CanBeNull] Exception cause)
			: base(cause)
		{
		}
	}
}
