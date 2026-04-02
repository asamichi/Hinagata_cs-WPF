using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Windows.Input;
using Hinagata.Views;
using Hinagata.Commands;

namespace Hinagata.ViewModels
{

    public class MainWindowViewModel
    {
        public ICommand OpenMVVMWindow => new EasyCommand(_ =>
        {
            //ウィンドウのインスタンス作成
            var mvvmWindow = new CntWindow();

            //サブ画面用の viewmodwlをセット
            //MVVMWindow.DataContext = new SubViewModel();

            //表示
            mvvmWindow.Show();
        });
    }
}
