using System;
using System.Collections.Generic;
using System.Text;

namespace MyUtility.CollectionExtentions
{
    public class EqualCompareFuncWrapper<T> : IEqualityComparer<T>
    {
        private readonly Func<T, T, bool> _compareFunc;
        public EqualCompareFuncWrapper(Func<T, T, bool> compareFunc1)
        {
            this._compareFunc = compareFunc1;
        }

        /// <summary>Determines whether the specified objects are equal.</summary>
        /// <param name="x">The first object of type T to compare.</param>
        /// <param name="y">The second object of type T to compare.</param>
        /// <returns>true if the specified objects are equal; otherwise, false.</returns>
        public bool Equals(T x, T y)
        {
            return this._compareFunc(x, y);
        }

        /// <summary>Returns a hash code for the specified object.</summary>
        /// <param name="obj">The <see cref="T:System.Object"></see> for which a hash code is to be returned.</param>
        /// <returns>A hash code for the specified object.</returns>
        /// <exception cref="T:System.ArgumentNullException">The type of <paramref name="obj">obj</paramref> is a reference type and <paramref name="obj">obj</paramref> is null.</exception>
        public int GetHashCode(T obj)
        {
            return obj.GetHashCode();
        }
    }
}
