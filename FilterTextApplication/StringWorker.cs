using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilterTextApplication
{
    public class StringWorker
    {
        private MainWindow _mainWindow;
        public StringWorker(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
        }
        private int NumberOfFiltres()
        {
            var i = 1;
            if (_mainWindow.filterBox2.SelectedIndex == 1)
                i++;
            if (_mainWindow.filterBox3.SelectedIndex == 1)
                i++;
            if (_mainWindow.filterBox4.SelectedIndex == 1)
                i++;
            return i;
        }

        public void FilterStrings()
        {
            var i = 0;
            _mainWindow.richTextBox.Document.Blocks.Clear();
            var numberOfFilters = NumberOfFiltres();
            List<string>[] lines = new List<string>[numberOfFilters];
            for (int j = 0; j < numberOfFilters; ++j)
                lines[j] = new List<string>();

            lines[i].Add(_mainWindow.textBox1.Text);
            if (_mainWindow.filterBox2.SelectedIndex == 0)
                lines[i].Add(_mainWindow.textBox2.Text);
            else if (_mainWindow.filterBox2.SelectedIndex == 1)
            {
                i++;
                lines[i].Add(_mainWindow.textBox2.Text);
            }

            if (_mainWindow.filterBox3.SelectedIndex == 0)
                lines[i].Add(_mainWindow.textBox3.Text);
            else if (_mainWindow.filterBox3.SelectedIndex == 1)
            {
                i++;
                lines[i].Add(_mainWindow.textBox3.Text);
            }

            if (_mainWindow.filterBox4.SelectedIndex == 0)
                lines[i].Add(_mainWindow.textBox4.Text);
            else if (_mainWindow.filterBox4.SelectedIndex == 1)
            {
                i++;
                lines[i].Add(_mainWindow.textBox4.Text);
            }

            foreach (var line in lines)
            {
                foreach (var str in _mainWindow.Text)
                {
                    bool accept = true;
                    foreach (var word in line)
                    {
                        accept = str.ToLower().Contains(word.ToLower()) && accept;
                    }
                    if (accept)
                    {
                        _mainWindow.richTextBox.AppendText(str);
                    }
                }
            }
        }
    }
}
