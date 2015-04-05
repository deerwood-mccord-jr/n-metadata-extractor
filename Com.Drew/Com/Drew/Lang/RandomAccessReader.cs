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
using System.IO;
using Com.Drew.Lang;
using JetBrains.Annotations;
using Sharpen;

namespace Com.Drew.Lang
{
	/// <summary>Base class for random access data reading operations of common data types.</summary>
	/// <remarks>
	/// Base class for random access data reading operations of common data types.
	/// <p/>
	/// By default, the reader operates with Motorola byte order (big endianness).  This can be changed by calling
	/// <code>setMotorolaByteOrder(boolean)</code>.
	/// <p/>
	/// Concrete implementations include:
	/// <ul>
	/// <li>
	/// <see cref="ByteArrayReader"/>
	/// </li>
	/// <li>
	/// <see cref="RandomAccessStreamReader"/>
	/// </li>
	/// </ul>
	/// </remarks>
	/// <author>Drew Noakes http://drewnoakes.com</author>
	public abstract class RandomAccessReader
	{
		private bool _isMotorolaByteOrder = true;

		/// <summary>Gets the byte value at the specified byte <code>index</code>.</summary>
		/// <remarks>
		/// Gets the byte value at the specified byte <code>index</code>.
		/// <p/>
		/// Implementations should not perform any bounds checking in this method. That should be performed
		/// in <code>validateIndex</code> and <code>isValidIndex</code>.
		/// </remarks>
		/// <param name="index">The index from which to read the byte</param>
		/// <returns>The read byte value</returns>
		/// <exception cref="System.ArgumentException"><code>index</code> or <code>count</code> are negative</exception>
		/// <exception cref="BufferBoundsException">if the requested byte is beyond the end of the underlying data source</exception>
		/// <exception cref="System.IO.IOException">if the byte is unable to be read</exception>
		protected internal abstract sbyte GetByte(int index);

		/// <summary>Returns the required number of bytes from the specified index from the underlying source.</summary>
		/// <param name="index">The index from which the bytes begins in the underlying source</param>
		/// <param name="count">The number of bytes to be returned</param>
		/// <returns>The requested bytes</returns>
		/// <exception cref="System.ArgumentException"><code>index</code> or <code>count</code> are negative</exception>
		/// <exception cref="BufferBoundsException">if the requested bytes extend beyond the end of the underlying data source</exception>
		/// <exception cref="System.IO.IOException">if the byte is unable to be read</exception>
		[NotNull]
		public abstract sbyte[] GetBytes(int index, int count);

		/// <summary>Ensures that the buffered bytes extend to cover the specified index.</summary>
		/// <remarks>
		/// Ensures that the buffered bytes extend to cover the specified index. If not, an attempt is made
		/// to read to that point.
		/// <p/>
		/// If the stream ends before the point is reached, a
		/// <see cref="BufferBoundsException"/>
		/// is raised.
		/// </remarks>
		/// <param name="index">the index from which the required bytes start</param>
		/// <param name="bytesRequested">the number of bytes which are required</param>
		/// <exception cref="System.IO.IOException">if the stream ends before the required number of bytes are acquired</exception>
		protected internal abstract void ValidateIndex(int index, int bytesRequested);

		/// <exception cref="System.IO.IOException"/>
		protected internal abstract bool IsValidIndex(int index, int bytesRequested);

		/// <summary>Returns the length of the data source in bytes.</summary>
		/// <remarks>
		/// Returns the length of the data source in bytes.
		/// <p/>
		/// This is a simple operation for implementations (such as
		/// <see cref="RandomAccessFileReader"/>
		/// and
		/// <see cref="ByteArrayReader"/>
		/// ) that have the entire data source available.
		/// <p/>
		/// Users of this method must be aware that sequentially accessed implementations such as
		/// <see cref="RandomAccessStreamReader"/>
		/// will have to read and buffer the entire data source in
		/// order to determine the length.
		/// </remarks>
		/// <returns>the length of the data source, in bytes.</returns>
		/// <exception cref="System.IO.IOException"/>
		public abstract long GetLength();

