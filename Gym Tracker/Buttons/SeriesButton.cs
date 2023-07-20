using Gym_Tracker.Managers;

namespace Gym_Tracker.Buttons
{
    internal sealed class SeriesButton : ContentView
    {
        public Grid SeriesButtonGrid { get; }
        public bool IsSeriesDone { get; set; }

        private readonly Label _amountOfRepsLabel;
        private readonly Label _weightOnRepLabel;
        private readonly Button _doneButton;

        private readonly int _thisWorkoutIndex;
        private readonly int _thisExerciseIndex;
        private readonly int _thisSeriesIndex;

        public SeriesButton(int amountOfReps, float weightOnRep, int thisWorkoutIndex, int thisExerciseIndex, int thisSeriesIndex, bool isSeriesDone = false)
        {
            _amountOfRepsLabel = new()
            {
                Text = amountOfReps.ToString(),
                TextColor = Color.FromRgb(255, 255, 255),
                HorizontalTextAlignment = TextAlignment.Center,
                BackgroundColor = isSeriesDone ? Color.FromRgb(127, 255, 0) : Color.FromRgb(128, 128, 128)
            };

            _weightOnRepLabel = new()
            {
                Text = weightOnRep.ToString(),
                TextColor = Color.FromRgb(255, 255, 255),
                HorizontalTextAlignment = TextAlignment.Center,
                BackgroundColor = isSeriesDone ? Color.FromRgb(127, 255, 0) : Color.FromRgb(128, 128, 128)
            };

            _doneButton = new()
            {
                Text = isSeriesDone ? "Cancel" : "Done",
                HorizontalOptions = LayoutOptions.Fill
            };

            _thisWorkoutIndex = thisWorkoutIndex;
            _thisExerciseIndex = thisExerciseIndex;
            _thisSeriesIndex = thisSeriesIndex;
            IsSeriesDone = isSeriesDone;

            _doneButton.Clicked += (sender, e) => DoneButtonClicked();

            SeriesButtonGrid = new Grid();
            SeriesButtonGrid.RowDefinitions.Add(new RowDefinition());
            SeriesButtonGrid.ColumnDefinitions.Add(new ColumnDefinition());
            SeriesButtonGrid.ColumnDefinitions.Add(new ColumnDefinition());
            SeriesButtonGrid.ColumnDefinitions.Add(new ColumnDefinition());

            SeriesButtonGrid.Children.Add(_amountOfRepsLabel);
            SeriesButtonGrid.Children.Add(_weightOnRepLabel);
            SeriesButtonGrid.Children.Add(_doneButton);

            Grid.SetRow(_amountOfRepsLabel, 0);
            Grid.SetColumn(_amountOfRepsLabel, 0);

            Grid.SetRow(_weightOnRepLabel, 0);
            Grid.SetColumn(_weightOnRepLabel, 1);

            Grid.SetRow(_doneButton, 0);
            Grid.SetColumn(_doneButton, 2);
        }

        public void DoneButtonClicked()
        {
            WorkoutManager.Series thisSeries = WorkoutManager.Instance.SavedWorkouts[_thisWorkoutIndex].Exercises[_thisExerciseIndex].Series[_thisSeriesIndex];
            thisSeries.IsDone = !thisSeries.IsDone;
            WorkoutManager.Instance.SavedWorkouts[_thisWorkoutIndex].Exercises[_thisExerciseIndex].Series[_thisSeriesIndex] = thisSeries;

            if (IsSeriesDone)
            {
                _amountOfRepsLabel.BackgroundColor = Color.FromRgb(128, 128, 128);
                _weightOnRepLabel.BackgroundColor = Color.FromRgb(128, 128, 128);
                _doneButton.Text = "Done";
            }
            else
            {
                _amountOfRepsLabel.BackgroundColor = Color.FromRgb(127, 255, 0);
                _weightOnRepLabel.BackgroundColor = Color.FromRgb(127, 255, 0);
                _doneButton.Text = "Cancel";
            }

            IsSeriesDone = !IsSeriesDone;
        }
    }
}
