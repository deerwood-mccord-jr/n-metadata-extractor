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
using System.Collections.Generic;
using Sharpen;

namespace Com.Drew.Metadata.Exif
{
	/// <summary>Base class for several Exif format tag directories.</summary>
	/// <author>Drew Noakes https://drewnoakes.com</author>
	public abstract class ExifDirectoryBase : Com.Drew.Metadata.Directory
	{
		public const int TagInteropIndex = unchecked((int)(0x0001));

		public const int TagInteropVersion = unchecked((int)(0x0002));

		/// <summary>The new subfile type tag.</summary>
		/// <remarks>
		/// The new subfile type tag.
		/// 0 = Full-resolution Image
		/// 1 = Reduced-resolution image
		/// 2 = Single page of multi-page image
		/// 3 = Single page of multi-page reduced-resolution image
		/// 4 = Transparency mask
		/// 5 = Transparency mask of reduced-resolution image
		/// 6 = Transparency mask of multi-page image
		/// 7 = Transparency mask of reduced-resolution multi-page image
		/// </remarks>
		public const int TagNewSubfileType = unchecked((int)(0x00FE));

		/// <summary>The old subfile type tag.</summary>
		/// <remarks>
		/// The old subfile type tag.
		/// 1 = Full-resolution image (Main image)
		/// 2 = Reduced-resolution image (Thumbnail)
		/// 3 = Single page of multi-page image
		/// </remarks>
		public const int TagSubfileType = unchecked((int)(0x00FF));

		public const int TagImageWidth = unchecked((int)(0x0100));

		public const int TagImageHeight = unchecked((int)(0x0101));

		/// <summary>
		/// When image format is no compression, this value shows the number of bits
		/// per component for each pixel.
		/// </summary>
		/// <remarks>
		/// When image format is no compression, this value shows the number of bits
		/// per component for each pixel. Usually this value is '8,8,8'.
		/// </remarks>
		public const int TagBitsPerSample = unchecked((int)(0x0102));

		public const int TagCompression = unchecked((int)(0x0103));

		/// <summary>Shows the color space of the image data components.</summary>
		/// <remarks>
		/// Shows the color space of the image data components.
		/// 0 = WhiteIsZero
		/// 1 = BlackIsZero
		/// 2 = RGB
		/// 3 = RGB Palette
		/// 4 = Transparency Mask
		/// 5 = CMYK
		/// 6 = YCbCr
		/// 8 = CIELab
		/// 9 = ICCLab
		/// 10 = ITULab
		/// 32803 = Color Filter Array
		/// 32844 = Pixar LogL
		/// 32845 = Pixar LogLuv
		/// 34892 = Linear Raw
		/// </remarks>
		public const int TagPhotometricInterpretation = unchecked((int)(0x0106));

		/// <summary>
		/// 1 = No dithering or halftoning
		/// 2 = Ordered dither or halftone
		/// 3 = Randomized dither
		/// </summary>
		public const int TagThresholding = unchecked((int)(0x0107));

		/// <summary>
		/// 1 = Normal
		/// 2 = Reversed
		/// </summary>
		public const int TagFillOrder = unchecked((int)(0x010A));

		public const int TagDocumentName = unchecked((int)(0x010D));

		public const int TagImageDescription = unchecked((int)(0x010E));

		public const int TagMake = unchecked((int)(0x010F));

		public const int TagModel = unchecked((int)(0x0110));

		/// <summary>The position in the file of raster data.</summary>
		public const int TagStripOffsets = unchecked((int)(0x0111));

		public const int TagOrientation = unchecked((int)(0x0112));

		/// <summary>Each pixel is composed of this many samples.</summary>
		public const int TagSamplesPerPixel = unchecked((int)(0x0115));

		/// <summary>The raster is codified by a single block of data holding this many rows.</summary>
		public const int TagRowsPerStrip = unchecked((int)(0x0116));

		/// <summary>The size of the raster data in bytes.</summary>
		public const int TagStripByteCounts = unchecked((int)(0x0117));

		public const int TagMinSampleValue = unchecked((int)(0x0118));

		public const int TagMaxSampleValue = unchecked((int)(0x0119));

		public const int TagXResolution = unchecked((int)(0x011A));

		public const int TagYResolution = unchecked((int)(0x011B));

		/// <summary>
		/// When image format is no compression YCbCr, this value shows byte aligns of
		/// YCbCr data.
		/// </summary>
		/// <remarks>
		/// When image format is no compression YCbCr, this value shows byte aligns of
		/// YCbCr data. If value is '1', Y/Cb/Cr value is chunky format, contiguous for
		/// each subsampling pixel. If value is '2', Y/Cb/Cr value is separated and
		/// stored to Y plane/Cb plane/Cr plane format.
		/// </remarks>
		public const int TagPlanarConfiguration = unchecked((int)(0x011C));

		public const int TagPageName = unchecked((int)(0x011D));

		public const int TagResolutionUnit = unchecked((int)(0x0128));

		public const int TagTransferFunction = unchecked((int)(0x012D));

