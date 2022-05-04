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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using Microsoft.Win32;
using System.Windows.Threading;

namespace SoundPlayer
{
    public partial class MainWindow : Window
    {
        ObservableCollection<string> soundfiles;

        public MainWindow()
        {
            InitializeComponent();

            soundfiles = new ObservableCollection<string>();
            listBox.ItemsSource = soundfiles; 
        }
        
        private void About_Click(object sender, RoutedEventArgs e)
        {
            About about = new About();
            about.Owner = this;
            about.ShowDialog();
        }

        private void AddFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Audio Files (*.mp3)|*.mp3";
            if (openFileDialog.ShowDialog() == true)
            {
                var soundfile = openFileDialog.FileName;
                soundfiles.Add(soundfile);
                mediaElement.Source = new Uri(soundfile, UriKind.Absolute);
            }
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void BtnMin_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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
            mediaElement.Pause();
            mediaElement.Position = TimeSpan.FromSeconds(sliderPosition.Value);
            mediaElement.Play();
        }
    }
}
