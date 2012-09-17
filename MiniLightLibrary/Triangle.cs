using System;

namespace MiniLight.Library
{
    class Triangle
    {


        #region Properties

        public static float TOLERANCE
        {
            get
            {
                // One mm seems reasonable
                return 1.0f / 1024.0f;
            }
        }

        public Vector3f Reflectivity { get { return _reflectivity; } }
        public Vector3f Emissivity { get { return _emissivity; } }

        #endregion Properties


        #region Fields

        private const float EPSILON = 1.0f / 1048576.0f;
        
        // Geometry
        private Vector3f[] _vertices;

        // Quality
        private Vector3f _reflectivity;
        private Vector3f _emissivity;
        
        #endregion Fields




        #region Methods
        
        public Triangle(Vector3f vec1, Vector3f vec2, Vector3f vec3, Vector3f reflectivity, Vector3f emissivity)
        {
            _vertices = new Vector3f[] {vec1, vec2, vec3};
            _reflectivity = reflectivity.GetClamped(Vector3f.Zero, Vector3f.Unit);
            _emissivity = emissivity.GetClamped(Vector3f.Zero, Vector3f.Max); ;
        }

        public void GetBound(ref float[] bound)
        {
            // Initialize
            for (int i = 6; i-- > 0; bound[i] = _vertices[2][i % 3]) { }

            // Expand
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0, d = 0, m = 0; j < 6; ++j, d = j / 3, m = j % 3)
                {
                    // Include some padding (proportional and fixed)
                    // The proportional part allows triangles with large coordinates
                    // to still have some padding in single-precision FP
                    float v = _vertices[i][m] +
                        ((d == 1 ? 1.0f : -1.0f) *
                        ((Math.Abs(_vertices[i][m]) * EPSILON) + Triangle.TOLERANCE));

                    bound[j] = ((bound[j] > v ? 1 : 0) ^ d) == 1 ? v : bound[j];
                }
            }
        }

        public bool GetIntersection(ref Vector3f rayOrigin, ref Vector3f rayDirection, ref float hitDistance)
        {
            // Find vectors for two edges sharing vert0
            Vector3f edge1 = _vertices[1] - _vertices[0];
            Vector3f edge2 = _vertices[2] - _vertices[0];

            // Begin calculating determinant - also used to calculate U parameter
            Vector3f pVector = rayDirection.GetCrossProduct(edge2);

            // If determinant is near zero, ray lies in plane of triangle
            float determinant = edge1.GetDotProduct(pVector);

            bool isHit = false;

            if ((determinant <= -EPSILON) | (determinant >= EPSILON))
            {
                float inverseDeterminant = 1.0f / determinant;

                // Calculate distance from vertex 0 to ray origin
                Vector3f tVector = rayOrigin - _vertices[0];

                // Calculate U parameter and test bounds
                float uParam = tVector.GetDotProduct(pVector) * inverseDeterminant;
                if ((uParam >= 0.0f) & (uParam <= 1.0f))
                {
                    // Prepare to test V parameter
                    Vector3f qVector = tVector.GetCrossProduct(edge1);

                    // Calculate V parameter and test bounds
                    float vParam = rayDirection.GetDotProduct(qVector) * inverseDeterminant;
                    if ((vParam >= 0.0f) & (uParam + vParam <= 1.0f))
                    {
                        // Calculate t, ray intersects triangle
                        hitDistance = edge2.GetDotProduct(qVector) * inverseDeterminant;

                        // Only allow intersections in the forward ray direction
                        isHit = (hitDistance >= 0.0f);
                    }
                }
            }

            return isHit;
        }

        /// <summary>
        /// Gets the sample point.
        /// </summary>
        /// <param name="random">The random object.</param>
        /// <returns>A vector of the sample point</returns>
        public Vector3f GetSamplePoint(ref RandomMwc random)
        {
            // Get two random floats
            float sqrt1 = (float)Math.Sqrt(random.GetFloat());
            float r2 = random.GetFloat();

            // Make barycentric coords
            float a = 1.0f - sqrt1;
            float b = (1.0f - r2) * sqrt1;

            // Make position from barycentrics
            // Calculate interpolation by using two edges
            // as axes scaled by the barycentrics
            return ((_vertices[1] - _vertices[0]) * a) + ((_vertices[2] - _vertices[0]) * b) + _vertices[0];
        }

        public Vector3f GetNormal()
        {
            return GetTangent().GetCrossProduct(_vertices[2] - _vertices[1]).Unitize();
        }

        public Vector3f GetTangent()
        {
            return (_vertices[1] - _vertices[0]).Unitize();
        }

        public float GetArea()
        {
            // Half area of parallelogram
            Vector3f pa2 = (_vertices[1] - _vertices[0]).GetCrossProduct(_vertices[2] - _vertices[1] );

            return (float)Math.Sqrt(pa2.GetDotProduct(pa2)) * 0.5f;
        }

        #endregion Methods


    }
}
