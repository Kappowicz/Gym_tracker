using Gym_Tracker.Managers;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using System.Collections.ObjectModel;

namespace Gym_Tracker
{
    public sealed partial class Progress : ContentPage
    {
        public ISeries[] Series { get; set; }

        private readonly ObservableCollection<double> _valuesOnChart;

        public Progress()
        {
            InitializeComponent();

            // Create a line series with the values collection
            LineSeries<double> lineSeries = new()
            {
                Values = new ObservableCollection<double>(),
                Fill = null
            };

            Series = new ISeries[] { lineSeries };

            _valuesOnChart = (ObservableCollection<double>)((LineSeries<double>)Series[0]).Values;

            BindingContext = this;

            UIManager.Instance.CurrentProgressPage = this;

            GenerateWorkoutVolumePoints();
        }

        public void AddRandomValue()
        {
            Random random = new();

            // Add a new random value to the series
            double newValue = random.Next(101);
            _valuesOnChart.Add(newValue);
        }

        public void AddNewValue(float value)
        {
            _valuesOnChart.Add(value);
        }

        private async void OptionsButtonClicked(object sender, EventArgs e)
        {
            string selectedOption = await DisplayActionSheet("Options", "Cancel", null, "Workout Volume", "Bench Press One Rep Max");

            switch (selectedOption)
            {
                case "Workout Volume":
                    _valuesOnChart.Clear();
                    OptionsButton.Text = "Workout Volume";
                    GenerateWorkoutVolumePoints();
                    break;
                case "Bench Press One Rep Max":
                    //TODO: Add functionality of one rep max calculator with existing data, something like this:
                    //https://www.muscleandstrength.com/tools/bench-press-calculator
                    _valuesOnChart.Clear();
                    OptionsButton.Text = "Bench Press One Rep Max";
                    AddRandomValue();
                    break;
                default: //just cancel the DisplayActionSheet when "Cancel" or outside of the Sheet's window is clicked
                    break;
            }
        }

        //TODO: Optimize to calculate workout volume after saving workout, don't need to do this all here
        public void GenerateWorkoutVolumePoints()
        {
            double valueToAdd;

            for (int i = 0; i < WorkoutManager.Instance.DoneWorkouts.Count; i++)
            {
                valueToAdd = 0;

                for (int j = 0; j < WorkoutManager.Instance.DoneWorkouts[i].Exercises.Count; j++)
                {
                    for (int k = 0; k < WorkoutManager.Instance.DoneWorkouts[i].Exercises[j].Series.Count; k++)
                    {
                        valueToAdd += WorkoutManager.Instance.DoneWorkouts[i].Exercises[j].Series[k].AmountOfReps *
                            WorkoutManager.Instance.DoneWorkouts[i].Exercises[j].Series[k].WeightOnRep;
                    }
                }

                _valuesOnChart.Add(valueToAdd);
            }
        }
    }
}
