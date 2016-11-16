using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace MakeOthello.ViewModel.MessageBox
{
    class WinMessageBoxViewModel : EndMessageBoxViewModel
    {
        public WinMessageBoxViewModel(int player, int cpu) : base(player, cpu, "Succeed !")
        {
        }

        public override SolidColorBrush Background
        {
            get
            {
                return Application.Current.Resources["ThemeColorRed"] as SolidColorBrush;
            }
        }
    }
}
