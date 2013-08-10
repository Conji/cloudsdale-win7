﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
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
using Cloudsdale_Win7.Win7_Lib;
using Cloudsdale_Win7.Win7_Lib;
using Cloudsdale_Win7.Win7_Lib.Models;
using Newtonsoft.Json.Linq;

namespace Cloudsdale_Win7.Controls {

    public delegate void TextChangedEventHandler(TextChangedEventArgs args);
    public class TextChangedEventArgs : EventArgs {
        public string NewText { get; set; }
        public string OldText { get; set; }
    }

    public class LinkDetectingTextBlock : TextBlock {
        static LinkDetectingTextBlock() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LinkDetectingTextBlock), new FrameworkPropertyMetadata(typeof(LinkDetectingTextBlock)));
        }
        public static DependencyProperty LinkedTextProperty = DependencyProperty.Register(
            "LinkedText", typeof (string), typeof (LinkDetectingTextBlock),
            new PropertyMetadata("", PropertyOnLinkedTextChanged));
        private static void PropertyOnLinkedTextChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs) {
            ((LinkDetectingTextBlock)dependencyObject).OnLinkedTextChange(
                new TextChangedEventArgs {
                    NewText = (string) dependencyPropertyChangedEventArgs.NewValue,
                    OldText = (string) dependencyPropertyChangedEventArgs.OldValue,
                });
        }

        public TextChangedEventHandler LinkedTextChanged;

        public string LinkedText {
            get { return (string) GetValue(LinkedTextProperty); }
            set { SetValue(LinkedTextProperty, value); }
        }

        protected virtual void OnLinkedTextChange(TextChangedEventArgs args) {
            if (LinkedTextChanged != null) LinkedTextChanged(args);

            var matches = Cloudsdale_Helpers.LinkRegex.Matches(args.NewText);
            var lastIndex = 0;
            Inlines.Clear();
            foreach (Match match in matches) {
                Inlines.Add(new Run(args.NewText.Substring(lastIndex, match.Index - lastIndex)));
                var hyperlink = new Hyperlink(new Run(match.Value));
                var link = match.Value;
                hyperlink.Click += (sender, eventArgs) => {
                    if (match.ToString().Contains("www.cloudsdale.org/clouds/"))
                    {
                        //Adds the clicked cloud link to the cloud list and subscribes the user to the channel.
                        string shortname;
                        string id;
                        
                        
                    }
                    MainWindow.Instance.CloudList.SelectedIndex = -1;
                    MainWindow.Instance.Frame.Navigate(new Browser());
                    Browser.Instance.Width = MainWindow.Instance.Width;
                    Browser.Instance.WebBrowser.Navigate(link);
                    Browser.Instance.WebAddress.Text = link;
                };
                Inlines.Add(hyperlink);
                lastIndex = match.Index + match.Length;
            }
            Inlines.Add(new Run(args.NewText.Substring(lastIndex)));
        }
    }
}
