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
using JetBrains.Annotations;
using Sharpen;

namespace Com.Drew.Lang
{
	/// <summary>Immutable class for holding a rational number without loss of precision.</summary>
	/// <remarks>
	/// Immutable class for holding a rational number without loss of precision.  Provides
	/// a familiar representation via toString() in form <code>numerator/denominator</code>.
	/// </remarks>
	/// <author>Drew Noakes http://drewnoakes.com</author>
	[System.Serializable]
	public class Rational : Number
	{
		private const long serialVersionUID = 510688928138848770L;

		/// <summary>Holds the numerator.</summary>
		private readonly long _numerator;

		/// <summary>Holds the denominator.</summary>
		private readonly long _denominator;

		/// <summary>Creates a new instance of Rational.</summary>
		/// <remarks>
		/// Creates a new instance of Rational.  Rational objects are immutable, so
		/// once you've set your numerator and denominator values here, you're stuck
		/// with them!
		/// </remarks>
		public Rational(long numerator, long denominator)
		{
			// TODO make Rational implement Number?
			_numerator = numerator;
			_denominator = denominator;
		}

		/// <summary>Returns the value of the specified number as a <code>double</code>.</summary>
		/// <remarks>
		/// Returns the value of the specified number as a <code>double</code>.
		/// This may involve rounding.
		/// </remarks>
		/// <returns>
		/// the numeric value represented by this object after conversion
		/// to type <code>double</code>.
		/// </returns>
		public override double DoubleValue()
		{
			return (double)_numerator / (double)_denominator;
		}

		/// <summary>Returns the value of the specified number as a <code>float</code>.</summary>
		/// <remarks>
		/// Returns the value of the specified number as a <code>float</code>.
		/// This may involve rounding.
		/// </remarks>
		/// <returns>
		/// the numeric value represented by this object after conversion
		/// to type <code>float</code>.
		/// </returns>
		public override float FloatValue()
		{
			return (float)_numerator / (float)_denominator;
		}

		/// <summary>Returns the value of the specified number as a <code>byte</code>.</summary>
		/// <remarks>
		/// Returns the value of the specified number as a <code>byte</code>.
		/// This may involve rounding or truncation.  This implementation simply
		/// casts the result of <code>doubleValue()</code> to <code>byte</code>.
		/// </remarks>
		/// <returns>
		/// the numeric value represented by this object after conversion
		/// to type <code>byte</code>.
		/// </returns>
		public sealed override sbyte ByteValue()
		{
			return unchecked((sbyte)DoubleValue());
		}

		/// <summary>Returns the value of the specified number as an <code>int</code>.</summary>
		/// <remarks>
		/// Returns the value of the specified number as an <code>int</code>.
		/// This may involve rounding or truncation.  This implementation simply
		/// casts the result of <code>doubleValue()</code> to <code>int</code>.
		/// </remarks>
		/// <returns>
		/// the numeric value represented by this object after conversion
		/// to type <code>int</code>.
		/// </returns>
		public sealed override int IntValue()
		{
			//  HACK: repeats Java behaviour
            double value = DoubleValue();

            if (double.IsNaN(value))
            {
                return 0;
            }

		    return (int) value;
		}

		/// <summary>Returns the value of the specified number as a <code>long</code>.</summary>
		/// <remarks>
		/// Returns the value of the specified number as a <code>long</code>.
		/// This may involve rounding or truncation.  This implementation simply
		/// casts the result of <code>doubleValue()</code> to <code>long</code>.
		/// </remarks>
		/// <returns>
		/// the numeric value represented by this object after conversion
		/// to type <code>long</code>.
		/// </returns>
		public sealed override long LongValue()
		{
			return (long)DoubleValue();
		}

		/// <summary>Returns the value of the specified number as a <code>short</code>.</summary>
		/// <remarks>
		/// Returns the value of the specified number as a <code>short</code>.
		/// This may involve rounding or truncation.  This implementation simply
		/// casts the result of <code>doubleValue()</code> to <code>short</code>.
		/// </remarks>
		/// <returns>
		/// the numeric value represented by this object after conversion
		/// to type <code>short</code>.
		/// </returns>
		public sealed override short ShortValue()
		{
			return (short)DoubleValue();
		}

		/// <summary>Returns the denominator.</summary>
		public long GetDenominator()
		{
			return this._denominator;
		}

		/// <summary>Returns the numerator.</summary>
		public long GetNumerator()
		{
			return this._numerator;
		}

		/// <summary>Returns the reciprocal value of this object as a new Rational.</summary>
		/// <returns>the reciprocal in a new object</returns>
		[NotNull]
		public virtual Com.Drew.Lang.Rational GetReciprocal()
		{
			return new Com.Drew.Lang.Rational(this._denominator, this._numerator);
		}

		/// <summary>
		/// Checks if this
		/// <see cref="Rational"/>
		/// number is an Integer, either positive or negative.
		/// </summary>
		public virtual bool IsInteger()
		{
			return _denominator == 1 || (_denominator != 0 && (_numerator % _denominator == 0)) || (_denominator == 0 && _numerator == 0);
		}