		public const int TagSoftware = unchecked((int)(0x0131));

		public const int TagDatetime = unchecked((int)(0x0132));

		public const int TagArtist = unchecked((int)(0x013B));

		public const int TagHostComputer = unchecked((int)(0x013C));

		public const int TagPredictor = unchecked((int)(0x013D));

		public const int TagWhitePoint = unchecked((int)(0x013E));

		public const int TagPrimaryChromaticities = unchecked((int)(0x013F));

		public const int TagTileWidth = unchecked((int)(0x0142));

		public const int TagTileLength = unchecked((int)(0x0143));

		public const int TagTileOffsets = unchecked((int)(0x0144));

		public const int TagTileByteCounts = unchecked((int)(0x0145));

		public const int TagSubIfdOffset = unchecked((int)(0x014a));

		public const int TagTransferRange = unchecked((int)(0x0156));

		public const int TagJpegTables = unchecked((int)(0x015B));

		public const int TagJpegProc = unchecked((int)(0x0200));

		public const int TagYcbcrCoefficients = unchecked((int)(0x0211));

		public const int TagYcbcrSubsampling = unchecked((int)(0x0212));

		public const int TagYcbcrPositioning = unchecked((int)(0x0213));

		public const int TagReferenceBlackWhite = unchecked((int)(0x0214));

		public const int TagRelatedImageFileFormat = unchecked((int)(0x1000));

		public const int TagRelatedImageWidth = unchecked((int)(0x1001));

		public const int TagRelatedImageHeight = unchecked((int)(0x1002));

		public const int TagRating = unchecked((int)(0x4746));

		public const int TagCfaRepeatPatternDim = unchecked((int)(0x828D));

		/// <summary>There are two definitions for CFA pattern, I don't know the difference...</summary>
		public const int TagCfaPattern2 = unchecked((int)(0x828E));

		public const int TagBatteryLevel = unchecked((int)(0x828F));

		public const int TagCopyright = unchecked((int)(0x8298));

		/// <summary>Exposure time (reciprocal of shutter speed).</summary>
		/// <remarks>Exposure time (reciprocal of shutter speed). Unit is second.</remarks>
		public const int TagExposureTime = unchecked((int)(0x829A));

		/// <summary>The actual F-number(F-stop) of lens when the image was taken.</summary>
		public const int TagFnumber = unchecked((int)(0x829D));

		public const int TagIptcNaa = unchecked((int)(0x83BB));

		public const int TagInterColorProfile = unchecked((int)(0x8773));

		/// <summary>Exposure program that the camera used when image was taken.</summary>
		/// <remarks>
		/// Exposure program that the camera used when image was taken. '1' means
		/// manual control, '2' program normal, '3' aperture priority, '4' shutter
		/// priority, '5' program creative (slow program), '6' program action
		/// (high-speed program), '7' portrait mode, '8' landscape mode.
		/// </remarks>
		public const int TagExposureProgram = unchecked((int)(0x8822));

		public const int TagSpectralSensitivity = unchecked((int)(0x8824));

		public const int TagIsoEquivalent = unchecked((int)(0x8827));

		/// <summary>Indicates the Opto-Electric Conversion Function (OECF) specified in ISO 14524.</summary>
		/// <remarks>
		/// Indicates the Opto-Electric Conversion Function (OECF) specified in ISO 14524.
		/// <p>
		/// OECF is the relationship between the camera optical input and the image values.
		/// <p>
		/// The values are:
		/// <ul>
		/// <li>Two shorts, indicating respectively number of columns, and number of rows.</li>
		/// <li>For each column, the column name in a null-terminated ASCII string.</li>
		/// <li>For each cell, an SRATIONAL value.</li>
		/// </ul>
		/// </remarks>
		public const int TagOptoElectricConversionFunction = unchecked((int)(0x8828));

		public const int TagInterlace = unchecked((int)(0x8829));

		public const int TagTimeZoneOffsetTiffEp = unchecked((int)(0x882A));

		public const int TagSelfTimerModeTiffEp = unchecked((int)(0x882B));

		/// <summary>Applies to ISO tag.</summary>
		/// <remarks>
		/// Applies to ISO tag.
		/// 0 = Unknown
		/// 1 = Standard Output Sensitivity
		/// 2 = Recommended Exposure Index
		/// 3 = ISO Speed
		/// 4 = Standard Output Sensitivity and Recommended Exposure Index
		/// 5 = Standard Output Sensitivity and ISO Speed
		/// 6 = Recommended Exposure Index and ISO Speed
		/// 7 = Standard Output Sensitivity, Recommended Exposure Index and ISO Speed
		/// </remarks>
		public const int TagSensitivityType = unchecked((int)(0x8830));

		public const int TagStandardOutputSensitivity = unchecked((int)(0x8831));

		public const int TagRecommendedExposureIndex = unchecked((int)(0x8832));

		/// <summary>Non-standard, but in use.</summary>
		public const int TagTimeZoneOffset = unchecked((int)(0x882A));

