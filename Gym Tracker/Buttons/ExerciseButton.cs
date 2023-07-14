namespace Gym_Tracker.Buttons
{
    internal class ExerciseButton : ContentView
    {
        public Grid ExerciseButtonGrid { get; }

        private readonly Image _exerciseImage;
        private readonly Button _thisExerciseButton;

        public ExerciseButton(string exerciseName, int thisExerciseIndex, IChosenIndex chooseExerciseHandler, string exerciseImagePath = UIManager.defaultImagePath)
        {
            _exerciseImage = new()
            {
                Source = ImageSource.FromFile(exerciseImagePath),
                MaximumHeightRequest = 50,
                MaximumWidthRequest = 50,
            };

            _thisExerciseButton = new()
            {
                Text = exerciseName,
                HorizontalOptions = LayoutOptions.Fill
            };

            _thisExerciseButton.Clicked += (sender, e) => chooseExerciseHandler.IndexChosen(thisExerciseIndex);

            ExerciseButtonGrid = new();
            ExerciseButtonGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto }); // Image column
            ExerciseButtonGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star }); // Button column

            ExerciseButtonGrid.Children.Add(_exerciseImage);
            ExerciseButtonGrid.Children.Add(_thisExerciseButton);

            Grid.SetRow(_exerciseImage, 0);
            Grid.SetColumn(_exerciseImage, 0);

            Grid.SetRow(_thisExerciseButton, 0);
            Grid.SetColumn(_thisExerciseButton, 1);
        }
    }
}
