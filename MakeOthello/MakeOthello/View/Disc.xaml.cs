using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace MakeOthello.View
{
    public enum DiscCondition
    {
        Black,
        White,
        Void,
        AbleBlack,
        AbleWhite
    }

    public sealed partial class Disc : Button
    {
        public Disc()
        {
            this.InitializeComponent();
        }

        public static readonly DependencyProperty DiscConditionProperty = DependencyProperty.Register(
            "DiscCondition", typeof(DiscCondition), typeof(Disc), new PropertyMetadata(default(DiscCondition)));

        public DiscCondition DiscCondition
        {
            get { return (DiscCondition) GetValue(DiscConditionProperty); }
            set { SetValue(DiscConditionProperty, value); }
        }
    }
}
