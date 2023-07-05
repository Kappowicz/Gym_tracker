using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.SkiaSharpView;
using Microsoft.Maui.Controls;
using SkiaSharp;
using System;
using System.Collections.ObjectModel;

namespace Gym_Tracker
{
    public partial class Progress : ContentPage
    {
        public Progress()
        {
            InitializeComponent();

            Random random = new Random();

            ObservableCollection<double> values = new ObservableCollection<double>();

            // Add initial random values to the series
            values.Add(random.Next(101));
            values.Add(random.Next(101));
            values.Add(random.Next(101));

            // Create a line series with the values collection
            var lineSeries = new LineSeries<double>
            {
                Values = values,
                Fill = null
            };

            // Set the series as an array
            Series = new ISeries[] { lineSeries };

            BindingContext = this;
        }

        public ISeries[] Series { get; set; }

        public void AddSeries()
        {
            Random random = new Random();

            // Add a new random value to the series
            double newValue = random.Next(101);
            ((ObservableCollection<double>)((LineSeries<double>)Series[0]).Values).Add(newValue);
        }

        public void AddSeriesButtonClicked(object sender, EventArgs e)
        {
            AddSeries();
        }
    }
}
