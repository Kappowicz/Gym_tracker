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

            SeriesButton thisSeriesButton = new(thisSeries.AmountOfReps, thisSeries.WeightOnRep, _thisWorkoutIndex, _thisExerciseIndex, i, thisSeries.IsDone);
            _seriesButtons.Add(thisSeriesButton);

            LoadExerciseVerticalStackLayout.Add(thisSeriesButton.SeriesButtonGrid);
        }
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
}