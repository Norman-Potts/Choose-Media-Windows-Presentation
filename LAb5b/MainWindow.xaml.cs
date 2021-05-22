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

namespace LAb5b
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        List<string> SummaryItems = new List<string>();

        Program program;
        public MainWindow()
        {
            InitializeComponent();
            /// Colour rectangle...
            program = new Program();
        }
        


        /// <summary>
        /// when list book gets clicked load the book list.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_ListBooks_Click(object sender, RoutedEventArgs e)
        {
            SummaryItems.Clear();

            ListBox_requestedList.Items.Clear();
            string[ , ] booklsit = program.ListBooks();
            for( int u = 0; u < booklsit.GetLength(0); u++ )
            {
                string item = booklsit[u,0];                
                ListBox_requestedList.Items.Add(item);
                string summary = booklsit[u, 1];
                SummaryItems.Add(summary);

            }

        }

        private void btn_ListMovies_Click(object sender, RoutedEventArgs e)
        {
            SummaryItems.Clear();
            ListBox_requestedList.Items.Clear();
            string[,] movielsit = program.ListMovies();
            for (int u = 0; u < movielsit.GetLength(0); u++)
            {
                string item = movielsit[u, 0];
                ListBox_requestedList.Items.Add(item);
                string summary = movielsit[u, 1];
                SummaryItems.Add(summary);
            }

        }

        private void btn_ListSongs_Click(object sender, RoutedEventArgs e)
        {
            SummaryItems.Clear();
            ListBox_requestedList.Items.Clear();
            string[,] songList = program.ListSongs();
            for (int u = 0; u < songList.GetLength(0); u++)
            {
                string item = songList[u, 0];
                ListBox_requestedList.Items.Add(item);
                string summary = songList[u, 1];
                SummaryItems.Add(summary);
            }
        }

        private void btn_ListAll_Click(object sender, RoutedEventArgs e)
        {
            SummaryItems.Clear();
            ListBox_requestedList.Items.Clear();
            string[,] all = program.ListAll();
            for (int u = 0; u < all.GetLength(0); u++)
            {
                string item = all[u, 0];
                ListBox_requestedList.Items.Add(item);
                string summary = all[u, 1];
                SummaryItems.Add(summary);
            }

        }

        private void btn_TitleSearch_Click(object sender, RoutedEventArgs e)
        {
            string searchTerm = "";
            searchTerm = txtBox_Search.Text;

            string[,] results = program.Search(searchTerm);
            for (int u = 0; u < results.GetLength(0); u++)
            {
                string item = results[u, 0];
                ListBox_requestedList.Items.Add(item);
                string summary = results[u, 1];
                SummaryItems.Add(summary);
            }




        }

        private void btn_Clear_Click(object sender, RoutedEventArgs e)
        {
            txtBox_Summary.Clear();
            ListBox_requestedList.Items.Clear();
            SummaryItems.Clear();
        }

        private void btn_Exit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }


        private void ListBox_requestedList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txtBox_Summary.Clear();
            
            if (ListBox_requestedList.Items.Count > 0)
            {
                int i = ListBox_requestedList.SelectedIndex;
                string summary = SummaryItems[i];
                txtBox_Summary.AppendText(summary);
            }

        }

        

    }
}
