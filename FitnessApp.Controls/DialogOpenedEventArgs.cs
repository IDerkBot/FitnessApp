using System.Windows;

namespace FitnessApp.Controls;

public class DialogOpenedEventArgs : RoutedEventArgs
{
    public DialogOpenedEventArgs(DialogSession session, RoutedEvent routedEvent)
    {
        Session = session ?? throw new ArgumentNullException(nameof(session));
        RoutedEvent = routedEvent;
    }

    /// <summary>
    /// Allows interaction with the current dialog session.
    /// </summary>
    public DialogSession Session { get; }
}