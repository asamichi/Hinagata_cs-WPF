using Hinagata.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;

namespace Hinagata
{
    //MVVM に沿っていない場合のクラス。実際には使用しないが学習メモとして残す。
    public class OldCntClass : INotifyPropertyChanged
    {

        private int _cnt;

        public int Cnt
        {
            get => _cnt;
            set
            {
                //変更が無い場合にはパフォーマンスを考慮して変更処理を実施しない。データバインディングの場合には通常より set 負荷が高いため。
                if (_cnt == value)
                {
                    return;
                }
                _cnt = value;
                //プロパティの変更をUI側に通知
                OnPropertyChanged();
            }
        }

        //ICommand プロパティの作成
        public ICommand DoublePlusBtnCommand => _plusBtnCommand;
        public ICommand DoubleMinusBtnCommand => _minusBtnCommand;

        private readonly EasyCommand _plusBtnCommand;
        private readonly EasyCommand _minusBtnCommand;

        public OldCntClass()
        {
            _cnt = 0;

            //それぞれのEasyCommand 型の中身を具体的に初期化する
            _plusBtnCommand = new EasyCommand(_ => Cnt += 2);
            _minusBtnCommand = new EasyCommand(_ => Cnt -= 2, _ => Cnt > 1);
        }


        //プロパティ変更時に呼び出す関数を指定
        public event PropertyChangedEventHandler? PropertyChanged;

        // 記述簡易化のための関数(本来ならこの中身を毎回 set に書かないといけない)
        private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

            //CanExecute の管理が必要な処理があるなら、関連する変更時に CanExecute の状態を更新する
            if (propertyName == nameof(Cnt))
            {
                _minusBtnCommand.RaiseCanExecuteChanged();
            }
        }
    }
}
