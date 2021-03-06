﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace MakeOthello.ViewModel.MessageBox
{
    class LoseMessageBoxViewModel:EndMessageBoxViewModel
    {
        public LoseMessageBoxViewModel(int player, int cpu) : base(player, cpu, "Failed")
        {
        }

        public override SolidColorBrush Background
        {
            get
            {
                return Application.Current.Resources["ThemeColorPurple"] as SolidColorBrush;
            }
        }
    }
}
