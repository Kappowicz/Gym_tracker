using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using System.Threading.Tasks;
namespace Gym_Tracker;

public partial class ChooseExercise : ContentPage
{
    private readonly VerticalStackLayout stackLayout;
    private readonly IChooseExercise chooseExerciseHandler;

    public ChooseExercise(IChooseExercise chooseExerciseHandler)
	{
		InitializeComponent();

        stackLayout = (VerticalStackLayout)FindByName("ChooseExerciseVerticalStackLayout");

        this.chooseExerciseHandler = chooseExerciseHandler;

        for (int i = 0; i < WorkoutManager.Instance.SavedExercies.Count; i++)
        {
            //Creates grid with a small image on the left and a button with name on the right 
            Button currentButton = new()
            {
                Text = WorkoutManager.Instance.SavedExercies[i].Name,
                HorizontalOptions = LayoutOptions.Fill
            };

            int currentIndex = i; //this makes pass to function actual index of currently created button
            currentButton.Clicked += (sender, e) => OnChoosenExerciseButtonClicked(currentIndex);

            Image currentImage = new()
            {
                Source = ImageSource.FromFile(WorkoutManager.Instance.SavedExercies[i].ImagePath),
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

    public void OnChoosenExerciseButtonClicked(int exerciseIndex)
    {
        chooseExerciseHandler.ExerciseChoosen(exerciseIndex);

        Navigation.PopAsync();
    }
}