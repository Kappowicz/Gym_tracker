using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym_Tracker.Buttons
{
    internal class ExerciseButton : ContentView
    {
        public Grid ExerciseButtonGrid { get; }

        private Image ExerciseImage { get; }
        private Button ThisExerciseButton { get; }

        public ExerciseButton(string exerciseName, int thisExerciseIndex, IChosenIndex chooseExerciseHandler, string exerciseImagePath = UIManager.defaultImagePath)
        {
            ExerciseImage = new()
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

            ThisExerciseButton.Clicked += (sender, e) => chooseExerciseHandler.IndexChosen(thisExerciseIndex);

            ExerciseButtonGrid = new();
            ExerciseButtonGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto }); // Image column
            ExerciseButtonGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star }); // Button column

            ExerciseButtonGrid.Children.Add(ExerciseImage);
            ExerciseButtonGrid.Children.Add(ThisExerciseButton);

            Grid.SetRow(ExerciseImage, 0);
            Grid.SetColumn(ExerciseImage, 0);

            Grid.SetRow(ThisExerciseButton, 0);
            Grid.SetColumn(ThisExerciseButton, 1);
        }
    }
}
