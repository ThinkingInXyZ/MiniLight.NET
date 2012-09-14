using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniLight.Library
{
    class Vector3f
    {
        #region Properties

        public float X
        {
            get { return _Coordinates[0]; }
            set { _Coordinates[0] = value; }
        }

        public float Y
        {
            get { return _Coordinates[1]; }
            set { _Coordinates[1] = value; }
        }

        public float Z
        {
            get { return _Coordinates[2]; }
            set { _Coordinates[2] = value; }
        }

        #endregion Properties


        #region Fields

        float[] _Coordinates;

        #endregion Fields


        #region Methods

        // Constructor
        public Vector3f(float xCoordinate, float yCoordinate, float zCoordinate)
        {
            _Coordinates = new float[3] { xCoordinate, yCoordinate, zCoordinate };
        }

        public static float GetDotProduct(Vector3f vector1, Vector3f vector2)
        {
            return (vector1.X * vector2.X) +
                   (vector1.Y * vector2.Y) +
                   (vector1.Z * vector2.Z);
        }

        public static Vector3f GetNegativeVector3f(Vector3f vector)
        {
            Vector3f negativeVector = new Vector3f(
                -vector.X,
                -vector.Y,
                -vector.Z
            );


            return negativeVector;
        }

        #endregion Methods


    }
}
