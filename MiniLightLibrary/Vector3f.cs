using System;

namespace MiniLight.Library
{
    class Vector3f
    {


        #region Properties

        /// <summary>
        /// The x coordinate.
        /// </summary>
        public float X
        {
            get { return _Coordinates[0]; }
            set { _Coordinates[0] = value; }
        }

        /// <summary>
        /// The y coordinate.
        /// </summary>
        public float Y
        {
            get { return _Coordinates[1]; }
            set { _Coordinates[1] = value; }
        }

        /// <summary>
        /// The z coordinate.
        /// </summary>
        public float Z
        {
            get { return _Coordinates[2]; }
            set { _Coordinates[2] = value; }
        }

        /// <summary>
        /// Access x, y or z coordinate using 0, 1 or 2
        /// </summary>
        /// <param name="i">the index to access</param>
        /// <returns>the requested element or throws an IndexOutOfRangeException if index is invalid.</returns>
        public float this[int i]
        {
            get
            {
                switch (i)
                {
                    case 0:
                        return this.X;
                    case 1:
                        return this.Y;
                    case 2:
                        return this.Z;
                    default:
                        throw new IndexOutOfRangeException();
                }
            }

            set
            {
                switch (i)
                {
                    case 0:
                        this.X = value;
                        break;
                    case 1:
                        this.Y = value;
                        break;
                    case 2:
                        this.Z = value;
                        break;
                    default:
                        throw new IndexOutOfRangeException();
                }
            }
        }

        /// <summary>
        /// The zero vector.
        /// </summary>
        public static Vector3f Zero { get { return _zero; } }

        /// <summary>
        /// The unit vector.
        /// </summary>
        public static Vector3f Unit { get { return _unit; } }

        /// <summary>
        /// The minimal vector.
        /// </summary>
        public static Vector3f Min { get { return _min; } }

        /// <summary>
        /// The maximum vector.
        /// </summary>
        public static Vector3f Max { get { return _max; } }

        #endregion Properties


        #region Fields

        private float[] _Coordinates;

        private static Vector3f _zero = new Vector3f(0.0f, 0.0f, 0.0f);
        private static Vector3f _unit = new Vector3f(1.0f, 1.0f, 1.0f);
        private static Vector3f _min = new Vector3f(float.MinValue, float.MinValue, float.MinValue);
        private static Vector3f _max = new Vector3f(float.MaxValue, float.MaxValue, float.MaxValue);

        #endregion Fields


        #region Methods

        #region Constructors
        /// <summary>
        /// This constructor creates a new vector from three float coordinates.
        /// </summary>
        /// <param name="xCoordinate">x coordinate</param>
        /// <param name="yCoordinate">y coordinate</param>
        /// <param name="zCoordinate">z coordinate</param>
        public Vector3f(float xCoordinate, float yCoordinate, float zCoordinate)
        {
            _Coordinates = new float[3] { xCoordinate, yCoordinate, zCoordinate };
        }

        /// <summary>
        /// This constructor copies another vector.
        /// </summary>
        /// <param name="vector">the vector to copy from</param>
        public Vector3f(Vector3f vector)
        {
            this.X = vector.X;
            this.Y = vector.Y;
            this.Z = vector.Z;
        }

        /// <summary>
        /// The default constructor is the zero vector
        /// </summary>
        public Vector3f()
        {
            this.X = 0.0f;
            this.Y = 0.0f;
            this.Z = 0.0f;
        }
        #endregion Constructors

        /// <summary>
        /// Calculates the dot product.
        /// </summary>
        /// <param name="vector">the other vector to multiply with</param>
        /// <returns>a new vector on which the dot product has been applied to</returns>
        public float GetDotProduct(Vector3f vector)
        {
            return (this.X * vector.X) +
                   (this.Y * vector.Y) +
                   (this.Z * vector.Z);
        }

        /// <summary>
        /// Calculates the negative vector.
        /// </summary>
        /// <returns>a new vector that is the negative of the current one</returns>
        public Vector3f GetNegative()
        {
            Vector3f negativeVector = new Vector3f(
                -this.X,
                -this.Y,
                -this.Z
            );


            return negativeVector;
        }

        /// <summary>
        /// Unitizes this vector
        /// </summary>
        /// <returns>a new vector which has been unitized</returns>
        public Vector3f Unitize()
        {
            float length = (float)Math.Sqrt(GetDotProduct(this));
            float oneOverLength = length != 0.0f ? 1.0f / length : 0.0f;

            return this * oneOverLength;
        }

