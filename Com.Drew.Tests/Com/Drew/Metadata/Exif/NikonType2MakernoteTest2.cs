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
using Com.Drew.Lang;
using Com.Drew.Metadata.Exif;
using Com.Drew.Metadata.Exif.Makernotes;
using Sharpen;

namespace Com.Drew.Metadata.Exif
{
	/// <author>Drew Noakes http://drewnoakes.com</author>
	public class NikonType2MakernoteTest2
	{
		private NikonType2MakernoteDirectory _nikonDirectory;

		private ExifIFD0Directory _exifIFD0Directory;

		private ExifSubIFDDirectory _exifSubIFDDirectory;

		private ExifThumbnailDirectory _thumbDirectory;

		/// <exception cref="System.Exception"/>
		[NUnit.Framework.SetUp]
		public virtual void SetUp()
		{
			Com.Drew.Metadata.Metadata metadata = ExifReaderTest.ProcessBytes("Tests/Data/nikonMakernoteType2b.jpg.app1");
			_nikonDirectory = metadata.GetDirectory<NikonType2MakernoteDirectory>();
			_exifIFD0Directory = metadata.GetDirectory<ExifIFD0Directory>();
			_exifSubIFDDirectory = metadata.GetDirectory<ExifSubIFDDirectory>();
			_thumbDirectory = metadata.GetDirectory<ExifThumbnailDirectory>();
			NUnit.Framework.Assert.IsNotNull(_nikonDirectory);
			NUnit.Framework.Assert.IsNotNull(_exifSubIFDDirectory);
		}

		/*
        [Nikon Makernote] Makernote Unknown 1 =
        [Nikon Makernote] ISO Setting = Unknown (0 0)
        [Nikon Makernote] Color Mode = COLOR
        [Nikon Makernote] Quality = NORMAL
        [Nikon Makernote] White Balance = AUTO
        [Nikon Makernote] Image Sharpening = AUTO
        [Nikon Makernote] Focus Mode = AF-C
        [Nikon Makernote] Flash Setting = NORMAL
        [Nikon Makernote] Makernote Unknown 2 = 4416/500
        [Nikon Makernote] ISO Selection = AUTO
        [Nikon Makernote] Unknown tag (0x0011) = 1300
        [Nikon Makernote] Image Adjustment = AUTO
        [Nikon Makernote] Adapter = OFF
        [Nikon Makernote] Focus Distance = 0
        [Nikon Makernote] Digital Zoom = No digital zoom
        [Nikon Makernote] AF Focus Position = Unknown ()
        [Nikon Makernote] Unknown tag (0x008f) =
        [Nikon Makernote] Unknown tag (0x0094) = 0
        [Nikon Makernote] Unknown tag (0x0095) = FPNR
        [Nikon Makernote] Unknown tag (0x0e00) = PrintIM
        [Nikon Makernote] Unknown tag (0x0e10) = 1394
    */
		/// <exception cref="System.Exception"/>
		[NUnit.Framework.Test]
		public virtual void TestNikonMakernote_MatchesKnownValues()
		{
			Sharpen.Tests.AreEqual("0 1 0 0", _nikonDirectory.GetString(NikonType2MakernoteDirectory.TagFirmwareVersion));
			Sharpen.Tests.AreEqual("0 0", _nikonDirectory.GetString(NikonType2MakernoteDirectory.TagIso1));
			Sharpen.Tests.AreEqual("COLOR", _nikonDirectory.GetString(NikonType2MakernoteDirectory.TagColorMode));
			Sharpen.Tests.AreEqual("NORMAL ", _nikonDirectory.GetString(NikonType2MakernoteDirectory.TagQualityAndFileFormat));
			Sharpen.Tests.AreEqual("AUTO        ", _nikonDirectory.GetString(NikonType2MakernoteDirectory.TagCameraWhiteBalance));
			Sharpen.Tests.AreEqual("AUTO  ", _nikonDirectory.GetString(NikonType2MakernoteDirectory.TagCameraSharpening));
			Sharpen.Tests.AreEqual("AF-C  ", _nikonDirectory.GetString(NikonType2MakernoteDirectory.TagAfType));
			Sharpen.Tests.AreEqual("NORMAL      ", _nikonDirectory.GetString(NikonType2MakernoteDirectory.TagFlashSyncMode));
			//        assertEquals(new Rational(4416,500), _nikonDirectory.getRational(NikonType3MakernoteDirectory.TAG_UNKNOWN_2));
			Sharpen.Tests.AreEqual("AUTO  ", _nikonDirectory.GetString(NikonType2MakernoteDirectory.TagIsoMode));
			Sharpen.Tests.AreEqual(1300, _nikonDirectory.GetInt(unchecked((int)(0x0011))));
			Sharpen.Tests.AreEqual("AUTO         ", _nikonDirectory.GetString(NikonType2MakernoteDirectory.TagImageAdjustment));
			Sharpen.Tests.AreEqual("OFF         ", _nikonDirectory.GetString(NikonType2MakernoteDirectory.TagAdapter));
			Sharpen.Tests.AreEqual(0, _nikonDirectory.GetInt(NikonType2MakernoteDirectory.TagManualFocusDistance));
			Sharpen.Tests.AreEqual(1, _nikonDirectory.GetInt(NikonType2MakernoteDirectory.TagDigitalZoom));
			Sharpen.Tests.AreEqual("                ", _nikonDirectory.GetString(unchecked((int)(0x008f))));
			Sharpen.Tests.AreEqual(0, _nikonDirectory.GetInt(unchecked((int)(0x0094))));
			Sharpen.Tests.AreEqual("FPNR", _nikonDirectory.GetString(unchecked((int)(0x0095))));
			Sharpen.Tests.AreEqual("80 114 105 110 116 73 77 0 48 49 48 48 0 0 13 0 1 0 22 0 22 0 2 0 1 0 0 0 3 0 94 0 0 0 7 0 0 0 0 0 8 0 0 0 0 0 9 0 0 0 0 0 10 0 0 0 0 0 11 0 -90 0 0 0 12 0 0 0 0 0 13 0 0 0 0 0 14 0 -66 0 0 0 0 1 5 0 0 0 1 1 1 0 0 0 9 17 0 0 16 39 0 0 11 15 0 0 16 39 0 0 -105 5 0 0 16 39 0 0 -80 8 0 0 16 39 0 0 1 28 0 0 16 39 0 0 94 2 0 0 16 39 0 0 -117 0 0 0 16 39 0 0 -53 3 0 0 16 39 0 0 -27 27 0 0 16 39 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0"
				, _nikonDirectory.GetString(unchecked((int)(0x0e00))));
			//        assertEquals("PrintIM", _nikonDirectory.getString(0x0e00));
			Sharpen.Tests.AreEqual(1394, _nikonDirectory.GetInt(unchecked((int)(0x0e10))));
		}

