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
using Com.Drew.Lang;
using JetBrains.Annotations;
using Sharpen;

namespace Com.Drew.Metadata.Ico
{
	/// <summary>Reads ICO (Windows Icon) file metadata.</summary>
	/// <remarks>
	/// Reads ICO (Windows Icon) file metadata.
	/// <ul>
	/// <li>https://en.wikipedia.org/wiki/ICO_(file_format)</li>
	/// </ul>
	/// </remarks>
	/// <author>Drew Noakes https://drewnoakes.com</author>
	public class IcoReader
	{
		public virtual void Extract([NotNull] SequentialReader reader, [NotNull] Com.Drew.Metadata.Metadata metadata)
		{
			reader.SetMotorolaByteOrder(false);
			int type;
			int imageCount;
			// Read header (ICONDIR structure)
			try
			{
				int reserved = reader.GetUInt16();
				if (reserved != 0)
				{
					IcoDirectory directory = new IcoDirectory();
					directory.AddError("Invalid header bytes");
					metadata.AddDirectory(directory);
					return;
				}
				type = reader.GetUInt16();
				if (type != 1 && type != 2)
				{
					IcoDirectory directory = new IcoDirectory();
					directory.AddError("Invalid type " + type + " -- expecting 1 or 2");
					metadata.AddDirectory(directory);
					return;
				}
				imageCount = reader.GetUInt16();
				if (imageCount == 0)
				{
					IcoDirectory directory = new IcoDirectory();
					directory.AddError("Image count cannot be zero");
					metadata.AddDirectory(directory);
					return;
				}
			}
			catch (IOException ex)
			{
				IcoDirectory directory = new IcoDirectory();
				directory.AddError("Exception reading ICO file metadata: " + ex.Message);
				metadata.AddDirectory(directory);
				return;
			}
			// Read each embedded image
			IcoDirectory directory_1 = null;
			try
			{
				for (int imageIndex = 0; imageIndex < imageCount; imageIndex++)
				{
					directory_1 = new IcoDirectory();
					metadata.AddDirectory(directory_1);
					directory_1.SetInt(IcoDirectory.TagImageType, type);
					directory_1.SetInt(IcoDirectory.TagImageWidth, reader.GetUInt8());
					directory_1.SetInt(IcoDirectory.TagImageHeight, reader.GetUInt8());
					directory_1.SetInt(IcoDirectory.TagColourPaletteSize, reader.GetUInt8());
					// Ignore this byte (normally zero, though .NET's System.Drawing.Icon.Save method writes 255)
					reader.GetUInt8();
					if (type == 1)
					{
						// Icon
						directory_1.SetInt(IcoDirectory.TagColourPlanes, reader.GetUInt16());
						directory_1.SetInt(IcoDirectory.TagBitsPerPixel, reader.GetUInt16());
					}
					else
					{
						// Cursor
						directory_1.SetInt(IcoDirectory.TagCursorHotspotX, reader.GetUInt16());
						directory_1.SetInt(IcoDirectory.TagCursorHotspotY, reader.GetUInt16());
					}
					directory_1.SetLong(IcoDirectory.TagImageSizeBytes, reader.GetUInt32());
					directory_1.SetLong(IcoDirectory.TagImageOffsetBytes, reader.GetUInt32());
				}
			}
			catch (IOException ex)
			{
				System.Diagnostics.Debug.Assert((directory_1 != null));
				directory_1.AddError("Exception reading ICO file metadata: " + ex.Message);
			}
		}
	}
}
