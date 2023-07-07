namespace Gym_Tracker;

public partial class LoadWorkout : ContentPage
{
    private readonly VerticalStackLayout stackLayout;
    private readonly int thisWorkoutIndex;
    public LoadWorkout(int thisWorkoutIndex)
    {
        InitializeComponent();

        stackLayout = (VerticalStackLayout)FindByName("LoadWorkoutVerticalStackLayout");

        this.thisWorkoutIndex = thisWorkoutIndex;

        GenerateWorkoutExercises();
    }

    public void GenerateWorkoutExercises()
    {
        for (int i = 0; i < WorkoutManager.Instance.SavedWorkouts[thisWorkoutIndex].Exercises.Count; i++)
        {
            Button currentButton = new()
            {
                Text = WorkoutManager.Instance.SavedWorkouts[thisWorkoutIndex].Exercises[i].Name,
                HorizontalOptions = LayoutOptions.Center,
            };

            stackLayout.Children.Add(currentButton);

            int currentIndex = i; //this makes pass to function actual index of currently created button
            currentButton.Clicked += (sender, e) => LoadExerciseButtonClicked(currentIndex);
        }
    }

    public void LoadExerciseButtonClicked(int exerciseIndex)
    {
        Navigation.PushAsync(new LoadExercise(thisWorkoutIndex, exerciseIndex));
    }
}