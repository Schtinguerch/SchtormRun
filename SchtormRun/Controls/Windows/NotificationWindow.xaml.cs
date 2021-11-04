using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using SchtormRun.Properties;

namespace SchtormRun.Controls.Windows
{
    /// <summary>
    /// Логика взаимодействия для NotificationWindow.xaml
    /// </summary>
    public partial class NotificationWindow : Window
    {
        private Storyboard _showNotificationStoryboard;
        private Storyboard _showProcessingStoryboard;
        private Storyboard _hideProcessingStoryborad;

        private readonly Uri _imageSuccessBaseUri = "Resources/Images/AppIcons/SuccessWhite.png".RelativeToAppUri();
        private readonly Uri _imageErrorBaseUri = "Resources/Images/AppIcons/ErrorWhite.png".RelativeToAppUri();
        private readonly Uri _imageProcesBaseUri = "Resources/Images/AppIcons/ProcessWhite.png".RelativeToAppUri();

        private Uri _imageUri;

        public Uri ImageSource
        {
            get => _imageUri;
            set
            {
                NotificationIconImage.Source = new BitmapImage(value);
                _imageUri = value;
            }
        }

        public NotificationWindow()
        {
            InitializeComponent();
            _showNotificationStoryboard = FindResource("ShowNotificationStoryoard") as Storyboard;

            _showProcessingStoryboard = FindResource("AppearanceProcessingStoryboard") as Storyboard;
            _hideProcessingStoryborad = FindResource("HideProcessingStoryBoard") as Storyboard;
        }

        public void ShowSuccess(string message, Uri imageSourceUri = null)
        {
            NotificationTextBlock.Text = message;
            NotificationBorder.Background = StringToBrush(Settings.Default.SuccessNotificationBackgroundColor);

            ImageSource = imageSourceUri != null? imageSourceUri : _imageSuccessBaseUri;
            StartNotificationShow();
        }

        public void ShowError(string message, Uri imageSourceUri = null)
        {
            NotificationTextBlock.Text = message;
            NotificationBorder.Background = StringToBrush(Settings.Default.ErrorNotificationBackgroundColor);

            ImageSource = imageSourceUri != null ? imageSourceUri : _imageErrorBaseUri;
            StartNotificationShow();
        }

        public void BeginProcessing(string message, Uri imageSourceUri = null)
        {
            Show();

            ImageSource = imageSourceUri != null ? imageSourceUri : _imageProcesBaseUri;
            _showProcessingStoryboard.Begin();

            NotificationTextBlock.Text = message;
            NotificationBorder.Background = StringToBrush(Settings.Default.ProcessNotificationBackgroudColor);

            LocateNotification();
        }

        public void EndProcessing()
        {
            _showProcessingStoryboard.Stop();
            _hideProcessingStoryborad.Begin();
        }

        private SolidColorBrush StringToBrush(string color) =>
            (SolidColorBrush) new BrushConverter().ConvertFromString(color);

        private void StartNotificationShow()
        {
            Show();
            _showNotificationStoryboard.Begin();

            LocateNotification();
        }

        private void LocateNotification()
        {
            Left = CenterNode.AppWindow.Left;
            Top = CenterNode.AppWindow.Top - Height - Settings.Default.AppBorderRadius;

            Width = NotificationTextBlock.ActualWidth + 86;
        }
    }
}
