namespace ApiCounter
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
            Interlocked.Increment(ref count);
        }

        public int GetCount()
        {
            return Interlocked.CompareExchange(ref count, 0, 0);
        }
    }
}