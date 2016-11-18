using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using MakeOthello.View;

namespace MakeOthello.ViewModel.MessageBox
{
    class EndMessageBoxViewModel:MessageBoxViewModel
    {
        private int _player;
        private int _cpu;

        public override SolidColorBrush Background
        {
            get
            {
                return Application.Current.Resources["ThemeColorPurple"] as SolidColorBrush;
            }
        }

        public override string OkButtonText
        {
            get { return "Play Again"; }
        }

        public override string QuitButtonText
        {
            get { return "Quit"; }
        }

        protected override void Quit(object param)
        {
            Navigate(typeof(MainPage));
        }

        protected override void Ok(object param)
        {
            Navigate(typeof(GamePage), new BoardViewModel(_player, _cpu));
        }

        public EndMessageBoxViewModel(int player, int cpu, string message)
        {
            _player = player;
            _cpu = cpu;
            Message = message;
        }
    }
}
