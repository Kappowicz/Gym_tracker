using Gym_Tracker.Buttons;
using static Gym_Tracker.WorkoutManager;

namespace Gym_Tracker;

public partial class ChooseExercise : ContentPage
{
    private readonly IChosenIndex _chooseExerciseHandler;

    public ChooseExercise(IChosenIndex _chooseExerciseHandler)
    {
        InitializeComponent();

        this._chooseExerciseHandler = _chooseExerciseHandler;

        GenerateAllExercisesGrids();
    }

    public void GenerateAllExercisesGrids()
    {
        GenerateExerciseGrid(WorkoutManager.Instance.SavedExercises);
    }

    public void DeleteAllExerciseGrids()
    {
        for (int i = ChooseExerciseVerticalStackLayout.Children.Count - 1; i >= 1; i--) //the first element is Entry
        {
            ChooseExerciseVerticalStackLayout.Children.RemoveAt(i);
        }
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
            WorkoutManager.Exercise thisExercise = exercises[i];

            ExerciseButton thisExerciseButton = new(thisExercise.Name, i, _chooseExerciseHandler, thisExercise.ImagePath);

            ChooseExerciseVerticalStackLayout.Children.Add(thisExerciseButton.ExerciseButtonGrid);
        }
    }
}