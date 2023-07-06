using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.SkiaSharpView;
using Microsoft.Maui.Controls;
using SkiaSharp;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Gym_Tracker
{
    public partial class Progress : ContentPage
    {
        //private Button OptionsButton;
        public Progress()
        {
            InitializeComponent();

            //OptionsButton = (Button)FindByName("OptionsButton");

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

        private async void OptionsButtonClicked(object sender, EventArgs e)
        {
            // Display options in a dialog
            string selectedOption = await DisplayActionSheet("Options", "Cancel", null, "Workout Volume", "Bench Press One Rep Max");

            // Handle the selected option
            switch (selectedOption)
            {
                case "Workout Volume":
                    ((ObservableCollection<double>)((LineSeries<double>)Series[0]).Values).Clear();
                    OptionsButton.Text = "Workout Volume";
                    GenerateWorkoutVolumePoints();
                    break;
                case "Bench Press One Rep Max":
                    ((ObservableCollection<double>)((LineSeries<double>)Series[0]).Values).Clear();
                    OptionsButton.Text = "Bench Press One Rep Max";
                    AddSeries();
                    break;
                default:
                    Debug.Fail("Progress button selected option not available!");
                    break;
            }
        }

        //TODO: Optimize to calculate workout volume after saving workout, dont need to do this all here
        public void GenerateWorkoutVolumePoints() 
        {
            for (int i = 0; i < WorkoutManager.Instance.DoneWorkouts.Count; i++)
            {
                double valueToAdd = 0;
                for (int j = 0; j < WorkoutManager.Instance.DoneWorkouts[i].Exercises.Count; j++)
                {
                    for (int k = 0; k < WorkoutManager.Instance.DoneWorkouts[i].Exercises[j].Series.Count; k++)
                    {
                        valueToAdd += WorkoutManager.Instance.DoneWorkouts[i].Exercises[j].Series[k].AmountOfReps *
                            WorkoutManager.Instance.DoneWorkouts[i].Exercises[j].Series[k].WeightOnRep;
                    }
                }
                ((ObservableCollection<double>)((LineSeries<double>)Series[0]).Values).Add(valueToAdd);
            }
        }
    }
}
