using Gym_Tracker.Managers;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using System.Collections.ObjectModel;

namespace Gym_Tracker
{
    public sealed partial class Progress : ContentPage
    {
        public ISeries[] Series { get; set; } //this has to be public to graph to work correctly
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

        public void AddNewValue(float valueToAdd)
        {
            if (valueToAdd <= 0)
            {
                return;
            }

            _valuesOnChart.Add(valueToAdd);
        }

        private async void OptionsButtonClicked(object sender, EventArgs e)
        {
            string[] existingOptions = new string[] { "Workout Volume", "Bench Press One Rep Max" };
            Dictionary<string, int> additionalOptions = WorkoutManager.Instance.GetAllDoneExercisesNames();
            string[] allOptions = existingOptions.Concat(additionalOptions.Keys).ToArray();

            string selectedOption = await DisplayActionSheet("Options", "Cancel", null, allOptions);

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
                default:
                    if (selectedOption == "Cancel" || string.IsNullOrEmpty(selectedOption))
                    {
                        break; //just cancel the DisplayActionSheet when "Cancel" or outside of the Sheet's window is clicked
                    }

                    GenerateExercisePoints(additionalOptions[selectedOption]);

                    break;

            }
        }

        //TODO: Optimize 
        public void GenerateExercisePoints(int exerciseIndex)
        {
            if (exerciseIndex < 0 || exerciseIndex > WorkoutManager.Instance.SavedExercises.Count - 1)
            {
                return;
            }

            _valuesOnChart.Clear();

            float valueToAdd;

            for (int i = 0; i < WorkoutManager.Instance.DoneWorkouts.Count; i++)
            {
                valueToAdd = 0;

                for (int j = 0; j < WorkoutManager.Instance.DoneWorkouts[i].Exercises.Count; j++)
                {
                    if (WorkoutManager.Instance.DoneWorkouts[i].Exercises[j].ThisExerciseDetailsIndex != exerciseIndex)
                    {
                        continue;
                    }

                    for (int k = 0; k < WorkoutManager.Instance.DoneWorkouts[i].Exercises[j].Series.Count; k++)
                    {
                        valueToAdd += WorkoutManager.Instance.DoneWorkouts[i].Exercises[j].Series[k].WeightOnRep *
                             WorkoutManager.Instance.DoneWorkouts[i].Exercises[j].Series[k].AmountOfReps;
                    }
                }

                AddNewValue(valueToAdd);
            }
        }

        //TODO: Optimize to calculate workout volume after saving workout, don't need to do this all here
        private void GenerateWorkoutVolumePoints()
        {
            float valueToAdd;

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

                AddNewValue(valueToAdd);
            }
        }
    }
}
