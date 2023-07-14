using Gym_Tracker.Buttons;
using Gym_Tracker.Managers;

namespace Gym_Tracker;

public partial class LoadExercise : ContentPage
{
    private readonly int _thisWorkoutIndex;
    private readonly int _thisExerciseIndex;

    //TODO: Add "start workout" button and logic
    public LoadExercise(int workoutIndex, int exerciseIndex)
    {
        InitializeComponent();

        if (LoadExerciseVerticalStackLayout.Count == 1)
        {
            workoutIndex = 0;
        }

        _thisWorkoutIndex = workoutIndex;
        _thisExerciseIndex = exerciseIndex;

        GenerateSeriesButtons();
    }

    public void GenerateSeriesButtons()
    {
        for (int i = 0; i < WorkoutManager.Instance.SavedWorkouts[_thisWorkoutIndex].Exercises[_thisExerciseIndex].Series.Count; i++)
        {
            WorkoutManager.Series thisSeries = WorkoutManager.Instance.SavedWorkouts[_thisWorkoutIndex].Exercises[_thisExerciseIndex].Series[i];

            SeriesButton thisSeriesButton = new(thisSeries.AmountOfReps, thisSeries.WeightOnRep, _thisWorkoutIndex, _thisExerciseIndex, i, thisSeries.IsDone);

            LoadExerciseVerticalStackLayout.Add(thisSeriesButton.SeriesButtonGrid);
        }
    }
}