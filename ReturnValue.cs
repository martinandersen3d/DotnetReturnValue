namespace ClassLib.ReturnUtils
{
    // A Sctruct that takes a Generic Type
    // Note: Succes/Failed is set automaticly
    public struct ReturnValue<T>
    {
        private T _value;
        public T Value
        {
            get { return _value; }
            set { _value = value; Success = true; }
        }

        public bool Success;
        public bool Failed => !Success;

        private Exception _error;
        public Exception Error
        {
            get { return _error; }
            set { _error = value; Success = false; }
        }
    }
}