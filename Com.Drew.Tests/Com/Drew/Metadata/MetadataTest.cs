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
using Com.Drew.Metadata.Exif;
using Sharpen;

namespace Com.Drew.Metadata
{
	/// <summary>JUnit test case for class Metadata.</summary>
	/// <author>Drew Noakes https://drewnoakes.com</author>
	public class MetadataTest
	{
		[NUnit.Framework.Test]
		public virtual void TestGetDirectoryWhenNotExists()
		{
			NUnit.Framework.Assert.IsNull(new Com.Drew.Metadata.Metadata().GetFirstDirectoryOfType<ExifSubIFDDirectory>());
		}

		/// <exception cref="System.Exception"/>
		[NUnit.Framework.Test]
		public virtual void TestHasErrors()
		{
			ExifSubIFDDirectory directory = new ExifSubIFDDirectory();
			directory.AddError("Test Error 1");
			Com.Drew.Metadata.Metadata metadata = new Com.Drew.Metadata.Metadata();
			Sharpen.Tests.IsFalse(metadata.HasErrors());
			metadata.AddDirectory(directory);
			Sharpen.Tests.IsTrue(metadata.HasErrors());
		}

		[NUnit.Framework.Test]
		public virtual void TestToString()
		{
			Com.Drew.Metadata.Metadata metadata = new Com.Drew.Metadata.Metadata();
			Sharpen.Tests.AreEqual("Metadata (0 directories)", metadata.ToString());
			metadata.AddDirectory(new ExifIFD0Directory());
			Sharpen.Tests.AreEqual("Metadata (1 directory)", metadata.ToString());
			metadata.AddDirectory(new ExifSubIFDDirectory());
			Sharpen.Tests.AreEqual("Metadata (2 directories)", metadata.ToString());
		}
	}
}