		public const int TagSelfTimerMode = unchecked((int)(0x882B));

		public const int TagExifVersion = unchecked((int)(0x9000));

		public const int TagDatetimeOriginal = unchecked((int)(0x9003));

		public const int TagDatetimeDigitized = unchecked((int)(0x9004));

		public const int TagComponentsConfiguration = unchecked((int)(0x9101));

		/// <summary>Average (rough estimate) compression level in JPEG bits per pixel.</summary>
		public const int TagCompressedAverageBitsPerPixel = unchecked((int)(0x9102));

		/// <summary>Shutter speed by APEX value.</summary>
		/// <remarks>
		/// Shutter speed by APEX value. To convert this value to ordinary 'Shutter Speed';
		/// calculate this value's power of 2, then reciprocal. For example, if the
		/// ShutterSpeedValue is '4', shutter speed is 1/(24)=1/16 second.
		/// </remarks>
		public const int TagShutterSpeed = unchecked((int)(0x9201));

		/// <summary>The actual aperture value of lens when the image was taken.</summary>
		/// <remarks>
		/// The actual aperture value of lens when the image was taken. Unit is APEX.
		/// To convert this value to ordinary F-number (F-stop), calculate this value's
		/// power of root 2 (=1.4142). For example, if the ApertureValue is '5',
		/// F-number is 1.4142^5 = F5.6.
		/// </remarks>
		public const int TagAperture = unchecked((int)(0x9202));

		public const int TagBrightnessValue = unchecked((int)(0x9203));

		public const int TagExposureBias = unchecked((int)(0x9204));

		/// <summary>Maximum aperture value of lens.</summary>
		/// <remarks>
		/// Maximum aperture value of lens. You can convert to F-number by calculating
		/// power of root 2 (same process of ApertureValue:0x9202).
		/// The actual aperture value of lens when the image was taken. To convert this
		/// value to ordinary f-number(f-stop), calculate the value's power of root 2
		/// (=1.4142). For example, if the ApertureValue is '5', f-number is 1.41425^5 = F5.6.
		/// </remarks>
		public const int TagMaxAperture = unchecked((int)(0x9205));

		/// <summary>Indicates the distance the autofocus camera is focused to.</summary>
		/// <remarks>Indicates the distance the autofocus camera is focused to.  Tends to be less accurate as distance increases.</remarks>
		public const int TagSubjectDistance = unchecked((int)(0x9206));

		/// <summary>Exposure metering method.</summary>
		/// <remarks>
		/// Exposure metering method. '0' means unknown, '1' average, '2' center
		/// weighted average, '3' spot, '4' multi-spot, '5' multi-segment, '6' partial,
		/// '255' other.
		/// </remarks>
		public const int TagMeteringMode = unchecked((int)(0x9207));

		public const int TagLightSource = unchecked((int)(0x9208));

		/// <summary>White balance (aka light source).</summary>
		/// <remarks>
		/// White balance (aka light source). '0' means unknown, '1' daylight,
		/// '2' fluorescent, '3' tungsten, '10' flash, '17' standard light A,
		/// '18' standard light B, '19' standard light C, '20' D55, '21' D65,
		/// '22' D75, '255' other.
		/// </remarks>
		public const int TagWhiteBalance = unchecked((int)(0x9208));

		/// <summary>
		/// 0x0  = 0000000 = No Flash
		/// 0x1  = 0000001 = Fired
		/// 0x5  = 0000101 = Fired, Return not detected
		/// 0x7  = 0000111 = Fired, Return detected
		/// 0x9  = 0001001 = On
		/// 0xd  = 0001101 = On, Return not detected
		/// 0xf  = 0001111 = On, Return detected
		/// 0x10 = 0010000 = Off
		/// 0x18 = 0011000 = Auto, Did not fire
		/// 0x19 = 0011001 = Auto, Fired
		/// 0x1d = 0011101 = Auto, Fired, Return not detected
		/// 0x1f = 0011111 = Auto, Fired, Return detected
		/// 0x20 = 0100000 = No flash function
		/// 0x41 = 1000001 = Fired, Red-eye reduction
		/// 0x45 = 1000101 = Fired, Red-eye reduction, Return not detected
		/// 0x47 = 1000111 = Fired, Red-eye reduction, Return detected
		/// 0x49 = 1001001 = On, Red-eye reduction
		/// 0x4d = 1001101 = On, Red-eye reduction, Return not detected
		/// 0x4f = 1001111 = On, Red-eye reduction, Return detected
		/// 0x59 = 1011001 = Auto, Fired, Red-eye reduction
		/// 0x5d = 1011101 = Auto, Fired, Red-eye reduction, Return not detected
		/// 0x5f = 1011111 = Auto, Fired, Red-eye reduction, Return detected
		/// 6543210 (positions)
		/// This is a bitmask.
		/// </summary>
		/// <remarks>
		/// 0x0  = 0000000 = No Flash
		/// 0x1  = 0000001 = Fired
		/// 0x5  = 0000101 = Fired, Return not detected
		/// 0x7  = 0000111 = Fired, Return detected
		/// 0x9  = 0001001 = On
		/// 0xd  = 0001101 = On, Return not detected
		/// 0xf  = 0001111 = On, Return detected
		/// 0x10 = 0010000 = Off
		/// 0x18 = 0011000 = Auto, Did not fire
		/// 0x19 = 0011001 = Auto, Fired
		/// 0x1d = 0011101 = Auto, Fired, Return not detected
		/// 0x1f = 0011111 = Auto, Fired, Return detected
		/// 0x20 = 0100000 = No flash function
		/// 0x41 = 1000001 = Fired, Red-eye reduction
		/// 0x45 = 1000101 = Fired, Red-eye reduction, Return not detected
		/// 0x47 = 1000111 = Fired, Red-eye reduction, Return detected
		/// 0x49 = 1001001 = On, Red-eye reduction
		/// 0x4d = 1001101 = On, Red-eye reduction, Return not detected
		/// 0x4f = 1001111 = On, Red-eye reduction, Return detected
		/// 0x59 = 1011001 = Auto, Fired, Red-eye reduction
		/// 0x5d = 1011101 = Auto, Fired, Red-eye reduction, Return not detected
		/// 0x5f = 1011111 = Auto, Fired, Red-eye reduction, Return detected
		/// 6543210 (positions)
		/// This is a bitmask.
		/// 0 = flash fired
		/// 1 = return detected
		/// 2 = return able to be detected
		/// 3 = unknown
		/// 4 = auto used
		/// 5 = unknown
		/// 6 = red eye reduction used
		/// </remarks>
		public const int TagFlash = unchecked((int)(0x9209));

