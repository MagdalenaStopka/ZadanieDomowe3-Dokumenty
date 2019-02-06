using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentProject.Models.Document
{
    public class Document
    {
        public string Name { get; set; }
        public string Content { get; set; }
        public static int LicznikDokumentow { get; private set; }

        public Document(string name, string tresc)
        {
            Name = name;
            Content = tresc;
            LicznikDokumentow++;
        }

        public Document() //konstruktor tylko w celu serializacji danych
        {

        }

        public override string ToString()
        {
            return Name + " " + Content;
        }
    }
}
