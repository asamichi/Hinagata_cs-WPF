using Hinagata.Commands;
using Hinagata.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;

namespace Hinagata.ViewModels
{
    internal class CntViewModel : ViewModelBase
    {

        //モデル側のメンバを持つ
        private readonly CntClass _model;


        private readonly EasyCommand _plusBtnCommand;
        private readonly EasyCommand _minusBtnCommand;

        //ICommand プロパティの作成
        public ICommand PlusBtnCommand => _plusBtnCommand;
        public ICommand MinusBtnCommand => _minusBtnCommand;

        //バインディング用のメンバは、モデル側の対応する値を直接参照するようにすればよい。
        public int Cnt => _model.Cnt;

        public CntViewModel()
        {
            _model = new CntClass();

            //それぞれのEasyCommand 型の中身を具体的に初期化する。OnPropertyChanged する必要があるのでこのクラスのメソッドを経由させる必要がある。
            _plusBtnCommand = new EasyCommand(_ => ExecPlus());
            _minusBtnCommand = new EasyCommand(_ => ExecMinus(),_ => _model.CanMinus());
        }

        //モデル側は WPF の話を知らないので、OnPropertyChanged はこちら側で受け持つ必要がある点に注意
        private void ExecPlus()
        {
            _model.Plus();
            OnPropertyChanged(nameof(Cnt));
            _minusBtnCommand.RaiseCanExecuteChanged();
        }

        private void ExecMinus()
        {
            _model.Minus();
            OnPropertyChanged(nameof(Cnt));
            _minusBtnCommand.RaiseCanExecuteChanged();
        }


        //ViewModelBase クラスを継承したので、これは毎回書かなくて良くなった
        ////プロパティ変更時に呼び出す関数を指定
        //public event PropertyChangedEventHandler? PropertyChanged;

        //// 記述簡易化のための関数(本来ならこの中身を毎回 set に書かないといけない)
        //private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        //    //CanExecute の管理が必要な処理があるなら、関連する変更時に CanExecute の状態を更新する
        //    if (propertyName == nameof(Cnt))
        //    {
        //        _minusBtnCommand.RaiseCanExecuteChanged();
        //    }
        //}
    }

}

