using DocumentProject.Managers;
using DocumentProject.Managers.DTO;
using DocumentProject.Managers.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZadanieDomowe3_Dokumenty
{
    class Program
    {
        static IDocumentMenager manager = new DocumentMenager();

        static void Main(string[] args)
        {
            string wybor = "0";

            while (true)
            {
                WyswietlMenu();
                wybor = Console.ReadLine();
                switch (wybor)
                {
                    case "1": WyswietlDokumenty(); break;
                    case "2": DodajDokument(); break;
                    case "4": ModyfikujDokument(); break;
                    case "3": UsunDokument(); break;
                    case "5": ZapiszWszystkieDane(); break;
                    case "Q": return;
                    default: break;
                }
            }
        }

        private static void ZapiszWszystkieDane()
        {
            manager.SaveAllData();
        }

        

        private static void WyswietlMenu()
        {
            Console.WriteLine("\n\n --------------- \n");
            string wyswietl = "Wpisz: \n1: Wyswietl dokumenty \n"
                + "2. Dodaj dokument \n"
                + "3.Usun dokument \n"
                + "4.Modyfikuj dokument \n"
                + "5.Zapisz zmiany do pliku"
                + "\n\n\nQ- wyjdz z programu";
            Console.WriteLine(wyswietl);
        }

        private static void UsunDokument()
        {
            Console.WriteLine("Podaj nzwę dokumentu do usninięcia:");
            string nazwa = Console.ReadLine();

            Console.WriteLine("Podaj treść dokumentu do usninięcia:");
            string tresc = Console.ReadLine();

            DocumentDTO dto = new DocumentDTO();
            dto.Name = nazwa;
            dto.Content = tresc;

            try
            {
                manager.DeleteDocument(dto);

            }
            catch (DocumentExceptions.DocumentNotFoundException exception)
            {
                Console.WriteLine(exception);
            }



        }
        private static void ModyfikujDokument()
        {
            Console.WriteLine("Podaj nzwę dokumentu DO modyfikacji:");
            string nazwa = Console.ReadLine();
           
            Console.WriteLine("Podaj treść dokumentu PO modyfikacji:");
            string tresc = Console.ReadLine();

            DocumentDTO dto = new DocumentDTO();
            dto.Name = nazwa;
            dto.Content = tresc;
            manager.ModifyDocument(dto);

            
            //manager.DeleteDocument(dto);

           // manager.AddDocument(new DocumentDTO() { Name = nazwa, Content = tresc });



        }

        private static void DodajDokument()
        {
            Console.WriteLine("Podaj nazwę");
            string nazwa = Console.ReadLine();

            Console.WriteLine("Podaj treść");
            string tresc = Console.ReadLine();

            try
            {
                manager.AddDocument(new DocumentDTO() { Name = nazwa, Content = tresc });
            }

            catch (ArgumentNullException ex)
            {
                Console.WriteLine("Nie podano danych! Oryginalna treść złapanego wyjątku:\n {0}", ex.Message);
            }
            catch (DocumentExceptions.DuplicateDataException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void WyswietlDokumenty()
        {
            var documentsList = manager.GetAllDocuments();

            foreach (var dto in documentsList)
            {
                Console.WriteLine("Nazwa: {0} Treść: {1}", dto.Name, dto.Content);
            }
        }

    }
}
