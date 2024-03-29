using CommunityToolkit.Mvvm.ComponentModel;

namespace FitnessApp.Core.ViewModels.AdminPages;

public class AdminSettingsViewModel : ObservableObject
{
    public AdminSettingsViewModel()
    {
        // // Initialize Profile Expander to be expanded
        // AccountExpander.IsExpanded = true;
    }

    // private void Expander_Expanded(object sender, System.Windows.RoutedEventArgs e)
    // {
    //     // // Remove Expanded Event Handler from all Expanders
    //     // // to prevent calling Expander_Expanded Event Handler 
    //     // // recursively.
    //     // AccountExpander.Expanded -= Expander_Expanded;
    //     // SecurityExpander.Expanded -= Expander_Expanded;
    //     // HelpExpander.Expanded -= Expander_Expanded;
    //     //
    //     // // Close all Expanders.
    //     // AccountExpander.IsExpanded = false;
    //     // SecurityExpander.IsExpanded = false;
    //     // HelpExpander.IsExpanded = false;
    //     //
    //     // // Re-add Expanded Event Handler to all Expanders.
    //     // AccountExpander.Expanded += Expander_Expanded;
    //     // SecurityExpander.Expanded += Expander_Expanded;
    //     // HelpExpander.Expanded += Expander_Expanded;
    //     //
    //     //
    //     // // Get Current Expander object from sender.
    //     // Expander currentExpander = sender as Expander;
    //     //
    //     // // Remove Expanded Event Handler from Current Expander.
    //     // currentExpander.Expanded -= Expander_Expanded;
    //     //
    //     // // Open current Expander only.
    //     // currentExpander.IsExpanded = true;
    //     //
    //     // // Re-add Expanded Event Handler to Current Expander.
    //     // currentExpander.Expanded += Expander_Expanded;
    // }
    
    // private void UpdateAccountButton_Click(object sender, System.Windows.RoutedEventArgs e)
    // {
    //     // // Empty Fields Validation
    //     // if (string.IsNullOrWhiteSpace(FirstNameTextBox.Text))
    //     //     AdminWindow.AdminWindowObject.MessagesSnackbar.MessageQueue.Enqueue("First Name Is Empty!");
    //     //
    //     // else if (string.IsNullOrWhiteSpace(LastNameTextBox.Text))
    //     //     AdminWindow.AdminWindowObject.MessagesSnackbar.MessageQueue.Enqueue("Last Name Is Empty!");
    //     //
    //     // else if (string.IsNullOrWhiteSpace(EmailTextBox.Text))
    //     //     AdminWindow.AdminWindowObject.MessagesSnackbar.MessageQueue.Enqueue("Email Is Empty!");
    //     //
    //     // // Check Email Validation
    //     // else if (!EmailTextBox.Text.Contains("@") || !EmailTextBox.Text.Contains(".com"))
    //     //     AdminWindow.AdminWindowObject.MessagesSnackbar.MessageQueue.Enqueue("Invalid E-mail");
    //
    //     // Check Email/Username Availability
    //     // else if (EmailTextBox.Text != AdminWindow.signedInAdmin.Email)
    //     // {
    //     //     if (App.Database.IsEmailTaken(EmailTextBox.Text))
    //     //         AdminWindow.AdminWindowObject.MessagesSnackbar.MessageQueue.Enqueue("E-mail is in use");
    //     // }
    //     //
    //     // else
    //     // {
    //     //
    //     //     // Update signedInUser User Model-
    //     //     AdminWindow.signedInAdmin.FirstName = FirstNameTextBox.Text;
    //     //     AdminWindow.signedInAdmin.LastName  = LastNameTextBox.Text;
    //     //     AdminWindow.signedInAdmin.Email     = EmailTextBox.Text;
    //     //
    //     //     // Update User's Account in database
    //     //     App.Database.UpdateAdminAccount(AdminWindow.signedInAdmin);
    //     //
    //     //     // Refresh UserWindow DataContext
    //     //     AdminWindow.AdminWindowObject.DataContext = null;
    //     //     AdminWindow.AdminWindowObject.DataContext = AdminWindow.signedInAdmin;
    //     //
    //     //     // Confirmation Message
    //     //     AdminWindow.AdminWindowObject.MessagesSnackbar.MessageQueue.Enqueue("Account Updated!");
    //     //
    //     // }
    // }
    
    // private void UpdatePasswordButton_Click(object sender, System.Windows.RoutedEventArgs e)
    // {
    //     // // Empty Fields Validation
    //     // if (string.IsNullOrWhiteSpace(OldPasswordTextBox.Password))
    //     //     AdminWindow.AdminWindowObject.MessagesSnackbar.MessageQueue.Enqueue("Please enter your old password!");
    //     //
    //     // else if (string.IsNullOrWhiteSpace(NewPasswordTextBox.Password))
    //     //     AdminWindow.AdminWindowObject.MessagesSnackbar.MessageQueue.Enqueue("Please enter your new password!");
    //     //
    //     // else if (NewPasswordTextBox.Password != ConfirmNewPasswordTextBox.Password)
    //     //     AdminWindow.AdminWindowObject.MessagesSnackbar.MessageQueue.Enqueue("New Password and Confirmation doesn't match!");
    //     //
    //     // // Password Validation
    //     // else if (NewPasswordTextBox.Password.Length < 7)
    //     //     AdminWindow.AdminWindowObject.MessagesSnackbar.MessageQueue.Enqueue("Password must be 7 characters or more");
    //     //
    //     // else if (App.Database.EncryptPassword(OldPasswordTextBox.Password) != AdminWindow.signedInAdmin.Password)
    //     //     AdminWindow.AdminWindowObject.MessagesSnackbar.MessageQueue.Enqueue("Old Password is Incorrect!");
    //     //
    //     // else
    //     // {
    //     //     // Update signedInAdmin User Model
    //     //     AdminWindow.signedInAdmin.Password = App.Database.EncryptPassword(NewPasswordTextBox.Password);
    //     //
    //     //     // Update Admin's Password in database
    //     //     App.Database.UpdateAdminPassword(AdminWindow.signedInAdmin);
    //     //
    //     //     // Confirmation Message
    //     //     AdminWindow.AdminWindowObject.MessagesSnackbar.MessageQueue.Enqueue("Password Updated!");
    //     // }
    // }
}