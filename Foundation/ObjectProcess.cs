using System.Collections.Generic;

namespace Foundation
{
    public partial class ObjectProcess<T> : ObjectBase
    {
        private protected List<T>? _rows;
        private protected List<Func<T, bool>>? _filters;

        public List<T> From {set {
            _rows = value;
        }}

        public Func<T, bool> Where {set{
            if (_filters == null)
                _filters = new List<Func<T, bool>>();
            _filters.Add(value);
        }}

        public void Execute()
        {
            if (_rows == null)
                return;
            OnStart();
            foreach(var v in _rows)
            {
                if (_filters == null)
                {
                    InRow(v);
                    continue;   
                }
                int acc = 0;
                for (int fc = 0; fc < _filters.Count; fc++)
                {
                    if(_filters[fc](v))
                        acc++;
                }
                if(acc == _filters.Count)
                    InRow(v);
            }
            OnEnd();
        }

        private protected void InRow(T row)        
        {
            EnterRow(row);
            LeaveRow(row);
        }

        public virtual void OnStart() {}
        public virtual void EnterRow(T row) {}
        public virtual void LeaveRow(T row) {}
        public virtual void OnEnd() {}

    }
}