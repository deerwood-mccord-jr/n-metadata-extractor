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
using System.IO;
using Com.Drew.Lang;
using JetBrains.Annotations;
using Sharpen;

namespace Com.Drew.Lang
{
	/// <author>Drew Noakes http://drewnoakes.com</author>
	public class StreamReader : SequentialReader
	{
		[NotNull]
		private readonly InputStream _stream;

		public StreamReader(InputStream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException();
			}
			_stream = stream;
		}

		/// <exception cref="System.IO.IOException"/>
		protected internal override sbyte GetByte()
		{
			int value = _stream.Read();
			if (value == -1)
			{
				throw new EOFException("End of data reached.");
			}
			return unchecked((sbyte)value);
		}

		/// <exception cref="System.IO.IOException"/>
		[NotNull]
		public override sbyte[] GetBytes(int count)
		{
			sbyte[] bytes = new sbyte[count];
			int totalBytesRead = 0;
			while (totalBytesRead != count)
			{
				int bytesRead = _stream.Read(bytes, totalBytesRead, count - totalBytesRead);
				if (bytesRead == -1)
				{
					throw new EOFException("End of data reached.");
				}
				totalBytesRead += bytesRead;
				System.Diagnostics.Debug.Assert((totalBytesRead <= count));
			}
			return bytes;
		}

		/// <exception cref="System.IO.IOException"/>
		public override void Skip(long n)
		{
			if (n < 0)
			{
				throw new ArgumentException("n must be zero or greater.");
			}
			long skippedCount = SkipInternal(n);
			if (skippedCount != n)
			{
				throw new EOFException(Sharpen.Extensions.StringFormat("Unable to skip. Requested %d bytes but skipped %d.", n, skippedCount));
			}
		}

		/// <exception cref="System.IO.IOException"/>
		public override bool TrySkip(long n)
		{
			if (n < 0)
			{
				throw new ArgumentException("n must be zero or greater.");
			}
			return SkipInternal(n) == n;
		}

		/// <exception cref="System.IO.IOException"/>
		private long SkipInternal(long n)
		{
			// It seems that for some streams, such as BufferedInputStream, that skip can return
			// some smaller number than was requested. So loop until we either skip enough, or
			// InputStream.skip returns zero.
			//
			// See http://stackoverflow.com/questions/14057720/robust-skipping-of-data-in-a-java-io-inputstream-and-its-subtypes
			//
			long skippedTotal = 0;
			while (skippedTotal != n)
			{
				long skipped = _stream.Skip(n - skippedTotal);
				System.Diagnostics.Debug.Assert((skipped >= 0));
				skippedTotal += skipped;
				if (skipped == 0)
				{
					break;
				}
			}
			return skippedTotal;
		}
	}
}
