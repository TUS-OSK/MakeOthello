using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using MakeOthello.Utility;

namespace MakeOthello.ViewModel.MessageBox
{
    public abstract class MessageBoxViewModel : ViewModelBase
    {
        private Visibility _visibility;
        private string _endtext;

        public static MessageBoxViewModel Empty
        {
            get
            {
                return new EmptyMessageBoxViewModel();           
            }
        }
        
        public ICommand OkCommand { get; set; }
        public ICommand QuitCommand { get; set; }
        public abstract SolidColorBrush Background { get;}
        public abstract string OkButtonText { get;}
        public abstract string QuitButtonText { get;}
        public string Message {
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

        public MessageBoxViewModel()
        {
            Visibility = Visibility.Visible;
            QuitCommand = new SimpleCommand(Quit);
            OkCommand = new SimpleCommand(Ok);
        }

        protected abstract void Quit(object param);
        protected abstract void Ok(object param);

        class EmptyMessageBoxViewModel:MessageBoxViewModel
        {
            public override SolidColorBrush Background { get; }
            public override string OkButtonText { get; }
            public override string QuitButtonText { get; }
            protected override void Quit(object param)
            {
            }
            protected override void Ok(object param)
            { 
            }
            public EmptyMessageBoxViewModel()
            {
                Visibility = Visibility.Collapsed;
            }
        }
    }
}
