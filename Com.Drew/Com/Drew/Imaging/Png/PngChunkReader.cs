using System.Collections.Generic;
using Com.Drew.Lang;
using JetBrains.Annotations;
using Sharpen;

namespace Com.Drew.Imaging.Png
{
	/// <author>Drew Noakes http://drewnoakes.com</author>
	public class PngChunkReader
	{
		private static readonly sbyte[] PngSignatureBytes = new sbyte[] { unchecked((sbyte)0x89), unchecked((int)(0x50)), unchecked((int)(0x4E)), unchecked((int)(0x47)), unchecked((int)(0x0D)), unchecked((int)(0x0A)), unchecked((int)(0x1A)), unchecked(
			(int)(0x0A)) };

		/// <exception cref="Com.Drew.Imaging.Png.PngProcessingException"/>
		/// <exception cref="System.IO.IOException"/>
		public virtual Iterable<PngChunk> Extract([NotNull] SequentialReader reader, [CanBeNull] ICollection<PngChunkType> desiredChunkTypes)
		{
			//
			// PNG DATA STREAM
			//
			// Starts with a PNG SIGNATURE, followed by a sequence of CHUNKS.
			//
			// PNG SIGNATURE
			//
			//   Always composed of these bytes: 89 50 4E 47 0D 0A 1A 0A
			//
			// CHUNK
			//
			//   4 - length of the data field (unsigned, but always within 31 bytes), may be zero
			//   4 - chunk type, restricted to [65,90] and [97,122] (A-Za-z)
			//   * - data field
			//   4 - CRC calculated from chunk type and chunk data, but not length
			//
			// CHUNK TYPES
			//
			//   Critical Chunk Types:
			//
			//     IHDR - image header, always the first chunk in the data stream
			//     PLTE - palette table, associated with indexed PNG images
			//     IDAT - image data chunk, of which there may be many
			//     IEND - image trailer, always the last chunk in the data stream
			//
			//   Ancillary Chunk Types:
			//
			//     Transparency information:  tRNS
			//     Colour space information:  cHRM, gAMA, iCCP, sBIT, sRGB
			//     Textual information:       iTXt, tEXt, zTXt
			//     Miscellaneous information: bKGD, hIST, pHYs, sPLT
			//     Time information:          tIME
			//
			reader.SetMotorolaByteOrder(true);
			// network byte order
			if (!Arrays.Equals(PngSignatureBytes, reader.GetBytes(PngSignatureBytes.Length)))
			{
				throw new PngProcessingException("PNG signature mismatch");
			}
			bool seenImageHeader = false;
			bool seenImageTrailer = false;
			IList<PngChunk> chunks = new AList<PngChunk>();
			ICollection<PngChunkType> seenChunkTypes = new HashSet<PngChunkType>();
			while (!seenImageTrailer)
			{
				// Process the next chunk.
				int chunkDataLength = reader.GetInt32();
				PngChunkType chunkType = new PngChunkType(reader.GetBytes(4));
				sbyte[] chunkData = reader.GetBytes(chunkDataLength);
				// Skip the CRC bytes at the end of the chunk
				// TODO consider verifying the CRC value to determine if we're processing bad data
				reader.Skip(4);
				if (seenChunkTypes.Contains(chunkType) && !chunkType.AreMultipleAllowed())
				{
					throw new PngProcessingException(Sharpen.Extensions.StringFormat("Observed multiple instances of PNG chunk '%s', for which multiples are not allowed", chunkType));
				}
				if (chunkType.Equals(PngChunkType.Ihdr))
				{
					seenImageHeader = true;
				}
				else
				{
					if (!seenImageHeader)
					{
						throw new PngProcessingException(Sharpen.Extensions.StringFormat("First chunk should be '%s', but '%s' was observed", PngChunkType.Ihdr, chunkType));
					}
				}
				if (chunkType.Equals(PngChunkType.Iend))
				{
					seenImageTrailer = true;
				}
				if (desiredChunkTypes == null || desiredChunkTypes.Contains(chunkType))
				{
					chunks.Add(new PngChunk(chunkType, chunkData));
				}
				seenChunkTypes.Add(chunkType);
			}
			return chunks.AsIterable();
		}
	}
}
