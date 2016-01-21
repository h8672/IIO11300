/*
* Copyright (C) JAMK/IT/Esa Salmikangas
* This file is part of the IIO11300 course project.
* Created: 12.1.2016 Modified: 21.1.2016
* Authors: Juha-Matti Kokkonen ,Esa Salmikangas
*/
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

namespace Tehtava1
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

        private void btnCalculate_Click(object sender, RoutedEventArgs e)
        {
            //TODO
            try
            {
                double result;
                double windowheight = Convert.ToDouble(txtHeight.Text.ToString());
                double windowwidth = Convert.ToDouble(txtWidht.Text.ToString());
                double karmi = Convert.ToDouble(txtKarmi.Text.ToString());
                //Laitetaan koot suorakulmiolle
                myRectangle.Height = windowheight;
                myRectangle.Width = windowwidth;
                myRectangle.StrokeThickness = karmi;
                //Ikkunan pinta-ala
                result = BusinessLogicWindow.CalculatePerimeter(windowheight, windowwidth);
                txtSqrPinta.Text = result.ToString();

                //Karmin piiri...
                result = BusinessLogicWindow.Calculate(windowwidth, windowheight, karmi);
                txtKarmiPiiri.Text = result.ToString();

                //Karmin pinta-ala...
                result = BusinessLogicWindow.CalculatePerimeter2(windowwidth, windowheight, karmi);
                txtKarmiPinta.Text = result.ToString();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //yield to an user that everything okay
            }
        }

    private void btnClose_Click(object sender, RoutedEventArgs e)
    {
      Application.Current.Shutdown();
    }
  }

 
}
