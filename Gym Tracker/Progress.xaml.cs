using LiveChartsCore.SkiaSharpView;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using LiveChartsCore.SkiaSharpView.VisualElements;
using LiveChartsCore.Defaults;
using System.Collections.ObjectModel;


namespace Gym_Tracker;

public partial class Progress : ContentPage
{
    public Progress()
    {
        InitializeComponent();

        BindingContext = this;
    }

    public ISeries[] Series { get; set; } =
    {
        new LineSeries<double>
        {
            Values = new List<double> { 2, 1, 3, 5, 3, 4, 6 },
            Fill = null
        }
    };

    public void AddSeries()
    {
        //TODO: there should be code to add new values
    }

    public void AddSeriesButtonClicked(object sender, EventArgs e)
    {
        AddSeries();
    }
}