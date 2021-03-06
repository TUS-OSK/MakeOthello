﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI;
using Windows.UI.Xaml.Media;
using MakeOthello.Utility;
using MakeOthello.View;
using MakeOthello.View.Controls;

namespace MakeOthello.ViewModel
{
    public class DiscViewModel : ViewModelBase
    {
        private SolidColorBrush _discColorBrush;
        private double _discOpacity;
        private DiscCondition _discCondition = DiscCondition.Void;

        public int Number;

        public double Height { get; set; }
        public double Width { get; set; }

        public DiscViewModel(int number, double size)
        {
            Height = size;
            Width = size;
            Number = number;
        }

        public ICommand DiscTapedCommand { get; set; }

        public DiscCondition DiscCondition
        {
            get { return _discCondition; }
            set
            {
                _discCondition = value;
                switch (value)
                {
                    case DiscCondition.Black:

                        DiscColorBrush = new SolidColorBrush(Color.FromArgb(255, 0, 50, 0));

                        DiscColorBrush = new SolidColorBrush(Colors.Black);

                        DiscOpacity = 1;
                        break;
                    case DiscCondition.White:

                        DiscColorBrush = new SolidColorBrush(Colors.White);
                        DiscOpacity = 1;
                        break;
                    case DiscCondition.Void:
                        DiscColorBrush = new SolidColorBrush(Colors.Transparent);
                        DiscOpacity = 1;
                        break;
                    case DiscCondition.AbleBlack:
                        DiscColorBrush = new SolidColorBrush(Colors.Black);
                        DiscOpacity = 0.15;
                        break;
                    case DiscCondition.AbleWhite:
                        DiscColorBrush = new SolidColorBrush(Colors.White);
                        DiscOpacity = 0.15;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(value), value, null);
                }
            }
        }

        public SolidColorBrush DiscColorBrush
        {
            get { return _discColorBrush; }
            private set
            {
                _discColorBrush = value;
                OnPropertyChanged();
            }
        }

        public double DiscOpacity
        {
            get { return _discOpacity; }
            private set
            {
                _discOpacity = value;
                OnPropertyChanged();
            }
        }
    }
}
