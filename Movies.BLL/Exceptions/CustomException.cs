using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.BLL.Exceptions
{
    public class CustomException: Exception
    {
        private string ExxMessage { get; set; }
        public CustomException() : base() { }

        public CustomException(string msj) : base()

        {
            ExxMessage = msj;
        }

        public override string Message
        {
            get
            {
                return ExxMessage;
            }
        }

        public override string StackTrace
        {
            get
            {
                return "";
            }
        }
    }
}
