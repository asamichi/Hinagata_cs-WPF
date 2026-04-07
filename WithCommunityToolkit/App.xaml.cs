using System.Configuration;
using System.Data;
using System.Windows;

using WithCommunityToolkit.Commons;
using WithCommunityToolkit.Views;
using WithCommunityToolkit.ViewModels;
using WithCommunityToolkit.Models;


namespace WithCommunityToolkit;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    { 
        //ストレージ
        JsonStorage<string, int> storage = new JsonStorage<string, int>("Cnt.json", new Dictionary<string, int>() { { "Value", 0 } });

        CntClass cntModel = new CntClass();

        //vm
        var cntViewModel = new CntViewModel(cntModel,storage);
        var evenOddViewModel = new EvenOddViewModel(cntModel);
        var cntWindowViewModel = new CntWindowViewModel(cntViewModel, evenOddViewModel);

        //初期起動するウィンドウ
        //var mainWindow = new MainWindow();
        //mainWindow.DataContext = cntViewModel;
        //mainWindow.Show();

        var cntWindow = new CntWindow();
        cntWindow.DataContext = cntWindowViewModel;
        cntWindow.Show();

    }

}