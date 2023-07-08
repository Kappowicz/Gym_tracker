namespace Gym_Tracker;

public partial class LoadExercise : ContentPage
{
    private readonly int thisWorkoutIndex;
    private readonly int thisExerciseIndex;

    public LoadExercise(int workoutIndex, int exerciseIndex)
    {
        InitializeComponent();

        if (LoadExerciseVerticalStackLayout.Count == 1) workoutIndex = 0;

        thisWorkoutIndex = workoutIndex;
        thisExerciseIndex = exerciseIndex;

        GenerateSeriesButtons();
    }

    public void GenerateSeriesButtons()
    {
        for (int i = 0; i < WorkoutManager.Instance.SavedWorkouts[thisWorkoutIndex].Exercises[thisExerciseIndex].Series.Count; i++)
        {
            WorkoutManager.Series thisSeries = WorkoutManager.Instance.SavedWorkouts[thisWorkoutIndex].Exercises[thisExerciseIndex].Series[i];

            SeriesButton thisSeriesButton = new(thisSeries.AmountOfReps, thisSeries.WeightOnRep);

            LoadExerciseVerticalStackLayout.Add(thisSeriesButton.SeriesButtonGrid);
        }
    }
}