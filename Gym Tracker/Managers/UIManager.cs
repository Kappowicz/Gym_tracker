namespace Gym_Tracker.Managers
{
    internal class UIManager
    {
        private static readonly Lazy<UIManager> lazyInstance = new(() => new UIManager()); //Singleton

        //Indexes of places where this buttons should be inserted in these pages
        public const int NewWorkoutButtonInMainMenuIndex = 1;
        public const int NewExerciseButtonInCreateNewWorkoutIndex = 2;
        public const int NewExerciseInLoadWorkoutIndex = 0;
        public const string defaultImagePath = "icon_workout.png";

        public static UIManager Instance => lazyInstance.Value;

        public MainPage CurrentMainPage { get; set; }

        private UIManager()
        {
        }
    }
}
