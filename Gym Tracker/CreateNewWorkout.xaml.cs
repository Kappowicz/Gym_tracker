using static Gym_Tracker.WorkoutManager;

namespace Gym_Tracker;

public partial class CreateNewWorkout : ContentPage, IChosenIndex
{
    private readonly VerticalStackLayout stackLayout;
    private Workout CurrentlyCreatedWorkout;

    public CreateNewWorkout()
    {
        InitializeComponent();

        stackLayout = (VerticalStackLayout)FindByName("CreateNewWorkoutVerticalStackLayout");

        CurrentlyCreatedWorkout = new Workout();
    }

    public void SaveAndGoBackButtonClicked(object sender, EventArgs e)
    {
        if (!IsWorkoutNameCorrectAndAvaliable()) return;

        SaveWorkout();

        UIManager.Instance.CurrentMainPage.AddButtonForCreatedWorkout();

        Navigation.PopAsync();
    }

    public void SaveWorkout()
    {
        CurrentlyCreatedWorkout.Name = WorkoutName.Text;

        WorkoutManager.Instance.SavedWorkouts.Add(CurrentlyCreatedWorkout);
    }

    private bool IsWorkoutNameCorrectAndAvaliable()
    {
        if (string.IsNullOrEmpty(WorkoutName.Text))
        {
            DisplayPopupEmptyWorkoutName().ConfigureAwait(false);

            return false;
        }

        if (WorkoutManager.Instance.SavedWorkouts.Any(workout => workout.Name == WorkoutName.Text))
        {
            DisplayPopupWorkoutNameTaken().ConfigureAwait(false);

            return false;
        }

        return true;
    }

    public void AddNewExerciseButtonClicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new ChooseExercise(this));
    }

    public async Task DisplayPopupWorkoutNameTaken()
    {
        await DisplayAlert("Workout Name", "Workout name is taken.", "OK");
    }

    public async Task DisplayPopupEmptyWorkoutName()
    {
        await DisplayAlert("Workout Name", "Workout name cannot be empty.", "OK");
    }

    public void IndexChosen(int exerciseIndex)
    {
        CurrentlyCreatedWorkout.Exercises.Add(WorkoutManager.Instance.SavedExercises[exerciseIndex]);

        Button currentButton = new()
        {
            Text = WorkoutManager.Instance.SavedExercises[exerciseIndex].Name,
            HorizontalOptions = LayoutOptions.Fill
        };

        stackLayout.Insert(UIManager.NewExerciseButtonInCreateNewWorkoutIndex, currentButton);
    }
}