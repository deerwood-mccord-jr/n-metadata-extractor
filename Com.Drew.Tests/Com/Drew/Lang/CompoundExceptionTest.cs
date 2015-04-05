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
using NUnit.Framework;
using Sharpen;

namespace Com.Drew.Lang
{
	/// <author>Drew Noakes http://drewnoakes.com</author>
	public class CompoundExceptionTest
	{
		/// <exception cref="System.Exception"/>
		[NUnit.Framework.Test]
		public virtual void TestGetMessage_NonNested()
		{
			try
			{
				throw new CompoundException("message");
			}
			catch (CompoundException e)
			{
				Sharpen.Tests.AreEqual("message", e.Message);
			}
		}

		/// <exception cref="System.Exception"/>
		[NUnit.Framework.Test]
		public virtual void TestGetMessage_Nested()
		{
			try
			{
				try
				{
					throw new IOException("io");
				}
				catch (IOException e)
				{
					throw new CompoundException("compound", e);
				}
			}
			catch (CompoundException e)
			{
				Sharpen.Tests.AreEqual("compound", e.Message);
				Exception innerException = e.GetInnerException();
				NUnit.Framework.Assert.IsNotNull(innerException);
				Sharpen.Tests.AreEqual("io", innerException.Message);
			}
		}

		/// <exception cref="System.Exception"/>
		[NUnit.Framework.Test]
		public virtual void TestNoInnerException()
		{
			try
			{
				throw new CompoundException("message", null);
			}
			catch (CompoundException e)
			{
				try
				{
					PrintStream nullStream = new PrintStream(new NullOutputStream());
					Sharpen.Runtime.PrintStackTrace(e, nullStream);
					Sharpen.Runtime.PrintStackTrace(e, new PrintWriter(nullStream));
				}
				catch (Exception)
				{
					NUnit.Framework.Assert.Fail("Exception during printStackTrace for CompoundException with no inner exception");
				}
			}
		}
	}
}
