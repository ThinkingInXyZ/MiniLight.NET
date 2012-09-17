using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniLight.Library
{
    class SpatialIndex
    {
        const int MAX_LEVELS = 44;
        const uint MAX_ITEMS = 8;

        private bool _isBranch;
        float[] _bound = new float[6];
        List<object> _vector = new List<object>();

        public SpatialIndex(Vector3f eyePosition, List<Triangle> items)
        {

            // Accomodate eye position (makes tracing algorithm simpler)
            for (int i = 6; i-- > 0; _bound[i] = eyePosition[i % 3]) { }

            // Accomodate all items
            for (int i = 0; i < items.Count; ++i)
            {
                float[] itemBound = new float[6];
                items[i].GetBound(ref itemBound);

                for (int j = 0; j < 6; ++j)
                {
                    if((_bound[j] > itemBound[j]) ^ (j > 2))
                    {
                        _bound[j] = itemBound[j];
                    }
                }
            }

            // Make cubical
            float maxSize = 0.0f;
            for (float b = _bound + 3; b-- > _bound; )
            {
            }
        }

        private SpatialIndex(SpatialIndex idx)
        {
        }
    }
}
