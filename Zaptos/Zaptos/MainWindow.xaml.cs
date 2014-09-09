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
using Zaptos;

namespace Zaptos
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            fileIO writer = new fileIO();
            string[] lines = {writer.ExtractFromRichTextBox(richTextEditor)};
            writer.WriteToFile(@"d:\temp.txt", lines);

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            fileIO reader = new fileIO();
            string[] lines = reader.ReadFromFile(@"d:\temp.txt");
            reader.WriteToFile(@"d:\temp2.txt", lines);
        }

        private void Debug_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F5)
            {
           //     Button_Click();
            }
        }
    }
}
