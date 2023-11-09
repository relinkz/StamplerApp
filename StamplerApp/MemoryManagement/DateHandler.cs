
using System.Diagnostics;

namespace StamplerApp.MemoryManagement
{
	public class DateHandler
	{
		private List<DateEntry> m_entries;

		private readonly string m_filePath;
		private readonly string m_filename;

		public DateHandler()
		{
			m_filename = "StamplerAppData3.txt";
			string localFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
			// string localFolderPath = "C:/Users/seb/OneDrive - Sigma Technology/Dokument";
			m_filePath = Path.Combine(localFolderPath, m_filename);
			
			m_entries = ReadFromFile();
			if ( m_entries.Count == 0 ) 
			{
				CreateDummyData();
			}
		}

		private void CreateFileIfNeeded()
		{
			if (!File.Exists(m_filePath))
			{
				try
				{
					File.Create(m_filePath).Close();
				}
				catch (Exception ex)
				{
					Debug.Assert(false, ex.Message);
					throw;
				}
			}
		}
		public List<DateEntry> ReadFromFile()
		{
			CreateFileIfNeeded();

			List<DateEntry> dates = new List<DateEntry>();
			try
			{
				using (StreamReader reader = new StreamReader(m_filePath))
				{
					string line;
					while ((line = reader.ReadLine()) != null)
					{
						var test = line.Split(" ");
						var dateEntry = new DateEntry(test[0], test[1]);
						dates.Add(dateEntry);
					}
				}
			}
			catch (Exception e)
			{
				Debug.Assert(false, e.Message);
				throw;
			}

			return dates;
		}

		public void AddEntry(DateTime start, DateTime end)
		{
			m_entries.Add(new DateEntry(start, end));
		}

		public void SortLatestDateFirst()
		{
			m_entries.Sort();
		}

		public void WriteToFile()
		{
			using (StreamWriter outputfile = new StreamWriter(m_filePath))
			{
				foreach (DateEntry dateEntry in m_entries)
				{
					outputfile.WriteLine(dateEntry.ToFileExport());
				}
			}
		}

		public void RemoveEntryAt(int id)
		{
			m_entries.RemoveAt(id);
		}
		public List<DateEntry> PeekNrOfEntries(int nrOfEntries)
		{
			List<DateEntry> peekList = new List<DateEntry>();

			for( int i = 0; i < nrOfEntries & i < m_entries.Count; i++ )
			{
				peekList.Add(m_entries[i]);
			}

			return peekList;
		}

		public void CreateDummyData()
		{
			string[] startHours = new string[] 
			{ "20231012083000", 
				"20231011080000", 
				"20231010083000", 
				"20231009080000" };
			string[] endHours = new string[] 
			{ 
				"20231012163000", 
				"20231011193054", 
				"20231010163004", 
				"20231009143018" };


			for(int i = 0; i < startHours.Length; i++)
			{
				var dateEntry = new DateEntry(startHours[i], endHours[i]);
				m_entries.Add(dateEntry);
			}
		}
	}
}
