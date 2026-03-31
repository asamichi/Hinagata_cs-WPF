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

namespace Hinagata
{ 
   /// <summary>
   /// Interaction logic for MainWindow.xaml
   /// </summary>
    public partial class MainWindow : Window
    {
        //バインディング用
        CntClass result = new CntClass();

        public MainWindow()
        {
            InitializeComponent();
            // result をバインディングの対象に指定
            CenterPanel.DataContext = result;
        }
    };


    public class CntClass : INotifyPropertyChanged
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
        public ICommand PlusBtnCommand => _plusBtnCommand;
        public ICommand MinusBtnCommand => _minusBtnCommand;

        private readonly EasyCommand _plusBtnCommand;
        private readonly EasyCommand _minusBtnCommand;

        public CntClass()
        {
            _cnt = 0;
            
            //それぞれのEasyCommand 型の中身を具体的に初期化する
            _plusBtnCommand = new EasyCommand(_ => Cnt++);
            _minusBtnCommand = new EasyCommand(_ => Cnt--, _ => Cnt > 0);
        }
        

        //プロパティ変更時に呼び出す関数を指定
        public event PropertyChangedEventHandler? PropertyChanged;

        // 記述簡易化のための関数(本来ならこの中身を毎回 set に書かないといけない)
        private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

            //CanExecute の管理が必要な処理があるなら、関連する変更時に CanExecute の状態を更新する
            if(propertyName == nameof(Cnt))
            {
                _minusBtnCommand.RaiseCanExecuteChanged();
            }
        }
    }




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
}