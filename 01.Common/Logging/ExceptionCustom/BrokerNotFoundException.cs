using System;
using System.Collections.Generic;
using System.Text;

namespace Logging.ExceptionCustom
{
    public class BrokerNotFoundException : Exception
    {
        public BrokerNotFoundException(string message) : base(message)
        {
        }

        public BrokerNotFoundException() : base()
        {
        }

        public BrokerNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
