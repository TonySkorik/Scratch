using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;
using System.Transactions;

namespace Fractional
{
	internal class Fractional
	{
		private readonly long _integerPart;
		private readonly ushort _fractionalPart;

		private readonly int _fractionalPartPower;
		private readonly bool _isNegative;
		
		public Fractional(string numberString)
		{
			var parts = numberString.Split('.', ',');
			var integerPartString = parts[0].TrimStart('0', '-');
			var fractionalPartString = parts.Length == 2 ? parts[1].TrimEnd('0') : "";
			
			_isNegative = numberString.StartsWith('-');

			_integerPart = string.IsNullOrEmpty(integerPartString) ? 0 : long.Parse(integerPartString);
			if (_integerPart != 0 && _isNegative)
			{
				_integerPart = -_integerPart;
			}

			_fractionalPart = string.IsNullOrEmpty(fractionalPartString) ? (ushort) 0 : ushort.Parse(fractionalPartString);
			_fractionalPartPower = fractionalPartString.Length;
		}

		public Fractional(long integerPart, ushort fractionalPart, bool isNegative, int fractionalPartPower)
		{
			_integerPart = integerPart;
			_fractionalPart = fractionalPart;
			_isNegative = integerPart < 0 || isNegative;
			_fractionalPartPower = fractionalPartPower;
		}
		
		public Fractional Add(Fractional right)
		{
			int leftFractionPowerAdjustment = 0;
			int rightFractionPowerAdjustment = 0;

			if (_fractionalPartPower >= right._fractionalPartPower)
			{
				rightFractionPowerAdjustment = _fractionalPartPower - right._fractionalPartPower;
			}
			else
			{
				leftFractionPowerAdjustment = right._fractionalPartPower - _fractionalPartPower;
			}

			var leftInt = GetAdjustedInt(leftFractionPowerAdjustment);
			var rightInt = right.GetAdjustedInt(rightFractionPowerAdjustment);
			
			var intResult = leftInt + rightInt;

			// adjust comma back
			var maxFractionPower = Math.Max(_fractionalPartPower, right._fractionalPartPower); // move comma this amount of symbols to the left
			var ret = GetFractionalFromResult(intResult, maxFractionPower);
			
			return ret;
		}
		
		public Fractional Subtract(Fractional right)
		{
			var add = right.ChangeSign();
			return Add(add);
		}

		public Fractional Multiply(Fractional right)
		{
			int leftFractionPowerAdjustment = 0;
			int rightFractionPowerAdjustment = 0;

			if (_fractionalPartPower >= right._fractionalPartPower)
			{
				rightFractionPowerAdjustment = _fractionalPartPower - right._fractionalPartPower;
			}
			else
			{
				leftFractionPowerAdjustment = right._fractionalPartPower - _fractionalPartPower;
			}

			var leftInt = GetAdjustedInt(leftFractionPowerAdjustment);
			var rightInt = right.GetAdjustedInt(rightFractionPowerAdjustment);

			var intResult = leftInt * rightInt;

			// adjust comma back
			var maxFractionPower = Math.Max(_fractionalPartPower, right._fractionalPartPower); // move comma this amount of symbols to the left
			var ret = GetFractionalFromResult(intResult, maxFractionPower);

			return ret;
		}
		
		#region Overrides

		public override string ToString()
		{
			var fractionalPartString = _fractionalPart.ToString();
			if (fractionalPartString.Length < _fractionalPartPower)
			{
				var delta = _fractionalPartPower - fractionalPartString.Length;
				fractionalPartString = fractionalPartString.PadLeft(fractionalPartString.Length + delta, '0');
			}
			return $"{(_isNegative && _integerPart == 0 ? "-" : "")}{_integerPart}.{fractionalPartString}";
		}

		#endregion

		/// <summary>
		/// Returns 0 if equal, -1 if is lower than right, 1 if greate than right
		/// </summary>
		/// <param name="right"></param>
		/// <returns></returns>
		public int CompareTo(Fractional right)
		{
			int leftFractionPowerAdjustment = 0;
			int rightFractionPowerAdjustment = 0;

			if (_fractionalPartPower >= right._fractionalPartPower)
			{
				rightFractionPowerAdjustment = _fractionalPartPower - right._fractionalPartPower;
			}
			else
			{
				leftFractionPowerAdjustment = right._fractionalPartPower - _fractionalPartPower;
			}
			
			var thisInteger = GetAdjustedInt(leftFractionPowerAdjustment);
			var rightInteger = right.GetAdjustedInt(rightFractionPowerAdjustment);

			var delta = thisInteger - rightInteger;
			
			if (delta == 0)
			{
				return 0;
			}

			if (delta < 0)
			{
				return -1;
			}

			return 1;
		}

		#region Service methods

		private Fractional GetFractionalFromResult(long result, int fractionPower)
		{
			var intPart = (result / (long)Math.Pow(10, fractionPower));
			var fractionPart = (ushort)(Math.Abs(result - intPart * (long)Math.Pow(10, fractionPower)));

			return new Fractional(intPart, fractionPart, result < 0, fractionPower);
		}

		private long GetAdjustedInt(int fractionPowerAdjustment)
		{
			var sign = _isNegative
				? -1
				: 1;

			var ret =
				_integerPart * (int)Math.Pow(10, _fractionalPartPower + fractionPowerAdjustment)
				+ sign * _fractionalPart * (int)Math.Pow(10, fractionPowerAdjustment);

			return ret;
		}

		private Fractional ChangeSign()
		{
			var newIsNegative = !_isNegative;
			var newIntegerPart = _integerPart * -1;
			return new Fractional(newIntegerPart, _fractionalPart, newIsNegative, _fractionalPartPower);
		}

		private int GetFractionalPower(string fractionalPartString)
		{
			int power = 0;
			if (string.IsNullOrEmpty(fractionalPartString))
			{
				return power;
			}

			while (fractionalPartString[power] == '0')
			{
				power++;
			}

			return power;
		}
		
		private ushort AdjustLength(ushort target, int length, int power)
		{
			if (target == 0)
			{
				return 0;
			}

			var initialLength = GetLength(target);
			if (initialLength >= length)
			{
				return target;
			}

			var delta = length - initialLength;
			ushort adjusted = (ushort)(target * (ushort)Math.Pow(10, delta - power));
			return adjusted;
		}

		private int GetLength(uint target, int power = 0)
		{
			if (target < 10)
			{
				return 1 + power;
			}

			uint value = target;
			int length = 1;

			while (value > 0)
			{
				length++;
				var divider = (uint)Math.Pow(10, length);
				value = target / divider;
			}

			return length + power;
		} 

		#endregion
	}
}
