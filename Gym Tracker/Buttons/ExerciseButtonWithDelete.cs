namespace Gym_Tracker.Buttons
{
    internal sealed class ExerciseButtonWithDelete : ContentView
    {
        public int ThisExerciseIndex { get; set; }
        public Grid ThisExerciseGrid { get; }

        public Button DeleteButton;
        private readonly ExerciseButton _thisExerciseButton;

        public ExerciseButtonWithDelete(string thisExerciseName, int thisExerciseIndex)
        {
            _thisExerciseButton = new(thisExerciseName);

            ThisExerciseIndex = thisExerciseIndex;

            DeleteButton = new()
            {
                Text = "Delete",
                HorizontalOptions = LayoutOptions.Start
            };

            ThisExerciseGrid = new();
            ThisExerciseGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto }); // Image column
            ThisExerciseGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star }); // Button column

            ThisExerciseGrid.Children.Add(DeleteButton);
            ThisExerciseGrid.Children.Add(_thisExerciseButton.ExerciseButtonGrid);

            Grid.SetRow(DeleteButton, 0);
            Grid.SetColumn(DeleteButton, 0);

            Grid.SetRow(_thisExerciseButton.ExerciseButtonGrid, 0);
            Grid.SetColumn(_thisExerciseButton.ExerciseButtonGrid, 1);
        }
    }
}