		/// <summary>Sets the endianness of this reader.</summary>
		/// <remarks>
		/// Sets the endianness of this reader.
		/// <ul>
		/// <li><code>true</code> for Motorola (or big) endianness (also known as network byte order), with MSB before LSB.</li>
		/// <li><code>false</code> for Intel (or little) endianness, with LSB before MSB.</li>
		/// </ul>
		/// </remarks>
		/// <param name="motorolaByteOrder"><code>true</code> for Motorola/big endian, <code>false</code> for Intel/little endian</param>
		public virtual void SetMotorolaByteOrder(bool motorolaByteOrder)
		{
			_isMotorolaByteOrder = motorolaByteOrder;
		}

		/// <summary>Gets the endianness of this reader.</summary>
		/// <remarks>
		/// Gets the endianness of this reader.
		/// <ul>
		/// <li><code>true</code> for Motorola (or big) endianness (also known as network byte order), with MSB before LSB.</li>
		/// <li><code>false</code> for Intel (or little) endianness, with LSB before MSB.</li>
		/// </ul>
		/// </remarks>
		public virtual bool IsMotorolaByteOrder()
		{
			return _isMotorolaByteOrder;
		}

		/// <summary>Returns an unsigned 8-bit int calculated from one byte of data at the specified index.</summary>
		/// <param name="index">position within the data buffer to read byte</param>
		/// <returns>the 8 bit int value, between 0 and 255</returns>
		/// <exception cref="System.IO.IOException">the buffer does not contain enough bytes to service the request, or index is negative</exception>
		public virtual short GetUInt8(int index)
		{
			ValidateIndex(index, 1);
			return (short)(GetByte(index) & unchecked((int)(0xFF)));
		}

		/// <summary>Returns a signed 8-bit int calculated from one byte of data at the specified index.</summary>
		/// <param name="index">position within the data buffer to read byte</param>
		/// <returns>the 8 bit int value, between 0x00 and 0xFF</returns>
		/// <exception cref="System.IO.IOException">the buffer does not contain enough bytes to service the request, or index is negative</exception>
		public virtual sbyte GetInt8(int index)
		{
			ValidateIndex(index, 1);
			return GetByte(index);
		}

		/// <summary>Returns an unsigned 16-bit int calculated from two bytes of data at the specified index.</summary>
		/// <param name="index">position within the data buffer to read first byte</param>
		/// <returns>the 16 bit int value, between 0x0000 and 0xFFFF</returns>
		/// <exception cref="System.IO.IOException">the buffer does not contain enough bytes to service the request, or index is negative</exception>
		public virtual int GetUInt16(int index)
		{
			ValidateIndex(index, 2);
			if (_isMotorolaByteOrder)
			{
				// Motorola - MSB first
				return (GetByte(index) << 8 & unchecked((int)(0xFF00))) | (GetByte(index + 1) & unchecked((int)(0xFF)));
			}
			else
			{
				// Intel ordering - LSB first
				return (GetByte(index + 1) << 8 & unchecked((int)(0xFF00))) | (GetByte(index) & unchecked((int)(0xFF)));
			}
		}

		/// <summary>Returns a signed 16-bit int calculated from two bytes of data at the specified index (MSB, LSB).</summary>
		/// <param name="index">position within the data buffer to read first byte</param>
		/// <returns>the 16 bit int value, between 0x0000 and 0xFFFF</returns>
		/// <exception cref="System.IO.IOException">the buffer does not contain enough bytes to service the request, or index is negative</exception>
		public virtual short GetInt16(int index)
		{
			ValidateIndex(index, 2);
			if (_isMotorolaByteOrder)
			{
				// Motorola - MSB first
				return (short)(((short)GetByte(index) << 8 & unchecked((int)(0xFF00))) | ((short)GetByte(index + 1) & (short)unchecked((int)(0xFF))));
			}
			else
			{
				// Intel ordering - LSB first
				return (short)(((short)GetByte(index + 1) << 8 & unchecked((int)(0xFF00))) | ((short)GetByte(index) & (short)unchecked((int)(0xFF))));
			}
		}

