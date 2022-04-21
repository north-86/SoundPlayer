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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.Windows.Threading;

namespace SoundPlayer
{
    public partial class Playback : Window
    {
        public Playback()
        {
            InitializeComponent();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Previous_Click(object sender, RoutedEventArgs e)
        {
            //
        }

        private void Play_Click(object sender, RoutedEventArgs e)
        {
            mediaElement.Position = TimeSpan.Zero;
            mediaElement.Play();
        }

        private void Pause_Click(object sender, RoutedEventArgs e)
        {
            if (mediaElement.CanPause)
            {
                mediaElement.Pause();
            }
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            mediaElement?.Stop();
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            //
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            mediaElement.Close();
        }

        private void MediaElement_MediaOpened(object sender, RoutedEventArgs e)
        {
            tbState.Text = mediaElement.Source.LocalPath;
            sliderPosition.Maximum = mediaElement.NaturalDuration.TimeSpan.TotalSeconds;
        }

        private void MediaElement_MediaFailed(object sender, ExceptionRoutedEventArgs e)
        {
            tbState.Text = e.ErrorException.Message.ToString();
        }

        private void MediaElement_MediaEnded(object sender, RoutedEventArgs e)
        {
            tbState.Text = "Playback is complete";
        }

        private void SliderVolume_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ((Slider)sender).SelectionEnd = e.NewValue;
        }

        private void SliderPosition_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            DispatcherTimer timer = new DispatcherTimer(TimeSpan.FromSeconds(1), DispatcherPriority.Normal, (object o, EventArgs ev) =>
            {
                mediaElement.Pause();
                mediaElement.Position = TimeSpan.FromSeconds(sliderPosition.Value);
                mediaElement.Play();
            }, this.Dispatcher);
            timer.Start(); 
        }
    }
}
