namespace Gym_Tracker
{
    internal class SeriesButton : ContentView
    {
        public Grid SeriesButtonGrid { get; }
        public bool IsSeriesDone { get; set; }

        private Label AmountOfRepsLabel { get; }
        private Label WeightOnRepLabel { get; }
        private Button DoneButton { get; }

        public SeriesButton(int amountOfReps, float weightOnRep, bool isSeriesDone = false)
        {
            AmountOfRepsLabel = new()
            {
                Text = amountOfReps.ToString(),
                TextColor = Color.FromRgb(255, 255, 255),
                HorizontalTextAlignment = TextAlignment.Center,
                BackgroundColor = isSeriesDone ? Color.FromRgb(127, 255, 0) : Color.FromRgb(128, 128, 128)
            };

            WeightOnRepLabel = new()
            {
                Text = weightOnRep.ToString(),
                TextColor = Color.FromRgb(255, 255, 255),
                HorizontalTextAlignment = TextAlignment.Center,
                BackgroundColor = isSeriesDone ? Color.FromRgb(127, 255, 0) : Color.FromRgb(128, 128, 128)
            };

            DoneButton = new()
            {
                Text = isSeriesDone ? "Cancel" : "Done",
                HorizontalOptions = LayoutOptions.Fill
            };

            IsSeriesDone = isSeriesDone;

            DoneButton.Clicked += (sender, e) => DoneButtonClicked();

            SeriesButtonGrid = new Grid();
            SeriesButtonGrid.RowDefinitions.Add(new RowDefinition());
            SeriesButtonGrid.ColumnDefinitions.Add(new ColumnDefinition());
            SeriesButtonGrid.ColumnDefinitions.Add(new ColumnDefinition());
            SeriesButtonGrid.ColumnDefinitions.Add(new ColumnDefinition());

            SeriesButtonGrid.Children.Add(AmountOfRepsLabel);
            SeriesButtonGrid.Children.Add(WeightOnRepLabel);
            SeriesButtonGrid.Children.Add(DoneButton);

            Grid.SetRow(AmountOfRepsLabel, 0);
            Grid.SetColumn(AmountOfRepsLabel, 0);

            Grid.SetRow(WeightOnRepLabel, 0);
            Grid.SetColumn(WeightOnRepLabel, 1);

            Grid.SetRow(DoneButton, 0);
            Grid.SetColumn(DoneButton, 2);
        }

        public void DoneButtonClicked()
        {
            if (IsSeriesDone)
            {
                AmountOfRepsLabel.BackgroundColor = Color.FromRgb(128, 128, 128);
                WeightOnRepLabel.BackgroundColor = Color.FromRgb(128, 128, 128);
                DoneButton.Text = "Done";
            }
            else
            {
                AmountOfRepsLabel.BackgroundColor = Color.FromRgb(127, 255, 0);
                WeightOnRepLabel.BackgroundColor = Color.FromRgb(127, 255, 0);
                DoneButton.Text = "Cancel";
            }

            IsSeriesDone = !IsSeriesDone;
        }
    }
}
