using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using MakeOthello.View;

namespace MakeOthello.ViewModel.MessageBox
{
    class ConfirmMessageBoxViewModel:MessageBoxViewModel
    {

        public override SolidColorBrush Background
        {
            get
            {
                return Application.Current.Resources["ThemeColorPurple"] as SolidColorBrush;
            }
        }

        public override string OkButtonText
        {
            get { return "OK"; }
        }

        public override string QuitButtonText
        {
            get { return "Cancel"; }
        }

        protected override void Ok(object param)
        {
            Navigate(typeof(MainPage));
        }

        protected override void Quit(object param)
        {
            Visibility = Visibility.Collapsed;
        }

        public ConfirmMessageBoxViewModel()
        {
            Message = "Quit this game and go back to the menu ？";
        }
    }
}
