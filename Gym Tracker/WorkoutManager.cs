namespace Gym_Tracker
{
    public class WorkoutManager
    {
        private static WorkoutManager instance;
        private const string defaultImagePath = "icon_workout.png";

        //TODO: These Saved values will be loaded from save file 
        public List<Workout> SavedWorkouts { get; }
        public List<Exercise> SavedExercies { get; }

        private WorkoutManager()
        {
            SavedWorkouts = new List<Workout> //Default workouts
            {
                new Workout(
                    "Full Body Workout",
                    new List<Exercise> { 
                        new Exercise(
                            "Bench Press",
                            new List<Series> { new Series(1, 2) }
                    )}
                ),
                new Workout(
                    "Leg Workout",
                    new List<Exercise> { 
                        new Exercise(
                            "Bench Press",
                            new List<Series> { new Series(1, 2) }
                    )}
                ),
                new Workout(
                    "Calves Workout",
                    new List<Exercise> { 
                        new Exercise(
                            "Bench Press",
                            new List<Series> { new Series(1, 2) }
                    )}
                ),
                new Workout(
                    "Chest Workout",
                    new List<Exercise> { 
                        new Exercise(
                            "Bench Press",
                            new List<Series> { new Series(1, 2) }
                    )}
                )
            };

            SavedExercies = new List<Exercise> //Default exercises
            {
                new Exercise("Bench Press"),
                new Exercise("Some other exercise"),
                new Exercise("Yes")
            };
        }

        public static WorkoutManager Instance //Singleton
        {
            get
            {
                instance ??= new WorkoutManager();
                return instance;
            }
        }

        public struct Workout
        {
            public string Name { get; set; }
            public List<Exercise> Exercises { get; set; }

            public Workout(string name, List<Exercise> exercises)
            {
                Name = name;
                Exercises = exercises;
            }

            public Workout()
            {
                Name = "";
                Exercises = new List<Exercise>();
            }
        }
        public struct Exercise
        {
            public string Name { get; set; }
            public List<Series> Series { get; set; }
            public string ImagePath { get; set; }

            public Exercise(string name, List<Series> series, string imagePath)
            {
                Name = name;
                Series = series;
                ImagePath = imagePath;
            }

            public Exercise(string name, List<Series> series)
            {
                Name = name;
                Series = series;
                ImagePath = defaultImagePath;
            }

            public Exercise(string name, string imagePath)
            {
                Name = name;
                Series = new List<Series>() { new Series(1, 2), new Series(1, 2), new Series(1, 2) };
                ImagePath = imagePath;
            }

            public Exercise(string name)
            {
                Name = name;
                Series = new List<Series>() { new Series(1, 2), new Series(1, 2), new Series(1, 2) };
                ImagePath = defaultImagePath;
            }
        }

        public struct Series
        {
            public int AmountOfReps { get; set; }
            public float WeightOnRep { get; set; }

            public Series(int amountOfReps, float weightOnRep)
            {
                AmountOfReps = amountOfReps;
                WeightOnRep = weightOnRep;
            }
        }
    }
}
