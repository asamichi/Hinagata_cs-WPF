using System;
using System.Collections.Generic;
using System.Text;

namespace Hinagata.ViewModels;

public class ContentControlCntViewModel
{
    public CntViewModel CntViewModel { get; }
    public EvenOddViewModel EvenOddViewModel{ get; }

    //コンストラクタで VM 受け取り
    public ContentControlCntViewModel(CntViewModel cntViewModel, EvenOddViewModel evenOddViewModel)
    {
        CntViewModel = cntViewModel;
        EvenOddViewModel = evenOddViewModel;
    }
}
