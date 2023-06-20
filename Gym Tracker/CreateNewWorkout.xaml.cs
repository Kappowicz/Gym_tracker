

namespace Gym_Tracker;

public partial class CreateNewWorkout : ContentPage
{
    public CreateNewWorkout()
    {
        InitializeComponent();
    }

    public void SaveAndGoBackButtonClicked(object sender, EventArgs e)
    {
        //There should be code to save created workout

        Navigation.PopAsync();
    }
}