using System.Diagnostics;
using static Gym_Tracker.WorkoutManager;

namespace Gym_Tracker;

public partial class CreateNewWorkout : ContentPage, IChooseExercise
{
    private readonly VerticalStackLayout stackLayout;
    private Workout CurrentlyCreatedWorkout;
    public CreateNewWorkout()
    {
        InitializeComponent();

        stackLayout = (VerticalStackLayout)FindByName("CreatenewWorkoutVerticalStackLayout");
        CurrentlyCreatedWorkout = new Workout();
    }

    public void SaveAndGoBackButtonClicked(object sender, EventArgs e)
    {
        if (IsWorkoutNameCorrectAndAvaliable())
        {
            WorkoutManager.Instance.Workouts.Add(
            new Workout(
            WorkoutName.Text,
            new List<Exercise> { new Exercise(
                        "Bench Press",
                        new List<Series> { new Series(1, 2) }
                    )}
            ));

            UIManager.Instance.CurrentMainPage.AddButtonForCreatedWorkout();

            CurrentlyCreatedWorkout.Name = WorkoutName.Text;

            WorkoutManager.Instance.Workouts.Add(CurrentlyCreatedWorkout);

            Navigation.PopAsync();
        }
    }

    private bool IsWorkoutNameCorrectAndAvaliable()
    {
        if (string.IsNullOrEmpty(WorkoutName.Text))
        {
            DisplayPopupEmptyWorkoutName();
            
            return false;
        }

        for (int i = 0; i < WorkoutManager.Instance.Workouts.Count; i++)
        {
            if (WorkoutName.Text == WorkoutManager.Instance.Workouts[i].Name)
            {
                DisplayPopupWorkoutNameTaken();
                return false;
            }
        }

        return true;
    }

    public void AddNewExerciseButtonClicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new ChooseExercise(this));
    }

    public async void DisplayPopupWorkoutNameTaken()
    {
        await DisplayAlert("Workout Name", "Workout name is taken.", "OK");
    }

    public async void DisplayPopupEmptyWorkoutName()
    {
        await DisplayAlert("Workout Name", "Workout name cannot be empty.", "OK");
    }

    public void ExerciseChoosen(int exerciseIndex)
    {
        CurrentlyCreatedWorkout.Exercises.Add(WorkoutManager.Instance.SavedExercies[exerciseIndex]);

        Button currentButton = new()
        {
            Text = WorkoutManager.Instance.SavedExercies[exerciseIndex].Name,
            HorizontalOptions = LayoutOptions.Fill
        };

        stackLayout.Insert(2, currentButton);
    }
}