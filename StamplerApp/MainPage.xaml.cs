
using StamplerApp.MemoryManagement;

namespace StamplerApp
{
	public partial class MainPage : ContentPage
	{
		private DateTime m_start;
		private DateTime m_end;
		private bool m_timerStarted;
		private bool m_timerEnded;
		private DateHandler m_handler;
		private readonly int m_peekCount;
		public MainPage()
		{
			InitializeComponent();
			m_peekCount = 10;

			m_handler = new DateHandler();
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();

			listView.ItemsSource = m_handler.PeekNrOfEntries(m_peekCount);
		}

		protected override void OnDisappearing()
		{
			m_handler.SortLatestDateFirst();
			m_handler.WriteToFile();
		}
		private void OnCounterClicked(object sender, EventArgs e)
		{
			if (m_timerStarted == false)
			{
				m_start = DateTime.Now;
				ElapsedTime.Text = "";
				TimeStart.Text = "Start: " + m_start.ToString();
				TimeEnd.Text = " -- ";
				TimerBtn.Source = "stop.png";

				m_timerStarted = true;
				m_timerEnded = false;

				DisplayWorkedTime();
			}
			else
			{
				m_end = DateTime.Now;
				TimeEnd.Text = "End: " + m_end.ToString();
				TimerBtn.Source = "play.png";
				m_timerEnded = true;
				m_timerStarted = false;

				var time = DateTime.Now - m_start;

				ElapsedTime.Text = $"Worked hours: {time.Hours}:{time.Minutes}";
				
				m_handler.AddEntry(m_start, m_end);
				m_handler.SortLatestDateFirst();
				m_handler.WriteToFile();
				
				listView.ItemsSource = m_handler.PeekNrOfEntries(m_peekCount);
			}
		}

		private async void DisplayWorkedTime()
		{
			while (m_timerEnded == false)
			{
				if (m_timerStarted)
				{
					var time = DateTime.Now - m_start;
					ElapsedTime.Text = $"Worked hours: {time.Hours}:{time.Minutes}";
				}

				await (Task.Delay(500));
			}
		}
		private async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
		{
			bool answer = await DisplayAlert("Remove Item?", "Would you like to remove this entry?", "Yes", "No");
			if (answer)
			{
				m_handler.RemoveEntryAt(args.SelectedItemIndex);
				m_handler.SortLatestDateFirst();
				listView.ItemsSource = m_handler.PeekNrOfEntries(m_peekCount);
			}
		}
	}
}