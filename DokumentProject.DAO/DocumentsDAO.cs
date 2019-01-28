using DocumentProject.Models.Document;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace DocumentProject.DAO
{
   public class DocumentsDAO
    {
        private const string fileName = "documents.xml";
        private List<Document> _documents;

        public DocumentsDAO()
        {

            Initialize();
            SaveDataToFile(_documents);

            _documents = ReadDataFromFile();
        }

        public void SaveAllChanges()
        {
            SaveDataToFile(_documents);
            _documents = ReadDataFromFile();
        }

        public List<Document> GetAllDocuments()
        {
            if (_documents == null || !_documents.Any())
                Initialize();
            return _documents;
        }

        public void CreateDocument(Document document)
        {
            _documents.Add(document);
        }

        public void DeleteDocument(Document document)
        {
            _documents.Remove(document);
        }
        public void ModifyDocument(Document document)
        {
            _documents.(document);
        }


        private void Initialize()
        {
            var d1 = new Document("Buty", "Musisz koniecznie kupić buty");
            var d2 = new Document("Smieci", "Wynies śmieci");
            _documents = new List<Document>() { d1, d2 };
        }

        private void SaveDataToFile(List<Document> data)
        {
            var serializer = new XmlSerializer(typeof(List<Document>));
            using (var fileStream = new FileStream(fileName, FileMode.Create))
            {
                serializer.Serialize(fileStream, data);
            }
        }

        public List<Document> ReadDataFromFile()
        {
            var serializer = new XmlSerializer(typeof(List<Document>));
            using (var fileStream = new FileStream(fileName, FileMode.Open))
            {
                var data = serializer.Deserialize(fileStream);
                var documents = data as List<Document>;
                return documents;
            }
        }

    }
}
