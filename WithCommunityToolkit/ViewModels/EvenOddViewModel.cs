using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Text;
using WithCommunityToolkit.Models;

namespace WithCommunityToolkit.ViewModels;

public partial class EvenOddViewModel : ObservableObject
{
    private readonly CntClass _model;
    public string EvenOddText => IsEvenOdd(_model.Cnt);

    public EvenOddViewModel(CntClass model)
    {
        _model = model;
        //変更時イベント登録
        _model.CntChanged += OnCntChanged;
    }

    //偶奇判定
    public string IsEvenOdd(int value)
    {
        if (_model.Cnt % 2 == 0)
        {
            return "偶数";
        }
        return "奇数";
    }


    //変更時の通知
    private void OnCntChanged()
    {
        OnPropertyChanged(nameof(EvenOddText));
    }
}
