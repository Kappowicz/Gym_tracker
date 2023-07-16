using Gym_Tracker.Interfaces;
using Gym_Tracker.Managers;
using static Gym_Tracker.Managers.WorkoutManager;

namespace Gym_Tracker;

public partial class CreateNewWorkout : ContentPage, IChosenIndex
{
    private Workout _currentlyCreatedWorkout;

    public CreateNewWorkout()
    {
        InitializeComponent();

        _currentlyCreatedWorkout = new Workout();
    }

    public void SaveAndGoBackButtonClicked(object sender, EventArgs e)
    {
        if (!IsWorkoutNameCorrectAndAvaliable())
        {
            return;
        }

        SaveWorkout();

        UIManager.Instance.CurrentMainPage.AddButtonForCreatedWorkout();

        _ = Navigation.PopAsync();
    }

    public void SaveWorkout()
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

    public void AddNewExerciseButtonClicked(object sender, EventArgs e)
    {
        _ = Navigation.PushAsync(new ChooseExercise(this));
    }

    public async Task DisplayPopUpWorkoutNameTaken()
    {
        await DisplayAlert("Workout Name", "Workout name is taken.", "OK");
    }

    public async Task DisplayPopUpEmptyWorkoutName()
    {
        await DisplayAlert("Workout Name", "Workout name cannot be empty.", "OK");
    }

    public void IndexChosen(int exerciseIndex)
    {
        _ = Navigation.PopAsync();

        //there has to be copying constructor to correctly update exercise data later
        _currentlyCreatedWorkout.Exercises.Add(new Exercise(WorkoutManager.Instance.SavedExercises[exerciseIndex]));

        Button currentButton = new()
        {
            Text = WorkoutManager.Instance.SavedExercises[exerciseIndex].Name,
            HorizontalOptions = LayoutOptions.Fill
        };

        CreateNewWorkoutVerticalStackLayout.Insert(UIManager.NewExerciseButtonInCreateNewWorkoutIndex, currentButton);
    }
}