		/// <summary>Get a 32-bit unsigned integer from the buffer, returning it as a long.</summary>
		/// <param name="index">position within the data buffer to read first byte</param>
		/// <returns>the unsigned 32-bit int value as a long, between 0x00000000 and 0xFFFFFFFF</returns>
		/// <exception cref="System.IO.IOException">the buffer does not contain enough bytes to service the request, or index is negative</exception>
		public virtual long GetUInt32(int index)
		{
			ValidateIndex(index, 4);
			if (_isMotorolaByteOrder)
			{
				// Motorola - MSB first (big endian)
				return (((long)GetByte(index)) << 24 & unchecked((long)(0xFF000000L))) | (((long)GetByte(index + 1)) << 16 & unchecked((long)(0xFF0000L))) | (((long)GetByte(index + 2)) << 8 & unchecked((long)(0xFF00L))) | (((long)GetByte(index + 3)) & unchecked(
					(long)(0xFFL)));
			}
			else
			{
				// Intel ordering - LSB first (little endian)
				return (((long)GetByte(index + 3)) << 24 & unchecked((long)(0xFF000000L))) | (((long)GetByte(index + 2)) << 16 & unchecked((long)(0xFF0000L))) | (((long)GetByte(index + 1)) << 8 & unchecked((long)(0xFF00L))) | (((long)GetByte(index)) & unchecked(
					(long)(0xFFL)));
			}
		}

		/// <summary>Returns a signed 32-bit integer from four bytes of data at the specified index the buffer.</summary>
		/// <param name="index">position within the data buffer to read first byte</param>
		/// <returns>the signed 32 bit int value, between 0x00000000 and 0xFFFFFFFF</returns>
		/// <exception cref="System.IO.IOException">the buffer does not contain enough bytes to service the request, or index is negative</exception>
		public virtual int GetInt32(int index)
		{
			ValidateIndex(index, 4);
			if (_isMotorolaByteOrder)
			{
				// Motorola - MSB first (big endian)
				return (GetByte(index) << 24 & unchecked((int)(0xFF000000))) | (GetByte(index + 1) << 16 & unchecked((int)(0xFF0000))) | (GetByte(index + 2) << 8 & unchecked((int)(0xFF00))) | (GetByte(index + 3) & unchecked((int)(0xFF)));
			}
			else
			{
				// Intel ordering - LSB first (little endian)
				return (GetByte(index + 3) << 24 & unchecked((int)(0xFF000000))) | (GetByte(index + 2) << 16 & unchecked((int)(0xFF0000))) | (GetByte(index + 1) << 8 & unchecked((int)(0xFF00))) | (GetByte(index) & unchecked((int)(0xFF)));
			}
		}

		/// <summary>Get a signed 64-bit integer from the buffer.</summary>
		/// <param name="index">position within the data buffer to read first byte</param>
		/// <returns>the 64 bit int value, between 0x0000000000000000 and 0xFFFFFFFFFFFFFFFF</returns>
		/// <exception cref="System.IO.IOException">the buffer does not contain enough bytes to service the request, or index is negative</exception>
		public virtual long GetInt64(int index)
		{
			ValidateIndex(index, 8);
			if (_isMotorolaByteOrder)
			{
				// Motorola - MSB first
				return ((long)GetByte(index) << 56 & unchecked((long)(0xFF00000000000000L))) | ((long)GetByte(index + 1) << 48 & unchecked((long)(0xFF000000000000L))) | ((long)GetByte(index + 2) << 40 & unchecked((long)(0xFF0000000000L))) | ((long)GetByte(index
					 + 3) << 32 & unchecked((long)(0xFF00000000L))) | ((long)GetByte(index + 4) << 24 & unchecked((long)(0xFF000000L))) | ((long)GetByte(index + 5) << 16 & unchecked((long)(0xFF0000L))) | ((long)GetByte(index + 6) << 8 & unchecked((long)(0xFF00L
					))) | ((long)GetByte(index + 7) & unchecked((long)(0xFFL)));
			}
			else
			{
				// Intel ordering - LSB first
				return ((long)GetByte(index + 7) << 56 & unchecked((long)(0xFF00000000000000L))) | ((long)GetByte(index + 6) << 48 & unchecked((long)(0xFF000000000000L))) | ((long)GetByte(index + 5) << 40 & unchecked((long)(0xFF0000000000L))) | ((long)GetByte
					(index + 4) << 32 & unchecked((long)(0xFF00000000L))) | ((long)GetByte(index + 3) << 24 & unchecked((long)(0xFF000000L))) | ((long)GetByte(index + 2) << 16 & unchecked((long)(0xFF0000L))) | ((long)GetByte(index + 1) << 8 & unchecked((long)(
					0xFF00L))) | ((long)GetByte(index) & unchecked((long)(0xFFL)));
			}
		}

