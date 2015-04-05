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
using System.Collections.Generic;
using Com.Drew.Imaging.Jpeg;
using Com.Drew.Lang;
using Com.Drew.Metadata.Xmp;
using Com.Drew.Tools;
using Sharpen;

namespace Com.Drew.Metadata.Xmp
{
	/// <author>Drew Noakes http://drewnoakes.com</author>
	public class XmpReaderTest
	{
		/// <exception cref="System.IO.IOException"/>
		public static XmpDirectory ProcessApp1Bytes(string filePath)
		{
			Com.Drew.Metadata.Metadata metadata = new Com.Drew.Metadata.Metadata();
			new XmpReader().Extract(FileUtil.ReadBytes(filePath), metadata, JpegSegmentType.App1);
			XmpDirectory directory = metadata.GetDirectory<XmpDirectory>();
			NUnit.Framework.Assert.IsNotNull(directory);
			return directory;
		}

		private XmpDirectory _directory;

		/// <exception cref="System.Exception"/>
		[NUnit.Framework.SetUp]
		public virtual void SetUp()
		{
			_directory = ProcessApp1Bytes("Tests/Data/withXmpAndIptc.jpg.app1.1");
		}

		/*
    [Xmp] Lens Information = 24/1 70/1 0/0 0/0
    [Xmp] Lens = EF24-70mm f/2.8L USM
    [Xmp] Serial Number = 380319450
    [Xmp] Firmware = 1.2.1
    [Xmp] Make = Canon
    [Xmp] Model = Canon EOS 7D
    [Xmp] Exposure Time = 1/125 sec
    [Xmp] Exposure Program = Manual control
    [Xmp] Aperture Value = F11
    [Xmp] F-Number = F11
    [Xmp] Focal Length = 57.0 mm
    [Xmp] Shutter Speed Value = 1/124 sec
    [Xmp] Date/Time Original = Sun Dec 12 11:41:35 GMT 2010
    [Xmp] Date/Time Digitized = Sun Dec 12 11:41:35 GMT 2010
    */
		/// <exception cref="System.Exception"/>
		[NUnit.Framework.Test]
		public virtual void TestExtract_LensInformation()
		{
			// Note that this tag really holds a rational array, but XmpReader doesn't parse arrays
			Sharpen.Tests.AreEqual("24/1 70/1 0/0 0/0", _directory.GetString(XmpDirectory.TagLensInfo));
		}

		//        Rational[] info = _directory.getRationalArray(XmpDirectory.TAG_LENS_INFO);
		//        assertEquals(new Rational(24, 1), info[0]);
		//        assertEquals(new Rational(70, 1), info[1]);
		//        assertEquals(new Rational(0, 0), info[2]);
		//        assertEquals(new Rational(0, 0), info[3]);
		/// <exception cref="System.Exception"/>
		[NUnit.Framework.Test]
		public virtual void TestExtract_HasXMPMeta()
		{
			NUnit.Framework.Assert.IsNotNull(_directory.GetXMPMeta());
		}

		/// <exception cref="System.Exception"/>
		[NUnit.Framework.Test]
		public virtual void TestExtract_Lens()
		{
			Sharpen.Tests.AreEqual("EF24-70mm f/2.8L USM", _directory.GetString(XmpDirectory.TagLens));
		}

		/*
    // this requires further research

    @Test
    public void testExtract_Format() throws Exception
    {
        assertEquals("image/tiff", _directory.getString(XmpDirectory.TAG_FORMAT));
    }

    @Test
    public void testExtract_Creator() throws Exception
    {
        assertEquals("", _directory.getString(XmpDirectory.TAG_CREATOR));
    }

    @Test
    public void testExtract_Rights() throws Exception
    {
        assertEquals("", _directory.getString(XmpDirectory.TAG_RIGHTS));
    }

    @Test
    public void testExtract_Description() throws Exception
    {
        assertEquals("", _directory.getString(XmpDirectory.TAG_DESCRIPTION));
    }
*/
		/// <exception cref="System.Exception"/>
		[NUnit.Framework.Test]
		public virtual void TestExtract_SerialNumber()
		{
			Sharpen.Tests.AreEqual("380319450", _directory.GetString(XmpDirectory.TagCameraSerialNumber));
		}

		/// <exception cref="System.Exception"/>
		[NUnit.Framework.Test]
		public virtual void TestExtract_Firmware()
		{
			Sharpen.Tests.AreEqual("1.2.1", _directory.GetString(XmpDirectory.TagFirmware));
		}

		/// <exception cref="System.Exception"/>
		[NUnit.Framework.Test]
		public virtual void TestExtract_Maker()
		{
			Sharpen.Tests.AreEqual("Canon", _directory.GetString(XmpDirectory.TagMake));
		}

