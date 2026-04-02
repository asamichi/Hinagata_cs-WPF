using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Hinagata.ViewModels;

namespace Hinagata.Views
{
    /// <summary>
    /// CntWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class CntWindow : Window
    {
        public CntWindow()
        {
            InitializeComponent();
            CenterPanel.DataContext = new CntViewModel();
        }
    }
}
