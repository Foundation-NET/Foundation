using System.ComponentModel;

namespace Foundation.Data.Types
{
    public interface ITypedColumn
    {
        Object Get();
        void Set(Object o);
        void Reset();
        void Undo();
        bool Virtual {get;set;}
        bool Changed {get;set;}
        string Name {get;set;}
        string? Caption {get;set;}
    }
    public abstract partial class TypedColumn<DataType> : ITypedColumn  where DataType : TypeBase, new()
    {
        private DataType _cur;
        private DataType? _orig;
        public bool Changed {get;set;}
        public string Name {get;set;}
        public string? Caption {get;set;}
        public bool Virtual {get;set;}
        public DataType Value {
            get {return _cur;}
            set {
                if (_orig == null)
                {
                    _cur = value;
                    _orig = value;
                } else if (_cur != _orig) {
                    _cur = value;
                    if (OnChange != null)
                        OnChange(this);
                    Changed = true;
                }
            }
        }
        public Action<TypedColumn<DataType>>? OnChange;
        public Action<TypedColumn<DataType>>? OnReset;
        public Action<TypedColumn<DataType>>? OnUndo;

        public TypedColumn()
        {
            Value = new DataType();
            _cur = new DataType();
            _orig = null;
            Changed = false;
        }
        public TypedColumn(string name)
        :this()
        {
            Name = name;
        }
        public void Reset()
        {
            if (OnReset != null)
                OnReset(this);
            _orig = _cur;
        }        
        public void Undo()
        {
            if (OnUndo != null)
                OnUndo(this);
            if (_orig != null)
                _cur = _orig;
        }
        public void Set(object o) => Value = (DataType)o;
        public object Get() => Value;

    }
}