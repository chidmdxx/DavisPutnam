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
using DavisPutnam.Model;

namespace DavisPutnam
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Clause> delta;
        List<Clause> result;
        public MainWindow()
        {
            InitializeComponent();
            delta = new List<Clause>();
            result = new List<Clause>();
        }

        private void transformButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void addClauseButton_Click(object sender, RoutedEventArgs e)
        {
            var list = inputBox.Text.Split(' ');
            var temp = new Clause();
            foreach (var s in list)
            {
                temp.AddElement(s);
            }
            delta.Add(temp);
            clauseBox.Text += string.Format("{0}{1}", Environment.NewLine, temp);
        }

        private void solveDavisPutnamButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void solveLSSButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void clearButton_Click(object sender, RoutedEventArgs e)
        {
            delta.Clear();
            result.Clear();
            clauseBox.Text = "";
            resultBox.Text = "";
        }
    }
}
