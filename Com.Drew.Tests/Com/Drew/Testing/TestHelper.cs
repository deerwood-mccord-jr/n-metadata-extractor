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
using Com.Drew.Testing;
using Sharpen;

namespace Com.Drew.Testing
{
	/// <author>Drew Noakes http://drewnoakes.com</author>
	public class TestHelper
	{
		public static sbyte[] SkipBytes(sbyte[] input, int countToSkip)
		{
			if (input.Length - countToSkip < 0)
			{
				throw new ArgumentException("Attempting to skip more bytes than exist in the array.");
			}
			sbyte[] output = new sbyte[input.Length - countToSkip];
			System.Array.Copy(input, countToSkip, output, 0, input.Length - countToSkip);
			return output;
		}
	}
}
