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
using System.IO;
using System.Text;
using Com.Drew.Lang;
using Com.Drew.Metadata;
using JetBrains.Annotations;
using Sharpen;
using Sharpen.Reflect;

namespace Com.Drew.Metadata
{
	/// <summary>
	/// Abstract base class for all directory implementations, having methods for getting and setting tag values of various
	/// data types.
	/// </summary>
	/// <author>Drew Noakes http://drewnoakes.com</author>
	public abstract class Directory
	{
		/// <summary>Map of values hashed by type identifiers.</summary>
		[NotNull]
		protected internal readonly IDictionary<int, object> _tagMap = new Dictionary<int, object>();

		/// <summary>A convenient list holding tag values in the order in which they were stored.</summary>
		/// <remarks>
		/// A convenient list holding tag values in the order in which they were stored.
		/// This is used for creation of an iterator, and for counting the number of
		/// defined tags.
		/// </remarks>
		[NotNull]
		protected internal readonly ICollection<Tag> _definedTagList = new AList<Tag>();

		[NotNull]
		private readonly ICollection<string> _errorList = new AList<string>(4);

		/// <summary>The descriptor used to interpret tag values.</summary>
		protected internal ITagDescriptor _descriptor;

		// ABSTRACT METHODS
		/// <summary>Provides the name of the directory, for display purposes.</summary>
		/// <remarks>Provides the name of the directory, for display purposes.  E.g. <code>Exif</code></remarks>
		/// <returns>the name of the directory</returns>
		[NotNull]
		public abstract string GetName();

		/// <summary>Provides the map of tag names, hashed by tag type identifier.</summary>
		/// <returns>the map of tag names</returns>
		[NotNull]
		protected internal abstract Dictionary<int, string> GetTagNameMap();

		protected internal Directory()
		{
		}

		// VARIOUS METHODS
		/// <summary>Indicates whether the specified tag type has been set.</summary>
		/// <param name="tagType">the tag type to check for</param>
		/// <returns>true if a value exists for the specified tag type, false if not</returns>
		public virtual bool ContainsTag(int tagType)
		{
			return _tagMap.ContainsKey(Sharpen.Extensions.ValueOf(tagType));
		}

		/// <summary>Returns an Iterator of Tag instances that have been set in this Directory.</summary>
		/// <returns>an Iterator of Tag instances</returns>
		[NotNull]
		public virtual ICollection<Tag> GetTags()
		{
			return _definedTagList;
		}

		/// <summary>Returns the number of tags set in this Directory.</summary>
		/// <returns>the number of tags set in this Directory</returns>
		public virtual int GetTagCount()
		{
			return _definedTagList.Count;
		}

		/// <summary>Sets the descriptor used to interpret tag values.</summary>
		/// <param name="descriptor">the descriptor used to interpret tag values</param>
		public virtual void SetDescriptor(ITagDescriptor descriptor)
		{
			if (descriptor == null)
			{
				throw new ArgumentNullException("cannot set a null descriptor");
			}
			_descriptor = descriptor;
		}

		/// <summary>Registers an error message with this directory.</summary>
		/// <param name="message">an error message.</param>
		public virtual void AddError(string message)
		{
			_errorList.Add(message);
		}

		/// <summary>Gets a value indicating whether this directory has any error messages.</summary>
		/// <returns>true if the directory contains errors, otherwise false</returns>
		public virtual bool HasErrors()
		{
			return _errorList.Count > 0;
		}

		/// <summary>Used to iterate over any error messages contained in this directory.</summary>
		/// <returns>an iterable collection of error message strings.</returns>
		[NotNull]
		public virtual Iterable<string> GetErrors()
		{
			return _errorList.AsIterable();
		}

		/// <summary>Returns the count of error messages in this directory.</summary>
		public virtual int GetErrorCount()
		{
			return _errorList.Count;
		}

		// TAG SETTERS
		/// <summary>Sets an <code>int</code> value for the specified tag.</summary>
		/// <param name="tagType">the tag's value as an int</param>
		/// <param name="value">the value for the specified tag as an int</param>
		public virtual void SetInt(int tagType, int value)
		{
			SetObject(tagType, value);
		}

