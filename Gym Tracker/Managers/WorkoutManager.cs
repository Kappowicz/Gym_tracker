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
                new ExerciseDetails("Bench Press", MusclesGroups.Chest, new List<float>(){10,10}),
                new ExerciseDetails("Squats", MusclesGroups.Legs),
                new ExerciseDetails("Lat Pull ups", MusclesGroups.Back)
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

        public enum MusclesGroups
        {
            Default = 0,
            Arms,
            Chest,
            Abs,
            Legs,
            Back
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

            public Workout(Workout workout)
            {
                Name = workout.Name;
                Exercises = new(workout.Exercises);
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

            public Series(Series series)
            {
                AmountOfReps = series.AmountOfReps;
                WeightOnRep = series.WeightOnRep;
            }
        }

        public struct ExerciseDetails
        {
            public string Name { get; set; }

            public string ImagePath { get; set; } = UIManager.defaultImagePath;

            public List<float> PreviousThisExerciseVolume { get; set; } = new List<float>();

            public MusclesGroups MusclesGroup { get; set; } = MusclesGroups.Default;

            public ExerciseDetails(string name, MusclesGroups muscleGroup)
            {
                Name = name;
                MusclesGroup = muscleGroup;
            }

            public ExerciseDetails(string name, string imagePath)
            {
                Name = name;
                ImagePath = imagePath;
            }

            public ExerciseDetails(string name, MusclesGroups muscleGroup, List<float> previousThisExerciseVolume)
            {
                Name = name;
                MusclesGroup = muscleGroup;
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

            public Exercise(int thisExerciseDetailsIndex, List<Series> series)
            {
                ThisExerciseDetailsIndex = thisExerciseDetailsIndex;
                Series = series;
            }

            public Exercise(Exercise exercise)
            {
                ThisExerciseDetailsIndex = exercise.ThisExerciseDetailsIndex;
                Series = new(exercise.Series);
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

            //TODO: Check if last opened section in progresspage is workoutvolume
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
        public Dictionary<string, int> GetAllDoneExercisesNames()
        {
            if (DoneWorkouts.Count == 0)
            {
                return new Dictionary<string, int>();
            }

            Dictionary<string, int> exercises = new();

            for (int i = 0; i < DoneWorkouts.Count; i++)
            {
                for (int j = 0; j < DoneWorkouts[i].Exercises.Count; j++)
                {
                    WorkoutManager.ExerciseDetails thisExercise = WorkoutManager.GetThisExerciseDetails(DoneWorkouts[i].Exercises[j].ThisExerciseDetailsIndex);
                    if (!exercises.ContainsKey(thisExercise.Name))
                    {
                        exercises.Add(thisExercise.Name, DoneWorkouts[i].Exercises[j].ThisExerciseDetailsIndex);
                    }
                }
            }

            return exercises;
        }

        public static void DeleteUndoneExercisesAndSeries(ref Workout workout)
        {
            for (int i = 0; i < workout.Exercises.Count; i++)
            {
                Exercise thisExercise = workout.Exercises[i];

                for (int j = 0; j < thisExercise.Series.Count; j++)
                {
                    if (!thisExercise.Series[j].IsDone)
                    {
                        thisExercise.Series.RemoveAt(j);
                        j--;
                    }
                }

                //Delete empty exercises
                if (thisExercise.Series.Count == 0)
                {
                    workout.Exercises.RemoveAt(i);
                    i--;
                }
            }
        }
    }
}
