using FitnessApp.SignUpPages;
using MaterialDesignThemes.Wpf;
using System.Windows;
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
            bool isAccountFound = App.Database.IsUserFound(EmailSignInTextBox.Text, PasswordSignInTextBox.Password);

            if (isAccountFound)
            {
                if (App.Database.AccountType == "User")
                {
                    // Open User Main Window
                    UserWindow userWindowTemp = new UserWindow(App.Database.AccountId);
                    userWindowTemp.Show();
                }
                else
                {
                    // Open Admin Main Window
                    AdminWindow adminWindowTemp = new AdminWindow(App.Database.AccountId);
                    adminWindowTemp.Show();
                }

                // Close Signing Window
                Close();
            }
            else
            {
                // Display error when the user is not found
                ErrorsSnackbar.MessageQueue?.Enqueue("Incorrect Email Or Password");
            }
        }


        private void CreateAnAccountButton_Click(object sender, RoutedEventArgs e)
        {
            SignUpPagesFrame.NavigationService.Navigate(SignUpPageObject);
        }
    }
}