﻿using Gym_Tracker.Managers;
namespace Gym_Tracker.Buttons
{
    internal sealed class SeriesButton : ContentView
    {
        public Grid SeriesButtonGrid { get; }
        public bool IsSeriesDone { get; set; }

        private readonly Entry _amountOfRepsEntry;
        private readonly Entry _weightOnRepEntry;
        private readonly Button _doneButton;

        private readonly int _thisWorkoutIndex;
        private readonly int _thisExerciseIndex;
        private readonly int _thisSeriesIndex;

        public SeriesButton(int amountOfReps, float weightOnRep, int thisWorkoutIndex, int thisExerciseIndex, int thisSeriesIndex, bool isSeriesDone = false)
        {
            _amountOfRepsEntry = new Entry
            {
                Text = amountOfReps.ToString(),
                HorizontalTextAlignment = TextAlignment.Center,
                Keyboard = Keyboard.Numeric,
            };
            _amountOfRepsEntry.Unfocused += (sender, e) => AmountOfRepsEntryUnfocused();

            _weightOnRepEntry = new()
            {
                Text = weightOnRep.ToString(),
                HorizontalTextAlignment = TextAlignment.Center,
                Keyboard = Keyboard.Numeric,
            };
            _weightOnRepEntry.Unfocused += (sender, e) => WeightOnRepEntryUnfocused();

            _thisWorkoutIndex = thisWorkoutIndex;
            _thisExerciseIndex = thisExerciseIndex;
            _thisSeriesIndex = thisSeriesIndex;
            IsSeriesDone = isSeriesDone;

            SeriesButtonGrid = new Grid();
            SeriesButtonGrid.RowDefinitions.Add(new RowDefinition());
            SeriesButtonGrid.ColumnDefinitions.Add(new ColumnDefinition());
            SeriesButtonGrid.ColumnDefinitions.Add(new ColumnDefinition());

            SeriesButtonGrid.Children.Add(_amountOfRepsEntry);
            SeriesButtonGrid.Children.Add(_weightOnRepEntry);

            Grid.SetRow(_amountOfRepsEntry, 0);
            Grid.SetColumn(_amountOfRepsEntry, 0);

            Grid.SetRow(_weightOnRepEntry, 0);
            Grid.SetColumn(_weightOnRepEntry, 1);

            if (WorkoutManager.Instance.StartedWorkoutIndex == thisWorkoutIndex)
            {
                _doneButton = new()
                {
                    Text = isSeriesDone ? "Cancel" : "Done",
                    HorizontalOptions = LayoutOptions.Fill,
                    Background = isSeriesDone ? Color.FromRgb(128, 255, 0) : Color.FromRgb(255, 255, 255),
                };

                _doneButton.Clicked += (sender, e) => DoneButtonClicked();

                SeriesButtonGrid.ColumnDefinitions.Add(new ColumnDefinition());

                SeriesButtonGrid.Children.Add(_doneButton);

                Grid.SetRow(_doneButton, 0);
                Grid.SetColumn(_doneButton, 2);
            }
            else //if it's not started workout, disable option to change values
            {
                _amountOfRepsEntry.IsReadOnly = true;
                _weightOnRepEntry.IsReadOnly = true;
            }
        }

        public void LoadExerciseDisappearing()
        {
            _ = SetDefaultValueIfNullAmountOfReps();

            _ = SetDefaultValueIfNullWeightOnRep();
        }

        private bool SetDefaultValueIfNullAmountOfReps()
        {
            if (string.IsNullOrEmpty(_amountOfRepsEntry.Text))
            {
                _amountOfRepsEntry.Text = "1";
                ChangeValueOfAmountOfReps(1);
                return true;
            }

            return false;
        }

        private bool SetDefaultValueIfNullWeightOnRep()
        {
            if (string.IsNullOrEmpty(_weightOnRepEntry.Text))
            {
                _weightOnRepEntry.Text = "1";
                ChangeValueOfWeightOnRep(1);
                return true;
            }

            return false;
        }

        private void AmountOfRepsEntryUnfocused()
        {
            if (SetDefaultValueIfNullAmountOfReps())
            {
                return;
            }

            ChangeValueOfAmountOfReps(int.Parse(_amountOfRepsEntry.Text));
        }

        private void ChangeValueOfAmountOfReps(int value)
        {
            WorkoutManager.Series thisSeries = WorkoutManager.Instance.SavedWorkouts[_thisWorkoutIndex].Exercises[_thisExerciseIndex].Series[_thisSeriesIndex];
            thisSeries.AmountOfReps = value;
            WorkoutManager.Instance.SavedWorkouts[_thisWorkoutIndex].Exercises[_thisExerciseIndex].Series[_thisSeriesIndex] = thisSeries;
        }

        private void WeightOnRepEntryUnfocused()
        {
            if (SetDefaultValueIfNullWeightOnRep())
            {
                return;
            }

            ChangeValueOfWeightOnRep(float.Parse(_weightOnRepEntry.Text));
        }

        private void ChangeValueOfWeightOnRep(float value)
        {
            WorkoutManager.Series thisSeries = WorkoutManager.Instance.SavedWorkouts[_thisWorkoutIndex].Exercises[_thisExerciseIndex].Series[_thisSeriesIndex];
            thisSeries.WeightOnRep = value;
            WorkoutManager.Instance.SavedWorkouts[_thisWorkoutIndex].Exercises[_thisExerciseIndex].Series[_thisSeriesIndex] = thisSeries;
        }

        private void DoneButtonClicked()
        {
            WorkoutManager.Series thisSeries = WorkoutManager.Instance.SavedWorkouts[_thisWorkoutIndex].Exercises[_thisExerciseIndex].Series[_thisSeriesIndex];
            thisSeries.IsDone = !thisSeries.IsDone;
            WorkoutManager.Instance.SavedWorkouts[_thisWorkoutIndex].Exercises[_thisExerciseIndex].Series[_thisSeriesIndex] = thisSeries;

            if (IsSeriesDone)
            {
                _doneButton.Background = Color.FromRgb(255, 255, 255);
                _doneButton.Text = "Done";
            }
            else
            {
                _doneButton.Background = Color.FromRgb(128, 255, 0);
                _doneButton.Text = "Cancel";
            }

            IsSeriesDone = !IsSeriesDone;
        }
    }
}
