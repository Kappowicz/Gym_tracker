namespace Gym_Tracker.Managers
{
    internal sealed class UIManager
    {
        private static readonly Lazy<UIManager> lazyInstance = new(() => new UIManager()); //Singleton
        public static UIManager Instance => lazyInstance.Value;

        public const string defaultImagePath = "icon_workout.png";

        public MainPage CurrentMainPage { get; set; }
        public Progress CurrentProgressPage { get; set; }
        public CreateNewWorkout CurrentCreateNewWorkout { get; set; }

        private UIManager()
        {
        }
    }
}
