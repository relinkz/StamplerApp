
namespace MemoryManagement
{
	public struct DateEntry
	{
		public DateTime day;
		public TimeSpan workhours;

		public override string ToString()
		{
			return day.ToShortDateString() + " " + workhours.Hours.ToString() + " " + workhours.Minutes.ToString();
		}
	}
}