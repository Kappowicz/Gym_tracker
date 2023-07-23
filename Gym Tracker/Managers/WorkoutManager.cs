namespace Gym_Tracker.Managers
{
    public sealed class WorkoutManager
    {
        private static readonly Lazy<WorkoutManager> lazyInstance = new(() => new WorkoutManager());
        public static WorkoutManager Instance => lazyInstance.Value;

        public int CurrentWorkoutIndex { get; set; } = -1; // -1 = unset
        public bool IsWorkoutStarted { get; set; } = false;

        //TODO: These Saved values will be loaded from save file 
        public List<Workout> DoneWorkouts { get; set; }
        public List<Workout> SavedWorkouts { get; }
        public List<ExerciseDetails> SavedExercises { get; set; }

        private WorkoutManager()
        {
            SavedWorkouts = new List<Workout> //Default workouts
            {
                new Workout(
                    "Full Body Workout",
                    new List<Exercise>()
                    {
                        new Exercise(1)
                    }),
                new Workout(
                    "Full Body Workout",
                    new List<Exercise>()
                    {
                        new Exercise(1)
                    }),
                new Workout(
                    "Full Body Workout",
                    new List<Exercise>()
                    {
                        new Exercise(1)
                    }),
            };

            SavedExercises = new List<ExerciseDetails> //Default exercises
            {
                new ExerciseDetails("Bench Press", new List<float>(){10,10}),
                new ExerciseDetails("Squats"),
                new ExerciseDetails("Lat Pull ups")
            };

            DoneWorkouts = new List<Workout> {
                new Workout(
                    "Full Body Workout",
                    new List<Exercise>()
                    {
                        new Exercise(1)
                    }),
               new Workout(
                    "Full Body Workout",
                    new List<Exercise>()
                    {
                        new Exercise(1)
                    }),
               new Workout(
                    "Full Body Workout",
                    new List<Exercise>()
                    {
                        new Exercise(1)
                    }),
            };
        }

        #region Workout structures

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

        public struct Series
        {
            public int AmountOfReps { get; set; }
            public float WeightOnRep { get; set; }
            public bool IsDone { get; set; } = false;

            public Series(int amountOfReps, float weightOnRep)
            {
                AmountOfReps = amountOfReps;
                WeightOnRep = weightOnRep;
            }
        }

        public struct ExerciseDetails
        {
            public string Name { get; set; }

            public string ImagePath { get; set; }

            public List<float> PreviousThisExerciseVolume { get; set; }

            public ExerciseDetails(string name)
            {
                Name = name;
                ImagePath = UIManager.defaultImagePath;
                PreviousThisExerciseVolume = new List<float>();
            }

            public ExerciseDetails(string name, string imagePath)
            {
                Name = name;
                ImagePath = imagePath;
                PreviousThisExerciseVolume = new List<float>();
            }

            public ExerciseDetails(string name, List<float> previousThisExerciseVolume)
            {
                Name = name;
                ImagePath = UIManager.defaultImagePath;
                PreviousThisExerciseVolume = previousThisExerciseVolume;
            }

            public ExerciseDetails(string name, string imagePath, List<float> previousThisExerciseVolume)
            {
                Name = name;
                ImagePath = imagePath;
                PreviousThisExerciseVolume = previousThisExerciseVolume;
            }
        }

        public struct Exercise
        {
            public int ThisExerciseDetailsIndex { get; set; }

            public List<Series> Series { get; set; }

            public Exercise(int thisExerciseDetailIndex, List<Series> series)
            {
                ThisExerciseDetailsIndex = thisExerciseDetailIndex;
                Series = series;
            }

            public Exercise(int thisExerciseDetailIndex)
            {
                ThisExerciseDetailsIndex = thisExerciseDetailIndex;
                Series = new List<Series>()
                {
                    new Series(1,2),
                    new Series(1,2),
                    new Series(1,2)
                };
            }
        }

        #endregion

        //TODO: There is still room for improvement
        public static float CalculateWorkoutVolume(Workout workout)
        {
            return workout.Exercises
                .SelectMany(exercise => exercise.Series)
                .Where(series => series.IsDone)
                .Sum(series => series.WeightOnRep * series.AmountOfReps);
        }

        public void AddWorkoutToDoneWorkouts(Workout workout)
        {
            DoneWorkouts.Add(workout);

            //When the progress page was opened, we have to add this separately to it
            if (UIManager.Instance.CurrentProgressPage is not null)
            {
                float volumeOfThisWorkout = CalculateWorkoutVolume(workout);
                UIManager.Instance.CurrentProgressPage.AddNewValue(volumeOfThisWorkout);
            }

        }
        public static ExerciseDetails GetThisExerciseDetails(int thisExerciseDetailIndex)
        {
            return WorkoutManager.Instance.SavedExercises[thisExerciseDetailIndex];
        }

        //TODO: optimize to add to list after every done workout, don't need to calculate this all in one place and time
        public string[] GetAllDoneExercisesNames()
        {
            if (DoneWorkouts.Count == 0)
            {
                return Array.Empty<string>();
            }

            List<string> exerciseNames = new();

            for (int i = 0; i < DoneWorkouts.Count; i++)
            {
                for (int j = 0; j < DoneWorkouts[i].Exercises.Count; j++)
                {
                    string thisExerciseName = WorkoutManager.GetThisExerciseDetails(DoneWorkouts[i].Exercises[j].ThisExerciseDetailsIndex).Name;
                    if (!exerciseNames.Contains(thisExerciseName))
                    {
                        exerciseNames.Add(thisExerciseName);
                    }
                }
            }

            return exerciseNames.ToArray();
        }
    }
}
