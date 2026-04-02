using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;

namespace Hinagata.Commands
{
    public class EasyCommand : ICommand
    {
        // 実行する処理のデリゲート
        private readonly Action<object?> _execute;
        // 実行可能な状態かを返すデリゲート。いつでも実行できる想定なら null で良い。
        private readonly Predicate<object?>? _canExecute;

        public EasyCommand(Action<object?> execute, Predicate<object?>? canExecute = null)
        {
            //execute が null ならエラーを投げる
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }


        //ICommand の実装
        //CanExecute を実行すると、_canExecute(parameter)実行を試みる。null の場合は常に true を返す
        public bool CanExecute(object? parameter) => _canExecute?.Invoke(parameter) ?? true;
        public void Execute(object? parameter) => _execute(parameter);

        public event EventHandler? CanExecuteChanged;


        //CanExecuteChangedがあれば CanExecuteChanged(this, EventArgs.Empty) を実行。this はインスタンス自分自身、EventArgs.Empty は渡したいものはないけど型をあわせるために空を渡す
        public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);


        //下記のように書けば、何かイベントが起こるたびにすべての要素について CanExecuteChanged を確認してくれる。が、計算量は多くなる。
        //public event EventHandler? CanExecuteChanged
        //{
        //    add { CommandManager.RequerySuggested += value; }
        //    remove { CommandManager.RequerySuggested -= value; }
        //}

    }


    //基底クラス、継承して使うことを前提としたクラスなので abstract を付ける
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        // 記述簡易化のための関数(本来ならこの中身を毎回 set に書かないといけない)
        public void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
