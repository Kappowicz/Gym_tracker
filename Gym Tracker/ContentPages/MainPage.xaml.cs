using Gym_Tracker.Managers;

namespace Gym_Tracker;

public sealed partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();

        UIManager.Instance.CurrentMainPage = this;
    }

    protected override void OnAppearing()
    {
        MainPageVerticalStackLayout.Children.Clear();

        WorkoutManager.Instance.CurrentWorkoutIndex = -1;

        InitializeWorkoutButtons();
        InitializeCreateWorkoutButton();

        base.OnAppearing();
    }

    private void InitializeWorkoutButtons()
    {
        for (int i = WorkoutManager.Instance.SavedWorkouts.Count - 1; i >= 0; i--) //Show the workouts from new to last
        {
            Button currentButton = new()
            {
                Text = WorkoutManager.Instance.SavedWorkouts[i].Name,
                HorizontalOptions = LayoutOptions.Center,
            };

            if (WorkoutManager.Instance.IsWorkoutStarted)
            {
                if (WorkoutManager.Instance.StartedWorkoutIndex == i)
                {
                    currentButton.Background = Color.FromRgb(128, 255, 0);
                }
            }

            MainPageVerticalStackLayout.Children.Add(currentButton);

            int currentIndex = i; //this makes pass to function actual index of currently created button
            currentButton.Clicked += (sender, e) => OnLoadWorkoutButtonClicked(currentIndex);
        }
    }

    private void InitializeCreateWorkoutButton()
    {
        Button createWorkoutButton = new()
        {
            Text = "Create New Workout",
            HorizontalOptions = LayoutOptions.Center
        };

        MainPageVerticalStackLayout.Children.Add(createWorkoutButton);

        createWorkoutButton.Clicked += OnCreateNewWorkoutButtonClicked;
    }

    private void OnCreateNewWorkoutButtonClicked(object sender, EventArgs e)
    {
        _ = Navigation.PushAsync(new CreateNewWorkout());
    }

    private void OnLoadWorkoutButtonClicked(int index)
    {
        Console.WriteLine("(Load Workout) This button index: " + index);

        _ = Navigation.PushAsync(new LoadWorkout(index));
    }

    public void AddButtonForCreatedWorkout()
    {
        Button currentButton = new()
        {
            Text = WorkoutManager.Instance.SavedWorkouts[^1].Name, //^1 is equal to WorkoutManager.Instance.SavedWorkouts.Count - 1
            HorizontalOptions = LayoutOptions.Center,
        };

        currentButton.Clicked += (sender, e) => OnLoadWorkoutButtonClicked(WorkoutManager.Instance.SavedWorkouts.Count - 1);
        MainPageVerticalStackLayout.Children.Add(currentButton);
    }
}

