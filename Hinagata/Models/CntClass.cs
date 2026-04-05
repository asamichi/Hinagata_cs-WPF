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
        public int Cnt { get; private set; }

        public CntClass(int initValue = 0)
        {
            Cnt = initValue;
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
