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

using System.Collections.Generic;
using System.IO;
using System.Text;
using Com.Drew.Lang;
using JetBrains.Annotations;
using Sharpen;

namespace Com.Drew.Lang
{
	/// <author>Drew Noakes http://drewnoakes.com</author>
	public class StringUtil
	{
	    [NotNull]
	    public static string Join(Iterable<string> strings, string delimiter)
	    {
	        return string.Join(delimiter, strings);
	    }
        
        [NotNull]
	    public static string Join(IList<string> strings, string delimiter)
	    {
	        return string.Join(delimiter, strings);
	    }

	    [NotNull]
		public static string Join<_T0>(Iterable<_T0> strings, string delimiter)
			where _T0 : CharSequence
		{
			int capacity = 0;
			int delimLength = delimiter.Length;
			Iterator<CharSequence> iter = strings.Iterator<CharSequence>();
			if (iter.HasNext())
			{
				capacity += iter.Next().Length + delimLength;
			}
			StringBuilder buffer = new StringBuilder(capacity);
			iter = strings.Iterator<CharSequence>();
			if (iter.HasNext())
			{
				buffer.Append(iter.Next());
				while (iter.HasNext())
				{
					buffer.Append(delimiter);
					buffer.Append(iter.Next());
				}
			}
			return buffer.ToString();
		}

		/// <exception cref="System.IO.IOException"/>
		[NotNull]
		public static string FromStream(InputStream stream)
		{
			BufferedReader reader = new BufferedReader(new InputStreamReader(stream));
			StringBuilder sb = new StringBuilder();
			string line;
			while ((line = reader.ReadLine()) != null)
			{
				sb.Append(line);
			}
			return sb.ToString();
		}

		public static int Compare(string s1, string s2)
		{
			bool null1 = s1 == null;
			bool null2 = s2 == null;
			if (null1 && null2)
			{
				return 0;
			}
			else
			{
				if (null1 && !null2)
				{
					return -1;
				}
				else
				{
					if (null2)
					{
						return 1;
					}
					else
					{
						return string.CompareOrdinal(s1, s2);
					}
				}
			}
		}

		[CanBeNull]
		public static string EscapeForWiki(string text)
		{
			if (text == null)
			{
				return null;
			}
			text = text.ReplaceAll("(\\W|^)(([A-Z][a-z0-9]+){2,})", "$1!$2");
			if (text != null && text.Length > 120)
			{
				text = Sharpen.Runtime.Substring(text, 0, 120) + "...";
			}
			if (text != null)
			{
				text = text.Replace("[", "`[`").Replace("]", "`]`").Replace("<", "`<`").Replace(">", "`>`").Replace("*", "`*`");
			}
			return text;
		}

		[NotNull]
		public static string UrlEncode(string name)
		{
			// Sufficient for now, it seems
			return name.Replace(" ", "%20");
		}
	}
}
