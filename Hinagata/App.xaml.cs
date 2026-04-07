using Hinagata.Commons;
using Hinagata.Models;
using Hinagata.ViewModels;
using Hinagata.Views;
using System.Configuration;
using System.Data;
using System.Windows;

namespace Hinagata
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        protected override void OnStartup(StartupEventArgs e)
        {

            //ストレージ
            JsonStorage<string, int> storage = new JsonStorage<string, int>("Cnt.json", new Dictionary<string, int>() { { "Value", 0 } });

            //共通して利用するモデル
            CntClass model = new CntClass();

            //viewmodel
            var cntViewModel = new CntViewModel(model, storage);
            var evenOddViewModel = new EvenOddViewModel(model);


            //コンテンツコントロールのウィンドウ用

            var contentControlCntViewModel = new ContentControlCntViewModel(cntViewModel, evenOddViewModel);

            Action openCCWindowAction = () =>
            {
                var CCWindow = new ContentControlTest();
                CCWindow.DataContext = contentControlCntViewModel;
                CCWindow.Show();
            };

            //始めに開くウィンドウを作成
            var mainWindow = new MainWindow();

            Action openMVVMWindowAction = () =>
            {
                //window 作成
                var mvvmWindow = new CntWindow();
                mvvmWindow.CntView.SetViewModel(cntViewModel);
                mvvmWindow.EvenOddView.SetViewModel(evenOddViewModel);
                mvvmWindow.Show();
            };

            var mainWindowViewModel = new MainWindowViewModel(openMVVMWindowAction,openCCWindowAction);
            mainWindow.DataContext = mainWindowViewModel;
            mainWindow.Show();



        }
    }

}
