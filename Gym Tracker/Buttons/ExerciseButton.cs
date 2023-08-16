using Gym_Tracker.Managers;

namespace Gym_Tracker.Buttons
{
    internal sealed class ExerciseButton : ContentView
    {
        public Grid ExerciseButtonGrid { get; }
        public Button ThisExerciseButton { get; }

        private readonly Image _exerciseImage;

        public ExerciseButton(string exerciseName, string exerciseImagePath = UIManager.defaultImagePath)
        {
            _exerciseImage = new()
            {
                Source = ImageSource.FromFile(exerciseImagePath),
                MaximumHeightRequest = 50,
                MaximumWidthRequest = 50,
            };

            ThisExerciseButton = new()
            {
                Text = exerciseName,
                HorizontalOptions = LayoutOptions.Fill
            };

            ExerciseButtonGrid = new();
            ExerciseButtonGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto }); // Image column
            ExerciseButtonGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star }); // Button column

            ExerciseButtonGrid.Children.Add(_exerciseImage);
            ExerciseButtonGrid.Children.Add(ThisExerciseButton);

            Grid.SetRow(_exerciseImage, 0);
            Grid.SetColumn(_exerciseImage, 0);

            Grid.SetRow(ThisExerciseButton, 0);
            Grid.SetColumn(ThisExerciseButton, 1);
        }
    }
}