		/// <summary>Sets an <code>int[]</code> (array) for the specified tag.</summary>
		/// <param name="tagType">the tag identifier</param>
		/// <param name="ints">the int array to store</param>
		public virtual void SetIntArray(int tagType, int[] ints)
		{
			SetObjectArray(tagType, ints);
		}

		/// <summary>Sets a <code>float</code> value for the specified tag.</summary>
		/// <param name="tagType">the tag's value as an int</param>
		/// <param name="value">the value for the specified tag as a float</param>
		public virtual void SetFloat(int tagType, float value)
		{
			SetObject(tagType, value);
		}

		/// <summary>Sets a <code>float[]</code> (array) for the specified tag.</summary>
		/// <param name="tagType">the tag identifier</param>
		/// <param name="floats">the float array to store</param>
		public virtual void SetFloatArray(int tagType, float[] floats)
		{
			SetObjectArray(tagType, floats);
		}

		/// <summary>Sets a <code>double</code> value for the specified tag.</summary>
		/// <param name="tagType">the tag's value as an int</param>
		/// <param name="value">the value for the specified tag as a double</param>
		public virtual void SetDouble(int tagType, double value)
		{
			SetObject(tagType, value);
		}

		/// <summary>Sets a <code>double[]</code> (array) for the specified tag.</summary>
		/// <param name="tagType">the tag identifier</param>
		/// <param name="doubles">the double array to store</param>
		public virtual void SetDoubleArray(int tagType, double[] doubles)
		{
			SetObjectArray(tagType, doubles);
		}

		/// <summary>Sets a <code>String</code> value for the specified tag.</summary>
		/// <param name="tagType">the tag's value as an int</param>
		/// <param name="value">the value for the specified tag as a String</param>
		public virtual void SetString(int tagType, string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("cannot set a null String");
			}
			SetObject(tagType, value);
		}

		/// <summary>Sets a <code>String[]</code> (array) for the specified tag.</summary>
		/// <param name="tagType">the tag identifier</param>
		/// <param name="strings">the String array to store</param>
		public virtual void SetStringArray(int tagType, string[] strings)
		{
			SetObjectArray(tagType, strings);
		}

		/// <summary>Sets a <code>boolean</code> value for the specified tag.</summary>
		/// <param name="tagType">the tag's value as an int</param>
		/// <param name="value">the value for the specified tag as a boolean</param>
		public virtual void SetBoolean(int tagType, bool value)
		{
			SetObject(tagType, value);
		}

		/// <summary>Sets a <code>long</code> value for the specified tag.</summary>
		/// <param name="tagType">the tag's value as an int</param>
		/// <param name="value">the value for the specified tag as a long</param>
		public virtual void SetLong(int tagType, long value)
		{
			SetObject(tagType, value);
		}

		/// <summary>Sets a <code>java.util.Date</code> value for the specified tag.</summary>
		/// <param name="tagType">the tag's value as an int</param>
		/// <param name="value">the value for the specified tag as a java.util.Date</param>
		public virtual void SetDate(int tagType, DateTime value)
		{
			SetObject(tagType, value);
		}

		/// <summary>Sets a <code>Rational</code> value for the specified tag.</summary>
		/// <param name="tagType">the tag's value as an int</param>
		/// <param name="rational">rational number</param>
		public virtual void SetRational(int tagType, Rational rational)
		{
			SetObject(tagType, rational);
		}

		/// <summary>Sets a <code>Rational[]</code> (array) for the specified tag.</summary>
		/// <param name="tagType">the tag identifier</param>
		/// <param name="rationals">the Rational array to store</param>
		public virtual void SetRationalArray(int tagType, Rational[] rationals)
		{
			SetObjectArray(tagType, rationals);
		}

		/// <summary>Sets a <code>byte[]</code> (array) for the specified tag.</summary>
		/// <param name="tagType">the tag identifier</param>
		/// <param name="bytes">the byte array to store</param>
		public virtual void SetByteArray(int tagType, sbyte[] bytes)
		{
			SetObjectArray(tagType, bytes);
		}

