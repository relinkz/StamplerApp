
using CommunityToolkit.Maui.Storage;
using System.Text;

namespace MemoryManagement
{
	public class DateSaver
	{
		IFileSaver m_fileSaver;
		CancellationTokenSource m_cancellationTokenSource;
		private readonly string m_filename = "StamplerAppData.txt";

		public DateSaver(IFileSaver fileSaver)
		{
			m_fileSaver = fileSaver;
			m_cancellationTokenSource = new CancellationTokenSource();
		}

		async public void SaveEntryByWaiting(DateEntry dateEntry)
		{
			var toEncode = dateEntry.AsBundledString();

			var encodedData = Encoding.Default.GetBytes(toEncode);
			using var stream = new MemoryStream(encodedData);

			var task = await m_fileSaver.SaveAsync(m_filename, stream, m_cancellationTokenSource.Token);
			if (!task.IsSuccessful) 
			{
				throw new Exception(task.Exception.ToString());
			}
		}
	}
}
