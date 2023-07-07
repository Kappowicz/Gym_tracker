using static Gym_Tracker.WorkoutManager;

namespace Gym_Tracker;

public partial class ChooseExercise : ContentPage
{
    private readonly VerticalStackLayout stackLayout;
    private readonly IChosenIndex chooseExerciseHandler;

    public ChooseExercise(IChosenIndex chooseExerciseHandler)
    {
        InitializeComponent();

        stackLayout = (VerticalStackLayout)FindByName("ChooseExerciseVerticalStackLayout");

        this.chooseExerciseHandler = chooseExerciseHandler;

        GenerateAllExercisesGrids();
    }

    public void GenerateAllExercisesGrids()
    {
        GenerateExerciseGrid(WorkoutManager.Instance.SavedExercises);
    }

    public void DeleteAllExerciseGrids()
    {
        for (int i = stackLayout.Children.Count - 1; i >= 1; i--) //the first element is Entry
        {
            stackLayout.Children.RemoveAt(i);
        }
    }

    public void OnChosenExerciseButtonClicked(int exerciseIndex)
    {
        chooseExerciseHandler.IndexChosen(exerciseIndex);

        Navigation.PopAsync();
    }

    public void OnExerciseNameSearchTextChanged(object sender, TextChangedEventArgs e)
    {
        string searchText = ((Entry)sender).Text.ToLower();

        // Filter exercises based on the search text
        List<Exercise> filteredExercises = WorkoutManager.Instance.SavedExercises
            .Where(exercise => exercise.Name.ToLower().Contains(searchText))
            .ToList();

        //TODO: It has a lot of room for improvement, for example save each added character and deleted buttons to set, and add deleted buttons when character deleted,
        DeleteAllExerciseGrids();

        GenerateExerciseGrid(filteredExercises);
    }

    public void GenerateExerciseGrid(List<Exercise> exercises)
    {
        for (int i = 0; i < exercises.Count; i++)
        {
            // Creates grid with a small image on the left and a button with name on the right 
            Button currentButton = new()
            {
                Text = exercises[i].Name,
                HorizontalOptions = LayoutOptions.Fill
            };

            int currentIndex = i; // Capture the current index value
            currentButton.Clicked += (sender, e) => OnChosenExerciseButtonClicked(currentIndex);

            Image currentImage = new()
            {
                Source = ImageSource.FromFile(exercises[i].ImagePath),
                MaximumHeightRequest = 50,
                MaximumWidthRequest = 50,
            };

            Grid grid = new();
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto }); // Image column
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star }); // Button column

            grid.Children.Add(currentImage);
            grid.Children.Add(currentButton);

            Grid.SetRow(currentImage, 0);
            Grid.SetColumn(currentImage, 0);

            Grid.SetRow(currentButton, 0);
            Grid.SetColumn(currentButton, 1);

            stackLayout.Children.Add(grid);
        }
    }
}