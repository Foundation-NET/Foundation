using System.ComponentModel;
using System;
using System.Reflection;
using System.Collections.Generic;
using System.Collections;

using Foundation.Data.Types;

namespace Foundation.Data.Entity
{
    public class EntityCollection : IEnumerable<DataEntity>
    {
        private Collection _col;

        public EntityCollection()
        {
            _col = new Collection();
        }

        public IEnumerator<DataEntity> GetEnumerator()
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

        public void Add(DataEntity col) => _col.Add(col);
        internal class Collection : IEnumerator<DataEntity>
        {
            private int curIndex;
            private DataEntity currCol;

            private List<DataEntity> _columns;

            public Collection()
            {
                _columns = new List<DataEntity>();
                curIndex = -1;
                currCol = new DataEntity();
            }

            public void Add(DataEntity col) => _columns.Add(col);

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

            public DataEntity Current
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