namespace Gym_Tracker;

public sealed partial class AboutPage : ContentPage
{
    //Icons from flaticon has to have their own button according to flaticon's license,
    //Livecharts doesn't have to but this is cool so I'll leave it there

    private const string GithubUrl = "https://github.com/Kappowicz/Gym_tracker";
    private const string ProgressChartsUrl = "https://lvcharts.com/";
    private const string StadisticsIconUrl = "https://www.flaticon.com/free-icons/stadistics";
    private const string WorkoutIconUrl = "https://www.flaticon.com/free-icons/workout";
    private const string UpArrowIconUrl = "https://www.flaticon.com/free-icons/up-arrow";

    public AboutPage()
    {
        InitializeComponent();
    }

    private async Task OpenUrlAsync(string url)
    {
        try
        {
            _ = await Launcher.OpenAsync(url);
        }
        catch (Exception ex)
        {
            await DisplayPopUpErrorURLMessage(ex.Message);
        }
    }

    private async Task DisplayPopUpErrorURLMessage(string message)
    {
        await DisplayAlert("URL Error", $"Error occurred while opening this URL: {message}.", "OK");
    }

    private async void OpenGithubLinkButtonClicked(object sender, EventArgs e)
    {
        await OpenUrlAsync(GithubUrl);
    }

    private async void ProgressChartsButtonClicked(object sender, EventArgs e)
    {
        await OpenUrlAsync(ProgressChartsUrl);
    }

    private async void StadisticsIconButtonClicked(object sender, EventArgs e)
    {
        await OpenUrlAsync(StadisticsIconUrl);
    }

    private async void WorkoutIconButtonClicked(object sender, EventArgs e)
    {
        await OpenUrlAsync(WorkoutIconUrl);
    }

    private async void UpArrowButtonClicked(object sender, EventArgs e)
    {
        await OpenUrlAsync(UpArrowIconUrl);
    }
}