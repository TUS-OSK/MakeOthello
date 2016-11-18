using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using MakeOthello.Utility;

namespace MakeOthello.ViewModel.MessageBox
{
    public class PassControlViewModel:ViewModelBase
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

        public PassControlViewModel()
        {
            Visibility = Visibility.Collapsed;
        }
    }
}
