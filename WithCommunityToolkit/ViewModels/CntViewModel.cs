using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using WithCommunityToolkit.Commons;
using WithCommunityToolkit.Models;

namespace WithCommunityToolkit.ViewModels;


//ObservableObject を継承しつつ、partial キーワードも必須。
public partial class CntViewModel : ObservableObject
{

    //モデル側のメンバを持つ
    private readonly CntClass _model;
    JsonStorage<string, int> _cntStorage;

    public CntViewModel(CntClass model, JsonStorage<string, int> storage)
    {
        //値の保存用のクラスと、既に保存された内容の読み込み
        _cntStorage = storage;
        Dictionary<string, int> saveData = _cntStorage.LoadJson();

        //カウンターモデルの設定と値の読み込み
        _model = model;

        //変更時のイベントを登録。値のセットより先に購読しないと、初期値が読まれないので注意！
        _model.CntChanged += OnCntChanged;

        _model.ChangeCnt(saveData["Value"]);


    }


    [ObservableProperty] //先頭小文字のフィールドを宣言すると、対応する先頭が大文字版のプロパティを自動作成
    [NotifyCanExecuteChangedFor(nameof(ExecMinusCommand))] //当該プロパティ変更時、自動的に ExecMinusCommand の CanExecute の評価が実施。ExecMinusCommand は直接宣言する必要はなく、[対応するメソッド]Coomand が自動生成される
    private int cnt;

    [RelayCommand]//この属性をメソッドにつけると、[メソッド名]Command が自動生成される。ICommand が必要なくなる
    private void ExecPlus()
    {
        _model.Plus();

        //保存
        _cntStorage.SaveJson(new Dictionary<string, int>() { { "Value", Cnt } });
    }

    [RelayCommand(CanExecute = nameof(CanMinus))]//CanExecute で使用するメソッド名を指定
    private void ExecMinus()
    {
        _model.Minus();

        //保存
        _cntStorage.SaveJson(new Dictionary<string, int>() { { "Value", Cnt } });

    }

    private bool CanMinus() => _model.CanMinus(); 

    private void OnCntChanged()
    {
        Cnt = _model.Cnt;
    }


    /*
     [NotifyPropertyChangedFor]
あるプロパティ変更時に別プロパティの変更通知を出す。
     
    [RelayCommand] は　async Task に付けると自動的に非同期のコマンドにしてくれる
     */
}

