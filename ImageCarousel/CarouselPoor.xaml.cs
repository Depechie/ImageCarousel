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

            _scrollAnimation = new DoubleAnimation()
            {
                EasingFunction = new SineEase(),
                Duration = TimeSpan.FromSeconds(0.5)
            };

            Storyboard.SetTarget(_scrollAnimation, this);
            Storyboard.SetTargetProperty(_scrollAnimation, new PropertyPath(_horizontalOffsetProperty));

            _scrollViewerStoryboard = new Storyboard();
            _scrollViewerStoryboard.Children.Add(_scrollAnimation);
            _scrollViewerStoryboard.Completed += _scrollViewerStoryboard_Completed;            
        }

        private static void OnHorizontalOffsetChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            CarouselPoor app = d as CarouselPoor;
            app.OnHorizontalOffsetChanged(e);
        }

        private void OnHorizontalOffsetChanged(DependencyPropertyChangedEventArgs e)
        {
            this.ImageCarousel.ScrollToHorizontalOffset((double)e.NewValue);
        }

        private void _scrollViewerStoryboard_Completed(object sender, EventArgs e)
        {
            //var itemOntdek = this.ImagesOntdek.Children[0];
            //this.ImagesOntdek.Children.RemoveAt(0);
            //ImageCarousel.ScrollToHorizontalOffset(0);
            //this.ImagesOntdek.Children.Add(itemOntdek);

            //++this.IndicatorOntdek.SelectedPivotIndex;
            //if (this.IndicatorOntdek.SelectedPivotIndex == this.IndicatorOntdek.ItemsCount)
            //    this.IndicatorOntdek.SelectedPivotIndex = 0;
        }

        private void LoadImages()
        {
            _images.ForEach(item => this.ImageList.Children.Add(item));
        }

        private void InitImages()
        {
            _images.Add(new Image() { Source = new BitmapImage(new Uri(@"/Images/sink1.jpg", UriKind.Relative)), Width = 480 });
            _images.Add(new Image() { Source = new BitmapImage(new Uri(@"/Images/sink2.jpg", UriKind.Relative)), Width = 480 });
            _images.Add(new Image() { Source = new BitmapImage(new Uri(@"/Images/sink3.jpg", UriKind.Relative)), Width = 480 });
            _images.Add(new Image() { Source = new BitmapImage(new Uri(@"/Images/sink4.jpg", UriKind.Relative)), Width = 480 });
            _images.Add(new Image() { Source = new BitmapImage(new Uri(@"/Images/sink5.jpg", UriKind.Relative)), Width = 480 });
        }

        private void ButtonLeft_Click(object sender, RoutedEventArgs e)
        {
            var startPosition = this.ImageCarousel.HorizontalOffset;
            _scrollAnimation.From = startPosition;
            _scrollAnimation.To = startPosition - 480;
            _scrollViewerStoryboard.Begin();
        }

        private void ButtonRight_Click(object sender, RoutedEventArgs e)
        {
            var startPosition = this.ImageCarousel.HorizontalOffset;
            _scrollAnimation.From = 0 + startPosition;
            _scrollAnimation.To = 480 + startPosition;
            _scrollViewerStoryboard.Begin();
        }
    }
}