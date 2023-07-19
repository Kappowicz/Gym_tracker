using Gym_Tracker.Managers;

namespace Gym_Tracker;

public partial class WorkoutSummary : ContentPage
{
    private readonly WorkoutManager.Workout _displayedWorkout;

    private const string UpArrowImagePath = "up_arrow.png";
    private const string DownArrowImagePath = "down_arrow.png";

    public WorkoutSummary(WorkoutManager.Workout doneWorkout)
    {
        InitializeComponent();

        _displayedWorkout = doneWorkout;

        DisplayWorkout();
    }

    private void DisplayWorkout()
    {
        //TODO: Change this to class SummaryButton
        for (int i = 0; i < _displayedWorkout.Exercises.Count; i++)
        {
            //TODO: Calculate this exercise volume
            float currentExerciseVolume = 11f;

            Button currentExerciseButton = new()
            {
                Text = _displayedWorkout.Exercises[i].Name,
                HorizontalOptions = LayoutOptions.Fill
            };

            Label currentExerciseVolumeLabel = new()
            {
                Text = currentExerciseVolume.ToString()
            };

            float previousWorkoutThisExerciseVolume = 12f;

            Image currentExerciseImage = new()
            {
                Source = currentExerciseVolume > previousWorkoutThisExerciseVolume ? UpArrowImagePath : DownArrowImagePath,
                MaximumHeightRequest = 50,
                MaximumWidthRequest = 50,
            };
            //_displayedWorkout.Exercises.Clicked += (sender, e) => chooseExerciseHandler.IndexChosen(thisExerciseIndex);

            Grid currentExerciseGrid = new();
            currentExerciseGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto }); // Image column
            currentExerciseGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star }); // Button column
            currentExerciseGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star }); // Button column

            currentExerciseGrid.Children.Add(currentExerciseButton);
            currentExerciseGrid.Children.Add(currentExerciseVolumeLabel);
            currentExerciseGrid.Children.Add(currentExerciseImage);

            Grid.SetRow(currentExerciseButton, 0);
            Grid.SetColumn(currentExerciseButton, 0);

            Grid.SetRow(currentExerciseVolumeLabel, 0);
            Grid.SetColumn(currentExerciseVolumeLabel, 1);

            Grid.SetRow(currentExerciseImage, 0);
            Grid.SetColumn(currentExerciseImage, 2);

            WorkoutSummaryVerticalStackLayout.Add(currentExerciseGrid);
        }
    }
}