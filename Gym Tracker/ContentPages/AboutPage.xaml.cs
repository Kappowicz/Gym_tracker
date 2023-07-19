namespace Gym_Tracker;

public partial class AboutPage : ContentPage
{
    public AboutPage()
    {
        InitializeComponent();
    }

    private async void OpenGithubLinkButtonClicked(object sender, EventArgs e)
    {
        _ = await Launcher.Default.OpenAsync("https://github.com/Kappowicz/Gym_tracker");
    }

    private async void ProgressChartsButtonClicked(object sender, EventArgs e)
    {
        _ = await Launcher.Default.OpenAsync("https://lvcharts.com/"); //That's cool stuff, I'll leave it there even if i don't have to
    }

    private async void StadisticsIconButtonClicked(object sender, EventArgs e)
    {
        _ = await Launcher.Default.OpenAsync("https://www.flaticon.com/free-icons/stadistics");
    }

    private async void WorkoutIconButtonClicked(object sender, EventArgs e)
    {
        _ = await Launcher.Default.OpenAsync("https://www.flaticon.com/free-icons/workout");
    }

    private async void UpDownArrowButtonClicked(object sender, EventArgs e)
    {
        _ = await Launcher.Default.OpenAsync("https://www.flaticon.com/free-icons/up-arrow");
    }
}