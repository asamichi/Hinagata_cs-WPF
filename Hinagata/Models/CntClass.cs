using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;

namespace Hinagata.Models
{

    public class CntClass
    {
        //値変更時のイベント
        public event Action? CntChanged;
        public int Cnt { get; private set; }

        public CntClass(int initValue = 0)
        {
            ChangeCnt(initValue);
        }

        public void Plus()
        {
            ChangeCnt(Cnt + 1);
        }

        public void Minus()
        {
            ChangeCnt(Cnt - 1);
        }


        //毎回2行書くことの防止と、初期化以外で値をセットするため
        public void ChangeCnt(int value)
        {
            Cnt = value;
            CntChanged?.Invoke();
        }

        //CanExecuteChangedのため
        public bool CanMinus()
        {
            return Cnt > 0;
        }
       
    }




}
