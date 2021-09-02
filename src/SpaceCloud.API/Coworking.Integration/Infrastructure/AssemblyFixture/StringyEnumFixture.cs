using System;

namespace Coworking.Integration.Tests
{
    public class StringyEnumFixture : IDisposable
    {
        private static bool _isInitialized;

        public StringyEnumFixture()
        {
            Initalize();
        }

        public void Dispose()
        {
            _isInitialized = false;
        }

        private void Initalize()
        {
            if (_isInitialized) return;
            _isInitialized = true;
        }
    }
}