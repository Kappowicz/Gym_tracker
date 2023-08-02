using Gym_Tracker.Managers;

namespace Gym_Tracker;

public sealed partial class LoadWorkout : ContentPage
{
    private readonly int _thisWorkoutIndex;
    private float _thisWorkoutVolume;

    public LoadWorkout(int thisWorkoutIndex)
    {
        InitializeComponent();

        NavigatedTo += (sender, e) => LoadWorkoutWasLoaded();

        _thisWorkoutIndex = thisWorkoutIndex;

        WorkoutManager.Instance.CurrentWorkoutIndex = thisWorkoutIndex;

        WorkoutManager.Workout thisWorkout = WorkoutManager.Instance.SavedWorkouts[_thisWorkoutIndex];

        LoadWorkoutContentPage.Title = thisWorkout.Name;

        GenerateWorkoutExercises();
    }

    private void LoadWorkoutWasLoaded()
    {
        //Update displayed workout volume
        //TODO: Check if some series was done, if no then don't calculate volume
        _thisWorkoutVolume = WorkoutManager.CalculateWorkoutVolume(WorkoutManager.Instance.SavedWorkouts[WorkoutManager.Instance.CurrentWorkoutIndex]);
        LoadWorkoutVolumeText.Text = $"Workout Volume: {_thisWorkoutVolume}kg.";

        if (WorkoutManager.Instance.IsWorkoutStarted)
        {
            SetStopWorkoutText();
        }
    }

    private void SetStopWorkoutText()
    {
        StartStopWorkoutButton.Text = _thisWorkoutVolume > 0 ? "Stop workout and save progress" : "Stop workout";
    }

    private void GenerateWorkoutExercises()
    {
        for (int i = 0; i < WorkoutManager.Instance.SavedWorkouts[_thisWorkoutIndex].Exercises.Count; i++)
        {
            WorkoutManager.Exercise thisExercise = WorkoutManager.Instance.SavedWorkouts[_thisWorkoutIndex].Exercises[i];

            Button currentButton = new()
            {
                Text = WorkoutManager.GetThisExerciseDetails(thisExercise.ThisExerciseDetailsIndex).Name,
                HorizontalOptions = LayoutOptions.Center,
            };

            LoadWorkoutVerticalStackLayout.Children.Insert(UIManager.NewExerciseInLoadWorkoutIndex, currentButton);

            int currentIndex = i; //this makes pass to function actual index of currently created button
            currentButton.Clicked += (sender, e) => LoadExerciseButtonClicked(currentIndex);
        }
    }

    private void LoadExerciseButtonClicked(int exerciseIndex)
    {
        if (!WorkoutManager.Instance.IsWorkoutStarted)
        {
            return;
        }

        Console.WriteLine("(Load Exercise) This button index: " + exerciseIndex);

        _ = Navigation.PushAsync(new LoadExercise(_thisWorkoutIndex, exerciseIndex));
    }

    private void StartStopWorkoutButtonClicked(object sender, EventArgs e)
    {
        if (WorkoutManager.Instance.IsWorkoutStarted)
        {
            StopWorkout();
        }
        else
        {
            StartWorkout();
        }
    }

    private void StartWorkout()
    {
        WorkoutManager.Instance.IsWorkoutStarted = true;

        SetStopWorkoutText();
    }

    private void StopWorkout()
    {
        WorkoutManager.Instance.IsWorkoutStarted = false;

        StartStopWorkoutButton.Text = "Start workout";

        if (_thisWorkoutVolume > 0)
        {
            //TODO: Save only done exercises and series to this variable
            WorkoutManager.Workout thisDoneWorkout = WorkoutManager.Instance.SavedWorkouts[_thisWorkoutIndex];
            WorkoutManager.Workout copiedWorkout = new(thisDoneWorkout);

            WorkoutManager.DeleteUndoneExercisesAndSeries(ref copiedWorkout);
            WorkoutManager.Instance.AddWorkoutToDoneWorkouts(copiedWorkout);

            Navigation.InsertPageBefore(new WorkoutSummary(copiedWorkout), this);
        }

        _ = Navigation.PopAsync();
    }
}