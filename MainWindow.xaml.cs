﻿using System.Collections.Generic;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using Cloudsdale_Win7.Views;
using Cloudsdale_Win7.Win7_Lib.Cloudsdale_Lib;
using Newtonsoft.Json.Linq;

namespace Cloudsdale_Win7
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {

        public static MainWindow Instance;
        public static JObject User;
        public static JToken CurrentCloud;
        public static int CloudIndex;

        
        public int MaxCharacters
        {
            get { return (int)GetValue(MaxCharactersProperty); }
            set { SetValue(MaxCharactersProperty, value); }
        }

        public MainWindow()
        {
            InitializeComponent();
            Instance = this;
            Height = UserSettings.Default.AppHeight;
            Width = UserSettings.Default.AppWidth;
        }

        private void CloudListSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CloudList.SelectedIndex >= 0)
            {
                CurrentCloud = (JToken)CloudList.SelectedItem;
                Frame.Navigate(GetCloudView(CurrentCloud));
                if (User["user"]["id"].ToString() != CurrentCloud["owner_id"].ToString())
                {
                    CloudView.Instance.acp.Visibility = Visibility.Collapsed;
                }
                CloudIndex = CloudList.SelectedIndex;
            }
        }

        public static DependencyProperty MaxCharactersProperty =
            DependencyProperty.Register("MaxCharacters", typeof(int), typeof(MainWindow),
                                        new FrameworkPropertyMetadata(200));
        private static readonly Dictionary<string, object> Clouds = new Dictionary<string, object>();
        private static object GetCloudView(JToken cloud)
        {
            if (Clouds.ContainsKey((string)cloud["id"]))
            {
                return Clouds[(string)cloud["id"]];
            }
            return Clouds[(string)cloud["id"]] = new CloudView(cloud);
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            try
            {
                SettingsWindow.Instance.Close();
            }catch
            {
            }
            try
            {
                UserInfo.Instance.Close();
            }catch
            {
            }
        }

        private void SaveSettings(object sender, System.ComponentModel.CancelEventArgs e)
        {
            UserSettings.Default.AppHeight = (int)Height;
            UserSettings.Default.AppWidth = (int)Width;
            UserSettings.Default.Save();
        }

        private void KeepCurrentCloud(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            CloudList.SelectedIndex = CloudIndex;
        }
    }
}