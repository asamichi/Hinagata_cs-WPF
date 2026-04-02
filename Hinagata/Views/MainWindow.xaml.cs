using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Hinagata.ViewModels;

namespace Hinagata.Views
{ 
   /// <summary>
   /// Interaction logic for MainWindow.xaml
   /// </summary>
    public partial class MainWindow : Window
    {
        //バインディング用
        OldCntClass result = new OldCntClass();

        public MainWindow()
        {
            InitializeComponent();
            // result をバインディングの対象に指定
            CenterPanel.DataContext = result;

            NewWindowBtn.DataContext = new MainWindowViewModel();
        }
    };

}