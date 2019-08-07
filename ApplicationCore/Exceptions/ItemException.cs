using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryManagement.ApplicationCore.Exceptions
{
    public class ItemException : Exception
    {
        public ItemException(string message) : base(message)
        {
        }
        protected ItemException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
        }       
        public ItemException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