		/*
        [Exif] Image Description =
        [Exif] Make = NIKON
        [Exif] Model = E995
        [Exif] X Resolution = 300 dots per inch
        [Exif] Y Resolution = 300 dots per inch
        [Exif] Resolution Unit = Inch
        [Exif] Software = E995v1.6
        [Exif] Date/Time = 2002:08:29 17:31:40
        [Exif] YCbCr Positioning = Center of pixel array
        [Exif] Exposure Time = 2439024/100000000 sec
        [Exif] F-Number = F2.6
        [Exif] Exposure Program = Program normal
        [Exif] ISO Speed Ratings = 100
        [Exif] Exif Version = 2.10
        [Exif] Date/Time Original = 2002:08:29 17:31:40
        [Exif] Date/Time Digitized = 2002:08:29 17:31:40
        [Exif] Components Configuration = YCbCr
        [Exif] Exposure Bias Value = 0 EV
        [Exif] Max Aperture Value = F1
        [Exif] Metering Mode = Multi-segment
        [Exif] White Balance = Unknown
        [Exif] Flash = Flash fired
        [Exif] Focal Length = 8.2 mm
        [Exif] User Comment =
        [Exif] FlashPix Version = 1.00
        [Exif] Color Space = sRGB
        [Exif] Exif Image Width = 2048 pixels
        [Exif] Exif Image Height = 1536 pixels
        [Exif] File Source = Digital Still Camera (DSC)
        [Exif] Scene Type = Directly photographed image
    */
		/// <exception cref="System.Exception"/>
		[NUnit.Framework.Test]
		public virtual void TestExifDirectory_MatchesKnownValues()
		{
			Sharpen.Tests.AreEqual("          ", _exifIFD0Directory.GetString(ExifIFD0Directory.TagImageDescription));
			Sharpen.Tests.AreEqual("NIKON", _exifIFD0Directory.GetString(ExifIFD0Directory.TagMake));
			Sharpen.Tests.AreEqual("E995", _exifIFD0Directory.GetString(ExifIFD0Directory.TagModel));
			Sharpen.Tests.AreEqual(300, _exifIFD0Directory.GetDouble(ExifIFD0Directory.TagXResolution), 0.001);
			Sharpen.Tests.AreEqual(300, _exifIFD0Directory.GetDouble(ExifIFD0Directory.TagYResolution), 0.001);
			Sharpen.Tests.AreEqual(2, _exifIFD0Directory.GetInt(ExifIFD0Directory.TagResolutionUnit));
			Sharpen.Tests.AreEqual("E995v1.6", _exifIFD0Directory.GetString(ExifIFD0Directory.TagSoftware));
			Sharpen.Tests.AreEqual("2002:08:29 17:31:40", _exifIFD0Directory.GetString(ExifIFD0Directory.TagDatetime));
			Sharpen.Tests.AreEqual(1, _exifIFD0Directory.GetInt(ExifIFD0Directory.TagYcbcrPositioning));
			Sharpen.Tests.AreEqual(new Rational(2439024, 100000000), _exifSubIFDDirectory.GetRational(ExifSubIFDDirectory.TagExposureTime));
			Sharpen.Tests.AreEqual(2.6, _exifSubIFDDirectory.GetDouble(ExifSubIFDDirectory.TagFnumber), 0.001);
			Sharpen.Tests.AreEqual(2, _exifSubIFDDirectory.GetInt(ExifSubIFDDirectory.TagExposureProgram));
			Sharpen.Tests.AreEqual(100, _exifSubIFDDirectory.GetInt(ExifSubIFDDirectory.TagIsoEquivalent));
			Sharpen.Tests.AreEqual("48 50 49 48", _exifSubIFDDirectory.GetString(ExifSubIFDDirectory.TagExifVersion));
			Sharpen.Tests.AreEqual("2002:08:29 17:31:40", _exifSubIFDDirectory.GetString(ExifSubIFDDirectory.TagDatetimeDigitized));
			Sharpen.Tests.AreEqual("2002:08:29 17:31:40", _exifSubIFDDirectory.GetString(ExifSubIFDDirectory.TagDatetimeOriginal));
			Sharpen.Tests.AreEqual("1 2 3 0", _exifSubIFDDirectory.GetString(ExifSubIFDDirectory.TagComponentsConfiguration));
			Sharpen.Tests.AreEqual(0, _exifSubIFDDirectory.GetInt(ExifSubIFDDirectory.TagExposureBias));
			Sharpen.Tests.AreEqual("0", _exifSubIFDDirectory.GetString(ExifSubIFDDirectory.TagMaxAperture));
			Sharpen.Tests.AreEqual(5, _exifSubIFDDirectory.GetInt(ExifSubIFDDirectory.TagMeteringMode));
			Sharpen.Tests.AreEqual(0, _exifSubIFDDirectory.GetInt(ExifSubIFDDirectory.TagWhiteBalance));
			Sharpen.Tests.AreEqual(1, _exifSubIFDDirectory.GetInt(ExifSubIFDDirectory.TagFlash));
			Sharpen.Tests.AreEqual(8.2, _exifSubIFDDirectory.GetDouble(ExifSubIFDDirectory.TagFocalLength), 0.001);
			Sharpen.Tests.AreEqual("0 0 0 0 0 0 0 0 32 32 32 32 32 32 32 32 32 32 32 32 32 32 32 32 32 32 32 32 32 32 32 32 32 32 32 32 32 32 32 32 32 32 32 32 32 32 32 32 32 32 32 32 32 32 32 32 32 32 32 32 32 32 32 32 32 32 32 32 32 32 32 32 32 32 32 32 32 32 32 32 32 32 32 32 32 32 32 32 32 32 32 32 32 32 32 32 32 32 32 32 32 32 32 32 32 32 32 32 32 32 32 32 32 32 32 32 32 32 32 32 32 32 32 32 32"
				, _exifSubIFDDirectory.GetString(ExifSubIFDDirectory.TagUserComment));
			Sharpen.Tests.AreEqual("48 49 48 48", _exifSubIFDDirectory.GetString(ExifSubIFDDirectory.TagFlashpixVersion));
			Sharpen.Tests.AreEqual(1, _exifSubIFDDirectory.GetInt(ExifSubIFDDirectory.TagColorSpace));
			Sharpen.Tests.AreEqual(2048, _exifSubIFDDirectory.GetInt(ExifSubIFDDirectory.TagExifImageWidth));
			Sharpen.Tests.AreEqual(1536, _exifSubIFDDirectory.GetInt(ExifSubIFDDirectory.TagExifImageHeight));
			Sharpen.Tests.AreEqual(3, _exifSubIFDDirectory.GetInt(ExifSubIFDDirectory.TagFileSource));
			Sharpen.Tests.AreEqual(1, _exifSubIFDDirectory.GetInt(ExifSubIFDDirectory.TagSceneType));
		}

