using System;
using System.Collections.Generic;
using System.Text;

namespace WithCommunityToolkit.ViewModels;


public class CntWindowViewModel
{
    public CntViewModel CntViewModel { get; }
    public EvenOddViewModel EvenOddViewModel { get; }

    public CntWindowViewModel(CntViewModel cntViewModel, EvenOddViewModel evenOddViewModel)
    {
        CntViewModel = cntViewModel;
        EvenOddViewModel = evenOddViewModel;
    }
}