		/// <summary>Focal length of lens used to take image.</summary>
		/// <remarks>
		/// Focal length of lens used to take image.  Unit is millimeter.
		/// Nice digital cameras actually save the focal length as a function of how far they are zoomed in.
		/// </remarks>
		public const int TagFocalLength = unchecked((int)(0x920A));

		public const int TagFlashEnergyTiffEp = unchecked((int)(0x920B));

		public const int TagSpatialFreqResponseTiffEp = unchecked((int)(0x920C));

		public const int TagNoise = unchecked((int)(0x920D));

		public const int TagFocalPlaneXResolutionTiffEp = unchecked((int)(0x920E));

		public const int TagFocalPlaneYResolutionTiffEp = unchecked((int)(0x920F));

		public const int TagImageNumber = unchecked((int)(0x9211));

		public const int TagSecurityClassification = unchecked((int)(0x9212));

		public const int TagImageHistory = unchecked((int)(0x9213));

		public const int TagSubjectLocationTiffEp = unchecked((int)(0x9214));

		public const int TagExposureIndexTiffEp = unchecked((int)(0x9215));

		public const int TagStandardIdTiffEp = unchecked((int)(0x9216));

		/// <summary>This tag holds the Exif Makernote.</summary>
		/// <remarks>
		/// This tag holds the Exif Makernote. Makernotes are free to be in any format, though they are often IFDs.
		/// To determine the format, we consider the starting bytes of the makernote itself and sometimes the
		/// camera model and make.
		/// <p>
		/// The component count for this tag includes all of the bytes needed for the makernote.
		/// </remarks>
		public const int TagMakernote = unchecked((int)(0x927C));

		public const int TagUserComment = unchecked((int)(0x9286));

		public const int TagSubsecondTime = unchecked((int)(0x9290));

		public const int TagSubsecondTimeOriginal = unchecked((int)(0x9291));

		public const int TagSubsecondTimeDigitized = unchecked((int)(0x9292));

		/// <summary>The image title, as used by Windows XP.</summary>
		public const int TagWinTitle = unchecked((int)(0x9C9B));

		/// <summary>The image comment, as used by Windows XP.</summary>
		public const int TagWinComment = unchecked((int)(0x9C9C));

		/// <summary>The image author, as used by Windows XP (called Artist in the Windows shell).</summary>
		public const int TagWinAuthor = unchecked((int)(0x9C9D));

		/// <summary>The image keywords, as used by Windows XP.</summary>
		public const int TagWinKeywords = unchecked((int)(0x9C9E));

		/// <summary>The image subject, as used by Windows XP.</summary>
		public const int TagWinSubject = unchecked((int)(0x9C9F));

		public const int TagFlashpixVersion = unchecked((int)(0xA000));

		/// <summary>Defines Color Space.</summary>
		/// <remarks>
		/// Defines Color Space. DCF image must use sRGB color space so value is
		/// always '1'. If the picture uses the other color space, value is
		/// '65535':Uncalibrated.
		/// </remarks>
		public const int TagColorSpace = unchecked((int)(0xA001));

		public const int TagExifImageWidth = unchecked((int)(0xA002));

		public const int TagExifImageHeight = unchecked((int)(0xA003));

		public const int TagRelatedSoundFile = unchecked((int)(0xA004));

		public const int TagFlashEnergy = unchecked((int)(0xA20B));

