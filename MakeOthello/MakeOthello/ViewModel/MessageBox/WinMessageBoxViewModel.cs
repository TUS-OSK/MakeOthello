using Windows.UI;
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
                return new SolidColorBrush(Color.FromArgb(255, 255, 56, 36));
            }
        }
    }
}
