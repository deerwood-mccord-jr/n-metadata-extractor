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
using Com.Drew.Imaging.Riff;
using Com.Drew.Lang;
using Com.Drew.Metadata.Exif;
using Com.Drew.Metadata.Icc;
using Com.Drew.Metadata.Xmp;
using JetBrains.Annotations;
using Sharpen;

namespace Com.Drew.Metadata.Webp
{
	/// <summary>
	/// Implementation of
	/// <see cref="Com.Drew.Imaging.Riff.RiffHandler"/>
	/// specialising in WebP support.
	/// Extracts data from chunk types:
	/// <ul>
	/// <li><code>"VP8X"</code>: width, height, is animation, has alpha</li>
	/// <li><code>"EXIF"</code>: full Exif data</li>
	/// <li><code>"ICCP"</code>: full ICC profile</li>
	/// <li><code>"XMP "</code>: full XMP data</li>
	/// </ul>
	/// </summary>
	public class WebpRiffHandler : RiffHandler
	{
		[NotNull]
		private readonly Com.Drew.Metadata.Metadata _metadata;

		public WebpRiffHandler([NotNull] Com.Drew.Metadata.Metadata metadata)
		{
			_metadata = metadata;
		}

		public virtual bool ShouldAcceptRiffIdentifier([NotNull] string identifier)
		{
			return identifier.Equals("WEBP");
		}

		public virtual bool ShouldAcceptChunk([NotNull] string fourCC)
		{
			return fourCC.Equals("VP8X") || fourCC.Equals("EXIF") || fourCC.Equals("ICCP") || fourCC.Equals("XMP ");
		}

		public virtual void ProcessChunk([NotNull] string fourCC, [NotNull] sbyte[] payload)
		{
			//        System.out.println("Chunk " + fourCC + " " + payload.length + " bytes");
			if (fourCC.Equals("EXIF"))
			{
				new ExifReader().Extract(new ByteArrayReader(payload), _metadata);
			}
			else
			{
				if (fourCC.Equals("ICCP"))
				{
					new IccReader().Extract(new ByteArrayReader(payload), _metadata);
				}
				else
				{
					if (fourCC.Equals("XMP "))
					{
						new XmpReader().Extract(payload, _metadata);
					}
					else
					{
						if (fourCC.Equals("VP8X") && payload.Length == 10)
						{
							RandomAccessReader reader = new ByteArrayReader(payload);
							reader.SetMotorolaByteOrder(false);
							try
							{
								// Flags
								//                boolean hasFragments = reader.getBit(0);
								bool isAnimation = reader.GetBit(1);
								//                boolean hasXmp = reader.getBit(2);
								//                boolean hasExif = reader.getBit(3);
								bool hasAlpha = reader.GetBit(4);
								//                boolean hasIcc = reader.getBit(5);
								// Image size
								int widthMinusOne = reader.GetInt24(4);
								int heightMinusOne = reader.GetInt24(7);
								WebpDirectory directory = new WebpDirectory();
								directory.SetInt(WebpDirectory.TagImageWidth, widthMinusOne + 1);
								directory.SetInt(WebpDirectory.TagImageHeight, heightMinusOne + 1);
								directory.SetBoolean(WebpDirectory.TagHasAlpha, hasAlpha);
								directory.SetBoolean(WebpDirectory.TagIsAnimation, isAnimation);
								_metadata.AddDirectory(directory);
							}
							catch (IOException e)
							{
								Sharpen.Runtime.PrintStackTrace(e, System.Console.Error);
							}
						}
					}
				}
			}
		}
	}
}