		/// <exception cref="System.Exception"/>
		[NUnit.Framework.Test]
		public virtual void TestExtract_Model()
		{
			Sharpen.Tests.AreEqual("Canon EOS 7D", _directory.GetString(XmpDirectory.TagModel));
		}

		/// <exception cref="System.Exception"/>
		[NUnit.Framework.Test]
		public virtual void TestExtract_ExposureTime()
		{
			// Note XmpReader doesn't parse this as a rational even though it appears to be... need more examples
			Sharpen.Tests.AreEqual("1/125", _directory.GetString(XmpDirectory.TagExposureTime));
		}

		//        assertEquals(new Rational(1, 125), _directory.getRational(XmpDirectory.TAG_EXPOSURE_TIME));
		/// <exception cref="System.Exception"/>
		[NUnit.Framework.Test]
		public virtual void TestExtract_ExposureProgram()
		{
			Sharpen.Tests.AreEqual(1, _directory.GetInt(XmpDirectory.TagExposureProgram));
		}

		/// <exception cref="System.Exception"/>
		[NUnit.Framework.Test]
		public virtual void TestExtract_FNumber()
		{
			Sharpen.Tests.AreEqual(new Rational(11, 1), _directory.GetRational(XmpDirectory.TagFNumber));
		}

		/// <exception cref="System.Exception"/>
		[NUnit.Framework.Test]
		public virtual void TestExtract_FocalLength()
		{
			Sharpen.Tests.AreEqual(new Rational(57, 1), _directory.GetRational(XmpDirectory.TagFocalLength));
		}

		/// <exception cref="System.Exception"/>
		[NUnit.Framework.Test]
		public virtual void TestExtract_ShutterSpeed()
		{
			Sharpen.Tests.AreEqual(new Rational(6965784, 1000000), _directory.GetRational(XmpDirectory.TagShutterSpeed));
		}

		/// <exception cref="System.Exception"/>
		[NUnit.Framework.Test]
		public virtual void TestExtract_OriginalDateTime()
		{
			DateTime? actual = _directory.GetDate(XmpDirectory.TagDatetimeOriginal);
			// Underlying string value (in XMP data) is: 2010-12-12T12:41:35.00+01:00
			Sharpen.Tests.AreEqual(new SimpleDateFormat("hh:mm:ss dd MM yyyy Z").Parse("11:41:35 12 12 2010 +0000"), actual);
			//        assertEquals(new SimpleDateFormat("HH:mm:ss dd MMM yyyy Z").parse("12:41:35 12 Dec 2010 +0100"), actual);
			Sharpen.Calendar calendar = new Sharpen.GregorianCalendar(2010, 12 - 1, 12, 11, 41, 35);
			calendar.SetTimeZone(Sharpen.Extensions.GetTimeZone("GMT"));
			Sharpen.Tests.AreEqual(calendar.GetTime(), actual);
		}

		/// <exception cref="System.Exception"/>
		[NUnit.Framework.Test]
		public virtual void TestExtract_DigitizedDateTime()
		{
			DateTime? actual = _directory.GetDate(XmpDirectory.TagDatetimeDigitized);
			// Underlying string value (in XMP data) is: 2010-12-12T12:41:35.00+01:00
			Sharpen.Tests.AreEqual(new SimpleDateFormat("hh:mm:ss dd MM yyyy Z").Parse("11:41:35 12 12 2010 +0000"), actual);
			//        assertEquals(new SimpleDateFormat("HH:mm:ss dd MMM yyyy Z").parse("12:41:35 12 Dec 2010 +0100"), actual);
			Sharpen.Calendar calendar = new Sharpen.GregorianCalendar(2010, 12 - 1, 12, 11, 41, 35);
			calendar.SetTimeZone(Sharpen.Extensions.GetTimeZone("GMT"));
			Sharpen.Tests.AreEqual(calendar.GetTime(), actual);
		}

		/// <exception cref="System.Exception"/>
		[NUnit.Framework.Test]
		public virtual void TestGetXmpProperties()
		{
			IDictionary<string, string> propertyMap = _directory.GetXmpProperties();
			Sharpen.Tests.AreEqual(179, propertyMap.Count);
			Sharpen.Tests.IsTrue(propertyMap.ContainsKey("photoshop:Country"));
			Sharpen.Tests.AreEqual("Deutschland", propertyMap.Get("photoshop:Country"));
			Sharpen.Tests.IsTrue(propertyMap.ContainsKey("tiff:ImageLength"));
			Sharpen.Tests.AreEqual("900", propertyMap.Get("tiff:ImageLength"));
		}
	}
}
