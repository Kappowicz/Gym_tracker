using Gym_Tracker.Interfaces;
using Gym_Tracker.Managers;
using static Gym_Tracker.Managers.WorkoutManager;

namespace Gym_Tracker;

public sealed partial class CreateNewWorkout : ContentPage, IChosenIndex
{
    private Workout _currentlyCreatedWorkout = new();

    public CreateNewWorkout()
    {
        InitializeComponent();
    }

    private void SaveAndGoBackButtonClicked(object sender, EventArgs e)
    {
        if (!IsWorkoutNameCorrectAndAvaliable())
        {
            return;
        }

        SaveWorkout();

        UIManager.Instance.CurrentMainPage.AddButtonForCreatedWorkout();

        _ = Navigation.PopAsync();
    }

    private void SaveWorkout()
    {
        _currentlyCreatedWorkout.Name = WorkoutName.Text;

        WorkoutManager.Instance.SavedWorkouts.Add(_currentlyCreatedWorkout);
    }

    private bool IsWorkoutNameCorrectAndAvaliable()
    {
        if (string.IsNullOrEmpty(WorkoutName.Text))
        {
            _ = DisplayPopUpEmptyWorkoutName().ConfigureAwait(false);

            return false;
        }

        if (WorkoutManager.Instance.SavedWorkouts.Any(workout => workout.Name == WorkoutName.Text))
        {
            _ = DisplayPopUpWorkoutNameTaken().ConfigureAwait(false);

            return false;
        }

        return true;
    }

    private void AddNewExerciseButtonClicked(object sender, EventArgs e)
    {
        _ = Navigation.PushAsync(new ChooseExercise(this));
    }

    private async Task DisplayPopUpWorkoutNameTaken()
    {
        await DisplayAlert("Workout Name", "Workout name is taken.", "OK");
    }

    private async Task DisplayPopUpEmptyWorkoutName()
    {
        await DisplayAlert("Workout Name", "Workout name cannot be empty.", "OK");
    }

    public void IndexChosen(int exerciseIndex)
    {
        _ = Navigation.PopAsync();

        //there has to be copying constructor to correctly update exercise data later
        _currentlyCreatedWorkout.Exercises.Add(new Exercise(exerciseIndex));

        Button currentButton = new()
        {
            Text = WorkoutManager.Instance.SavedExercises[exerciseIndex].Name,
            HorizontalOptions = LayoutOptions.Fill
        };

        CreateNewWorkoutVerticalStackLayout.Add(currentButton);
    }
}