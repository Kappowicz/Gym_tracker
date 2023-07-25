using Gym_Tracker.Buttons;
using Gym_Tracker.Interfaces;
using Gym_Tracker.Managers;
using static Gym_Tracker.Managers.WorkoutManager;

namespace Gym_Tracker;

public sealed partial class ChooseExercise : ContentPage
{
    private readonly IChosenIndex _chooseExerciseHandler;
    private WorkoutManager.MusclesGroups _selectedMusclesGroups = MusclesGroups.Default;
    private string _currentSearchedText = "";

    public ChooseExercise(IChosenIndex chooseExerciseHandler)
    {
        InitializeComponent();

        _chooseExerciseHandler = chooseExerciseHandler;

        GenerateMusclesGroupButtons();

        GenerateAllExercisesGrids();
    }

    public void GenerateAllExercisesGrids()
    {
        GenerateExerciseGrid(WorkoutManager.Instance.SavedExercises);
    }

    public void DeleteAllExerciseGrids()
    {
        for (int i = ChooseExerciseVerticalStackLayout.Children.Count - 1; i >= 0; i--)
        {
            ChooseExerciseVerticalStackLayout.Children.RemoveAt(i);
        }
    }

    public void OnExerciseNameSearchTextChanged(object sender, TextChangedEventArgs e)
    {
        _currentSearchedText = ((Entry)sender).Text.ToLower();

        CalculateAndDisplayExercises();
    }

    public void GenerateMusclesGroupButtons()
    {
        for (int i = 0; i < Enum.GetValues(typeof(MusclesGroups)).Length; i++)
        {
            MusclesGroups thisMuscleGroup = (MusclesGroups)Enum.GetValues(typeof(MusclesGroups)).GetValue(i);

            Button thisButton = new()
            {
                Text = thisMuscleGroup.ToString(),
                CornerRadius = 20,
            };

            int capturedValue = i;
            thisButton.Clicked += (sender, e) => OnMusclesGroupsButtonClicked(sender, e, capturedValue);

            MuscleGroupsHorizontalStackLayout.Add(thisButton);
        }
    }

    private void OnMusclesGroupsButtonClicked(object sender, EventArgs e, int value)
    {
        SetDefaultAllMuscleGroupButtonsColors();

        // Change the background color of the clicked button
        if (sender is Button clickedButton)
        {
            clickedButton.Background = Color.FromRgb(127, 255, 0);
        }

        MusclesGroups[] allMusclesGroups = (MusclesGroups[])Enum.GetValues(typeof(MusclesGroups));
        _selectedMusclesGroups = allMusclesGroups[value];

        CalculateAndDisplayExercises();
    }

    public void SetDefaultAllMuscleGroupButtonsColors()
    {
        for (int i = 0; i < MuscleGroupsHorizontalStackLayout.Children.Count; i++)
        {
            if (MuscleGroupsHorizontalStackLayout.Children[i] is Button muscleButton)
            {
                // Reset the background color of all buttons to the default color
                muscleButton.Background = Color.FromRgb(255, 255, 255);
            }
        }
    }

    public void CalculateAndDisplayExercises()
    {
        // Filter exercises based on the search text and selected muscle group
        List<ExerciseDetails> filteredExercises = WorkoutManager.Instance.SavedExercises
            .Where(exercise => exercise.Name.ToLower().Contains(_currentSearchedText))
            .Where(exercise => exercise.MusclesGroup == _selectedMusclesGroups ||
            _selectedMusclesGroups == WorkoutManager.MusclesGroups.Default)
            .ToList();

        DeleteAllExerciseGrids();

        GenerateExerciseGrid(filteredExercises);
    }

    public void GenerateExerciseGrid(List<ExerciseDetails> exerciseDetails)
    {
        for (int i = 0; i < exerciseDetails.Count; i++)
        {
            WorkoutManager.ExerciseDetails thisExercise = exerciseDetails[i];

            ExerciseButton thisExerciseButton = new(thisExercise.Name, i, _chooseExerciseHandler, thisExercise.ImagePath);

            ChooseExerciseVerticalStackLayout.Children.Add(thisExerciseButton.ExerciseButtonGrid);
        }
    }
}