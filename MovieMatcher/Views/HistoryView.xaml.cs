﻿using MovieMatcher.Models.Api;
using MovieMatcher.Models.Database;
using MovieMatcher.Stores;
using MovieMatcher.ViewModels;
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
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MovieMatcher.Views
{
    /// <summary>
    /// Interaction logic for HistoryView.xaml
    /// </summary>
    public partial class HistoryView : UserControl
    {
        public HistoryView()
        {
            InitializeComponent();
            List<Review> databaseItems = Database.GetReviewedItems(UserInfo.Id);

            DateTime currentTime = new DateTime();
            ListBox box = new ListBox();
            foreach (Review item in databaseItems)
            {
                if (item.DateChanged.Date != currentTime.Date)
                {
                    currentTime = item.DateChanged.Date;

                    ListBoxItem lstBoxItm = new ListBoxItem();
                    StackPanel sPanel = new StackPanel();
                    Label lbl = new Label();
                    box = new ListBox();

                    sPanel.Background = (Brush)(new BrushConverter().ConvertFromString("#FF272727"));
                    sPanel.Effect = new DropShadowEffect();
                    lstBoxItm.HorizontalContentAlignment = HorizontalAlignment.Stretch;

                    lbl.Content = currentTime;
                    lbl.Foreground = Brushes.White;
                    lbl.Background = (Brush)(new BrushConverter().ConvertFromString("#202020"));
                    lbl.FontSize = 16;

                    box.HorizontalAlignment = HorizontalAlignment.Center;
                    box.Background = null;
                    box.BorderBrush = null;
                    box.SetValue(ScrollViewer.HorizontalScrollBarVisibilityProperty, ScrollBarVisibility.Disabled);
                    
                    ItemsPanelTemplate template = new ItemsPanelTemplate();
                    var wrpP = new FrameworkElementFactory(typeof(WrapPanel));
                    wrpP.SetValue(WrapPanel.OrientationProperty, Orientation.Horizontal);
                    template.VisualTree = wrpP;
                    box.ItemsPanel = template;

                    sPanel.Children.Add(lbl);
                    sPanel.Children.Add(box);
                    lstBoxItm.Content = sPanel;
                    ResultBox.Items.Add(lstBoxItm);
                }

                ListBoxItem itmBox;
                if (item.IsShow)
                {
                    Show show = Api.GetShow(item.ContentId);
                    itmBox = CreateItem(show, item.Liked, item.Watched);
                }
                else
                {
                    Movie movie = Api.GetMovie(item.ContentId);
                    itmBox = CreateItem(movie, item.Liked, item.Watched);
                }
                box.Items.Add(itmBox);
            }

        }
        public ListBoxItem CreateItem(Content s, bool liked, bool watched)
        {
            ListBoxItem itm = new ListBoxItem();

            Button btn = new Button();
            btn.HorizontalAlignment = HorizontalAlignment.Left;
            btn.VerticalAlignment = VerticalAlignment.Top;
            btn.FontFamily = new FontFamily("Verdana");
            btn.Background = (Brush)(new BrushConverter().ConvertFromString("#3cb9f4"));

            Grid grd = new Grid();

            Image img = new Image();
            if (s.poster_path == null)
            {
                img.Source = new BitmapImage(new Uri(@"/Images/SamplePoster.png", UriKind.Relative));
            }
            else
            {
                img.Source = new BitmapImage(new Uri($"{Api.ImageBase}{Api.W185}{s.poster_path}"));
            }

            img.Stretch = Stretch.Fill;
            img.Width = 130;

            StackPanel sPanel = new StackPanel();
            sPanel.HorizontalAlignment = HorizontalAlignment.Right;
            sPanel.VerticalAlignment = VerticalAlignment.Top;
            sPanel.Background = (Brush)(new BrushConverter().ConvertFromString("#FF272727"));
            sPanel.Background.Opacity = 0.6;

            Image likedImg = new Image();
            if (liked)
            {
                likedImg.Source = new BitmapImage(new Uri(@"/Images/Like.png", UriKind.Relative));
            }
            else
            {
                likedImg.Source = new BitmapImage(new Uri(@"/Images/Dislike.png", UriKind.Relative));
            }
            likedImg.Width = 25;
            likedImg.Height = 25;
            sPanel.Children.Add(likedImg);

            if (watched)
            {
                Image watchedImg = new Image();
                watchedImg.Source = new BitmapImage(new Uri(@"/Images/eye.png", UriKind.Relative));
                watchedImg.Width = 25;
                watchedImg.Height = 25;
                sPanel.Children.Add(watchedImg);
            }


            btn.DataContext = s;
            btn.Click += DetailScreen_Clicked;

            grd.Children.Add(img);
            grd.Children.Add(sPanel);
            btn.Content = grd;
            itm.Content = btn;
            return itm;
        }

        private void DetailScreen_Clicked(object sender, RoutedEventArgs e)
        {
            Button RealButton = (Button)sender;
            var tmp = (Content)RealButton.DataContext;
            DetailViewStore.Id = tmp.id;
            DetailViewStore.MediaType = tmp.media_type;

            Application.Current.Windows[0].DataContext = new DetailViewModel();
        }
    }
}