		public const int TagSpatialFreqResponse = unchecked((int)(0xA20C));

		public const int TagFocalPlaneXResolution = unchecked((int)(0xA20E));

		public const int TagFocalPlaneYResolution = unchecked((int)(0xA20F));

		/// <summary>Unit of FocalPlaneXResolution/FocalPlaneYResolution.</summary>
		/// <remarks>
		/// Unit of FocalPlaneXResolution/FocalPlaneYResolution. '1' means no-unit,
		/// '2' inch, '3' centimeter.
		/// Note: Some of Fujifilm's digicam(e.g.FX2700,FX2900,Finepix4700Z/40i etc)
		/// uses value '3' so it must be 'centimeter', but it seems that they use a
		/// '8.3mm?'(1/3in.?) to their ResolutionUnit. Fuji's BUG? Finepix4900Z has
		/// been changed to use value '2' but it doesn't match to actual value also.
		/// </remarks>
		public const int TagFocalPlaneResolutionUnit = unchecked((int)(0xA210));

		public const int TagSubjectLocation = unchecked((int)(0xA214));

		public const int TagExposureIndex = unchecked((int)(0xA215));

		public const int TagSensingMethod = unchecked((int)(0xA217));

		public const int TagFileSource = unchecked((int)(0xA300));

		public const int TagSceneType = unchecked((int)(0xA301));

		public const int TagCfaPattern = unchecked((int)(0xA302));

		/// <summary>
		/// This tag indicates the use of special processing on image data, such as rendering
		/// geared to output.
		/// </summary>
		/// <remarks>
		/// This tag indicates the use of special processing on image data, such as rendering
		/// geared to output. When special processing is performed, the reader is expected to
		/// disable or minimize any further processing.
		/// Tag = 41985 (A401.H)
		/// Type = SHORT
		/// Count = 1
		/// Default = 0
		/// 0 = Normal process
		/// 1 = Custom process
		/// Other = reserved
		/// </remarks>
		public const int TagCustomRendered = unchecked((int)(0xA401));

		/// <summary>This tag indicates the exposure mode set when the image was shot.</summary>
		/// <remarks>
		/// This tag indicates the exposure mode set when the image was shot. In auto-bracketing
		/// mode, the camera shoots a series of frames of the same scene at different exposure settings.
		/// Tag = 41986 (A402.H)
		/// Type = SHORT
		/// Count = 1
		/// Default = none
		/// 0 = Auto exposure
		/// 1 = Manual exposure
		/// 2 = Auto bracket
		/// Other = reserved
		/// </remarks>
		public const int TagExposureMode = unchecked((int)(0xA402));

		/// <summary>This tag indicates the white balance mode set when the image was shot.</summary>
		/// <remarks>
		/// This tag indicates the white balance mode set when the image was shot.
		/// Tag = 41987 (A403.H)
		/// Type = SHORT
		/// Count = 1
		/// Default = none
		/// 0 = Auto white balance
		/// 1 = Manual white balance
		/// Other = reserved
		/// </remarks>
		public const int TagWhiteBalanceMode = unchecked((int)(0xA403));

		/// <summary>This tag indicates the digital zoom ratio when the image was shot.</summary>
		/// <remarks>
		/// This tag indicates the digital zoom ratio when the image was shot. If the
		/// numerator of the recorded value is 0, this indicates that digital zoom was
		/// not used.
		/// Tag = 41988 (A404.H)
		/// Type = RATIONAL
		/// Count = 1
		/// Default = none
		/// </remarks>
		public const int TagDigitalZoomRatio = unchecked((int)(0xA404));

		/// <summary>
		/// This tag indicates the equivalent focal length assuming a 35mm film camera,
		/// in mm.
		/// </summary>
		/// <remarks>
		/// This tag indicates the equivalent focal length assuming a 35mm film camera,
		/// in mm. A value of 0 means the focal length is unknown. Note that this tag
		/// differs from the FocalLength tag.
		/// Tag = 41989 (A405.H)
		/// Type = SHORT
		/// Count = 1
		/// Default = none
		/// </remarks>
		public const int Tag35mmFilmEquivFocalLength = unchecked((int)(0xA405));

		/// <summary>This tag indicates the type of scene that was shot.</summary>
		/// <remarks>
		/// This tag indicates the type of scene that was shot. It can also be used to
		/// record the mode in which the image was shot. Note that this differs from
		/// the scene type (SceneType) tag.
		/// Tag = 41990 (A406.H)
		/// Type = SHORT
		/// Count = 1
		/// Default = 0
		/// 0 = Standard
		/// 1 = Landscape
		/// 2 = Portrait
		/// 3 = Night scene
		/// Other = reserved
		/// </remarks>
		public const int TagSceneCaptureType = unchecked((int)(0xA406));

		/// <summary>This tag indicates the degree of overall image gain adjustment.</summary>
		/// <remarks>
		/// This tag indicates the degree of overall image gain adjustment.
		/// Tag = 41991 (A407.H)
		/// Type = SHORT
		/// Count = 1
		/// Default = none
		/// 0 = None
		/// 1 = Low gain up
		/// 2 = High gain up
		/// 3 = Low gain down
		/// 4 = High gain down
		/// Other = reserved
		/// </remarks>
		public const int TagGainControl = unchecked((int)(0xA407));

