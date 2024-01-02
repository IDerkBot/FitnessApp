using System.Windows.Controls;
using FitnessApp.Windows;

namespace FitnessApp.Wpf.Views.SignUpPages
{
    /// <summary>
    /// Interaction logic for SignUpPage.xaml
    /// </summary>
    public partial class SignUpPage : Page
    {
        public SignUpPage()
        {
            InitializeComponent();
            SigningWindow.SignUpPageObject = this;
        }
    }
}
