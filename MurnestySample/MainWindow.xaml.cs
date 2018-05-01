﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MurnestySample.View;

namespace MurnestySample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // open sample view
            Sample1 s1 = new Sample1();
            s1.Show();
            this.Hide();

            //Window window = null;
            //using (FileStream fs = new FileStream(@"..\..\View\Sample1.xaml", FileMode.Open, FileAccess.Read))
            //{
            //    // Get the root element, which we know is a Window
            //    window = (Window)XamlReader.Load(fs);
            //    StackPanel panel = (StackPanel) window.Content;
            //    Button1 = (Button) panel.Children[1];
            //}
        }
    }
}
