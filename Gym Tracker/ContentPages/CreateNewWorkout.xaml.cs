using Gym_Tracker.Buttons;
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

        SetSaveAndGoBackButtonColor();

        CreateNewWorkoutVerticalStackLayout.ChildAdded += (sender, e) => SetSaveAndGoBackButtonColor();
        CreateNewWorkoutVerticalStackLayout.ChildRemoved += (sender, e) => SetSaveAndGoBackButtonColor();
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

    private void SetSaveAndGoBackButtonColor()
    {
        SaveAndGoBackButton.Background = CreateNewWorkoutVerticalStackLayout.Children.Count > 0 ? (Brush)Color.FromRgb(255, 255, 255) : (Brush)Color.FromRgb(128, 128, 128);
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

        Grid grid = new();
        grid.RowDefinitions.Add(new RowDefinition());
        grid.ColumnDefinitions.Add(new ColumnDefinition());
        grid.ColumnDefinitions.Add(new ColumnDefinition());

        ExerciseDetails thisExercise = WorkoutManager.Instance.SavedExercises[exerciseIndex];

        ExerciseButton currentButton = new(thisExercise.Name, exerciseIndex, null);

        Button deleteButton = new()
        {
            Text = "Delete",
            HorizontalOptions = LayoutOptions.Start
        };

        grid.Children.Add(deleteButton);
        grid.Children.Add(currentButton.ExerciseButtonGrid);

        Grid.SetRow(deleteButton, 0);
        Grid.SetColumn(deleteButton, 0);

        Grid.SetRow(currentButton.ExerciseButtonGrid, 0);
        Grid.SetColumn(currentButton.ExerciseButtonGrid, 1);

        CreateNewWorkoutVerticalStackLayout.Add(grid);
    }
}