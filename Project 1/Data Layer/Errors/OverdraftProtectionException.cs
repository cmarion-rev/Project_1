using System;

namespace Data_Layer.Errors
{
    [System.Serializable]
    public class OverdraftProtectionException : Exception
    {
        public OverdraftProtectionException() { }
        
        public OverdraftProtectionException(string message) : base(message) { }
        
        public OverdraftProtectionException(string message, Exception inner) : base(message, inner) { }
        
        protected OverdraftProtectionException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}