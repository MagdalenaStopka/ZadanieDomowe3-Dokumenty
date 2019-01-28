using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentProject.Managers.Exceptions
{
  public  class DocumentExceptions
    {
        public class DuplicateDataException : Exception
        {

            public DuplicateDataException(string message) : base(message)
            {
            }
        }

        public class DocumentNotFoundException : Exception
        {
            public DocumentNotFoundException(string message) : base(message) { }
        }
    }
}
