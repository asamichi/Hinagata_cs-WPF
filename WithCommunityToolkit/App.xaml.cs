using System.Configuration;
using System.Data;
using System.Windows;

using WithCommunityToolkit.Commons;
using WithCommunityToolkit.Views;
using WithCommunityToolkit.ViewModels;
using WithCommunityToolkit.Models;

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;


namespace WithCommunityToolkit;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {

        HostApplicationBuilder builder = Host.CreateApplicationBuilder();

        //ストレージ登録
        builder.Services.AddTransient<JsonStorage<string,int>>(sp =>
        {
            return new JsonStorage<string, int>("Cnt.json", new Dictionary<string, int>() { { "Value", 0 } });
        });

        //モデル登録
        builder.Services.AddTransient<CntClass>();

        //vm 登録
        builder.Services.AddTransient<CntViewModel>();
        builder.Services.AddTransient<EvenOddViewModel>();
        builder.Services.AddTransient<CntWindowViewModel>();

        IHost host = builder.Build();

        var cntWindow = new CntWindow();
        cntWindow.DataContext = host.Services.GetRequiredService<CntWindowViewModel>();
        cntWindow.Show();


        ////ストレージ
        //JsonStorage<string, int> storage = new JsonStorage<string, int>("Cnt.json", new Dictionary<string, int>() { { "Value", 0 } });

        //CntClass cntModel = new CntClass();

        ////vm
        //var cntViewModel = new CntViewModel(cntModel,storage);
        //var evenOddViewModel = new EvenOddViewModel(cntModel);
        //var cntWindowViewModel = new CntWindowViewModel(cntViewModel, evenOddViewModel);

        ////初期起動するウィンドウ
        ////var mainWindow = new MainWindow();
        ////mainWindow.DataContext = cntViewModel;
        ////mainWindow.Show();

        //var cntWindow = new CntWindow();
        //cntWindow.DataContext = cntWindowViewModel;
        //cntWindow.Show();

    }

}