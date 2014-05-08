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
        List<Clause> Delta { get; set; }
        List<Clause> Result { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            Delta = new List<Clause>();
            Result = new List<Clause>();
            inputBox.Focus();
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
            Delta.Add(temp);
            clauseBox.Text += string.Format("{0}{1}", temp, Environment.NewLine);
            inputBox.Text = "";
            inputBox.Focus();
        }

        private void solveDavisPutnamButton_Click(object sender, RoutedEventArgs e)
        {
            resultBox.Text = "";
            Result.Clear();
            var solution = new Solution();
            var satisfasible = solution.dp(Delta);
            var builder = new StringBuilder();
            Result = solution.Delta;
            builder.AppendFormat("Satisfasible: {0}{1}", !satisfasible, Environment.NewLine);
            builder.AppendFormat("Time in milliseconds: {0}{1}", solution.Time, Environment.NewLine);
            builder.AppendFormat("Steps: {0}{1}", solution.Steps, Environment.NewLine);
            builder.AppendLine("Delta");
            foreach (var c in Result)
            {
                builder.AppendLine(c.ToString());
            }
            resultBox.Text = builder.ToString();
        }

        private void solveLSSButton_Click(object sender, RoutedEventArgs e)
        {
            resultBox.Text = "";
            Result.Clear();
            var solution = new Solution();
            var satisfasible = solution.lsm(Delta);
            var builder = new StringBuilder();
            Result = solution.Delta;
            builder.AppendFormat("Satisfasible: {0}{1}", !satisfasible, Environment.NewLine);
            builder.AppendFormat("Time in milliseconds: {0}{1}", solution.Time, Environment.NewLine);
            builder.AppendFormat("Steps: {0}{1}", solution.Steps, Environment.NewLine);
            builder.AppendLine("Delta");
            foreach (var c in Result)
            {
                builder.AppendLine(c.ToString());
            }
            resultBox.Text = builder.ToString();
        }

        private void clearButton_Click(object sender, RoutedEventArgs e)
        {
            Delta.Clear();
            Result.Clear();
            clauseBox.Text = "";
            resultBox.Text = "";
        }
    }
}
