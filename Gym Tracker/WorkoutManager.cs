namespace Gym_Tracker
{
    internal class WorkoutManager
    {
        private static WorkoutManager _instance;
        public List<Workout> Workouts;

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
        }

        public static WorkoutManager Instance //Singleton
        {
            get
            {
                _instance ??= new WorkoutManager();
                return _instance;
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
        }
        public struct Exercise
        {
            public string Name;
            public List<Series> Series;

            public Exercise(string name, List<Series> series)
            {
                Name = name;
                Series = series;
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
