namespace Gym_Tracker;

public partial class LoadExercise : ContentPage
{
    private readonly VerticalStackLayout stackLayout;

    private readonly int thisWorkoutIndex;
    private readonly int thisExerciseIndex;

    public LoadExercise(int workoutIndex, int exerciseIndex)
    {
        InitializeComponent();

        thisWorkoutIndex = workoutIndex;
        thisExerciseIndex = exerciseIndex;

        stackLayout = (VerticalStackLayout)FindByName("LoadExerciseVerticalStackLayout");

        GenerateSeriesButtons();
    }

    public void GenerateSeriesButtons()
    {
        for (int i = 0; i < WorkoutManager.Instance.SavedWorkouts[thisWorkoutIndex].Exercises[thisExerciseIndex].Series.Count; i++)
        {
            WorkoutManager.Series thisSeries = WorkoutManager.Instance.SavedWorkouts[thisWorkoutIndex].Exercises[thisExerciseIndex].Series[i];

            SeriesButton thisSeriesButton = new(thisSeries.AmountOfReps, thisSeries.WeightOnRep);

            stackLayout.Add(thisSeriesButton.SeriesButtonGrid);
        }
    }
}