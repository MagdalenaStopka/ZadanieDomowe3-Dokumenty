using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DocumentProject.Managers;
using DocumentProject.Managers.DTO;
using DocumentProject.Managers.Exceptions;

namespace DocumentProjectWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public IDocumentMenager manager;

        public MainWindow()
        {
            InitializeComponent();
            WyswietlPracownikowBTN.Content += " :) ";
            manager = DocumentMenager.GetInstance();

        }

        private void WyswietlPracownikowBTN_Click(object sender, RoutedEventArgs e)
        {
            var listaDokumentow = new List<DocumentDTO>();
            listaDokumentow = manager.GetAllDocuments();

            string wynik = string.Empty;
            foreach (var d in listaDokumentow)
            {
                wynik += string.Format("Nazwa: {0}, Treść: {1}", d.Name, d.Content);
            }

            MessageBox.Show(wynik);
        }



        private void DodajDokumentBtn_Click(object sender, RoutedEventArgs e)
        {

            string nazwa = NazwaDokumentuTxtBox.Text;
            string tresc = trescDokumentuTxtBox.Text;

            try
            {
                manager.AddDocument(new DocumentDTO() { Name = nazwa, Content = tresc });
            }

            catch (ArgumentNullException ex)
            {
                MessageBox.Show(string.Format("Nie podano danych! Oryginalna treść złapanego wyjątku:\n {0}", ex.Message));
            }
            catch (DocumentExceptions.DuplicateDataException ex)
            {
                MessageBox.Show(ex.Message);
            }

            finally
            {
                NazwaDokumentuTxtBox.Text = string.Empty;
                trescDokumentuTxtBox.Text = string.Empty;
            }

            OdswiezListeDokumentow();
        }

        private void OdswiezListeBTN_Click(object sender, RoutedEventArgs e)
        {
            OdswiezListeDokumentow();
        }

        private void OdswiezListeDokumentow()
        {
            ListaPracownikowListView.Items.Clear();
            var listaDokumentow = new List<DocumentDTO>();
            listaDokumentow = manager.GetAllDocuments();

            foreach (var dokumentDto in listaDokumentow)
                ListaPracownikowListView.Items.Add(dokumentDto);
        }

        private void EdytujWybranego_Click(object sender, RoutedEventArgs e)
        {
            ZmianaWybranego();
        }

        private void ZapiszBTN_Click(object sender, RoutedEventArgs e)
        {
            var nazwa = EdytowanyNazwa.Text;
            var tresc = EdytowanyTresc.Text;


            var dto = new DocumentDTO() { Name = nazwa, Content = tresc };
            manager.ModifyDocument(dto);
            EdytowanyNazwa.Text = string.Empty;
            EdytowanyTresc.Text = string.Empty;
        }

        private void ListaPracownikowListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //ZmianaWybranego();
            //UsuniecieWybranego();
        }


        private void ZmianaWybranego()
        {
            var selected = ListaPracownikowListView.SelectedItem;
            if (selected == null) return;
            var wybranyDokument = selected as DocumentDTO;
            EdytowanyNazwa.Text = wybranyDokument.Name;
            EdytowanyTresc.Text = wybranyDokument.Content;
        }

        private void UsuniecieWybranego()
        {
            var selected = ListaPracownikowListView.SelectedItem;
            if (selected == null) return;
            var wybranyDokument = selected as DocumentDTO;
            UsuwanyNazwa.Text = wybranyDokument.Name;
            UsuwanyTresc.Text = wybranyDokument.Content;
        }
        private void UsunDokumentBTN_Click(object sender, RoutedEventArgs e)
        {
            {
                UsuniecieWybranego();
            }


        }
        private void ZapiszDokumentBTN_Click(object sender, RoutedEventArgs e)
        {

            var nazwa = UsuwanyNazwa.Text;
            var tresc = UsuwanyTresc.Text;


            var dto = new DocumentDTO() { Name = nazwa, Content = tresc };
            try
            {
                manager.DeleteDocument(dto);


            }
            catch (DocumentExceptions.DocumentNotFoundException exception)
            {
                MessageBox.Show(exception.Message);
            }
            finally
            {
                UsuwanyNazwa.Text = string.Empty;
                UsuwanyTresc.Text = string.Empty;
            }
        }



        private void UsuwanyNazwa_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void UsuwanyTresc_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ZapiszWszystkieZmianyBTN_Click(object sender, RoutedEventArgs e)
        {
            manager.SaveAllData();

        }
    }
}