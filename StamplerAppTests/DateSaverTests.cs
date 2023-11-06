using MemoryManagement;
using NSubstitute;
using CommunityToolkit.Maui.Storage;

namespace StamplerAppTests
{
	public class DateSaverTests
	{
		DateSaver m_dateSaver;
		IFileSaver m_mockFileSaver;
		
		[SetUp]
		public void Setup()
		{
			m_mockFileSaver = Substitute.For<IFileSaver>();
			m_dateSaver = new DateSaver(m_mockFileSaver);
		}

		[Test]
		public void SaveSuccessfull()
		{
			DateEntry entry = new DateEntry();
			entry.day = DateTime.Now;
			entry.workhours = TimeSpan.Zero;

			m_dateSaver.SaveEntryByWaiting(entry);

			m_mockFileSaver.Received().SaveAsync("StamplerAppData.txt", Arg.Any<Stream>(), Arg.Any<CancellationToken>());
		}
	}
}