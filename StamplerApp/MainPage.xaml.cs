
using System.Diagnostics;
using CommunityToolkit.Maui.Storage;
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

		public MainPage(IFileSaver fileSaver)
		{
			InitializeComponent();
			m_entries = new List<DateEntry>();
			m_saver = new DateSaver(fileSaver);
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

				DateEntry entry = new DateEntry();
				string timeForm = "HH:mm.ss";
				entry.ShiftStart = m_start.ToString(timeForm);
				entry.ShiftEnd = m_end.ToString(timeForm);
				entry.TimeWorked = $"{time.Hours} {time.Minutes}";
				entry.Date = m_start.Date.ToString("d");
				entry.PersonalNotes = "Notes here";
				entry.ImageUrl = "normal.png";

				ElapsedTime.Text = $"Worked hours: {time.Hours}::{time.Minutes}::{time.Seconds}";
				string date = m_start.Date + " H:" + time.Hours.ToString() + " Min:" + time.Minutes.ToString();
				m_entries.Add(entry);
			}
		}

		private async void OnLoadClicked(object sender, EventArgs e)
		{
			ReadFromFile();
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
		public async void ReadFromFile()
		{
			try
			{
				var result = await FilePicker.Default.PickAsync();

				if (result.FileName != "StamplerAppData.txt")
				{
					Debug.Assert(false, "File must have exact name");
				}

				using var stream = await result.OpenReadAsync();
				using StreamReader reader = new StreamReader(stream);

				bool done = false;
				while (!done)
				{
					var task = await reader.ReadLineAsync();
					if (task == null)
					{
						done = true;
						continue;
					}
					var entry = new DateEntry(task);
					m_entries.Add(entry);
				}

				listView.ItemsSource = m_entries;
			}
			catch (Exception e)
			{
			}
		}
	}
}