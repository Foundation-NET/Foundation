using System.ComponentModel;
using System;
using System.Reflection;
using System.Collections.Generic;
using System.Collections;

using Foundation.Data.Types;

namespace Foundation.Data.Entity
{
    public class ColumnCollection : IEnumerable<ITypedColumn>
    {
        private Collection _col;

        public ColumnCollection()
        {
            _col = new Collection();
        }

        public IEnumerator<ITypedColumn> GetEnumerator()
        {
            return _col;
        }
        private IEnumerator GetEnumerator1()
        {
            return this.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator1();
        }

        public void Add(ITypedColumn col) => _col.Add(col);
        internal class Collection : IEnumerator<ITypedColumn>
        {
            private int curIndex;
            private ITypedColumn currCol;

            private List<ITypedColumn> _columns;

            public Collection()
            {
                _columns = new List<ITypedColumn>();
                curIndex = -1;
                currCol = new NullColumn();
            }

            public void Add(ITypedColumn col) => _columns.Add(col);

            public bool MoveNext()
            {
                //Avoids going beyond the end of the collection.
                if (++curIndex >= _columns.Count)
                {
                    return false;
                }
                else
                {
                    // Set current box to next item in collection.
                    currCol = _columns[curIndex];
                }
                return true;
            }

            public void Reset() { curIndex = -1; }

            void IDisposable.Dispose() { }

            public ITypedColumn Current
            {
                get { return currCol; }
            }

            object IEnumerator.Current
            {
                get {return Current;}
            }
        }
    }
}