		/// <exception cref="System.IO.IOException"/>
		public virtual float GetS15Fixed16(int index)
		{
			ValidateIndex(index, 4);
			if (_isMotorolaByteOrder)
			{
				float res = (GetByte(index) & unchecked((int)(0xFF))) << 8 | (GetByte(index + 1) & unchecked((int)(0xFF)));
				int d = (GetByte(index + 2) & unchecked((int)(0xFF))) << 8 | (GetByte(index + 3) & unchecked((int)(0xFF)));
				return (float)(res + d / 65536.0);
			}
			else
			{
				// this particular branch is untested
				float res = (GetByte(index + 3) & unchecked((int)(0xFF))) << 8 | (GetByte(index + 2) & unchecked((int)(0xFF)));
				int d = (GetByte(index + 1) & unchecked((int)(0xFF))) << 8 | (GetByte(index) & unchecked((int)(0xFF)));
				return (float)(res + d / 65536.0);
			}
		}

		/// <exception cref="System.IO.IOException"/>
		public virtual float GetFloat32(int index)
		{
			return Sharpen.Extensions.IntBitsToFloat(GetInt32(index));
		}

		/// <exception cref="System.IO.IOException"/>
		public virtual double GetDouble64(int index)
		{
			return Sharpen.Extensions.LongBitsToDouble(GetInt64(index));
		}

		/// <exception cref="System.IO.IOException"/>
		[NotNull]
		public virtual string GetString(int index, int bytesRequested)
		{
			return Sharpen.Runtime.GetStringForBytes(GetBytes(index, bytesRequested));
		}

		/// <exception cref="System.IO.IOException"/>
		[NotNull]
		public virtual string GetString(int index, int bytesRequested, string charset)
		{
			sbyte[] bytes = GetBytes(index, bytesRequested);
			try
			{
				return Sharpen.Runtime.GetStringForBytes(bytes, charset);
			}
			catch (UnsupportedEncodingException)
			{
				return Sharpen.Runtime.GetStringForBytes(bytes);
			}
		}

		/// <summary>
		/// Creates a String from the _data buffer starting at the specified index,
		/// and ending where <code>byte=='\0'</code> or where <code>length==maxLength</code>.
		/// </summary>
		/// <param name="index">The index within the buffer at which to start reading the string.</param>
		/// <param name="maxLengthBytes">
		/// The maximum number of bytes to read.  If a zero-byte is not reached within this limit,
		/// reading will stop and the string will be truncated to this length.
		/// </param>
		/// <returns>The read string.</returns>
		/// <exception cref="System.IO.IOException">The buffer does not contain enough bytes to satisfy this request.</exception>
		[NotNull]
		public virtual string GetNullTerminatedString(int index, int maxLengthBytes)
		{
			// NOTE currently only really suited to single-byte character strings
			sbyte[] bytes = GetBytes(index, maxLengthBytes);
			// Count the number of non-null bytes
			int length = 0;
			while (length < bytes.Length && bytes[length] != '\0')
			{
				length++;
			}
			return Sharpen.Runtime.GetStringForBytes(bytes, 0, length);
		}
	}
}