        /// <summary>
        /// Calculates the cross product of this and another vector
        /// </summary>
        /// <param name="vector">the other vector</param>
        /// <returns>a new vector that has been cross multiplied</returns>
        public Vector3f GetCrossProduct(Vector3f vector)
        {
            return new Vector3f(
                (this.Y * vector.Z) - (this.Z * vector.Y),
                (this.Z * vector.X) - (this.X * vector.Z),
                (this.X * vector.Y) - (this.Y * vector.X)
            );
        }

        /// <summary>
        /// Checks if the vector is the zero vector
        /// </summary>
        /// <returns>true if it is else false</returns>
        public bool IsZero()
        {
            return ((this.X == 0.0f) & (this.Y == 0.0f) & (this.Z == 0.0f));
        }

        /// <summary>
        /// Clamps the vector between min and max
        /// </summary>
        /// <param name="min">the lower boundary</param>
        /// <param name="max">the upper boundary</param>
        /// <returns>the clamped vector</returns>
        public Vector3f GetClamped(Vector3f min, Vector3f max)
        {
            Vector3f r = new Vector3f(this);

            r.X = this.X >= min.X ? r.X : min.X;
            r.X = this.X <= min.X ? r.X : max.X;

            r.Y = this.Y >= min.Y ? r.Y : min.Y;
            r.Y = this.Y <= min.Y ? r.Y : max.Y;

            r.Z = this.Z >= min.Z ? r.Z : min.Z;
            r.Z = this.Z <= min.Z ? r.Z : max.Z;

            return r;
        }

        #region Operators

        /// <summary>
        /// Provides addition of two vectors
        /// </summary>
        /// <param name="lhsVector">the vector on the left hand side</param>
        /// <param name="rhsVector">the vector on the right hand side</param>
        /// <returns>a new vector that is the result of the addition</returns>
        public static Vector3f operator +(Vector3f lhsVector, Vector3f rhsVector)
        {
            return new Vector3f(
                lhsVector.X + rhsVector.X,
                lhsVector.Y + rhsVector.Y,
                lhsVector.Z + rhsVector.Z
            );
        }

        /// <summary>
        /// Provides subtraction of two vectors
        /// </summary>
        /// <param name="lhsVector">the vector on the left hand side</param>
        /// <param name="rhsVector">the vector on the right hand side</param>
        /// <returns>a new vector that is the result of the subtraction</returns>
        public static Vector3f operator -(Vector3f lhsVector, Vector3f rhsVector)
        {
            return new Vector3f(
                lhsVector.X - rhsVector.X,
                lhsVector.Y - rhsVector.Y,
                lhsVector.Z - rhsVector.Z
            );
        }

        /// <summary>
        /// Provides multiplication of two vectors
        /// </summary>
        /// <param name="lhsVector">the vector on the left hand side</param>
        /// <param name="rhsVector">the vector on the right hand side</param>
        /// <returns>a new vector that is the result of the multiplication</returns>
        public static Vector3f operator *(Vector3f lhsVector, Vector3f rhsVector)
        {
            return new Vector3f(
                lhsVector.X * rhsVector.X,
                lhsVector.Y * rhsVector.Y,
                lhsVector.Z * rhsVector.Z
            );
        }

        /// <summary>
        /// Provides scalar multipliation of a vector
        /// </summary>
        /// <param name="lhsVector">the vector on the left hand side</param>
        /// <param name="f">the scalar value</param>
        /// <returns>a new vector that is the result of the scalar multiplication</returns>
        public static Vector3f operator *(Vector3f lhsVector, float f)
        {
            return new Vector3f(
                lhsVector.X * f,
                lhsVector.Y * f,
                lhsVector.Z * f
            );
        }

        /// <summary>
        /// Provides scalar division of two vectors
        /// </summary>
        /// <param name="lhsVector">the vector on the left hand side</param>
        /// <param name="f">the scalar value</param>
        /// <returns>a new vector that is the result of the division</returns>
        public static Vector3f operator /(Vector3f lhsVector, float f)
        {
            float oneOverFloat = 1.0f / f;

            return new Vector3f(
                lhsVector.X * oneOverFloat,
                lhsVector.Y * oneOverFloat,
                lhsVector.Z * oneOverFloat
            );
        }

        #endregion Operators

        #endregion Methods


    }
}
