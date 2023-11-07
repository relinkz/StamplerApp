
using System.Diagnostics;

namespace MemoryManagement
{
	public class DateSaver
	{
		private readonly string m_filePath;
		private readonly string m_filename;

		public DateSaver()
		{
			m_filename = "StamplerAppData2.txt";
			string localFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
			m_filePath = Path.Combine(localFolderPath, m_filename);
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
						var dateEntry = new DateEntry(line);
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

		public void AppendOnFile(DateEntry date)
		{
			try
			{
				File.AppendAllText(m_filePath, date.AsBundledString() + "\n");
			}
			catch (Exception e )
			{
				Debug.Assert(false, e.Message);
				throw;
			}
		}
	}
}
