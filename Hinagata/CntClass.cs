using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;

namespace Hinagata
{

    public class CntClass
    {
        public int Cnt { get; private set; }

        public CntClass()
        {
            Cnt = 0;
        }

        public void Plus()
        {
            Cnt++;
        }

        public void Minus()
        {
            Cnt--;
        }

        //CanExecuteChangedのため
        public bool CanMinus()
        {
            return Cnt > 0;
        }
       
    }




}
