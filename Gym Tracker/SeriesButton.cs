namespace Gym_Tracker
{
    internal class SeriesButton : ContentView
    {
        public Grid SeriesButtonGrid;
        public Label AmountOfRepsLabel;
        public Label WeightOnRepLabel;
        public Button DoneButton;
        public bool IsSeriesDone;

        public SeriesButton(int amountOfReps, float weightOnRep, bool isSeriesDone = false)
        {
            Label amountOfRepsLabel = new()
            {
                Text = amountOfReps.ToString(),
                TextColor = Color.FromRgb(255, 255, 255),
                HorizontalTextAlignment = TextAlignment.Center,
                BackgroundColor = isSeriesDone ? Color.FromRgb(127, 255, 0) : Color.FromRgb(128, 128, 128)
            };

            Label weightOnRepLabel = new()
            {
                Text = weightOnRep.ToString(),
                TextColor = Color.FromRgb(255, 255, 255),
                HorizontalTextAlignment = TextAlignment.Center,
                BackgroundColor = isSeriesDone ? Color.FromRgb(127, 255, 0) : Color.FromRgb(128, 128, 128)
            };

            Button doneButton = new()
            {
                Text = isSeriesDone ? "Cancel" : "Done",
                HorizontalOptions = LayoutOptions.Fill
            };

            AmountOfRepsLabel = amountOfRepsLabel;
            WeightOnRepLabel = weightOnRepLabel;
            DoneButton = doneButton;
            IsSeriesDone = isSeriesDone;

            DoneButton.Clicked += (sender, e) => DoneButtonClicked();

            SeriesButtonGrid = new Grid();
            SeriesButtonGrid.RowDefinitions.Add(new RowDefinition());
            SeriesButtonGrid.ColumnDefinitions.Add(new ColumnDefinition());
            SeriesButtonGrid.ColumnDefinitions.Add(new ColumnDefinition());
            SeriesButtonGrid.ColumnDefinitions.Add(new ColumnDefinition());

            SeriesButtonGrid.Children.Add(amountOfRepsLabel);
            SeriesButtonGrid.Children.Add(weightOnRepLabel);
            SeriesButtonGrid.Children.Add(doneButton);

            Grid.SetRow(amountOfRepsLabel, 0);
            Grid.SetColumn(amountOfRepsLabel, 0);

            Grid.SetRow(weightOnRepLabel, 0);
            Grid.SetColumn(weightOnRepLabel, 1);

            Grid.SetRow(doneButton, 0);
            Grid.SetColumn(doneButton, 2);
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
