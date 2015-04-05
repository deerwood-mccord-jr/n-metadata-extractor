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
using Com.Drew.Metadata.Exif;
using Com.Drew.Metadata.Exif.Makernotes;
using Sharpen;

namespace Com.Drew.Metadata.Exif
{
	/// <author>Drew Noakes http://drewnoakes.com</author>
	public class SonyType1MakernoteTest
	{
		/// <exception cref="System.Exception"/>
		[NUnit.Framework.Test]
		public virtual void TestSonyType1Makernote()
		{
			SonyType1MakernoteDirectory directory = ExifReaderTest.ProcessBytes<SonyType1MakernoteDirectory>("Tests/Data/sonyType1.jpg.app1");
			NUnit.Framework.Assert.IsNotNull(directory);
			Sharpen.Tests.IsFalse(directory.HasErrors());
			SonyType1MakernoteDescriptor descriptor = new SonyType1MakernoteDescriptor(directory);
			NUnit.Framework.Assert.IsNull(directory.GetObject(SonyType1MakernoteDirectory.TagColorTemperature));
			NUnit.Framework.Assert.IsNull(descriptor.GetColorTemperatureDescription());
			NUnit.Framework.Assert.IsNull(directory.GetObject(SonyType1MakernoteDirectory.TagSceneMode));
			NUnit.Framework.Assert.IsNull(descriptor.GetSceneModeDescription());
			NUnit.Framework.Assert.IsNull(directory.GetObject(SonyType1MakernoteDirectory.TagZoneMatching));
			NUnit.Framework.Assert.IsNull(descriptor.GetZoneMatchingDescription());
			NUnit.Framework.Assert.IsNull(directory.GetObject(SonyType1MakernoteDirectory.TagDynamicRangeOptimiser));
			NUnit.Framework.Assert.IsNull(descriptor.GetDynamicRangeOptimizerDescription());
			NUnit.Framework.Assert.IsNull(directory.GetObject(SonyType1MakernoteDirectory.TagImageStabilisation));
			NUnit.Framework.Assert.IsNull(descriptor.GetImageStabilizationDescription());
			NUnit.Framework.Assert.IsNull(directory.GetObject(SonyType1MakernoteDirectory.TagColorMode));
			NUnit.Framework.Assert.IsNull(descriptor.GetColorModeDescription());
			Sharpen.Tests.AreEqual("On (Shooting)", descriptor.GetAntiBlurDescription());
			Sharpen.Tests.AreEqual("Program", descriptor.GetExposureModeDescription());
			Sharpen.Tests.AreEqual("Off", descriptor.GetLongExposureNoiseReductionDescription());
			Sharpen.Tests.AreEqual("Off", descriptor.GetMacroDescription());
			Sharpen.Tests.AreEqual("Normal", descriptor.GetJpegQualityDescription());
		}
	}
}
