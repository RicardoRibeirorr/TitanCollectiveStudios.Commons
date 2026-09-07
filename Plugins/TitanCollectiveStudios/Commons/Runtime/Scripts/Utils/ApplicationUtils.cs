using UnityEngine;

namespace TitanCollectiveStudios.Commons.Utils
{
    public static class ApplicationUtils
    {
        private static bool _isQuitting;

        public static bool IsQuitting => _isQuitting;


        [RuntimeInitializeOnLoadMethod]
        private static void Initialize()
        {
            Application.quitting += () =>
            {
                _isQuitting = true;
            };
        }
    }
}