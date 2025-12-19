using System.Windows;
using System.Windows.Input;

namespace Turismo_V1.Views
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        #region Fields

        public LoginWindow()
        {
            InitializeComponent();
        }

    
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}
