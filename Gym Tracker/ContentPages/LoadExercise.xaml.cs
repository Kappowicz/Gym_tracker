using Gym_Tracker.Buttons;
using Gym_Tracker.Managers;

namespace Gym_Tracker;

public sealed partial class LoadExercise : ContentPage
{
    private readonly int _thisWorkoutIndex;
    private readonly int _thisExerciseIndex;

    private readonly List<SeriesButton> _seriesButtons = new();

    public LoadExercise(int workoutIndex, int exerciseIndex)
    {
        InitializeComponent();

        _thisWorkoutIndex = workoutIndex;
        _thisExerciseIndex = exerciseIndex;

        WorkoutManager.Exercise thisExercise = WorkoutManager.Instance.SavedWorkouts[_thisWorkoutIndex].Exercises[_thisExerciseIndex];

        LoadExerciseContentPage.Title = WorkoutManager.GetThisExerciseDetails(thisExercise.ThisExerciseDetailsIndex).Name;

        GenerateSeriesButtons();
    }

    private void GenerateSeriesButtons()
    {
        for (int i = 0; i < WorkoutManager.Instance.SavedWorkouts[_thisWorkoutIndex].Exercises[_thisExerciseIndex].Series.Count; i++)
        {
            WorkoutManager.Series thisSeries = WorkoutManager.Instance.SavedWorkouts[_thisWorkoutIndex].Exercises[_thisExerciseIndex].Series[i];

            SeriesButton thisSeriesButton = new(thisSeries.AmountOfReps, thisSeries.WeightOnRep, _thisWorkoutIndex, _thisExerciseIndex, i, this, thisSeries.IsDone);
            _seriesButtons.Add(thisSeriesButton);

            LoadExerciseVerticalStackLayout.Add(thisSeriesButton.SeriesButtonGrid);
        }

        if (WorkoutManager.Instance.StartedWorkoutIndex == _thisWorkoutIndex)
        {
            Button createNewSeriesButton = new()
            {
                Text = "Create New Series",
            };

            createNewSeriesButton.Clicked += (sender, e) => CreateNewSeriesButtonClicked();

            LoadExerciseVerticalStackLayout.Add(createNewSeriesButton);
        }
    }

    private void CreateNewSeriesButtonClicked()
    {
        WorkoutManager.Series seriesToAdd = WorkoutManager.Instance.SavedWorkouts[_thisWorkoutIndex].Exercises[_thisExerciseIndex].Series.Count > 0
            ? new(WorkoutManager.Instance.SavedWorkouts[_thisWorkoutIndex].Exercises[_thisExerciseIndex].Series[^1])
            : new(1, 1);
        WorkoutManager.Instance.SavedWorkouts[_thisWorkoutIndex].Exercises[_thisExerciseIndex].Series.Add(seriesToAdd);

        int thisSeriesIndex = WorkoutManager.Instance.SavedWorkouts[_thisWorkoutIndex].Exercises[_thisExerciseIndex].Series.Count - 1;
        SeriesButton thisSeriesButton = new(seriesToAdd.AmountOfReps, seriesToAdd.WeightOnRep, _thisWorkoutIndex, _thisExerciseIndex, thisSeriesIndex, this, seriesToAdd.IsDone);
        _seriesButtons.Add(thisSeriesButton);

        LoadExerciseVerticalStackLayout.Insert(LoadExerciseVerticalStackLayout.Children.Count - 1, thisSeriesButton.SeriesButtonGrid);
    }

    //TODO: Change this to some better event handling option
    protected override void OnDisappearing()
    {
        for (int i = 0; i < _seriesButtons.Count; i++)
        {
            _seriesButtons[i].LoadExerciseDisappearing();
        }

        base.OnDisappearing();
    }

    public void DeleteSeriesButtonClicked(int deletedSeriesButtonIndex)
    {
        LoadExerciseVerticalStackLayout.RemoveAt(deletedSeriesButtonIndex);
        _seriesButtons.RemoveAt(deletedSeriesButtonIndex);
        WorkoutManager.Instance.SavedWorkouts[_thisWorkoutIndex].Exercises[_thisExerciseIndex].Series.RemoveAt(deletedSeriesButtonIndex);

        for (int i = deletedSeriesButtonIndex; i < _seriesButtons.Count; i++)
        {
            _seriesButtons[i].ThisSeriesIndex--;
        }
    }
}