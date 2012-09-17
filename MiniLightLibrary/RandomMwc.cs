
namespace MiniLight.Library
{
    class RandomMwc
    {
        /// <summary>
        /// Default seeds.
        /// </summary>
        readonly uint[] SEEDS = { 521288629u, 362436069u };


        /// <summary>
        /// The place to save the seeds
        /// </summary>
        private uint[] _seeds = new uint[2];

        /// <summary>
        /// Constructor, initializes seeds
        /// </summary>
        /// <param name="seed">an optional seed</param>
        public RandomMwc(uint seed = 0u)
        {
            _seeds[0] = (0u != seed) ? seed : SEEDS[0];
            _seeds[1] = (0u != seed) ? seed : SEEDS[1];
        }

        /// <summary>
        /// Gets the next random unsigned integer
        /// </summary>
        /// <returns>
        /// a random unsigned integer
        /// </returns>
        public uint GetUint()
        {
            _seeds[0] = 18000u * (_seeds[0] & 0xFFFFu) + (_seeds[0] >> 16);
            _seeds[1] = 30903u * (_seeds[1] & 0xFFFFu) + (_seeds[1] >> 16);

            return (_seeds[0] << 16) + (_seeds[1] & 0xFFFFu);
        }

        /// <summary>
        /// Gets the next random float
        /// </summary>
        /// <returns>a random float</returns>
        public float GetFloat()
        {
            return ((float)GetUint()) / 4294967296.0f;
        }
    }
}
