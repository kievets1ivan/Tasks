using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks.IntegrationTests.Base
{
    public class GenericComparer<T> : IEqualityComparer<T>
    {
        private readonly Func<T, object> item;


        public GenericComparer(Func<T, object> item)
        {
            this.item = item;
        }

        public bool Equals(T obj1, T obj2)
        {
            if (obj1 == null || obj2 == null)
                return false;

            var expected = item.Invoke(obj1);
            var result = item.Invoke(obj2);
            return expected.Equals(result);
        }

        public int GetHashCode(T obj)
        {
            return obj.GetHashCode();
        }
    }
}
