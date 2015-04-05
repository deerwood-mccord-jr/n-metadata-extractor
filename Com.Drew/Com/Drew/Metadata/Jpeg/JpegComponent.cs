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
using JetBrains.Annotations;
using Sharpen;

namespace Com.Drew.Metadata.Jpeg
{
	/// <summary>
	/// Stores information about a JPEG image component such as the component id, horiz/vert sampling factor and
	/// quantization table number.
	/// </summary>
	/// <author>Drew Noakes http://drewnoakes.com</author>
	[System.Serializable]
	public class JpegComponent
	{
		private const long serialVersionUID = 61121257899091914L;

		private readonly int _componentId;

		private readonly int _samplingFactorByte;

		private readonly int _quantizationTableNumber;

		public JpegComponent(int componentId, int samplingFactorByte, int quantizationTableNumber)
		{
			_componentId = componentId;
			_samplingFactorByte = samplingFactorByte;
			_quantizationTableNumber = quantizationTableNumber;
		}

		public virtual int GetComponentId()
		{
			return _componentId;
		}

		/// <summary>Returns the component name (one of: Y, Cb, Cr, I, or Q)</summary>
		/// <returns>the component name</returns>
		[CanBeNull]
		public virtual string GetComponentName()
		{
			switch (_componentId)
			{
				case 1:
				{
					return "Y";
				}

				case 2:
				{
					return "Cb";
				}

				case 3:
				{
					return "Cr";
				}

				case 4:
				{
					return "I";
				}

				case 5:
				{
					return "Q";
				}
			}
			return null;
		}

		public virtual int GetQuantizationTableNumber()
		{
			return _quantizationTableNumber;
		}

		public virtual int GetHorizontalSamplingFactor()
		{
			return _samplingFactorByte & unchecked((int)(0x0F));
		}

		public virtual int GetVerticalSamplingFactor()
		{
			return (_samplingFactorByte >> 4) & unchecked((int)(0x0F));
		}
	}
}