		/// <summary>
		/// This tag indicates the direction of contrast processing applied by the camera
		/// when the image was shot.
		/// </summary>
		/// <remarks>
		/// This tag indicates the direction of contrast processing applied by the camera
		/// when the image was shot.
		/// Tag = 41992 (A408.H)
		/// Type = SHORT
		/// Count = 1
		/// Default = 0
		/// 0 = Normal
		/// 1 = Soft
		/// 2 = Hard
		/// Other = reserved
		/// </remarks>
		public const int TagContrast = unchecked((int)(0xA408));

		/// <summary>
		/// This tag indicates the direction of saturation processing applied by the camera
		/// when the image was shot.
		/// </summary>
		/// <remarks>
		/// This tag indicates the direction of saturation processing applied by the camera
		/// when the image was shot.
		/// Tag = 41993 (A409.H)
		/// Type = SHORT
		/// Count = 1
		/// Default = 0
		/// 0 = Normal
		/// 1 = Low saturation
		/// 2 = High saturation
		/// Other = reserved
		/// </remarks>
		public const int TagSaturation = unchecked((int)(0xA409));

		/// <summary>
		/// This tag indicates the direction of sharpness processing applied by the camera
		/// when the image was shot.
		/// </summary>
		/// <remarks>
		/// This tag indicates the direction of sharpness processing applied by the camera
		/// when the image was shot.
		/// Tag = 41994 (A40A.H)
		/// Type = SHORT
		/// Count = 1
		/// Default = 0
		/// 0 = Normal
		/// 1 = Soft
		/// 2 = Hard
		/// Other = reserved
		/// </remarks>
		public const int TagSharpness = unchecked((int)(0xA40A));

		/// <summary>
		/// This tag indicates information on the picture-taking conditions of a particular
		/// camera model.
		/// </summary>
		/// <remarks>
		/// This tag indicates information on the picture-taking conditions of a particular
		/// camera model. The tag is used only to indicate the picture-taking conditions in
		/// the reader.
		/// Tag = 41995 (A40B.H)
		/// Type = UNDEFINED
		/// Count = Any
		/// Default = none
		/// The information is recorded in the format shown below. The data is recorded
		/// in Unicode using SHORT type for the number of display rows and columns and
		/// UNDEFINED type for the camera settings. The Unicode (UCS-2) string including
		/// Signature is NULL terminated. The specifics of the Unicode string are as given
		/// in ISO/IEC 10464-1.
		/// Length  Type        Meaning
		/// ------+-----------+------------------
		/// 2       SHORT       Display columns
		/// 2       SHORT       Display rows
		/// Any     UNDEFINED   Camera setting-1
		/// Any     UNDEFINED   Camera setting-2
		/// :       :           :
		/// Any     UNDEFINED   Camera setting-n
		/// </remarks>
		public const int TagDeviceSettingDescription = unchecked((int)(0xA40B));

		/// <summary>This tag indicates the distance to the subject.</summary>
		/// <remarks>
		/// This tag indicates the distance to the subject.
		/// Tag = 41996 (A40C.H)
		/// Type = SHORT
		/// Count = 1
		/// Default = none
		/// 0 = unknown
		/// 1 = Macro
		/// 2 = Close view
		/// 3 = Distant view
		/// Other = reserved
		/// </remarks>
		public const int TagSubjectDistanceRange = unchecked((int)(0xA40C));

		/// <summary>This tag indicates an identifier assigned uniquely to each image.</summary>
		/// <remarks>
		/// This tag indicates an identifier assigned uniquely to each image. It is
		/// recorded as an ASCII string equivalent to hexadecimal notation and 128-bit
		/// fixed length.
		/// Tag = 42016 (A420.H)
		/// Type = ASCII
		/// Count = 33
		/// Default = none
		/// </remarks>
		public const int TagImageUniqueId = unchecked((int)(0xA420));

		/// <summary>String.</summary>
		public const int TagCameraOwnerName = unchecked((int)(0xA430));

		/// <summary>String.</summary>
		public const int TagBodySerialNumber = unchecked((int)(0xA431));

		/// <summary>An array of four Rational64u numbers giving focal and aperture ranges.</summary>
		public const int TagLensSpecification = unchecked((int)(0xA432));

		/// <summary>String.</summary>
		public const int TagLensMake = unchecked((int)(0xA433));

		/// <summary>String.</summary>
		public const int TagLensModel = unchecked((int)(0xA434));

		/// <summary>String.</summary>
		public const int TagLensSerialNumber = unchecked((int)(0xA435));

		/// <summary>Rational64u.</summary>
		public const int TagGamma = unchecked((int)(0xA500));

		public const int TagPrintIm = unchecked((int)(0xC4A5));

		public const int TagPanasonicTitle = unchecked((int)(0xC6D2));

