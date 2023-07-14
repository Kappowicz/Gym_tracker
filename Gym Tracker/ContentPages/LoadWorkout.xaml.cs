using Gym_Tracker.Managers;

namespace Gym_Tracker;

public partial class LoadWorkout : ContentPage
{
    private readonly int _thisWorkoutIndex;

    public LoadWorkout(int thisWorkoutIndex)
    {
        InitializeComponent();

        _thisWorkoutIndex = thisWorkoutIndex;

        WorkoutManager.Instance.CurrentWorkoutIndex = thisWorkoutIndex;

        GenerateWorkoutExercises();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        //Update displayed workout volume
        //TODO: Check if some series was done, if no then don't calculate volume
        float value = WorkoutManager.Instance.CalculateWorkoutVolume(WorkoutManager.Instance.CurrentWorkoutIndex);
        LoadWorkoutVolumeText.Text = $"Workout Volume: {value}kg. Workout Index = {WorkoutManager.Instance.CurrentWorkoutIndex}";
    }

    public void GenerateWorkoutExercises()
    {
        for (int i = 0; i < WorkoutManager.Instance.SavedWorkouts[_thisWorkoutIndex].Exercises.Count; i++)
        {
            Button currentButton = new()
            {
                Text = WorkoutManager.Instance.SavedWorkouts[_thisWorkoutIndex].Exercises[i].Name,
                HorizontalOptions = LayoutOptions.Center,
            };

            LoadWorkoutVerticalStackLayout.Children.Insert(UIManager.NewExerciseInLoadWorkoutIndex, currentButton);

            int currentIndex = i; //this makes pass to function actual index of currently created button
            currentButton.Clicked += (sender, e) => LoadExerciseButtonClicked(currentIndex);
        }
    }

    public void LoadExerciseButtonClicked(int exerciseIndex)
    {
        Console.WriteLine("(Load Exercise) This button index: " + exerciseIndex);

        _ = Navigation.PushAsync(new LoadExercise(_thisWorkoutIndex, exerciseIndex));
    }

    public void StartWorkoutButtonClicked(object sender, EventArgs e)
    {
        //TODO: whole logic to control workout
    }
}