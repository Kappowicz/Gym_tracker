namespace Gym_Tracker
{
    internal class WorkoutManager
    {
        private static WorkoutManager instance;
        public List<Workout> Workouts;
        public List<Exercise> SavedExercies;

        private const string defaultImagePath = "icon_workout.png";

        private WorkoutManager()
        {
            Workouts = new List<Workout> //Default Workout
            {
                new Workout(
                    "Full Body Workout",
                    new List<Exercise> { new Exercise(
                        "Bench Press",
                        new List<Series> { new Series(1, 2) }
                    )}
                ),
                new Workout(
                    "Leg Workout",
                    new List<Exercise> { new Exercise(
                        "Bench Press",
                        new List<Series> { new Series(1, 2) }
                    )}
                ),
                new Workout(
                    "Calves Workout",
                    new List<Exercise> { new Exercise(
                        "Bench Press",
                        new List<Series> { new Series(1, 2) }
                    )}
                ),
                new Workout(
                    "Chest Workout",
                    new List<Exercise> { new Exercise(
                        "Bench Press",
                        new List<Series> { new Series(1, 2) }
                    )}
                )
            };

            SavedExercies = new List<Exercise>
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
            public string Name;
            public List<Exercise> Exercises;

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
            public string Name;
            public List<Series> Series;
            public string ImagePath;

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
                Series = new List<Series>() { new Series(1,2), new Series(1, 2), new Series(1, 2)};
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
            public int AmountOfReps;
            public float WeightOnRep;

            public Series(int amountOfReps, float weightOnRep)
            {
                AmountOfReps = amountOfReps;
                WeightOnRep = weightOnRep;
            }
        }
    }
}
