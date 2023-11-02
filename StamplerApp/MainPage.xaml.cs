
namespace StamplerApp
{
	public partial class MainPage : ContentPage
	{
		private DateTime m_start;
		private DateTime m_end;
		private bool m_timerStarted;
		private bool m_timerEnded;

		public MainPage()
		{
			InitializeComponent();
		}

		private void OnCounterClicked(object sender, EventArgs e)
		{
			if (m_timerStarted == false)
			{
				m_start = DateTime.Now;
				ElapsedTime.Text = "";
				TimeStart.Text = m_start.ToString();
				TimeEnd.Text = "";
				TimerBtn.Text = "End Day";
				m_timerStarted = true;
				m_timerEnded = false;
				
				DisplayWorkedTime();
			}
			else
			{
				m_end = DateTime.Now;
				TimeEnd.Text = m_end.ToString();
				TimerBtn.Text = "Start Day";
				m_timerEnded = true;
				m_timerStarted = false;
				
				var time = DateTime.Now - m_start;

				ElapsedTime.Text = $"Worked hours: { time.Hours }::{time.Minutes}::{time.Seconds}";
				var entry = new MemoryManagement.DateEntry();
				entry.day = m_start.Date;
				entry.workhours = time;
			}

			SemanticScreenReader.Announce(TimerBtn.Text);
		}

		private async void DisplayWorkedTime()
		{
			while(m_timerEnded == false)
			{
				if (m_timerStarted)
				{
					var time = DateTime.Now - m_start;
					ElapsedTime.Text = $"Time: { time.Hours }::{time.Minutes}::{time.Seconds}";
				}

				await (Task.Delay(500));
			}
		}
	}
}