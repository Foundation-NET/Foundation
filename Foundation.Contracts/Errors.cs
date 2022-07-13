namespace Foundation.Contracts
{
    public abstract class ContractException : System.Exception
    {
        private string _contractName;
        public  virtual string msg {get;}
        public ContractException(string contractName)
        {
            _contractName = contractName;
            msg = string.Empty;
        }
        public override string Message => _contractName + ": " + msg;
    }
    public class NoContractorException : ContractException
    {
        public NoContractorException(string name):base(name) {}
        public override string msg => "No Contractor defined";
    }
    public class ContratNullException : ContractException
    {
        public ContratNullException(string name):base(name) {}
        public override string msg => "Field cannot be null";
    }
    public class ContratValidationException : ContractException
    {
        public ContratValidationException(string name):base(name) {}
        public override string msg => "Field did not pass validation";
    }
    public class ContratDuplicateException : ContractException
    {
        public ContratDuplicateException(string name):base(name) {}
        public override string msg => "Cannot have duplicate contract";
    }
}