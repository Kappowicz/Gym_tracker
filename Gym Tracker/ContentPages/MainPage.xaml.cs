namespace Gym_Tracker;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();

        UIManager.Instance.CurrentMainPage = this;

        InitializeWorkoutButtons();
        InitializeCreateWorkoutButton();
    }

    protected override void OnAppearing()
    {
        base.OnDisappearing();

        WorkoutManager.Instance.CurrentWorkoutIndex = -1;
    }

    public void InitializeWorkoutButtons()
    {
        for (int i = WorkoutManager.Instance.SavedWorkouts.Count - 1; i >= 0; i--) //Show the workouts from new to last
        {
            Button currentButton = new()
            {
                Text = WorkoutManager.Instance.SavedWorkouts[i].Name,
                HorizontalOptions = LayoutOptions.Center,
            };

            MainPageVerticalStackLayout.Children.Add(currentButton);

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

        MainPageVerticalStackLayout.Children.Add(createWorkoutButton);

        createWorkoutButton.Clicked += OnCreateNewWorkoutButtonClicked;
    }

    public void OnCreateNewWorkoutButtonClicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new CreateNewWorkout());
    }

    public void OnLoadWorkoutButtonClicked(int index)
    {
        Console.WriteLine("(Load Workout) This button index: " + index);

        Navigation.PushAsync(new LoadWorkout(index));
    }

    public void AddButtonForCreatedWorkout()
    {
        Button currentButton = new()
        {
            Text = WorkoutManager.Instance.SavedWorkouts[^1].Name, //^1 is equal to WorkoutManager.Instance.SavedWorkouts.Count - 1
            HorizontalOptions = LayoutOptions.Center,
        };

        currentButton.Clicked += (sender, e) => OnLoadWorkoutButtonClicked(WorkoutManager.Instance.SavedWorkouts.Count - 1);
        MainPageVerticalStackLayout.Children.Insert(UIManager.NewWorkoutButtonInMainMenuIndex, currentButton);
    }
}

