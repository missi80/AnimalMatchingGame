namespace AnimalMatchingGame
{
    public partial class MainPage : ContentPage
    {
		Button lastClicked;
		bool findingMatch = false;
		int matchesFound;
		int tenthsOfSecondsElapsed = 0;

        public MainPage()
        {
            InitializeComponent();
        }

		private void PlayAgainButton_Clicked(object sender, EventArgs e)
		{
			AnimalButtons.IsVisible = true;
			PlayAgainButton.IsVisible = false;
			List<string> animalEmoji = [
				"🐶", "🐶",
				"🦜", "🦜",
				"🐯", "🐯",
				"🐧", "🐧",
				"🦋", "🦋",
				"🐥", "🐥",
				"🐿️", "🐿️",
				"🦣", "🦣"
				];

			foreach (var button in AnimalButtons.Children.OfType<Button>())
			{
				int index = Random.Shared.Next(animalEmoji.Count);
				string nextEmoji = animalEmoji[index];
				button.Text = nextEmoji;
				animalEmoji.RemoveAt(index);
			}
			Dispatcher.StartTimer(TimeSpan.FromSeconds(.1), TimerTick);
		}

		private bool TimerTick()
		{
			if (!this.IsLoaded) return false;
			tenthsOfSecondsElapsed++;
			TimeElapsed.Text = "Time elapsed: " + (tenthsOfSecondsElapsed / 10F).ToString("0.0s");
			if (PlayAgainButton.IsVisible)
			{
				tenthsOfSecondsElapsed = 0;
				return false;
			}
			return true;
		}

		private void Button_Clicked(object sender, EventArgs e)
		{
			if (sender is Button bt) {
				if (!string.IsNullOrWhiteSpace(bt.Text) && findingMatch == false) {
					bt.BackgroundColor = Colors.Red;
					lastClicked = bt;
					findingMatch = true;
				}
				else
				{
					if ((bt != lastClicked) && (bt.Text == lastClicked.Text) && !string.IsNullOrWhiteSpace(bt.Text)) {
						matchesFound++;
						lastClicked.Text = "";
						bt.Text = "";
					}
					lastClicked.BackgroundColor = Colors.LightBlue;
					bt.BackgroundColor = Colors.LightBlue;
					findingMatch = false;
				}
			}
			if(matchesFound == 8)
			{
				matchesFound = 0;
				AnimalButtons.IsVisible = false;
				PlayAgainButton.IsVisible = true;
			}
		}
	}

}
