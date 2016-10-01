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
        public ICommand OkCommand { get; set; }

        public Visibility Visibility
        {
            get { return _visibility; }
            set
            {
                _visibility = value;
                OnPropertyChanged();
            }
        }

        public PopUpControleViewModel()
        {
            Visibility = Visibility.Collapsed;
        }
    }
}
