namespace Gym_Tracker;

public partial class AboutPage : ContentPage
{
    public AboutPage()
    {
        InitializeComponent();
    }

    private async void OpenGithubLinkButtonClicked(object sender, EventArgs e)
    {
        // Navigate to the specified URL in the system browser.
        await Launcher.Default.OpenAsync("https://github.com/Kappowicz/Gym_tracker");
    }

    private async void StadisticsIconButtonClicked(object sender, EventArgs e)
    {
        // Navigate to the specified URL in the system browser.
        await Launcher.Default.OpenAsync("https://www.flaticon.com/free-icons/stadistics");
    }

    private async void WorkoutIconButtonClicked(Object sender, EventArgs e)
    {
        // Navigate to the specified URL in the system browser.
        await Launcher.Default.OpenAsync("https://www.flaticon.com/free-icons/workout");
    }
}