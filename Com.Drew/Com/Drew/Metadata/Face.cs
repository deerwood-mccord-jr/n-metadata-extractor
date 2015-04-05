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
using System.Text;
using Com.Drew.Metadata;
using JetBrains.Annotations;
using Sharpen;

namespace Com.Drew.Metadata
{
	/// <summary>Class to hold information about a detected or recognized face in a photo.</summary>
	/// <remarks>
	/// Class to hold information about a detected or recognized face in a photo.
	/// <p/>
	/// When a face is <em>detected</em>, the camera believes that a face is present at a given location in
	/// the image, but is not sure whose face it is.  When a face is <em>recognised</em>, then the face is
	/// both detected and identified as belonging to a known person.
	/// </remarks>
	/// <author>Philipp Sandhaus, Drew Noakes</author>
	public class Face
	{
		private readonly int _x;

		private readonly int _y;

		private readonly int _width;

		private readonly int _height;

		[CanBeNull]
		private readonly string _name;

		[CanBeNull]
		private readonly Age _age;

		public Face(int x, int y, int width, int height, string name, Age age)
		{
			_x = x;
			_y = y;
			_width = width;
			_height = height;
			_name = name;
			_age = age;
		}

		public virtual int GetX()
		{
			return _x;
		}

		public virtual int GetY()
		{
			return _y;
		}

		public virtual int GetWidth()
		{
			return _width;
		}

		public virtual int GetHeight()
		{
			return _height;
		}

		[CanBeNull]
		public virtual string GetName()
		{
			return _name;
		}

		[CanBeNull]
		public virtual Age GetAge()
		{
			return _age;
		}

		public override bool Equals(object o)
		{
			if (this == o)
			{
				return true;
			}
			if (o == null || GetType() != o.GetType())
			{
				return false;
			}
			Com.Drew.Metadata.Face face = (Com.Drew.Metadata.Face)o;
			if (_height != face._height)
			{
				return false;
			}
			if (_width != face._width)
			{
				return false;
			}
			if (_x != face._x)
			{
				return false;
			}
			if (_y != face._y)
			{
				return false;
			}
			if (_age != null ? !_age.Equals(face._age) : face._age != null)
			{
				return false;
			}
			if (_name != null ? !_name.Equals(face._name) : face._name != null)
			{
				return false;
			}
			return true;
		}

		public override int GetHashCode()
		{
			int result = _x;
			result = 31 * result + _y;
			result = 31 * result + _width;
			result = 31 * result + _height;
			result = 31 * result + (_name != null ? _name.GetHashCode() : 0);
			result = 31 * result + (_age != null ? _age.GetHashCode() : 0);
			return result;
		}

		[NotNull]
		public override string ToString()
		{
			StringBuilder result = new StringBuilder();
			result.Append("x: ").Append(_x);
			result.Append(" y: ").Append(_y);
			result.Append(" width: ").Append(_width);
			result.Append(" height: ").Append(_height);
			if (_name != null)
			{
				result.Append(" name: ").Append(_name);
			}
			if (_age != null)
			{
				result.Append(" age: ").Append(_age.ToFriendlyString());
			}
			return result.ToString();
		}
	}
}