		/// <summary>Returns a string representation of the object of form <code>numerator/denominator</code>.</summary>
		/// <returns>a string representation of the object.</returns>
		[NotNull]
		public override string ToString()
		{
			return _numerator + "/" + _denominator;
		}

		/// <summary>
		/// Returns the simplest representation of this
		/// <see cref="Rational"/>
		/// 's value possible.
		/// </summary>
		[NotNull]
		public virtual string ToSimpleString(bool allowDecimal)
		{
			if (_denominator == 0 && _numerator != 0)
			{
				return ToString();
			}
			else
			{
				if (IsInteger())
				{
					return Sharpen.Extensions.ToString(IntValue());
				}
				else
				{
					if (_numerator != 1 && _denominator % _numerator == 0)
					{
						// common factor between denominator and numerator
						long newDenominator = _denominator / _numerator;
						return new Com.Drew.Lang.Rational(1, newDenominator).ToSimpleString(allowDecimal);
					}
					else
					{
						Com.Drew.Lang.Rational simplifiedInstance = GetSimplifiedInstance();
						if (allowDecimal)
						{
							string doubleString = simplifiedInstance.DoubleValue().ToString();
							if (doubleString.Length < 5)
							{
								return doubleString;
							}
						}
						return simplifiedInstance.ToString();
					}
				}
			}
		}

		/// <summary>
		/// Decides whether a brute-force simplification calculation should be avoided
		/// by comparing the maximum number of possible calculations with some threshold.
		/// </summary>
		/// <returns>true if the simplification should be performed, otherwise false</returns>
		private bool TooComplexForSimplification()
		{
			double maxPossibleCalculations = (((double)(Math.Min(_denominator, _numerator) - 1) / 5d) + 2);
			int maxSimplificationCalculations = 1000;
			return maxPossibleCalculations > maxSimplificationCalculations;
		}

		/// <summary>
		/// Compares two
		/// <see cref="Rational"/>
		/// instances, returning true if they are mathematically
		/// equivalent.
		/// </summary>
		/// <param name="obj">
		/// the
		/// <see cref="Rational"/>
		/// to compare this instance to.
		/// </param>
		/// <returns>
		/// true if instances are mathematically equivalent, otherwise false.  Will also
		/// return false if <code>obj</code> is not an instance of
		/// <see cref="Rational"/>
		/// .
		/// </returns>
		public override bool Equals(object obj)
		{
			if (obj == null || !(obj is Com.Drew.Lang.Rational))
			{
				return false;
			}
			Com.Drew.Lang.Rational that = (Com.Drew.Lang.Rational)obj;
			return this.DoubleValue() == that.DoubleValue();
		}

		public override int GetHashCode()
		{
			return (23 * (int)_denominator) + (int)_numerator;
		}

		/// <summary>
		/// <p>
		/// Simplifies the
		/// <see cref="Rational"/>
		/// number.</p>
		/// <p>
		/// Prime number series: 1, 2, 3, 5, 7, 9, 11, 13, 17</p>
		/// <p>
		/// To reduce a rational, need to see if both numerator and denominator are divisible
		/// by a common factor.  Using the prime number series in ascending order guarantees
		/// the minimum number of checks required.</p>
		/// <p>
		/// However, generating the prime number series seems to be a hefty task.  Perhaps
		/// it's simpler to check if both d & n are divisible by all numbers from 2 -&gt;
		/// (Math.min(denominator, numerator) / 2).  In doing this, one can check for 2
		/// and 5 once, then ignore all even numbers, and all numbers ending in 0 or 5.
		/// This leaves four numbers from every ten to check.</p>
		/// <p>
		/// Therefore, the max number of pairs of modulus divisions required will be:</p>
		/// <code><pre>
		/// 4   Math.min(denominator, numerator) - 1
		/// -- * ------------------------------------ + 2
		/// 10                    2
		/// <p/>
		/// Math.min(denominator, numerator) - 1
		/// = ------------------------------------ + 2
		/// 5
		/// </pre></code>
		/// </summary>
		/// <returns>
		/// a simplified instance, or if the Rational could not be simplified,
		/// returns itself (unchanged)
		/// </returns>
		[NotNull]
		public virtual Com.Drew.Lang.Rational GetSimplifiedInstance()
		{
			if (TooComplexForSimplification())
			{
				return this;
			}
			for (int factor = 2; factor <= Math.Min(_denominator, _numerator); factor++)
			{
				if ((factor % 2 == 0 && factor > 2) || (factor % 5 == 0 && factor > 5))
				{
					continue;
				}
				if (_denominator % factor == 0 && _numerator % factor == 0)
				{
					// found a common factor
					return new Com.Drew.Lang.Rational(_numerator / factor, _denominator / factor);
				}
			}
			return this;
		}
	}
}
