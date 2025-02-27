﻿#region --- License ---
/*
Copyright (c) 2006 - 2008 The Open Toolkit library.

Permission is hereby granted, free of charge, to any person obtaining a copy of
this software and associated documentation files (the "Software"), to deal in
the Software without restriction, including without limitation the rights to
use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies
of the Software, and to permit persons to whom the Software is furnished to do
so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
 */
#endregion

#if !MONOGAME && !OPENTK && !SHARPDX && !XNA && !UNITY3D

using System;
using System.Linq;
using System.Runtime.InteropServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SharpNav.Geometry
{
	/// <summary>
	/// Represents a 3D vector using three single-precision floating-point numbers.
	/// </summary>
	/// <remarks>
	/// The Vector3 structure is suitable for interoperation with unmanaged code requiring three consecutive floats.
	/// </remarks>
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public struct Vector3 : IEquatable<Vector3>
	{
		#region Fields

		/// <summary>
		/// The X component of the Vector3.
		/// </summary>
		public float X;

		/// <summary>
		/// The Y component of the Vector3.
		/// </summary>
		public float Y;

		/// <summary>
		/// The Z component of the Vector3.
		/// </summary>
		public float Z;

		#endregion

		#region Constructors

		/// <summary>
		/// Constructs a new instance.
		/// </summary>
		/// <param name="value">The value that will initialize this instance.</param>
		public Vector3(float value)
		{
			X = value;
			Y = value;
			Z = value;
		}

		/// <summary>
		/// Constructs a new Vector3.
		/// </summary>
		/// <param name="x">The x component of the Vector3.</param>
		/// <param name="y">The y component of the Vector3.</param>
		/// <param name="z">The z component of the Vector3.</param>
		public Vector3(float x, float y, float z)
		{
			X = x;
			Y = y;
			Z = z;
		}

		/// <summary>
		/// Constructs a new Vector3 from the given Vector3.
		/// </summary>
		/// <param name="v">The Vector3 to copy components from.</param>
		public Vector3(Vector3 v)
		{
			X = v.X;
			Y = v.Y;
			Z = v.Z;
		}

		#endregion

		#region Public Members

		/// <summary>
		/// Gets or sets the value at the index of the Vector.
		/// </summary>
		public float this[int index]
		{
			get
			{
				switch (index)
				{
					case 0:
						return X;
					case 1:
						return Y;
					case 2:
						return Z;
					default:
						throw new ArgumentOutOfRangeException("You tried to access this vector at index: " + index);
				}
			}

			set
			{
				switch (index)
				{
					case 0:
						X = value;
						break;
					case 1:
						Y = value;
						break;
					case 2:
						Z = value;
						break;
					default:
						throw new ArgumentOutOfRangeException("You tried to set this vector at index: " + index);
				}
			}
		}

		#region Instance

		#region public void Add()

		/// <summary>Add the Vector passed as parameter to this instance.</summary>
		/// <param name="right">Right operand. This parameter is only read from.</param>
		[Obsolete("Use static Add() method instead.")]
		public void Add(Vector3 right)
		{
			this.X += right.X;
			this.Y += right.Y;
			this.Z += right.Z;
		}

		/// <summary>Add the Vector passed as parameter to this instance.</summary>
		/// <param name="right">Right operand. This parameter is only read from.</param>
		[Obsolete("Use static Add() method instead.")]
		public void Add(ref Vector3 right)
		{
			this.X += right.X;
			this.Y += right.Y;
			this.Z += right.Z;
		}

		#endregion public void Add()

		#region public void Sub()

		/// <summary>Subtract the Vector passed as parameter from this instance.</summary>
		/// <param name="right">Right operand. This parameter is only read from.</param>
		[Obsolete("Use static Subtract() method instead.")]
		public void Sub(Vector3 right)
		{
			this.X -= right.X;
			this.Y -= right.Y;
			this.Z -= right.Z;
		}

		/// <summary>Subtract the Vector passed as parameter from this instance.</summary>
		/// <param name="right">Right operand. This parameter is only read from.</param>
		[Obsolete("Use static Subtract() method instead.")]
		public void Sub(ref Vector3 right)
		{
			this.X -= right.X;
			this.Y -= right.Y;
			this.Z -= right.Z;
		}

		#endregion public void Sub()

		#region public void Mult()

		/// <summary>Multiply this instance by a scalar.</summary>
		/// <param name="f">Scalar operand.</param>
		[Obsolete("Use static Multiply() method instead.")]
		public void Mult(float f)
		{
			this.X *= f;
			this.Y *= f;
			this.Z *= f;
		}

		#endregion public void Mult()

		#region public void Div()

		/// <summary>Divide this instance by a scalar.</summary>
		/// <param name="f">Scalar operand.</param>
		[Obsolete("Use static Divide() method instead.")]
		public void Div(float f)
		{
			float mult = 1.0f / f;
			this.X *= mult;
			this.Y *= mult;
			this.Z *= mult;
		}

		#endregion public void Div()

		#region public float Length

		/// <summary>
		/// Gets the length (magnitude) of the vector.
		/// </summary>
		/// <see cref="LengthSquared"/>
		public float Length()
		{
			return (float)System.Math.Sqrt(X * X + Y * Y + Z * Z);
		}

		#endregion

		#region public float LengthSquared

		/// <summary>
		/// Gets the square of the vector length (magnitude).
		/// </summary>
		/// <remarks>
		/// This property avoids the costly square root operation required by the Length property. This makes it more suitable
		/// for comparisons.
		/// </remarks>
		/// <see cref="Length"/>
		public float LengthSquared()
		{
			return X * X + Y * Y + Z * Z;
		}

		#endregion

		/// <summary>
		/// Returns a copy of the Vector3 scaled to unit length.
		/// </summary>
		public Vector3 Normalized()
		{
			Vector3 v = this;
			v.Normalize();
			return v;
		}

		#region public void Normalize()

		/// <summary>
		/// Scales the Vector3 to unit length.
		/// </summary>
		public void Normalize()
		{
			float scale = 1.0f / this.Length();
			X *= scale;
			Y *= scale;
			Z *= scale;
		}

		#endregion

		#region public void Scale()

		/// <summary>
		/// Scales the current Vector3 by the given amounts.
		/// </summary>
		/// <param name="sx">The scale of the X component.</param>
		/// <param name="sy">The scale of the Y component.</param>
		/// <param name="sz">The scale of the Z component.</param>
		[Obsolete("Use static Multiply() method instead.")]
		public void Scale(float sx, float sy, float sz)
		{
			this.X = X * sx;
			this.Y = Y * sy;
			this.Z = Z * sz;
		}

		/// <summary>Scales this instance by the given parameter.</summary>
		/// <param name="scale">The scaling of the individual components.</param>
		[Obsolete("Use static Multiply() method instead.")]
		public void Scale(Vector3 scale)
		{
			this.X *= scale.X;
			this.Y *= scale.Y;
			this.Z *= scale.Z;
		}

		/// <summary>Scales this instance by the given parameter.</summary>
		/// <param name="scale">The scaling of the individual components.</param>
		[Obsolete("Use static Multiply() method instead.")]
		public void Scale(ref Vector3 scale)
		{
			this.X *= scale.X;
			this.Y *= scale.Y;
			this.Z *= scale.Z;
		}

		#endregion public void Scale()

		#endregion

		#region Static

		#region Fields

		/// <summary>
		/// Defines a unit-length Vector3 that points towards the X-axis.
		/// </summary>
		public static readonly Vector3 UnitX = new Vector3(1, 0, 0);

		/// <summary>
		/// Defines a unit-length Vector3 that points towards the Y-axis.
		/// </summary>
		public static readonly Vector3 UnitY = new Vector3(0, 1, 0);

		/// <summary>
		/// /// Defines a unit-length Vector3 that points towards the Z-axis.
		/// </summary>
		public static readonly Vector3 UnitZ = new Vector3(0, 0, 1);

		/// <summary>
		/// Defines a zero-length Vector3.
		/// </summary>
		public static readonly Vector3 Zero = new Vector3(0, 0, 0);

		/// <summary>
		/// Defines an instance with all components set to 1.
		/// </summary>
		public static readonly Vector3 One = new Vector3(1, 1, 1);

		/// <summary>
		/// Defines the size of the Vector3 struct in bytes.
		/// </summary>
		public static readonly int SizeInBytes = Marshal.SizeOf(new Vector3());

		#endregion

		#region Obsolete

		#region Sub

		/// <summary>
		/// Subtract one Vector from another
		/// </summary>
		/// <param name="a">First operand</param>
		/// <param name="b">Second operand</param>
		/// <returns>Result of subtraction</returns>
		[Obsolete("Use static Subtract() method instead.")]
		public static Vector3 Sub(Vector3 a, Vector3 b)
		{
			a.X -= b.X;
			a.Y -= b.Y;
			a.Z -= b.Z;
			return a;
		}

		/// <summary>
		/// Subtract one Vector from another
		/// </summary>
		/// <param name="a">First operand</param>
		/// <param name="b">Second operand</param>
		/// <param name="result">Result of subtraction</param>
		[Obsolete("Use static Subtract() method instead.")]
		public static void Sub(ref Vector3 a, ref Vector3 b, out Vector3 result)
		{
			result.X = a.X - b.X;
			result.Y = a.Y - b.Y;
			result.Z = a.Z - b.Z;
		}

		#endregion

		#region Mult

		/// <summary>
		/// Multiply a vector and a scalar
		/// </summary>
		/// <param name="a">Vector operand</param>
		/// <param name="f">Scalar operand</param>
		/// <returns>Result of the multiplication</returns>
		[Obsolete("Use static Multiply() method instead.")]
		public static Vector3 Mult(Vector3 a, float f)
		{
			a.X *= f;
			a.Y *= f;
			a.Z *= f;
			return a;
		}

		/// <summary>
		/// Multiply a vector and a scalar
		/// </summary>
		/// <param name="a">Vector operand</param>
		/// <param name="f">Scalar operand</param>
		/// <param name="result">Result of the multiplication</param>
		[Obsolete("Use static Multiply() method instead.")]
		public static void Mult(ref Vector3 a, float f, out Vector3 result)
		{
			result.X = a.X * f;
			result.Y = a.Y * f;
			result.Z = a.Z * f;
		}

		#endregion

		#region Div

		/// <summary>
		/// Divide a vector by a scalar
		/// </summary>
		/// <param name="a">Vector operand</param>
		/// <param name="f">Scalar operand</param>
		/// <returns>Result of the division</returns>
		[Obsolete("Use static Divide() method instead.")]
		public static Vector3 Div(Vector3 a, float f)
		{
			float mult = 1.0f / f;
			a.X *= mult;
			a.Y *= mult;
			a.Z *= mult;
			return a;
		}

		/// <summary>
		/// Divide a vector by a scalar
		/// </summary>
		/// <param name="a">Vector operand</param>
		/// <param name="f">Scalar operand</param>
		/// <param name="result">Result of the division</param>
		[Obsolete("Use static Divide() method instead.")]
		public static void Div(ref Vector3 a, float f, out Vector3 result)
		{
			float mult = 1.0f / f;
			result.X = a.X * mult;
			result.Y = a.Y * mult;
			result.Z = a.Z * mult;
		}

		#endregion

		#endregion

		#region Add

		/// <summary>
		/// Adds two vectors.
		/// </summary>
		/// <param name="a">Left operand.</param>
		/// <param name="b">Right operand.</param>
		/// <returns>Result of operation.</returns>
		public static Vector3 Add(Vector3 a, Vector3 b)
		{
			Add(ref a, ref b, out a);
			return a;
		}

		/// <summary>
		/// Adds two vectors.
		/// </summary>
		/// <param name="a">Left operand.</param>
		/// <param name="b">Right operand.</param>
		/// <param name="result">Result of operation.</param>
		public static void Add(ref Vector3 a, ref Vector3 b, out Vector3 result)
		{
			result = new Vector3(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
		}

		#endregion

		#region Subtract

		/// <summary>
		/// Subtract one Vector from another
		/// </summary>
		/// <param name="a">First operand</param>
		/// <param name="b">Second operand</param>
		/// <returns>Result of subtraction</returns>
		public static Vector3 Subtract(Vector3 a, Vector3 b)
		{
			Subtract(ref a, ref b, out a);
			return a;
		}

		/// <summary>
		/// Subtract one Vector from another
		/// </summary>
		/// <param name="a">First operand</param>
		/// <param name="b">Second operand</param>
		/// <param name="result">Result of subtraction</param>
		public static void Subtract(ref Vector3 a, ref Vector3 b, out Vector3 result)
		{
			result = new Vector3(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
		}

		#endregion

		#region Multiply

		/// <summary>
		/// Multiplies a vector by a scalar.
		/// </summary>
		/// <param name="vector">Left operand.</param>
		/// <param name="scale">Right operand.</param>
		/// <returns>Result of the operation.</returns>
		public static Vector3 Multiply(Vector3 vector, float scale)
		{
			Multiply(ref vector, scale, out vector);
			return vector;
		}

		/// <summary>
		/// Multiplies a vector by a scalar.
		/// </summary>
		/// <param name="vector">Left operand.</param>
		/// <param name="scale">Right operand.</param>
		/// <param name="result">Result of the operation.</param>
		public static void Multiply(ref Vector3 vector, float scale, out Vector3 result)
		{
			result = new Vector3(vector.X * scale, vector.Y * scale, vector.Z * scale);
		}

		/// <summary>
		/// Multiplies a vector by the components a vector (scale).
		/// </summary>
		/// <param name="vector">Left operand.</param>
		/// <param name="scale">Right operand.</param>
		/// <returns>Result of the operation.</returns>
		public static Vector3 Multiply(Vector3 vector, Vector3 scale)
		{
			Multiply(ref vector, ref scale, out vector);
			return vector;
		}

		/// <summary>
		/// Multiplies a vector by the components of a vector (scale).
		/// </summary>
		/// <param name="vector">Left operand.</param>
		/// <param name="scale">Right operand.</param>
		/// <param name="result">Result of the operation.</param>
		public static void Multiply(ref Vector3 vector, ref Vector3 scale, out Vector3 result)
		{
			result = new Vector3(vector.X * scale.X, vector.Y * scale.Y, vector.Z * scale.Z);
		}

		#endregion

		#region Divide

		/// <summary>
		/// Divides a vector by a scalar.
		/// </summary>
		/// <param name="vector">Left operand.</param>
		/// <param name="scale">Right operand.</param>
		/// <returns>Result of the operation.</returns>
		public static Vector3 Divide(Vector3 vector, float scale)
		{
			Divide(ref vector, scale, out vector);
			return vector;
		}

		/// <summary>
		/// Divides a vector by a scalar.
		/// </summary>
		/// <param name="vector">Left operand.</param>
		/// <param name="scale">Right operand.</param>
		/// <param name="result">Result of the operation.</param>
		public static void Divide(ref Vector3 vector, float scale, out Vector3 result)
		{
			Multiply(ref vector, 1 / scale, out result);
		}

		/// <summary>
		/// Divides a vector by the components of a vector (scale).
		/// </summary>
		/// <param name="vector">Left operand.</param>
		/// <param name="scale">Right operand.</param>
		/// <returns>Result of the operation.</returns>
		public static Vector3 Divide(Vector3 vector, Vector3 scale)
		{
			Divide(ref vector, ref scale, out vector);
			return vector;
		}

		/// <summary>
		/// Divide a vector by the components of a vector (scale).
		/// </summary>
		/// <param name="vector">Left operand.</param>
		/// <param name="scale">Right operand.</param>
		/// <param name="result">Result of the operation.</param>
		public static void Divide(ref Vector3 vector, ref Vector3 scale, out Vector3 result)
		{
			result = new Vector3(vector.X / scale.X, vector.Y / scale.Y, vector.Z / scale.Z);
		}

		#endregion

		#region ComponentMin

		/// <summary>
		/// Calculate the component-wise minimum of two vectors
		/// </summary>
		/// <param name="a">First operand</param>
		/// <param name="b">Second operand</param>
		/// <returns>The component-wise minimum</returns>
		public static Vector3 ComponentMin(Vector3 a, Vector3 b)
		{
			a.X = a.X < b.X ? a.X : b.X;
			a.Y = a.Y < b.Y ? a.Y : b.Y;
			a.Z = a.Z < b.Z ? a.Z : b.Z;
			return a;
		}

		/// <summary>
		/// Calculate the component-wise minimum of two vectors
		/// </summary>
		/// <param name="a">First operand</param>
		/// <param name="b">Second operand</param>
		/// <param name="result">The component-wise minimum</param>
		public static void ComponentMin(ref Vector3 a, ref Vector3 b, out Vector3 result)
		{
			result.X = a.X < b.X ? a.X : b.X;
			result.Y = a.Y < b.Y ? a.Y : b.Y;
			result.Z = a.Z < b.Z ? a.Z : b.Z;
		}

		#endregion

		#region ComponentMax

		/// <summary>
		/// Calculate the component-wise maximum of two vectors
		/// </summary>
		/// <param name="a">First operand</param>
		/// <param name="b">Second operand</param>
		/// <returns>The component-wise maximum</returns>
		public static Vector3 ComponentMax(Vector3 a, Vector3 b)
		{
			a.X = a.X > b.X ? a.X : b.X;
			a.Y = a.Y > b.Y ? a.Y : b.Y;
			a.Z = a.Z > b.Z ? a.Z : b.Z;
			return a;
		}

		/// <summary>
		/// Calculate the component-wise maximum of two vectors
		/// </summary>
		/// <param name="a">First operand</param>
		/// <param name="b">Second operand</param>
		/// <param name="result">The component-wise maximum</param>
		public static void ComponentMax(ref Vector3 a, ref Vector3 b, out Vector3 result)
		{
			result.X = a.X > b.X ? a.X : b.X;
			result.Y = a.Y > b.Y ? a.Y : b.Y;
			result.Z = a.Z > b.Z ? a.Z : b.Z;
		}

		#endregion

		#region Min

		/// <summary>
		/// Returns the Vector3 with the minimum magnitude
		/// </summary>
		/// <param name="left">Left operand</param>
		/// <param name="right">Right operand</param>
		/// <returns>The minimum Vector3</returns>
		public static Vector3 Min(Vector3 left, Vector3 right)
		{
			return left.LengthSquared() < right.LengthSquared() ? left : right;
		}

		#endregion

		#region Max

		/// <summary>
		/// Returns the Vector3 with the minimum magnitude
		/// </summary>
		/// <param name="left">Left operand</param>
		/// <param name="right">Right operand</param>
		/// <returns>The minimum Vector3</returns>
		public static Vector3 Max(Vector3 left, Vector3 right)
		{
			return left.LengthSquared() >= right.LengthSquared() ? left : right;
		}

		#endregion

		#region Clamp

		/// <summary>
		/// Clamp a vector to the given minimum and maximum vectors
		/// </summary>
		/// <param name="vec">Input vector</param>
		/// <param name="min">Minimum vector</param>
		/// <param name="max">Maximum vector</param>
		/// <returns>The clamped vector</returns>
		public static Vector3 Clamp(Vector3 vec, Vector3 min, Vector3 max)
		{
			vec.X = vec.X < min.X ? min.X : vec.X > max.X ? max.X : vec.X;
			vec.Y = vec.Y < min.Y ? min.Y : vec.Y > max.Y ? max.Y : vec.Y;
			vec.Z = vec.Z < min.Z ? min.Z : vec.Z > max.Z ? max.Z : vec.Z;
			return vec;
		}

		/// <summary>
		/// Clamp a vector to the given minimum and maximum vectors
		/// </summary>
		/// <param name="vec">Input vector</param>
		/// <param name="min">Minimum vector</param>
		/// <param name="max">Maximum vector</param>
		/// <param name="result">The clamped vector</param>
		public static void Clamp(ref Vector3 vec, ref Vector3 min, ref Vector3 max, out Vector3 result)
		{
			result.X = vec.X < min.X ? min.X : vec.X > max.X ? max.X : vec.X;
			result.Y = vec.Y < min.Y ? min.Y : vec.Y > max.Y ? max.Y : vec.Y;
			result.Z = vec.Z < min.Z ? min.Z : vec.Z > max.Z ? max.Z : vec.Z;
		}

		#endregion

		#region Normalize

		/// <summary>
		/// Scale a vector to unit length
		/// </summary>
		/// <param name="vec">The input vector</param>
		/// <returns>The normalized vector</returns>
		public static Vector3 Normalize(Vector3 vec)
		{
			float scale = 1.0f / vec.Length();
			vec.X *= scale;
			vec.Y *= scale;
			vec.Z *= scale;
			return vec;
		}

		/// <summary>
		/// Scale a vector to unit length
		/// </summary>
		/// <param name="vec">The input vector</param>
		/// <param name="result">The normalized vector</param>
		public static void Normalize(ref Vector3 vec, out Vector3 result)
		{
			float scale = 1.0f / vec.Length();
			result.X = vec.X * scale;
			result.Y = vec.Y * scale;
			result.Z = vec.Z * scale;
		}

		#endregion

		#region Dot

		/// <summary>
		/// Calculate the dot (scalar) product of two vectors
		/// </summary>
		/// <param name="left">First operand</param>
		/// <param name="right">Second operand</param>
		/// <returns>The dot product of the two inputs</returns>
		public static float Dot(Vector3 left, Vector3 right)
		{
			return left.X * right.X + left.Y * right.Y + left.Z * right.Z;
		}

		/// <summary>
		/// Calculate the dot (scalar) product of two vectors
		/// </summary>
		/// <param name="left">First operand</param>
		/// <param name="right">Second operand</param>
		/// <param name="result">The dot product of the two inputs</param>
		public static void Dot(ref Vector3 left, ref Vector3 right, out float result)
		{
			result = left.X * right.X + left.Y * right.Y + left.Z * right.Z;
		}

		#endregion

		#region Cross

		/// <summary>
		/// Caclulate the cross (vector) product of two vectors
		/// </summary>
		/// <param name="left">First operand</param>
		/// <param name="right">Second operand</param>
		/// <returns>The cross product of the two inputs</returns>
		public static Vector3 Cross(Vector3 left, Vector3 right)
		{
			Vector3 result;
			Cross(ref left, ref right, out result);
			return result;
		}

		/// <summary>
		/// Caclulate the cross (vector) product of two vectors
		/// </summary>
		/// <param name="left">First operand</param>
		/// <param name="right">Second operand</param>
		/// <returns>The cross product of the two inputs</returns>
		/// <param name="result">The cross product of the two inputs</param>
		public static void Cross(ref Vector3 left, ref Vector3 right, out Vector3 result)
		{
			result = new Vector3(left.Y * right.Z - left.Z * right.Y,
				left.Z * right.X - left.X * right.Z,
				left.X * right.Y - left.Y * right.X);
		}

		#endregion

		#region Lerp

		/// <summary>
		/// Returns a new Vector that is the linear blend of the 2 given Vectors
		/// </summary>
		/// <param name="a">First input vector</param>
		/// <param name="b">Second input vector</param>
		/// <param name="blend">The blend factor. a when blend=0, b when blend=1.</param>
		/// <returns>a when blend=0, b when blend=1, and a linear combination otherwise</returns>
		public static Vector3 Lerp(Vector3 a, Vector3 b, float blend)
		{
			a.X = blend * (b.X - a.X) + a.X;
			a.Y = blend * (b.Y - a.Y) + a.Y;
			a.Z = blend * (b.Z - a.Z) + a.Z;
			return a;
		}

		/// <summary>
		/// Returns a new Vector that is the linear blend of the 2 given Vectors
		/// </summary>
		/// <param name="a">First input vector</param>
		/// <param name="b">Second input vector</param>
		/// <param name="blend">The blend factor. a when blend=0, b when blend=1.</param>
		/// <param name="result">a when blend=0, b when blend=1, and a linear combination otherwise</param>
		public static void Lerp(ref Vector3 a, ref Vector3 b, float blend, out Vector3 result)
		{
			result.X = blend * (b.X - a.X) + a.X;
			result.Y = blend * (b.Y - a.Y) + a.Y;
			result.Z = blend * (b.Z - a.Z) + a.Z;
		}

		#endregion

		#region Barycentric

		/// <summary>
		/// Interpolate 3 Vectors using Barycentric coordinates
		/// </summary>
		/// <param name="a">First input Vector</param>
		/// <param name="b">Second input Vector</param>
		/// <param name="c">Third input Vector</param>
		/// <param name="u">First Barycentric Coordinate</param>
		/// <param name="v">Second Barycentric Coordinate</param>
		/// <returns>a when u=v=0, b when u=1,v=0, c when u=0,v=1, and a linear combination of a,b,c otherwise</returns>
		public static Vector3 BaryCentric(Vector3 a, Vector3 b, Vector3 c, float u, float v)
		{
			return a + u * (b - a) + v * (c - a);
		}

		/// <summary>Interpolate 3 Vectors using Barycentric coordinates</summary>
		/// <param name="a">First input Vector.</param>
		/// <param name="b">Second input Vector.</param>
		/// <param name="c">Third input Vector.</param>
		/// <param name="u">First Barycentric Coordinate.</param>
		/// <param name="v">Second Barycentric Coordinate.</param>
		/// <param name="result">Output Vector. a when u=v=0, b when u=1,v=0, c when u=0,v=1, and a linear combination of a,b,c otherwise</param>
		public static void BaryCentric(ref Vector3 a, ref Vector3 b, ref Vector3 c, float u, float v, out Vector3 result)
		{
			result = a; // copy

			Vector3 temp = b; // copy
			Subtract(ref temp, ref a, out temp);
			Multiply(ref temp, u, out temp);
			Add(ref result, ref temp, out result);

			temp = c; // copy
			Subtract(ref temp, ref a, out temp);
			Multiply(ref temp, v, out temp);
			Add(ref result, ref temp, out result);
		}

		#endregion

		#region CalculateAngle

		/// <summary>
		/// Calculates the angle (in radians) between two vectors.
		/// </summary>
		/// <param name="first">The first vector.</param>
		/// <param name="second">The second vector.</param>
		/// <returns>Angle (in radians) between the vectors.</returns>
		/// <remarks>Note that the returned angle is never bigger than the constant Pi.</remarks>
		public static float CalculateAngle(Vector3 first, Vector3 second)
		{
			return (float)System.Math.Acos((Vector3.Dot(first, second)) / (first.Length() * second.Length()));
		}

		/// <summary>Calculates the angle (in radians) between two vectors.</summary>
		/// <param name="first">The first vector.</param>
		/// <param name="second">The second vector.</param>
		/// <param name="result">Angle (in radians) between the vectors.</param>
		/// <remarks>Note that the returned angle is never bigger than the constant Pi.</remarks>
		public static void CalculateAngle(ref Vector3 first, ref Vector3 second, out float result)
		{
			float temp;
			Vector3.Dot(ref first, ref second, out temp);
			result = (float)System.Math.Acos(temp / (first.Length() * second.Length()));
		}

		#endregion

		#endregion

		#region Swizzle

		#region 3-component

		/// <summary>
		/// Gets or sets an OpenTK.Vector3 with the X, Z, and Y components of this instance.
		/// </summary>
		[JsonIgnore]
		public Vector3 Xzy { get { return new Vector3(X, Z, Y); } set { X = value.X; Z = value.Y; Y = value.Z; } }

		/// <summary>
		/// Gets or sets an OpenTK.Vector3 with the Y, X, and Z components of this instance.
		/// </summary>
		[JsonIgnore]
		public Vector3 Yxz { get { return new Vector3(Y, X, Z); } set { Y = value.X; X = value.Y; Z = value.Z; } }

		/// <summary>
		/// Gets or sets an OpenTK.Vector3 with the Y, Z, and X components of this instance.
		/// </summary>
		[JsonIgnore]
		public Vector3 Yzx { get { return new Vector3(Y, Z, X); } set { Y = value.X; Z = value.Y; X = value.Z; } }

		/// <summary>
		/// Gets or sets an OpenTK.Vector3 with the Z, X, and Y components of this instance.
		/// </summary>
		[JsonIgnore]
		public Vector3 Zxy { get { return new Vector3(Z, X, Y); } set { Z = value.X; X = value.Y; Y = value.Z; } }

		/// <summary>
		/// Gets or sets an OpenTK.Vector3 with the Z, Y, and X components of this instance.
		/// </summary>
		[JsonIgnore]
		public Vector3 Zyx { get { return new Vector3(Z, Y, X); } set { Z = value.X; Y = value.Y; X = value.Z; } }

		#endregion

		#endregion

		#region Operators

		/// <summary>
		/// Adds two instances.
		/// </summary>
		/// <param name="left">The first instance.</param>
		/// <param name="right">The second instance.</param>
		/// <returns>The result of the calculation.</returns>
		public static Vector3 operator +(Vector3 left, Vector3 right)
		{
			left.X += right.X;
			left.Y += right.Y;
			left.Z += right.Z;
			return left;
		}

		/// <summary>
		/// Subtracts two instances.
		/// </summary>
		/// <param name="left">The first instance.</param>
		/// <param name="right">The second instance.</param>
		/// <returns>The result of the calculation.</returns>
		public static Vector3 operator -(Vector3 left, Vector3 right)
		{
			left.X -= right.X;
			left.Y -= right.Y;
			left.Z -= right.Z;
			return left;
		}

		/// <summary>
		/// Negates an instance.
		/// </summary>
		/// <param name="vec">The instance.</param>
		/// <returns>The result of the calculation.</returns>
		public static Vector3 operator -(Vector3 vec)
		{
			vec.X = -vec.X;
			vec.Y = -vec.Y;
			vec.Z = -vec.Z;
			return vec;
		}

		/// <summary>
		/// Multiplies an instance by a scalar.
		/// </summary>
		/// <param name="vec">The instance.</param>
		/// <param name="scale">The scalar.</param>
		/// <returns>The result of the calculation.</returns>
		public static Vector3 operator *(Vector3 vec, float scale)
		{
			vec.X *= scale;
			vec.Y *= scale;
			vec.Z *= scale;
			return vec;
		}

		/// <summary>
		/// Multiplies an instance by a scalar.
		/// </summary>
		/// <param name="scale">The scalar.</param>
		/// <param name="vec">The instance.</param>
		/// <returns>The result of the calculation.</returns>
		public static Vector3 operator *(float scale, Vector3 vec)
		{
			vec.X *= scale;
			vec.Y *= scale;
			vec.Z *= scale;
			return vec;
		}

		/// <summary>
		/// Component-wise multiplication between the specified instance by a scale vector.
		/// </summary>
		/// <param name="scale">Left operand.</param>
		/// <param name="vec">Right operand.</param>
		/// <returns>Result of multiplication.</returns>
		public static Vector3 operator *(Vector3 vec, Vector3 scale)
		{
			vec.X *= scale.X;
			vec.Y *= scale.Y;
			vec.Z *= scale.Z;
			return vec;
		}

		/// <summary>
		/// Divides an instance by a scalar.
		/// </summary>
		/// <param name="vec">The instance.</param>
		/// <param name="scale">The scalar.</param>
		/// <returns>The result of the calculation.</returns>
		public static Vector3 operator /(Vector3 vec, float scale)
		{
			float mult = 1.0f / scale;
			vec.X *= mult;
			vec.Y *= mult;
			vec.Z *= mult;
			return vec;
		}

		/// <summary>
		/// Compares two instances for equality.
		/// </summary>
		/// <param name="left">The first instance.</param>
		/// <param name="right">The second instance.</param>
		/// <returns>True, if left equals right; false otherwise.</returns>
		public static bool operator ==(Vector3 left, Vector3 right)
		{
			return left.Equals(right);
		}

		/// <summary>
		/// Compares two instances for inequality.
		/// </summary>
		/// <param name="left">The first instance.</param>
		/// <param name="right">The second instance.</param>
		/// <returns>True, if left does not equa lright; false otherwise.</returns>
		public static bool operator !=(Vector3 left, Vector3 right)
		{
			return !left.Equals(right);
		}

		#endregion

		#region Overrides

		#region public override string ToString()

		private static string listSeparator = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator;
		/// <summary>
		/// Returns a System.String that represents the current Vector3.
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return String.Format("({0}{3} {1}{3} {2})", X, Y, Z, listSeparator);
		}

		#endregion

		#region public override int GetHashCode()

		/// <summary>
		/// Returns the hashcode for this instance.
		/// </summary>
		/// <returns>A System.Int32 containing the unique hashcode for this instance.</returns>
		public override int GetHashCode()
		{
			return X.GetHashCode() ^ Y.GetHashCode() ^ Z.GetHashCode();
		}

		#endregion

		#region public override bool Equals(object obj)

		/// <summary>
		/// Indicates whether this instance and a specified object are equal.
		/// </summary>
		/// <param name="obj">The object to compare to.</param>
		/// <returns>True if the instances are equal; false otherwise.</returns>
		public override bool Equals(object obj)
		{
			if (!(obj is Vector3))
				return false;

			return this.Equals((Vector3)obj);
		}

		#endregion

		#endregion

		#endregion

		#region IEquatable<Vector3> Members

		/// <summary>Indicates whether the current vector is equal to another vector.</summary>
		/// <param name="other">A vector to compare with this vector.</param>
		/// <returns>true if the current vector is equal to the vector parameter; otherwise, false.</returns>
		public bool Equals(Vector3 other)
		{
			return
				X == other.X &&
				Y == other.Y &&
				Z == other.Z;
		}

		#endregion
	}
}

#endif
