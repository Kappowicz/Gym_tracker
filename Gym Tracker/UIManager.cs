namespace Gym_Tracker
{
    internal class UIManager
    {
        private static readonly Lazy<UIManager> lazyInstance = new(() => new UIManager()); //Singleton

        public static UIManager Instance => lazyInstance.Value;

        public MainPage CurrentMainPage { get; set; }

        private UIManager()
        {
        }
    }
}
