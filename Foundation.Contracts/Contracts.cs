using System;

namespace Foundation.Contracts
{
    public class Contract
    {
        private Object _contrator;
        private List<string> _contracts;
        private string _currentContract;
        private Contract.Type _type;
        public Contract(object o)
        {
            _contracts = new List<string>();
            _contrator = o;
            _currentContract = string.Empty;
            _type = Type.Object;
        }
        public Contract(Contract.Type type, object? o = null)
        {
            if (type == Type.Static)
            {
                _type = type;
                _contrator = new object();
                _contracts = new List<string>();
                _currentContract = string.Empty;
                
            } else if (type == Type.Object)
            {
                _contracts = new List<string>();
                _contrator = o;
                _currentContract = string.Empty;
                _type = type;
            }
        }

        public enum Type
        {
            Static,
            Object
        }

        public void New(string contractName, bool runOnce = false)
        {
            string cName = contractName;
            if (_contracts.Contains(cName) && runOnce)
            {
                throw new ContratDuplicateException(_currentContract);
            } else if(!_contracts.Contains(cName) && runOnce)
            {
                _contracts.Add(cName);
            } else if (_contracts.Contains(cName) && !runOnce)
            {
                Random rand = new Random();
                string uniq = "";
                char letter;
                int randValue;
                for (int i = 0; i < 21; i++)
                {
                    randValue = rand.Next(0, 26);
                    letter = Convert.ToChar(randValue + 65);
                    uniq = uniq + letter;
                }
                cName = contractName + "-" + uniq;
                _contracts.Add(cName);
            } else if(!_contracts.Contains(cName) && !runOnce)
            {
                _contracts.Add(cName);
            }
            if(_contrator == null)
            {
                throw new NoContractorException(_currentContract);
            }
            _currentContract = cName;
        }
        public void Require(object? o)
        {
            if(o==null)
                throw new ContratNullException(_currentContract);
        }
        public void Require(object o, Func<object, bool> validator)
        {
            Require(o);
            if(!validator(o))
                throw new ContratValidationException(_currentContract);
        }
    }
}