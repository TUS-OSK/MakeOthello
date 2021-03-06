﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using MakeOthello.Utility;
using MakeOthello.View;

namespace MakeOthello.ViewModel
{
    class SelectPageViewModel : ViewModelBase
    {
        private SolidColorBrush _level1Background;
        private SolidColorBrush _level2Background;
        private SolidColorBrush _level3Background;
        private SolidColorBrush _discBackground;
        public ICommand GoNextCommand { get; set; }
        public ICommand ChangeColorCommand { get; set; }
        public ICommand SetLevelCommand { get; set; }
        private int CpuLevel;
        private int DiscColor;

        public string ColorText
        {
            get
            {
                switch (DiscColor)
                {
                    case 1:
                        return "AI goes first (Tap to change)";
                    case -1:
                        return "You go first (Tap to change)";
                    default:
                        return string.Empty;
                }
            }
        }

        public SolidColorBrush Level1Background
        {
            get { return _level1Background; }
            set
            {
                _level1Background = value;
                OnPropertyChanged();
            }
        }
        public SolidColorBrush Level2Background
        {
            get { return _level2Background; }
            set
            {
                _level2Background = value;
                OnPropertyChanged();
            }
        }
        public SolidColorBrush Level3Background
        {
            get { return _level3Background; }
            set
            {
                _level3Background = value;
                OnPropertyChanged();
            }
        }
        public SolidColorBrush DiskBackground
        {
            get { return _discBackground; }
            set
            {
                _discBackground = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(ColorText));
            }
        }


        private SolidColorBrush _nonActiveColor = Application.Current.Resources["ThemeColorDark"] as SolidColorBrush;


        public SelectPageViewModel()
        {
            
            Level1Background = new SolidColorBrush(Colors.White);
            Level2Background = _nonActiveColor;
            Level3Background = _nonActiveColor;
            CpuLevel = 1;
            DiskBackground =new SolidColorBrush(Colors.Black);
            DiscColor = -1;
            
            SetLevelCommand = new SimpleCommand(param =>
            {
                switch (param.ToString())
                {
                    case "1":
                        Level1Background = new SolidColorBrush(Colors.White);
                        Level2Background = _nonActiveColor;
                        Level3Background = _nonActiveColor;
                        CpuLevel = 1;
                        break;
                    case "2":
                        Level2Background = new SolidColorBrush(Colors.White);
                        Level1Background = _nonActiveColor;
                        Level3Background = _nonActiveColor;
                        CpuLevel = 2;
                        break;
                    case "3":
                        Level3Background = new SolidColorBrush(Colors.White);
                        Level2Background = _nonActiveColor;
                        Level1Background = _nonActiveColor;
                        CpuLevel = 3;
                        break;
                }
            });
            ChangeColorCommand=new SimpleCommand(param =>
                {
                    if (param.ToString()=="0")
                    {
                        if (DiscColor==-1)
                        {
                            DiskBackground=new SolidColorBrush(Colors.White);
                            DiscColor = 1;
                        }
                        else
                        {
                            DiskBackground=new SolidColorBrush(Colors.Black);
                            DiscColor = -1;
                        }
                            
                    }
                });

            GoNextCommand = new SimpleCommand(param =>
            {
                Navigate(typeof(GamePage), new BoardViewModel(DiscColor, CpuLevel));
            });
        }
    }
}
