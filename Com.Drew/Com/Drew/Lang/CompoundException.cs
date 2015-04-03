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
using System.Text;
using JetBrains.Annotations;
using Sharpen;

namespace Com.Drew.Lang
{
	/// <summary>
	/// Represents a compound exception, as modelled in JDK 1.4, but
	/// unavailable in previous versions.
	/// </summary>
	/// <remarks>
	/// Represents a compound exception, as modelled in JDK 1.4, but
	/// unavailable in previous versions.  This class allows support
	/// of these previous JDK versions.
	/// </remarks>
	/// <author>Drew Noakes https://drewnoakes.com</author>
	[System.Serializable]
	public class CompoundException : Exception
	{
		private const long serialVersionUID = -9207883813472069925L;

		[CanBeNull]
		private readonly Exception _innerException;

		public CompoundException([CanBeNull] string msg)
			: this(msg, null)
		{
		}

		public CompoundException([CanBeNull] Exception exception)
			: this(null, exception)
		{
		}

		public CompoundException([CanBeNull] string msg, [CanBeNull] Exception innerException)
			: base(msg)
		{
			_innerException = innerException;
		}

		[CanBeNull]
		public virtual Exception GetInnerException()
		{
			return _innerException;
		}

		[NotNull]
		public override string ToString()
		{
			StringBuilder @string = new StringBuilder();
			@string.Append(base.Sharpen.Extensions.ConvertToString());
			if (_innerException != null)
			{
				@string.Append("\n");
				@string.Append("--- inner exception ---");
				@string.Append("\n");
				@string.Append(Sharpen.Extensions.ConvertToString(_innerException));
			}
			return Sharpen.Extensions.ConvertToString(@string);
		}

//		public override void PrintStackTrace([NotNull] PrintStream s)
//		{
//			base.Sharpen.Runtime.PrintStackTrace(s);
//			if (_innerException != null)
//			{
//				s.Println("--- inner exception ---");
//				Sharpen.Runtime.PrintStackTrace(_innerException, s);
//			}
//		}
//
//		public override void PrintStackTrace([NotNull] PrintWriter s)
//		{
//			base.Sharpen.Runtime.PrintStackTrace(s);
//			if (_innerException != null)
//			{
//				s.WriteLine("--- inner exception ---");
//				Sharpen.Runtime.PrintStackTrace(_innerException, s);
//			}
//		}
//
//		public override void PrintStackTrace()
//		{
//			base.Sharpen.Runtime.PrintStackTrace();
//			if (_innerException != null)
//			{
//				System.Console.Error.Println("--- inner exception ---");
//				Sharpen.Runtime.PrintStackTrace(_innerException);
//			}
//		}
	}
}
