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

namespace Tehtava2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            comboBox.Text = BLLotto.GameName(comboBox.SelectedIndex);
            comboBox.Items.Add(BLLotto.GameName(0));
            comboBox.Items.Add(BLLotto.GameName(1));
            comboBox.Items.Add(BLLotto.GameName(2));
            DrawTextBox.Text = "1";
            comboBox.SelectedIndex = 0;
            DrawsTextBox.Text = "";
        }

        private void Drawbtn_Click(object sender, RoutedEventArgs e) {
            int draws = Convert.ToInt32(DrawTextBox.Text);
            for(int i = 0; i < draws; i++) {
                int[] luvut = BLLotto.DrawNumber(comboBox.SelectedIndex);
                DrawsTextBox.Text = DrawsTextBox.Text + BLLotto.GameName(comboBox.SelectedIndex) + ":";
                int test = 0;
                foreach (int luku in luvut)
                {
                    if (DrawsTextBox.Text == "" || test == 0) DrawsTextBox.Text = DrawsTextBox.Text + " " + luku.ToString();
                    else DrawsTextBox.Text = DrawsTextBox.Text + ", " + luku.ToString();
                    test++;
                }
                DrawsTextBox.Text = DrawsTextBox.Text + ".\n";
            }
        }

        private void Clearbtn_Click(object sender, RoutedEventArgs e) {
            DrawTextBox.Text = "1";
            DrawsTextBox.Text = "";
        }

        private void button2_Click(object sender, RoutedEventArgs e) {
            Close();
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            comboBox.Text = BLLotto.GameName(comboBox.SelectedIndex);
        }

        private void DrawTextBlock_TextInput(object sender, TextCompositionEventArgs e)
        {
            DrawTextBox.Text = DrawTextBox.Text + sender.ToString();
        }
    }
}