		/*
        [Exif Thumbnail] Thumbnail Compression = JPEG (old-style)
        [Exif Thumbnail] X Resolution = 72 dots per inch
        [Exif Thumbnail] Y Resolution = 72 dots per inch
        [Exif Thumbnail] Resolution Unit = Inch
        [Exif Thumbnail] Thumbnail Offset = 1494 bytes
        [Exif Thumbnail] Thumbnail Length = 6077 bytes
    */
		/// <exception cref="System.Exception"/>
		[NUnit.Framework.Test]
		public virtual void TestExifThumbnailDirectory_MatchesKnownValues()
		{
			Sharpen.Tests.AreEqual(6, _thumbDirectory.GetInt(ExifThumbnailDirectory.TagThumbnailCompression));
			Sharpen.Tests.AreEqual(1494, _thumbDirectory.GetInt(ExifThumbnailDirectory.TagThumbnailOffset));
			Sharpen.Tests.AreEqual(6077, _thumbDirectory.GetInt(ExifThumbnailDirectory.TagThumbnailLength));
			Sharpen.Tests.AreEqual(1494, _thumbDirectory.GetInt(ExifThumbnailDirectory.TagThumbnailOffset));
			Sharpen.Tests.AreEqual(72, _thumbDirectory.GetInt(ExifThumbnailDirectory.TagXResolution));
			Sharpen.Tests.AreEqual(72, _thumbDirectory.GetInt(ExifThumbnailDirectory.TagYResolution));
		}
	}
}
