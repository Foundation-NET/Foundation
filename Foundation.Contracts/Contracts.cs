using System;

namespace Foundation.Contracts
{
    public class Contract
    {
        private Object _contrator;
        private List<string> _contracts;
        private string _currentContract;
        public Contract(object o)
        {
            _contracts = new List<string>();
            _contrator = o;
            _currentContract = string.Empty;
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