using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;
using MakeOthello.Utility;
using MakeOthello.View;

namespace MakeOthello.ViewModel
{
    public class PopUpControleViewModel : ViewModelBase
    {
        private Visibility _visibility;
        private string _endtext;
        
        public ICommand OkCommand { get; set; }
        public ICommand QuitCommand { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public string EndText {
            get { return _endtext; }
            set
            {
                _endtext = value;
                OnPropertyChanged();
            }
        }

        public Visibility Visibility
        {
            get { return _visibility; }
            set
            {
                _visibility = value;
                OnPropertyChanged();
            }
        }

        public PopUpControleViewModel(double width)
        {
            Visibility = Visibility.Collapsed;
            Width = width;
            Height = width/2;
        }


    }
}
