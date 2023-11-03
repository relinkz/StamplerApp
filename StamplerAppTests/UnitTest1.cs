namespace StamplerAppTests
{
	public class Tests
	{
		MemoryManagement.DateSaver m_dateSaver;
		[SetUp]
		public void Setup()
		{
			m_dateSaver = new MemoryManagement.DateSaver();
		}

		[Test]
		public void Test1()
		{
			Assert.True(m_dateSaver.DoStuff());
		}
	}
}