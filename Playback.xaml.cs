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

        private void mediaElement_MediaOpened(object sender, RoutedEventArgs e)
        {
            //
        }

        private void mediaElement_MediaFailed(object sender, ExceptionRoutedEventArgs e)
        {
            tbState.Text = "Playback error";
        }

        private void mediaElement_MediaEnded(object sender, RoutedEventArgs e)
        {
            tbState.Text = "Playback is complete";
        }
    }
}
