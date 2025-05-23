// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

// Description: A class that implements ICollection<ushort> for a sequence of numbers [0..n-1].

using System.Collections;

namespace MS.Internal
{
    internal class SequentialUshortCollection : ICollection<ushort>
    {
        public SequentialUshortCollection(ushort count)
        {
            _count = count;
        }

        #region ICollection<ushort> Members

        public void Add(ushort item)
        {
            throw new NotSupportedException();
        }

        public void Clear()
        {
            throw new NotSupportedException();
        }

        public bool Contains(ushort item)
        {
            return item < _count;
        }

        public void CopyTo(ushort[] array, int arrayIndex)
        {
            ArgumentNullException.ThrowIfNull(array);

            if (array.Rank != 1)
            {
                throw new ArgumentException(SR.Collection_BadRank);
            }

            // The extra "arrayIndex >= array.Length" check in because even if _collection.Count
            // is 0 the index is not allowed to be equal or greater than the length
            // (from the MSDN ICollection docs)
            ArgumentOutOfRangeException.ThrowIfNegative(arrayIndex);
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(arrayIndex, array.Length);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(arrayIndex, array.Length - Count);

            for (ushort i = 0; i < _count; ++i)
                array[arrayIndex + i] = i;
        }

        public int Count
        {
            get { return _count; }
        }

        public bool IsReadOnly
        {
            get { return true; }
        }

        public bool Remove(ushort item)
        {
            throw new NotSupportedException();
        }

        #endregion

        #region IEnumerable<ushort> Members

        public IEnumerator<ushort> GetEnumerator()
        {
            for (ushort i = 0; i < _count; ++i)
                yield return i;
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<ushort>)this).GetEnumerator();
        }

        #endregion

        private ushort _count;
    }
}

