
using DocumentProject.Managers.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentProject.Managers
{
  public  interface IDocumentMenager
    {
        void DeleteDocument(DocumentDTO dto);
        void AddDocument(DocumentDTO dto);
        List<DocumentDTO> GetAllDocuments();
        void SaveAllData();
        void ModifyDocument(DocumentDTO dto);
    }
}