		/// <summary>Sets a <code>Object</code> for the specified tag.</summary>
		/// <param name="tagType">the tag's value as an int</param>
		/// <param name="value">the value for the specified tag</param>
		/// <exception cref="System.ArgumentNullException">if value is <code>null</code></exception>
		public virtual void SetObject(int tagType, object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("cannot set a null object");
			}
			if (!_tagMap.ContainsKey(Sharpen.Extensions.ValueOf(tagType)))
			{
				_definedTagList.Add(new Tag(tagType, this));
			}
			//        else {
			//            final Object oldValue = _tagMap.get(tagType);
			//            if (!oldValue.equals(value))
			//                addError(Sharpen.Extensions.StringFormat("Overwritten tag 0x%s (%s).  Old=%s, New=%s", Integer.toHexString(tagType), getTagName(tagType), oldValue, value));
			//        }
			_tagMap.Put(tagType, value);
		}

		/// <summary>Sets an array <code>Object</code> for the specified tag.</summary>
		/// <param name="tagType">the tag's value as an int</param>
		/// <param name="array">the array of values for the specified tag</param>
		public virtual void SetObjectArray(int tagType, object array)
		{
			// for now, we don't do anything special -- this method might be a candidate for removal once the dust settles
			SetObject(tagType, array);
		}

		// TAG GETTERS
		/// <summary>Returns the specified tag's value as an int, if possible.</summary>
		/// <remarks>
		/// Returns the specified tag's value as an int, if possible.  Every attempt to represent the tag's value as an int
		/// is taken.  Here is a list of the action taken depending upon the tag's original type:
		/// <ul>
		/// <li> int - Return unchanged.
		/// <li> Number - Return an int value (real numbers are truncated).
		/// <li> Rational - Truncate any fractional part and returns remaining int.
		/// <li> String - Attempt to parse string as an int.  If this fails, convert the char[] to an int (using shifts and OR).
		/// <li> Rational[] - Return int value of first item in array.
		/// <li> byte[] - Return int value of first item in array.
		/// <li> int[] - Return int value of first item in array.
		/// </ul>
		/// </remarks>
		/// <exception cref="MetadataException">if no value exists for tagType or if it cannot be converted to an int.</exception>
		/// <exception cref="Com.Drew.Metadata.MetadataException"/>
		public virtual int? GetInt(int tagType)
		{
			int? integer = GetInteger(tagType);
			if (integer != null)
			{
				return integer;
			}
			object o = GetObject(tagType);
			if (o == null)
			{
				throw new MetadataException("Tag '" + GetTagName(tagType) + "' has not been set -- check using containsTag() first");
			}
			throw new MetadataException("Tag '" + tagType + "' cannot be converted to int.  It is of type '" + o.GetType() + "'.");
		}

		/// <summary>Returns the specified tag's value as an Integer, if possible.</summary>
		/// <remarks>
		/// Returns the specified tag's value as an Integer, if possible.  Every attempt to represent the tag's value as an
		/// Integer is taken.  Here is a list of the action taken depending upon the tag's original type:
		/// <ul>
		/// <li> int - Return unchanged
		/// <li> Number - Return an int value (real numbers are truncated)
		/// <li> Rational - Truncate any fractional part and returns remaining int
		/// <li> String - Attempt to parse string as an int.  If this fails, convert the char[] to an int (using shifts and OR)
		/// <li> Rational[] - Return int value of first item in array if length &gt; 0
		/// <li> byte[] - Return int value of first item in array if length &gt; 0
		/// <li> int[] - Return int value of first item in array if length &gt; 0
		/// </ul>
		/// If the value is not found or cannot be converted to int, <code>null</code> is returned.
		/// </remarks>
		[CanBeNull]
		public virtual int? GetInteger(int tagType)
		{
			object o = GetObject(tagType);
			if (o == null)
			{
				return null;
			}
			if (o.IsNumber())
			{
				return Number.GetInstance(o).IntValue();
			}
			else
			{
				if (o is string)
				{
					try
					{
						return System.Convert.ToInt32((string)o);
					}
					catch (FormatException)
					{
						// convert the char array to an int
						string s = (string)o;
						sbyte[] bytes = Sharpen.Runtime.GetBytesForString(s);
						long val = 0;
						foreach (sbyte aByte in bytes)
						{
							val = val << 8;
							val += (aByte & unchecked((int)(0xff)));
						}
						return (int)val;
					}
				}
				else
				{
					if (o is Rational[])
					{
						Rational[] rationals = (Rational[])o;
						if (rationals.Length == 1)
						{
							return rationals[0].IntValue();
						}
					}
					else
					{
						if (o is sbyte[])
						{
							sbyte[] bytes = (sbyte[])o;
							if (bytes.Length == 1)
							{
								return (int)bytes[0];
							}
						}
						else
						{
							if (o is int[])
							{
								int[] ints = (int[])o;
								if (ints.Length == 1)
								{
									return ints[0];
								}
							}
						}
					}
				}
			}
			return null;
		}

		/// <summary>Gets the specified tag's value as a String array, if possible.</summary>
		/// <remarks>
		/// Gets the specified tag's value as a String array, if possible.  Only supported
		/// where the tag is set as String[], String, int[], byte[] or Rational[].
		/// </remarks>
		/// <param name="tagType">the tag identifier</param>
		/// <returns>the tag's value as an array of Strings. If the value is unset or cannot be converted, <code>null</code> is returned.</returns>
		[CanBeNull]
		public virtual string[] GetStringArray(int tagType)
		{
			object o = GetObject(tagType);
			if (o == null)
			{
				return null;
			}
			if (o is string[])
			{
				return (string[])o;
			}
			if (o is string)
			{
				return new string[] { (string)o };
			}
			if (o is int[])
			{
				int[] ints = (int[])o;
				string[] strings = new string[ints.Length];
				for (int i = 0; i < strings.Length; i++)
				{
					strings[i] = Sharpen.Extensions.ToString(ints[i]);
				}
				return strings;
			}
			else
			{
				if (o is sbyte[])
				{
					sbyte[] bytes = (sbyte[])o;
					string[] strings = new string[bytes.Length];
					for (int i = 0; i < strings.Length; i++)
					{
						strings[i] = System.Convert.ToString(bytes[i]);
					}
					return strings;
				}
				else
				{
					if (o is Rational[])
					{
						Rational[] rationals = (Rational[])o;
						string[] strings = new string[rationals.Length];
						for (int i = 0; i < strings.Length; i++)
						{
							strings[i] = rationals[i].ToSimpleString(false);
						}
						return strings;
					}
				}
			}
			return null;
		}

		/// <summary>Gets the specified tag's value as an int array, if possible.</summary>
		/// <remarks>
		/// Gets the specified tag's value as an int array, if possible.  Only supported
		/// where the tag is set as String, Integer, int[], byte[] or Rational[].
		/// </remarks>
		/// <param name="tagType">the tag identifier</param>
		/// <returns>the tag's value as an int array</returns>
		[CanBeNull]
		public virtual int[] GetIntArray(int tagType)
		{
			object o = GetObject(tagType);
			if (o == null)
			{
				return null;
			}
			if (o is int[])
			{
				return (int[])o;
			}
			if (o is Rational[])
			{
				Rational[] rationals = (Rational[])o;
				int[] ints = new int[rationals.Length];
				for (int i = 0; i < ints.Length; i++)
				{
					ints[i] = rationals[i].IntValue();
				}
				return ints;
			}
			if (o is short[])
			{
				short[] shorts = (short[])o;
				int[] ints = new int[shorts.Length];
				for (int i = 0; i < shorts.Length; i++)
				{
					ints[i] = shorts[i];
				}
				return ints;
			}
			if (o is sbyte[])
			{
				sbyte[] bytes = (sbyte[])o;
				int[] ints = new int[bytes.Length];
				for (int i = 0; i < bytes.Length; i++)
				{
					ints[i] = bytes[i];
				}
				return ints;
			}
			if (o is CharSequence)
			{
				CharSequence str = (CharSequence)o;
				int[] ints = new int[str.Length];
				for (int i = 0; i < str.Length; i++)
				{
					ints[i] = str[i];
				}
				return ints;
			}
			if (o is int)
			{
				return new int[] { (int)o };
			}
			return null;
		}

		/// <summary>Gets the specified tag's value as an byte array, if possible.</summary>
		/// <remarks>
		/// Gets the specified tag's value as an byte array, if possible.  Only supported
		/// where the tag is set as String, Integer, int[], byte[] or Rational[].
		/// </remarks>
		/// <param name="tagType">the tag identifier</param>
		/// <returns>the tag's value as a byte array</returns>
		[CanBeNull]
		public virtual sbyte[] GetByteArray(int tagType)
		{
			object o = GetObject(tagType);
			if (o == null)
			{
				return null;
			}
			else
			{
				if (o is Rational[])
				{
					Rational[] rationals = (Rational[])o;
					sbyte[] bytes = new sbyte[rationals.Length];
					for (int i = 0; i < bytes.Length; i++)
					{
						bytes[i] = rationals[i].ByteValue();
					}
					return bytes;
				}
				else
				{
					if (o is sbyte[])
					{
						return (sbyte[])o;
					}
					else
					{
						if (o is int[])
						{
							int[] ints = (int[])o;
							sbyte[] bytes = new sbyte[ints.Length];
							for (int i = 0; i < ints.Length; i++)
							{
								bytes[i] = unchecked((sbyte)ints[i]);
							}
							return bytes;
						}
						else
						{
							if (o is short[])
							{
								short[] shorts = (short[])o;
								sbyte[] bytes = new sbyte[shorts.Length];
								for (int i = 0; i < shorts.Length; i++)
								{
									bytes[i] = unchecked((sbyte)shorts[i]);
								}
								return bytes;
							}
							else
							{
								if (o is CharSequence)
								{
									CharSequence str = (CharSequence)o;
									sbyte[] bytes = new sbyte[str.Length];
									for (int i = 0; i < str.Length; i++)
									{
										bytes[i] = unchecked((sbyte)str[i]);
									}
									return bytes;
								}
							}
						}
					}
				}
			}
			if (o is int)
			{
				return new sbyte[] { ((int)o).ByteValue() };
			}
			return null;
		}

		/// <summary>Returns the specified tag's value as a double, if possible.</summary>
		/// <exception cref="Com.Drew.Metadata.MetadataException"/>
		public virtual double GetDouble(int tagType)
		{
			double? value = GetDoubleObject(tagType);
			if (value != null)
			{
				return value.Value;
			}
			object o = GetObject(tagType);
			if (o == null)
			{
				throw new MetadataException("Tag '" + GetTagName(tagType) + "' has not been set -- check using containsTag() first");
			}
			throw new MetadataException("Tag '" + tagType + "' cannot be converted to a double.  It is of type '" + o.GetType() + "'.");
		}

		/// <summary>Returns the specified tag's value as a Double.</summary>
		/// <remarks>Returns the specified tag's value as a Double.  If the tag is not set or cannot be converted, <code>null</code> is returned.</remarks>
		[CanBeNull]
		public virtual double? GetDoubleObject(int tagType)
		{
			object o = GetObject(tagType);
			if (o == null)
			{
				return null;
			}
			if (o is string)
			{
				try
				{
					return double.Parse((string)o);
				}
				catch (FormatException)
				{
					return null;
				}
			}
			if (o.IsNumber())
			{
				return Number.GetInstance(o).DoubleValue();
			}
			return null;
		}

		/// <summary>Returns the specified tag's value as a float, if possible.</summary>
		/// <exception cref="Com.Drew.Metadata.MetadataException"/>
		public virtual float GetFloat(int tagType)
		{
			float? value = GetFloatObject(tagType);
			if (value != null)
			{
				return value.Value;
			}
			object o = GetObject(tagType);
			if (o == null)
			{
				throw new MetadataException("Tag '" + GetTagName(tagType) + "' has not been set -- check using containsTag() first");
			}
			throw new MetadataException("Tag '" + tagType + "' cannot be converted to a float.  It is of type '" + o.GetType() + "'.");
		}

		/// <summary>Returns the specified tag's value as a float.</summary>
		/// <remarks>Returns the specified tag's value as a float.  If the tag is not set or cannot be converted, <code>null</code> is returned.</remarks>
		[CanBeNull]
		public virtual float? GetFloatObject(int tagType)
		{
			object o = GetObject(tagType);
			if (o == null)
			{
				return null;
			}
			if (o is string)
			{
				try
				{
					return float.Parse((string)o);
				}
				catch (FormatException)
				{
					return null;
				}
			}
			if (o.IsNumber())
			{
				return Number.GetInstance(o).FloatValue();
			}
			return null;
		}

		/// <summary>Returns the specified tag's value as a long, if possible.</summary>
		/// <exception cref="Com.Drew.Metadata.MetadataException"/>
		public virtual long GetLong(int tagType)
		{
			long? value = GetLongObject(tagType);
			if (value != null)
			{
				return value.Value;
			}
			object o = GetObject(tagType);
			if (o == null)
			{
				throw new MetadataException("Tag '" + GetTagName(tagType) + "' has not been set -- check using containsTag() first");
			}
			throw new MetadataException("Tag '" + tagType + "' cannot be converted to a long.  It is of type '" + o.GetType() + "'.");
		}

		/// <summary>Returns the specified tag's value as a long.</summary>
		/// <remarks>Returns the specified tag's value as a long.  If the tag is not set or cannot be converted, <code>null</code> is returned.</remarks>
		[CanBeNull]
		public virtual long? GetLongObject(int tagType)
		{
			object o = GetObject(tagType);
			if (o == null)
			{
				return null;
			}
			if (o is string)
			{
				try
				{
					return System.Convert.ToInt64((string)o);
				}
				catch (FormatException)
				{
					return null;
				}
			}
			if (o.IsNumber())
			{
				return Number.GetInstance(o).LongValue();
			}
			return null;
		}

		/// <summary>Returns the specified tag's value as a boolean, if possible.</summary>
		/// <exception cref="Com.Drew.Metadata.MetadataException"/>
		public virtual bool GetBoolean(int tagType)
		{
			bool? value = GetBooleanObject(tagType);
			if (value != null)
			{
				return value.Value;
			}
			object o = GetObject(tagType);
			if (o == null)
			{
				throw new MetadataException("Tag '" + GetTagName(tagType) + "' has not been set -- check using containsTag() first");
			}
			throw new MetadataException("Tag '" + tagType + "' cannot be converted to a boolean.  It is of type '" + o.GetType() + "'.");
		}

		/// <summary>Returns the specified tag's value as a boolean.</summary>
		/// <remarks>Returns the specified tag's value as a boolean.  If the tag is not set or cannot be converted, <code>null</code> is returned.</remarks>
		[CanBeNull]
		public virtual bool? GetBooleanObject(int tagType)
		{
			object o = GetObject(tagType);
			if (o == null)
			{
				return null;
			}
			if (o is bool)
			{
				return (bool)o;
			}
			if (o is string)
			{
				try
				{
					return bool.Parse((string)o);
				}
				catch (FormatException)
				{
					return null;
				}
			}
			if (o.IsNumber())
			{
				return (Number.GetInstance(o).DoubleValue() != 0);
			}
			return null;
		}

		/// <summary>Returns the specified tag's value as a java.util.Date.</summary>
		/// <remarks>
		/// Returns the specified tag's value as a java.util.Date.  If the value is unset or cannot be converted, <code>null</code> is returned.
		/// <p/>
		/// If the underlying value is a
		/// <see cref="string"/>
		/// , then attempts will be made to parse the string as though it is in
		/// the current
		/// <see cref="System.TimeZoneInfo"/>
		/// .  If the
		/// <see cref="System.TimeZoneInfo"/>
		/// is known, call the overload that accepts one as an argument.
		/// </remarks>
		[CanBeNull]
		public virtual DateTime? GetDate(int tagType)
		{
			return GetDate(tagType, null);
		}

		/// <summary>Returns the specified tag's value as a java.util.Date.</summary>
		/// <remarks>
		/// Returns the specified tag's value as a java.util.Date.  If the value is unset or cannot be converted, <code>null</code> is returned.
		/// <p/>
		/// If the underlying value is a
		/// <see cref="string"/>
		/// , then attempts will be made to parse the string as though it is in
		/// the
		/// <see cref="System.TimeZoneInfo"/>
		/// represented by the
		/// <paramref name="timeZone"/>
		/// parameter (if it is non-null).  Note that this parameter
		/// is only considered if the underlying value is a string and parsing occurs, otherwise it has no effect.
		/// </remarks>
		[CanBeNull]
		public virtual DateTime? GetDate(int tagType, TimeZoneInfo timeZone)
		{
			object o = GetObject(tagType);
			if (o == null)
			{
				return null;
			}
			if (o is DateTime)
			{
				return (DateTime)o;
			}
			if (o is string)
			{
				// This seems to cover all known Exif date strings
				// Note that "    :  :     :  :  " is a valid date string according to the Exif spec (which means 'unknown date'): http://www.awaresystems.be/imaging/tiff/tifftags/privateifd/exif/datetimeoriginal.html
				string[] datePatterns = new string[] { "yyyy:MM:dd HH:mm:ss", "yyyy:MM:dd HH:mm", "yyyy-MM-dd HH:mm:ss", "yyyy-MM-dd HH:mm", "yyyy.MM.dd HH:mm:ss", "yyyy.MM.dd HH:mm" };
				string dateString = (string)o;
				foreach (string datePattern in datePatterns)
				{
					try
					{
						DateFormat parser = new SimpleDateFormat(datePattern);
						if (timeZone != null)
						{
							parser.SetTimeZone(timeZone);
						}
						return parser.Parse(dateString);
					}
					catch (ParseException)
					{
					}
				}
			}
			// simply try the next pattern
			return null;
		}

		/// <summary>Returns the specified tag's value as a Rational.</summary>
		/// <remarks>Returns the specified tag's value as a Rational.  If the value is unset or cannot be converted, <code>null</code> is returned.</remarks>
		[CanBeNull]
		public virtual Rational GetRational(int tagType)
		{
			object o = GetObject(tagType);
			if (o == null)
			{
				return null;
			}
			if (o is Rational)
			{
				return (Rational)o;
			}
			if (o is int)
			{
				return new Rational((int)o, 1);
			}
			if (o is long)
			{
				return new Rational((long)o, 1);
			}
			// NOTE not doing conversions for real number types
			return null;
		}

		/// <summary>Returns the specified tag's value as an array of Rational.</summary>
		/// <remarks>Returns the specified tag's value as an array of Rational.  If the value is unset or cannot be converted, <code>null</code> is returned.</remarks>
		[CanBeNull]
		public virtual Rational[] GetRationalArray(int tagType)
		{
			object o = GetObject(tagType);
			if (o == null)
			{
				return null;
			}
			if (o is Rational[])
			{
				return (Rational[])o;
			}
			return null;
		}

		/// <summary>Returns the specified tag's value as a String.</summary>
		/// <remarks>
		/// Returns the specified tag's value as a String.  This value is the 'raw' value.  A more presentable decoding
		/// of this value may be obtained from the corresponding Descriptor.
		/// </remarks>
		/// <returns>
		/// the String representation of the tag's value, or
		/// <code>null</code> if the tag hasn't been defined.
		/// </returns>
		[CanBeNull]
		public virtual string GetString(int tagType)
		{
			object o = GetObject(tagType);
			if (o == null)
			{
				return null;
			}
			if (o is Rational)
			{
				return ((Rational)o).ToSimpleString(true);
			}
			if (o.GetType().IsArray)
			{
				// handle arrays of objects and primitives
				int arrayLength = Sharpen.Runtime.GetArrayLength(o);
				Type componentType = o.GetType().GetElementType();
				bool isObjectArray = typeof(object).IsAssignableFrom(componentType);
				bool isFloatArray = componentType.FullName.Equals("float");
				bool isDoubleArray = componentType.FullName.Equals("double");
				bool isIntArray = componentType.FullName.Equals("int");
				bool isLongArray = componentType.FullName.Equals("long");
				bool isByteArray = componentType.FullName.Equals("byte");
				bool isShortArray = componentType.FullName.Equals("short");
				StringBuilder @string = new StringBuilder();
				for (int i = 0; i < arrayLength; i++)
				{
					if (i != 0)
					{
						@string.Append(' ');
					}
					if (isObjectArray)
					{
						@string.Append(Sharpen.Runtime.GetArrayValue(o, i).ToString());
					}
					else
					{
						if (isIntArray)
						{
							@string.Append(Sharpen.Runtime.GetInt(o, i));
						}
						else
						{
							if (isShortArray)
							{
								@string.Append(Sharpen.Runtime.GetShort(o, i));
							}
							else
							{
								if (isLongArray)
								{
									@string.Append(Sharpen.Runtime.GetLong(o, i));
								}
								else
								{
									if (isFloatArray)
									{
										@string.Append(Sharpen.Runtime.GetFloat(o, i));
									}
									else
									{
										if (isDoubleArray)
										{
											@string.Append(Sharpen.Runtime.GetDouble(o, i));
										}
										else
										{
											if (isByteArray)
											{
												@string.Append(Sharpen.Runtime.GetByte(o, i));
											}
											else
											{
												AddError("Unexpected array component type: " + componentType.FullName);
											}
										}
									}
								}
							}
						}
					}
				}
				return @string.ToString();
			}
			// Note that several cameras leave trailing spaces (Olympus, Nikon) but this library is intended to show
			// the actual data within the file.  It is not inconceivable that whitespace may be significant here, so we
			// do not trim.  Also, if support is added for writing data back to files, this may cause issues.
			// We leave trimming to the presentation layer.
			return o.ToString();
		}

		[CanBeNull]
		public virtual string GetString(int tagType, string charset)
		{
			sbyte[] bytes = GetByteArray(tagType);
			if (bytes == null)
			{
				return null;
			}
			try
			{
				return Sharpen.Runtime.GetStringForBytes(bytes, charset);
			}
			catch (UnsupportedEncodingException)
			{
				return null;
			}
		}

		/// <summary>Returns the object hashed for the particular tag type specified, if available.</summary>
		/// <param name="tagType">the tag type identifier</param>
		/// <returns>the tag's value as an Object if available, else <code>null</code></returns>
		[CanBeNull]
		public virtual object GetObject(int tagType)
		{
			return _tagMap.Get(Sharpen.Extensions.ValueOf(tagType));
		}

		// OTHER METHODS
		/// <summary>Returns the name of a specified tag as a String.</summary>
		/// <param name="tagType">the tag type identifier</param>
		/// <returns>the tag's name as a String</returns>
		[NotNull]
		public virtual string GetTagName(int tagType)
		{
			Dictionary<int, string> nameMap = GetTagNameMap();
			if (!nameMap.ContainsKey(tagType))
			{
				string hex = Sharpen.Extensions.ToHexString(tagType);
				while (hex.Length < 4)
				{
					hex = "0" + hex;
				}
				return "Unknown tag (0x" + hex + ")";
			}
			return nameMap.Get(tagType);
		}

		/// <summary>
		/// Provides a description of a tag's value using the descriptor set by
		/// <code>setDescriptor(Descriptor)</code>.
		/// </summary>
		/// <param name="tagType">the tag type identifier</param>
		/// <returns>the tag value's description as a String</returns>
		[CanBeNull]
		public virtual string GetDescription(int tagType)
		{
			System.Diagnostics.Debug.Assert((_descriptor != null));
			return _descriptor.GetDescription(tagType);
		}
	}
}
