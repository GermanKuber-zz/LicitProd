using System.Collections;
using System.Collections.Generic;

namespace LicitProd.Infrastructure
{
    public class Option<T> : IEnumerable<T>
    {
        public T[] Content { get; }

        private Option(T[] content)
        {
            this.Content = content;
        }

        public static Option<T> Some(T value) =>
            new Option<T>(new[] { value });

        public static Option<T> None() =>
            new Option<T>(new T[0]);

        public IEnumerator<T> GetEnumerator() =>
            ((IEnumerable<T>)this.Content).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() =>
                 GetEnumerator();
    }
}
