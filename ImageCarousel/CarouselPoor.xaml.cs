using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Microsoft.Phone.Controls;

namespace ImageCarousel
{
    public partial class CarouselPoor : PhoneApplicationPage
    {
        private List<Image> _images = new List<Image>(); 

        public CarouselPoor()
        {
            InitializeComponent();

            this.InitImages();
            this.LoadImages();
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
    }
}