using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using Microsoft.Win32;

namespace FilterTextApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string _path;
        public List<string> Text = new List<string>();
        private StringWorker _stringWorker;
      
        public MainWindow()
        {
            InitializeComponent();
            _stringWorker = new StringWorker(this);
        }
        
        private async void OpenFile(object sender, RoutedEventArgs e) 
        {
            OpenFileDialog dialog = new OpenFileDialog() { Filter = ".txt|*.txt" };
            if (dialog.ShowDialog() == true)
            {
                    richTextBox.Document.Blocks.Clear();
                    _path = dialog.FileName;
                await ReadFile();
                Text = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd).Text.Split('\n').ToList();
            }
        }

        private async Task ReadFile()
        {
            using (FileStream fs = new FileStream(_path, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                using (StreamReader sr = new StreamReader(fs, Encoding.GetEncoding(1251)))
                {
                    richTextBox.AppendText(sr.ReadToEnd());
                }
            }
        }

        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {
            _stringWorker.FilterStrings();
        }
        
        private void TextBox1_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (textBox1.Text.Length != 0)
            {
                textBox2.IsEnabled = true;
                filterBox2.IsEnabled = true;
            }
            else
            {
                textBox2.Clear();
                textBox2.IsEnabled = false;
                filterBox2.IsEnabled = false;
            }
        }

        private void TextBox2_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (textBox2.Text.Length != 0)
            {
                textBox3.IsEnabled = true;
                filterBox3.IsEnabled = true;
            }
            else
            {
                textBox3.Clear();
                textBox3.IsEnabled = false;
                filterBox3.IsEnabled = false;
            }
        }

        private void TextBox3_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (textBox3.Text.Length != 0)
            {
                textBox4.IsEnabled = true;
                filterBox4.IsEnabled = true;
            }
            else
            {
                textBox4.Clear();
                textBox4.IsEnabled = false;
                filterBox4.IsEnabled = false;
            }
        }
        public void RichTextBox_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] fileName = (string[])e.Data.GetData(DataFormats.FileDrop);
                richTextBox.Document.Blocks.Clear();
                richTextBox.AppendText(File.ReadAllText(fileName[0], Encoding.Default));
            }
        }

        private void RichTextBox_OnPreviewDragOver(object sender, DragEventArgs e)
        {
            e.Handled = true;
            e.Effects = DragDropEffects.All;
            e.Data.GetData(DataFormats.FileDrop)?.ToString();
        }
    }
}
    

