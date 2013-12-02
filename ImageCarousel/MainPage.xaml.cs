using System;
using System.Windows;
using Microsoft.Phone.Controls;

namespace ImageCarousel
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        private void ButtonPoor_OnClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/CarouselPoor.xaml", UriKind.Relative));
        }
    }
}