
using MemoryManagement;

namespace StamplerApp
{
	public partial class MainPage : ContentPage
	{
		private DateTime m_start;
		private DateTime m_end;
		private bool m_timerStarted;
		private bool m_timerEnded;
		private List<DateEntry> m_entries;
		private DateSaver m_saver;

		public MainPage()
		{
			InitializeComponent();
			m_entries = new List<DateEntry>();
			m_saver = new DateSaver();
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();

			m_entries = m_saver.ReadFromFile();
			m_entries.Reverse();

			listView.ItemsSource = m_entries;
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

				DateEntry entry = new DateEntry(m_start, m_end);

				ElapsedTime.Text = $"Worked hours: {time.Hours}::{time.Minutes}::{time.Seconds}";
				string date = m_start.Date + " H:" + time.Hours.ToString() + " Min:" + time.Minutes.ToString();
				
				m_entries.Add(entry);
			}
		}

		private async void OnLoadClicked(object sender, EventArgs e)
		{
			listView.ItemsSource = m_saver.ReadFromFile();
		}

		private async void DisplayWorkedTime()
		{
			while (m_timerEnded == false)
			{
				if (m_timerStarted)
				{
					var time = DateTime.Now - m_start;
					ElapsedTime.Text = $"Time: {time.Hours}::{time.Minutes}::{time.Seconds}";
				}

				await (Task.Delay(500));
			}
		}
	}
}