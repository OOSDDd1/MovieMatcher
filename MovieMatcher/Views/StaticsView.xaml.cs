﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LiveCharts;
using LiveCharts.Helpers;
using LiveCharts.Wpf;

namespace MovieMatcher.Views
{
    /// <summary>
    /// Interaction logic for StaticsView.xaml
    /// </summary>
    public partial class StaticsView : UserControl, INotifyPropertyChanged
    {
        private Visibility vs = Visibility.Hidden;
        private BooleanToVisibilityConverter converter;
        public dynamic MariaSeries, CharlesSeries, JohnSeries, Testseries;


        private HashSet<dynamic> TestSet;

        public SeriesCollection ChartSeries { get; set; }

        public StaticsView()
        {
            ChartSeries = new SeriesCollection();
            InitializeComponent();
            //TestSet---------------------------------------------------------------
            TestSet = new();

            MariaSeries = new ExpandoObject();
            MariaSeries.Title = "Maria";
            MariaSeries.Size = 5;
            MariaSeries.Visible = true;

            CharlesSeries = new ExpandoObject();
            CharlesSeries.Title = "Charles";
            CharlesSeries.Size = 3;
            CharlesSeries.Visible = true;

            JohnSeries = new ExpandoObject();
            JohnSeries.Title = "John";
            JohnSeries.Size = 7;
            JohnSeries.Visible = true;

            Testseries = new ExpandoObject();
            Testseries.Title = "Test";
            Testseries.Size = 4;
            Testseries.Visible = true;

            TestSet.Add(MariaSeries);
            TestSet.Add(CharlesSeries);
            TestSet.Add(JohnSeries);
            TestSet.Add(Testseries);
            //TestSet-----------------------------------------------------------------------------
            GenerateChart();

            DataContext = this;
        }

        public void GenerateChart()
        {
            StackPanel StkPnl = new StackPanel();
            StkPnl.Orientation = Orientation.Horizontal;
            List<string> ls = new List<string>();
            foreach (dynamic item in TestSet)
            {
                CheckBox chkBx = GenerateCheckBox((string)item.Title, (bool)item.Visible);
                StkPnl.Children.Add(chkBx);
                AddColumnSeries(chkBx, (string)item.Title, (int) item.Size);
            }
            ls.Add("XasItem1");
            XBar.Labels = ls;

            Grid.Children.Add(StkPnl);

        }

        public CheckBox GenerateCheckBox(string content, bool visible)
        {
            CheckBox ChkBx = new CheckBox();
            ChkBx.Content = content;
            ChkBx.IsChecked = visible;
            ChkBx.Checked += ClmVis;
            ChkBx.Unchecked += ClmVis;
            return ChkBx;
        }

        public void AddColumnSeries(CheckBox chkBx, string Title, int num)
        {
            ColumnSeries ClmnSrs = new ColumnSeries();
            ClmnSrs.Title = Title;
            List<int> LsValues = new List<int> { num };
            ClmnSrs.Values = LsValues.AsChartValues();
            chkBx.DataContext = ClmnSrs;
            ClmnSrs.MaxWidth = 1000;
            ClmnSrs.ColumnPadding = 0;
            ChartSeries.Add(ClmnSrs);
        }

        public void ClmVis(object sender, RoutedEventArgs e)
        {
            CheckBox chkBx = (CheckBox)sender;
            ColumnSeries clmnSrs = (ColumnSeries)chkBx.DataContext;

            clmnSrs.Visibility = ((bool)chkBx.IsChecked) ? Visibility.Visible : Visibility.Collapsed;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}