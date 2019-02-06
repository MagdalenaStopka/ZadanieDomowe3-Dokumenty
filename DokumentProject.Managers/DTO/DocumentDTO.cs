using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentProject.Managers.DTO
{
  public  class DocumentDTO
    {
        
    
        public string Name { get; set; }
        public string Content { get; set; }

        public override string ToString()
        {
            return string.Format("Nazwa: {0}    Tresc: {1}", Name, Content);
        }
    }
    
}
