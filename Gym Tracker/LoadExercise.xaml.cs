namespace Gym_Tracker;

public partial class LoadExercise : ContentPage
{
    private readonly VerticalStackLayout stackLayout;

	private readonly int thisWorkoutIndex;
	private readonly int thisExerciseIndex;

	public LoadExercise(int workoutIndex, int exerciseIndex)
	{
		InitializeComponent();

        thisWorkoutIndex = workoutIndex;
        thisExerciseIndex = exerciseIndex;

        stackLayout = (VerticalStackLayout)FindByName("LoadExerciseVerticalStackLayout");

        GenerateSeriesButtons();
    }

	public void GenerateSeriesButtons()
	{
		for (int i = 0; i < WorkoutManager.Instance.SavedWorkouts[thisWorkoutIndex].Exercises[thisExerciseIndex].Series.Count; i++)
		{
            //Creates grid with AmountOfReps, WeightOnRep and done button next to each other in this exact order
            Label amountOfRepsLabel = new()
            {
                Text = WorkoutManager.Instance.SavedWorkouts[thisWorkoutIndex].Exercises[thisExerciseIndex].Series[i].AmountOfReps.ToString(),
                TextColor = Color.FromRgb(255, 255, 255),
                HorizontalTextAlignment = TextAlignment.Center,
                BackgroundColor = Color.FromRgb(128, 128, 128)
            };

            Label weightOnRepLabel = new()
            {
                Text = WorkoutManager.Instance.SavedWorkouts[thisWorkoutIndex].Exercises[thisExerciseIndex].Series[i].WeightOnRep.ToString(),
                TextColor = Color.FromRgb(255, 255, 255),
                HorizontalTextAlignment = TextAlignment.Center, 
                BackgroundColor = Color.FromRgb(128, 128, 128)
            };

            Button doneButton = new()
            {
                Text = "Done",
                HorizontalOptions = LayoutOptions.Fill
            };

            int currentIndex = i; // Capture the current index value
            doneButton.Clicked += (sender, e) => OnSeriesDoneButtonClicked(currentIndex);

            Grid grid = new();
            grid.RowDefinitions.Add(new RowDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition());

            grid.Children.Add(amountOfRepsLabel);
            grid.Children.Add(weightOnRepLabel);
            grid.Children.Add(doneButton);

            Grid.SetRow(amountOfRepsLabel, 0);
            Grid.SetColumn(amountOfRepsLabel, 0);

            Grid.SetRow(weightOnRepLabel, 0);
            Grid.SetColumn(weightOnRepLabel, 1);

            Grid.SetRow(doneButton, 0);
            Grid.SetColumn(doneButton, 2);

            stackLayout.Children.Add(grid);
        }
	}

    public void OnSeriesDoneButtonClicked(int index)
    {
        //TODO: Add logic to "Done" button clicked
    }
}