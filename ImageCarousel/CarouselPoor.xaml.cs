using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using Microsoft.Phone.Controls;

namespace ImageCarousel
{
    public partial class CarouselPoor : PhoneApplicationPage
    {
        private List<Image> _images = new List<Image>();
        private DoubleAnimation _scrollAnimation;
        private Storyboard _scrollViewerStoryboard;

        //We create dependency property on the MainPage, because we can't target the HorizontalOffset from the ScrollViewer directly from the StoryBoard
        //So we will be passing this through when the one on the MainPage changes!
        private DependencyProperty _horizontalOffsetProperty = DependencyProperty.Register("HorizontalOffset", typeof(double), typeof(CarouselPoor), new PropertyMetadata(0.0, OnHorizontalOffsetChanged));

        public CarouselPoor()
        {
            InitializeComponent();

            this.InitImages();
            this.LoadImages();
            this.ValidateNavigation();

            _scrollAnimation = new DoubleAnimation()
            {
                EasingFunction = new SineEase(),
                Duration = TimeSpan.FromSeconds(0.5)
            };

            Storyboard.SetTarget(_scrollAnimation, this);
            Storyboard.SetTargetProperty(_scrollAnimation, new PropertyPath(_horizontalOffsetProperty));

            _scrollViewerStoryboard = new Storyboard();
            _scrollViewerStoryboard.Children.Add(_scrollAnimation);
        }

        private static void OnHorizontalOffsetChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //Pass the value through from this static method to the ScrollViewer
            CarouselPoor app = d as CarouselPoor;
            app.OnHorizontalOffsetChanged(e);
        }

        private void OnHorizontalOffsetChanged(DependencyPropertyChangedEventArgs e)
        {
            this.ValidateNavigation();
            this.ImageCarousel.ScrollToHorizontalOffset((double)e.NewValue);
        }

        private void ValidateNavigation()
        {
            if (this.Indicator.SelectedPivotIndex == 0)
                this.ButtonLeft.Visibility = Visibility.Collapsed;
            else
                this.ButtonLeft.Visibility = Visibility.Visible;

            if (this.Indicator.SelectedPivotIndex == this.Indicator.ItemsCount - 1)
                this.ButtonRight.Visibility = Visibility.Collapsed;
            else
                this.ButtonRight.Visibility = Visibility.Visible;
        }

        private void InitImages()
        {
            _images.Add(new Image() { Source = new BitmapImage(new Uri(@"/Images/sink1.jpg", UriKind.Relative)), Width = 480 });
            _images.Add(new Image() { Source = new BitmapImage(new Uri(@"/Images/sink2.jpg", UriKind.Relative)), Width = 480 });
            _images.Add(new Image() { Source = new BitmapImage(new Uri(@"/Images/sink3.jpg", UriKind.Relative)), Width = 480 });
            _images.Add(new Image() { Source = new BitmapImage(new Uri(@"/Images/sink4.jpg", UriKind.Relative)), Width = 480 });
            _images.Add(new Image() { Source = new BitmapImage(new Uri(@"/Images/sink5.jpg", UriKind.Relative)), Width = 480 });
        }

        private void LoadImages()
        {
            _images.ForEach(item => this.ImageList.Children.Add(item));
        }

        private void ButtonLeft_Click(object sender, RoutedEventArgs e)
        {
            var startPosition = this.ImageCarousel.HorizontalOffset;
            if (startPosition > 0)
            {
                _scrollAnimation.From = startPosition;
                _scrollAnimation.To = startPosition - 480;
                --this.Indicator.SelectedPivotIndex;
                _scrollViewerStoryboard.Begin();
            }
        }

        private void ButtonRight_Click(object sender, RoutedEventArgs e)
        {
            var startPosition = this.ImageCarousel.HorizontalOffset;            
            _scrollAnimation.From = 0 + startPosition;
            _scrollAnimation.To = 480 + startPosition;
            ++this.Indicator.SelectedPivotIndex;
            _scrollViewerStoryboard.Begin();
        }
    }
}