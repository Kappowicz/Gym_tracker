using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using System.Threading.Tasks;
namespace Gym_Tracker;

public partial class ChooseExercise : ContentPage
{
    private readonly VerticalStackLayout stackLayout;

    public ChooseExercise()
	{
		InitializeComponent();

        stackLayout = (VerticalStackLayout)FindByName("ChooseExerciseVerticalStackLayout");

        for(int i = 0; i < WorkoutManager.Instance.SavedExercies.Count; i++)
        {
            //Creates grid with a small image on the left and a button with name on the right 
            Button currentButton = new()
            {
                Text = WorkoutManager.Instance.SavedExercies[i].Name,
                HorizontalOptions = LayoutOptions.Fill
            };

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
}