		public const int TagPanasonicTitle2 = unchecked((int)(0xC6D3));

		public const int TagPadding = unchecked((int)(0xEA1C));

		public const int TagLens = unchecked((int)(0xFDEA));

		// TODO duplicate tag
		// TODO duplicate tag
		protected internal static void AddExifTagNames(Dictionary<int?, string> map)
		{
			map.Put(TagInteropIndex, "Interoperability Index");
			map.Put(TagInteropVersion, "Interoperability Version");
			map.Put(TagNewSubfileType, "New Subfile Type");
			map.Put(TagSubfileType, "Subfile Type");
			map.Put(TagImageWidth, "Image Width");
			map.Put(TagImageHeight, "Image Height");
			map.Put(TagBitsPerSample, "Bits Per Sample");
			map.Put(TagCompression, "Compression");
			map.Put(TagPhotometricInterpretation, "Photometric Interpretation");
			map.Put(TagThresholding, "Thresholding");
			map.Put(TagFillOrder, "Fill Order");
			map.Put(TagDocumentName, "Document Name");
			map.Put(TagImageDescription, "Image Description");
			map.Put(TagMake, "Make");
			map.Put(TagModel, "Model");
			map.Put(TagStripOffsets, "Strip Offsets");
			map.Put(TagOrientation, "Orientation");
			map.Put(TagSamplesPerPixel, "Samples Per Pixel");
			map.Put(TagRowsPerStrip, "Rows Per Strip");
			map.Put(TagStripByteCounts, "Strip Byte Counts");
			map.Put(TagMinSampleValue, "Minimum sample value");
			map.Put(TagMaxSampleValue, "Maximum sample value");
			map.Put(TagXResolution, "X Resolution");
			map.Put(TagYResolution, "Y Resolution");
			map.Put(TagPlanarConfiguration, "Planar Configuration");
			map.Put(TagPageName, "Page Name");
			map.Put(TagResolutionUnit, "Resolution Unit");
			map.Put(TagTransferFunction, "Transfer Function");
			map.Put(TagSoftware, "Software");
			map.Put(TagDatetime, "Date/Time");
			map.Put(TagArtist, "Artist");
			map.Put(TagPredictor, "Predictor");
			map.Put(TagHostComputer, "Host Computer");
			map.Put(TagWhitePoint, "White Point");
			map.Put(TagPrimaryChromaticities, "Primary Chromaticities");
			map.Put(TagTileWidth, "Tile Width");
			map.Put(TagTileLength, "Tile Length");
			map.Put(TagTileOffsets, "Tile Offsets");
			map.Put(TagTileByteCounts, "Tile Byte Counts");
			map.Put(TagSubIfdOffset, "Sub IFD Pointer(s)");
			map.Put(TagTransferRange, "Transfer Range");
			map.Put(TagJpegTables, "JPEG Tables");
			map.Put(TagJpegProc, "JPEG Proc");
			map.Put(TagYcbcrCoefficients, "YCbCr Coefficients");
			map.Put(TagYcbcrSubsampling, "YCbCr Sub-Sampling");
			map.Put(TagYcbcrPositioning, "YCbCr Positioning");
			map.Put(TagReferenceBlackWhite, "Reference Black/White");
			map.Put(TagRelatedImageFileFormat, "Related Image File Format");
			map.Put(TagRelatedImageWidth, "Related Image Width");
			map.Put(TagRelatedImageHeight, "Related Image Height");
			map.Put(TagRating, "Rating");
			map.Put(TagCfaRepeatPatternDim, "CFA Repeat Pattern Dim");
			map.Put(TagCfaPattern2, "CFA Pattern");
			map.Put(TagBatteryLevel, "Battery Level");
			map.Put(TagCopyright, "Copyright");
			map.Put(TagExposureTime, "Exposure Time");
			map.Put(TagFnumber, "F-Number");
			map.Put(TagIptcNaa, "IPTC/NAA");
			map.Put(TagInterColorProfile, "Inter Color Profile");
			map.Put(TagExposureProgram, "Exposure Program");
			map.Put(TagSpectralSensitivity, "Spectral Sensitivity");
			map.Put(TagIsoEquivalent, "ISO Speed Ratings");
			map.Put(TagOptoElectricConversionFunction, "Opto-electric Conversion Function (OECF)");
			map.Put(TagInterlace, "Interlace");
			map.Put(TagTimeZoneOffsetTiffEp, "Time Zone Offset");
			map.Put(TagSelfTimerModeTiffEp, "Self Timer Mode");
			map.Put(TagSensitivityType, "Sensitivity Type");
			map.Put(TagStandardOutputSensitivity, "Standard Output Sensitivity");
			map.Put(TagRecommendedExposureIndex, "Recommended Exposure Index");
			map.Put(TagTimeZoneOffset, "Time Zone Offset");
			map.Put(TagSelfTimerMode, "Self Timer Mode");
			map.Put(TagExifVersion, "Exif Version");
			map.Put(TagDatetimeOriginal, "Date/Time Original");
			map.Put(TagDatetimeDigitized, "Date/Time Digitized");
			map.Put(TagComponentsConfiguration, "Components Configuration");
			map.Put(TagCompressedAverageBitsPerPixel, "Compressed Bits Per Pixel");
			map.Put(TagShutterSpeed, "Shutter Speed Value");
			map.Put(TagAperture, "Aperture Value");
			map.Put(TagBrightnessValue, "Brightness Value");
			map.Put(TagExposureBias, "Exposure Bias Value");
			map.Put(TagMaxAperture, "Max Aperture Value");
			map.Put(TagSubjectDistance, "Subject Distance");
			map.Put(TagMeteringMode, "Metering Mode");
			map.Put(TagLightSource, "Light Source");
			map.Put(TagWhiteBalance, "White Balance");
			map.Put(TagFlash, "Flash");
			map.Put(TagFocalLength, "Focal Length");
			map.Put(TagFlashEnergyTiffEp, "Flash Energy");
			map.Put(TagSpatialFreqResponseTiffEp, "Spatial Frequency Response");
			map.Put(TagNoise, "Noise");
			map.Put(TagFocalPlaneXResolutionTiffEp, "Focal Plane X Resolution");
			map.Put(TagFocalPlaneYResolutionTiffEp, "Focal Plane Y Resolution");
			map.Put(TagImageNumber, "Image Number");
			map.Put(TagSecurityClassification, "Security Classification");
			map.Put(TagImageHistory, "Image History");
			map.Put(TagSubjectLocationTiffEp, "Subject Location");
			map.Put(TagExposureIndexTiffEp, "Exposure Index");
			map.Put(TagStandardIdTiffEp, "TIFF/EP Standard ID");
			map.Put(TagMakernote, "Makernote");
			map.Put(TagUserComment, "User Comment");
			map.Put(TagSubsecondTime, "Sub-Sec Time");
			map.Put(TagSubsecondTimeOriginal, "Sub-Sec Time Original");
			map.Put(TagSubsecondTimeDigitized, "Sub-Sec Time Digitized");
			map.Put(TagWinTitle, "Windows XP Title");
			map.Put(TagWinComment, "Windows XP Comment");
			map.Put(TagWinAuthor, "Windows XP Author");
			map.Put(TagWinKeywords, "Windows XP Keywords");
			map.Put(TagWinSubject, "Windows XP Subject");
			map.Put(TagFlashpixVersion, "FlashPix Version");
			map.Put(TagColorSpace, "Color Space");
			map.Put(TagExifImageWidth, "Exif Image Width");
			map.Put(TagExifImageHeight, "Exif Image Height");
			map.Put(TagRelatedSoundFile, "Related Sound File");
			map.Put(TagFlashEnergy, "Flash Energy");
			map.Put(TagSpatialFreqResponse, "Spatial Frequency Response");
			map.Put(TagFocalPlaneXResolution, "Focal Plane X Resolution");
			map.Put(TagFocalPlaneYResolution, "Focal Plane Y Resolution");
			map.Put(TagFocalPlaneResolutionUnit, "Focal Plane Resolution Unit");
			map.Put(TagSubjectLocation, "Subject Location");
			map.Put(TagExposureIndex, "Exposure Index");
			map.Put(TagSensingMethod, "Sensing Method");
			map.Put(TagFileSource, "File Source");
			map.Put(TagSceneType, "Scene Type");
			map.Put(TagCfaPattern, "CFA Pattern");
			map.Put(TagCustomRendered, "Custom Rendered");
			map.Put(TagExposureMode, "Exposure Mode");
			map.Put(TagWhiteBalanceMode, "White Balance Mode");
			map.Put(TagDigitalZoomRatio, "Digital Zoom Ratio");
			map.Put(Tag35mmFilmEquivFocalLength, "Focal Length 35");
			map.Put(TagSceneCaptureType, "Scene Capture Type");
			map.Put(TagGainControl, "Gain Control");
			map.Put(TagContrast, "Contrast");
			map.Put(TagSaturation, "Saturation");
			map.Put(TagSharpness, "Sharpness");
			map.Put(TagDeviceSettingDescription, "Device Setting Description");
			map.Put(TagSubjectDistanceRange, "Subject Distance Range");
			map.Put(TagImageUniqueId, "Unique Image ID");
			map.Put(TagCameraOwnerName, "Camera Owner Name");
			map.Put(TagBodySerialNumber, "Body Serial Number");
			map.Put(TagLensSpecification, "Lens Specification");
			map.Put(TagLensMake, "Lens Make");
			map.Put(TagLensModel, "Lens Model");
			map.Put(TagLensSerialNumber, "Lens Serial Number");
			map.Put(TagGamma, "Gamma");
			map.Put(TagPrintIm, "Print IM");
			map.Put(TagPanasonicTitle, "Panasonic Title");
			map.Put(TagPanasonicTitle2, "Panasonic Title (2)");
			map.Put(TagPadding, "Padding");
			map.Put(TagLens, "Lens");
		}
	}
}
