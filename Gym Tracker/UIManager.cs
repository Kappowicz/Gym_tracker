namespace Gym_Tracker
{
    internal class UIManager
    {
        public MainPage CurrentMainPage;

        private static UIManager instance;

        public static UIManager Instance //Singleton
        {
            get
            {
                instance ??= new UIManager();
                return instance;
            }
        }
    }


}
