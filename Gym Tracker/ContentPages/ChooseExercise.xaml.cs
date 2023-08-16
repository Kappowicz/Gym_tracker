using Gym_Tracker.Buttons;
using Gym_Tracker.Managers;

namespace Gym_Tracker;

public sealed partial class ChooseExercise : ContentPage
{
    private WorkoutManager.MusclesGroups _selectedMusclesGroups = WorkoutManager.MusclesGroups.All;
    private string _currentSearchedText = "";

    public ChooseExercise()
    {
        InitializeComponent();

        GenerateMusclesGroupButtons();

        GenerateAllExercisesGrids();
    }

    private void GenerateAllExercisesGrids()
    {
        GenerateExerciseGrid(WorkoutManager.Instance.SavedExercises);
    }

    private void DeleteAllExerciseGrids()
    {
        for (int i = ChooseExerciseVerticalStackLayout.Children.Count - 1; i >= 0; i--)
        {
            ChooseExerciseVerticalStackLayout.Children.RemoveAt(i);
        }
    }

    private void OnExerciseNameSearchTextChanged(object sender, TextChangedEventArgs e)
    {
        _currentSearchedText = ((Entry)sender).Text.ToLower();

        CalculateAndDisplayExercises();
    }

    private void GenerateMusclesGroupButtons()
    {
        for (int i = 0; i < Enum.GetValues(typeof(WorkoutManager.MusclesGroups)).Length; i++)
        {
            WorkoutManager.MusclesGroups thisMuscleGroup = (WorkoutManager.MusclesGroups)Enum.GetValues(typeof(WorkoutManager.MusclesGroups)).GetValue(i);

            Button thisButton = new()
            {
                Text = thisMuscleGroup.ToString(),
                CornerRadius = 20,
                Background = thisMuscleGroup == _selectedMusclesGroups ? Color.FromRgb(128, 255, 0) : Color.FromRgb(255, 255, 255),
            };

            int capturedValue = i;
            thisButton.Clicked += (sender, e) => OnMusclesGroupsButtonClicked(sender, capturedValue);

            MuscleGroupsHorizontalStackLayout.Add(thisButton);
        }
    }

    private void OnMusclesGroupsButtonClicked(object sender, int value)
    {
        SetDefaultAllMuscleGroupButtonsColors();

        // Change the background color of the clicked button
        if (sender is Button clickedButton)
        {
            clickedButton.Background = Color.FromRgb(127, 255, 0);
        }

        WorkoutManager.MusclesGroups[] allMusclesGroups = (WorkoutManager.MusclesGroups[])Enum.GetValues(typeof(WorkoutManager.MusclesGroups));
        _selectedMusclesGroups = allMusclesGroups[value];

        CalculateAndDisplayExercises();
    }

    private void SetDefaultAllMuscleGroupButtonsColors()
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

    private void CalculateAndDisplayExercises()
    {
        Dictionary<WorkoutManager.ExerciseDetails, int> filteredExercises = new();

        for (int i = 0; i < WorkoutManager.Instance.SavedExercises.Count; i++)
        {
            WorkoutManager.ExerciseDetails thisExercise = WorkoutManager.Instance.SavedExercises[i];

            if (!thisExercise.Name.ToLower().Contains(_currentSearchedText))
            {
                continue;
            }

            if (thisExercise.MusclesGroup != _selectedMusclesGroups)
            {
                if (_selectedMusclesGroups != WorkoutManager.MusclesGroups.All)
                {
                    continue;
                }
            }

            filteredExercises.Add(thisExercise, i);
        }

        DeleteAllExerciseGrids();

        GenerateExerciseGrid(filteredExercises);
    }

    private void GenerateExerciseGrid(Dictionary<WorkoutManager.ExerciseDetails, int> exerciseDetails)
    {
        for (int i = 0; i < exerciseDetails.Count; i++)
        {
            WorkoutManager.ExerciseDetails thisExercise = exerciseDetails.ElementAt(i).Key;
            int thisExerciseIndex = exerciseDetails.ElementAt(i).Value;

            ExerciseButton thisExerciseButton = new(thisExercise.Name, thisExercise.ImagePath);

            int gatherIndex = thisExerciseIndex;
            thisExerciseButton.ThisExerciseButton.Clicked += (sender, e) => UIManager.Instance.CurrentCreateNewWorkout?.IndexChosen(gatherIndex);

            ChooseExerciseVerticalStackLayout.Children.Add(thisExerciseButton.ExerciseButtonGrid);
        }
    }

    //Generate whole list first time, so don't need to check anything
    private void GenerateExerciseGrid(List<WorkoutManager.ExerciseDetails> exerciseDetails)
    {
        for (int i = 0; i < exerciseDetails.Count; i++)
        {
            WorkoutManager.ExerciseDetails thisExercise = exerciseDetails[i];

            ExerciseButton thisExerciseButton = new(thisExercise.Name, thisExercise.ImagePath);

            int gatherIndex = i;
            thisExerciseButton.ThisExerciseButton.Clicked += (sender, e) => UIManager.Instance.CurrentCreateNewWorkout?.IndexChosen(gatherIndex);

            ChooseExerciseVerticalStackLayout.Children.Add(thisExerciseButton.ExerciseButtonGrid);
        }
    }
}