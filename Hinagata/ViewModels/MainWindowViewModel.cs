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
        private readonly Action _openMVVMWindowAction;
        private readonly Action _openCCWindowAction;

        //コンストラクタで必要な情報を受け取る
        public MainWindowViewModel(Action openMVVMWindowAction, Action openCCWindowAction)
        {
            _openMVVMWindowAction = openMVVMWindowAction;
            _openCCWindowAction = openCCWindowAction;
        }

        
        public ICommand OpenMVVMWindow => new EasyCommand(_ =>
        {
            _openMVVMWindowAction?.Invoke();
            ////ウィンドウのインスタンス作成
            //var mvvmWindow = new CntWindow();

            ////サブ画面用の viewmodwlをセット
            ////MVVMWindow.DataContext = new SubViewModel();

            ////表示
            //mvvmWindow.Show();
        });

        public ICommand OpenCCWindow => new EasyCommand(_ =>
        {
            _openCCWindowAction?.Invoke();
        });
    }
}
