using System.Diagnostics;

namespace Gym_Tracker;

public partial class MainPage : ContentPage
{
    private readonly VerticalStackLayout stackLayout;

    public MainPage()
    {
        InitializeComponent();

        UIManager.Instance.CurrentMainPage = this;

        stackLayout = (VerticalStackLayout)FindByName("MainPageVerticalStackLayout");

        InitializeWorkoutButtons();
        InitializeCreateWorkoutButton();
    }

    public void InitializeWorkoutButtons()
    {
        for (int i = WorkoutManager.Instance.Workouts.Count - 1; i >= 0; i--) //Show the workouts from new to last
        {
            Button currentButton = new()
            {
                Text = WorkoutManager.Instance.Workouts[i].Name,
                HorizontalOptions = LayoutOptions.Center,

            };

            stackLayout.Children.Add(currentButton);

            int currentIndex = i; //this makes pass to function actual index of currently created button
            currentButton.Clicked += (sender, e) => OnLoadWorkoutButtonClicked(currentIndex);
        }
    }

    public void InitializeCreateWorkoutButton()
    {
        Button createWorkoutButton = new()
        {
            Text = "Create New Workout",
            HorizontalOptions = LayoutOptions.Center
        };

        stackLayout.Children.Add(createWorkoutButton);

        createWorkoutButton.Clicked += OnCreateNewWorkoutButtonClicked;
    }

    public void OnCreateNewWorkoutButtonClicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new CreateNewWorkout());
    }

    public void OnLoadWorkoutButtonClicked(int index)
    {
        //There will be code to open specified workout
        Debug.WriteLine("This button index: " + index);
    }

    public void AddButtonForCreatedWorkout()
    {
        Button currentButton = new()
        {
            Text = WorkoutManager.Instance.Workouts[^1].Name, //^1 is equal to WorkoutManager.Instance.Workouts.Count - 1
            HorizontalOptions = LayoutOptions.Center,

        };

        stackLayout.Children.Insert(1, currentButton);
    }
}

