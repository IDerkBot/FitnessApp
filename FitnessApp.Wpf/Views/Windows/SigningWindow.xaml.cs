using FitnessApp.SignUpPages;
using MaterialDesignThemes.Wpf;
using System.Windows;
using FitnessApp.Core;
using FitnessApp.Wpf;
using FitnessApp.Wpf.Views.SignUpPages;

namespace FitnessApp.Windows
{
    /// <summary>
    /// Interaction logic for SigningWindow.xaml
    /// </summary>
    public partial class SigningWindow : Window
    {
        public static SigningWindow? SigningWindowObject;
        public static SignUpPage? SignUpPageObject;
        public static SetUpProfilePage? SetUpProfilePageObject;


        public SigningWindow()
        {
            InitializeComponent();
            
            SigningWindowObject = this;

            // Initialize UserWindowPages Objects
            SignUpPageObject = new SignUpPage();
            SetUpProfilePageObject = new SetUpProfilePage();

            // Intialize ErrorMessagesQueue and Assign it to ErrorsSnackbar's MessageQueue
            var errorMessagesQueue = new SnackbarMessageQueue(TimeSpan.FromMilliseconds(2000));
            ErrorsSnackbar.MessageQueue = errorMessagesQueue;
        }

        private void SignInButton_Click(object sender, RoutedEventArgs e)
        {
            
        }


        private void CreateAnAccountButton_Click(object sender, RoutedEventArgs e)
        {
            SignUpPagesFrame.NavigationService.Navigate(SignUpPageObject);
        }
    }
}