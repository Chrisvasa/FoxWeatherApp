namespace WeatherAPI.Tests
{
        public class ApiCallCounter
        {
            private int count;

            public ApiCallCounter()
            {
                count = 0;
            }

            public void Increment()
            {
                count++;
            }

            public int GetCount()
            {
                return count;
            }
        }
}