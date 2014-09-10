﻿using System;
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

        void Debug_Event()
        {
            fileIO writer = new fileIO();
            string[] lines = { writer.ExtractFromRichTextBox(richTextEditor) };
            writer.WriteToFile(@"d:\Source.txt", lines);
        }

        string[] Read()
        {
            fileIO reader = new fileIO();
            string[] lines = reader.ReadFromFile(@"d:\Source.txt");
            return lines;

        }
        
        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F5)
            {
                Debug_Event();
            }
        }

        private void Debug_Click(object sender, RoutedEventArgs e)
        {
            Debug_Event();
        }

        private void GenTokSet_Click(object sender, RoutedEventArgs e)
        {
                   string[] lines= Read();
                   List<string> tokenset = new List<string>();
                   Patterns Pattern_object = new Patterns();
                   tokenset = Pattern_object.Pattern_Matching(lines);
                   fileIO writer = new fileIO();
                   writer.WriteToFile(@"d:\tokenset.txt", tokenset.ToArray());
                   
        }

    }
}
