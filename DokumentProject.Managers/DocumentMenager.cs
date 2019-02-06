using DocumentProject.DAO;
using DocumentProject.Managers.DTO;
using DocumentProject.Managers.Exceptions;
using DocumentProject.Models.Document;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static DocumentProject.Managers.Exceptions.DocumentExceptions;

namespace DocumentProject.Managers
{
    public class DocumentMenager : IDocumentMenager
    {

        DocumentsDAO dao;

        private static DocumentMenager instance;

        private DocumentMenager()
        {
            dao = DocumentsDAO.GetInstance();
        }

        public static DocumentMenager GetInstance()
        {
            if (instance == null)
                instance = new DocumentMenager();

            return instance;
        }

        public List<DocumentDTO> GetAllDocuments()
        {
            List<DocumentDTO> result = new List<DocumentDTO>();
            var documents = dao.GetAllDocuments();
            foreach (var document in documents)
            {
                var dto = new DocumentDTO()
                {
                    Name = document.Name,
                    Content = document.Content,
                };

                result.Add(dto);
            }

            //List<WorkerDto> result2 = _workers.Select(x => new WorkerDto() { Firstname = x.Firstname, Lastname = x.Lastname }).ToList();
            return result;
        }
        public void AddDocument(DocumentDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name) || string.IsNullOrWhiteSpace(dto.Content))
                throw new ArgumentNullException("Nieprawidłowy zestaw danych");

            var documents = dao.GetAllDocuments();

            if (documents.Any(x => x.Name == dto.Name && x.Content == dto.Content))
                throw new DocumentExceptions.DuplicateDataException("Taki dokument juz istnieje");

            dao.CreateDocument(new Document(dto.Name, dto.Content));
        }

        public void DeleteDocument(DocumentDTO dto)
        {
            Document d = null;

            var documents = dao.GetAllDocuments();
            var documentToDelete = documents.Where(x => x.Name.ToUpper() == dto.Name.ToUpper());

            if (!documentToDelete.Any())
                throw new DocumentExceptions.DocumentNotFoundException("Nie ma takiego dokumentu");

            if (documentToDelete.Count() > 1)
            {
                var documentEntity = documentToDelete.FirstOrDefault(x => x.Content == dto.Content);
                d = documentEntity ?? throw new DocumentExceptions.DocumentNotFoundException("Nie ma takiego pracownika");
            }

            else
            {
                d = documentToDelete.First();
            }

            dao.DeleteDocument(d);
        }



        public void SaveAllData()
        {
            dao.SaveAllChanges();
        }

        public void ModifyDocument(DocumentDTO dto)
        {

            var documents = dao.GetAllDocuments();
            var documentToModify = documents.Where(x => x.Name.ToUpper() == dto.Name.ToUpper()).FirstOrDefault();

            if (documentToModify == null)
                throw new DocumentNotFoundException("brak takiego dokumentu");

            documentToModify.Content = dto.Content;
            var documents3 = dao.GetAllDocuments();
        }
    }
}
