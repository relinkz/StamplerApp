using System.Diagnostics;

namespace StamplerApp.MemoryManagement
{
	public class DateEntry : IEquatable<DateEntry>, IComparable<DateEntry>
	{
		internal enum FormatType : int
		{
			Date = 0,
			Time = 1,
			Both = 2
		}

		private readonly DateTime m_start;
		private readonly DateTime m_end;
		
		static private readonly string[] m_readFormats = new string[] { "yyyyMMdd", "HHmmss", "yyyyMMddHHmmss" };
		static private readonly string[] m_displayFormats = new string[] { "yyyy-MM-dd", "HH:mm:ss", "yyyy-MM-dd HH:mm:ss" };

		private DateTime TryConvertStringToDateTime(string date)
		{
			DateTime parsedDate;

			var dateTimeStyles = System.Globalization.DateTimeStyles.AllowWhiteSpaces | System.Globalization.DateTimeStyles.AdjustToUniversal;
			if (!DateTime.TryParseExact(date, m_readFormats[(int)FormatType.Both], null, dateTimeStyles, out parsedDate))
			{
				throw new ArgumentException("Invalid date format", nameof(date));
			}

			return parsedDate;
		}
		public DateEntry(string timestrStart, string timestrEnd)
		{
			try
			{
				m_start = TryConvertStringToDateTime(timestrStart);
				m_end = TryConvertStringToDateTime(timestrEnd);
			}
			catch (Exception e)
			{
				Debug.Assert(false, e.Message);

				throw;
			}
		}

		public DateEntry(DateEntry toCopy)
		{
			m_start = toCopy.m_start;
			m_end = toCopy.m_end;
		}

		public DateEntry(DateTime start, DateTime end) 
		{
			m_start = start;
			m_end = end;
		}

		public int CompareTo(DateEntry other)
		{
			if (other == null)
			{
				throw new ArgumentException("A date is null when sorting");
			}
			else
			{
				return m_start.CompareTo(other.m_start) * -1;
			}
		}

		public bool Equals(DateEntry other)
		{
			if (other == null)
			{
				return false;
			}
			if (this.m_start == other.m_start)
			{
				return true;
			}
			return false;
		}

		public string ToFileExport()
		{
			var stringStart = m_start.ToString(m_readFormats[(int)FormatType.Both]);
			var stringEnd = m_end.ToString(m_readFormats[(int)FormatType.Both]);

			return stringStart + " " + stringEnd; 
		}

		public string ShiftStart
		{
			get
			{
				return "start: " + m_start.ToString(m_displayFormats[(int)FormatType.Time]);
			}
		}
		public string ShiftEnd
		{
			get
			{
				return "end: " + m_end.ToString(m_displayFormats[(int)FormatType.Time]);
			}
		}
		public string Date
		{
			get
			{
				return m_start.ToString(m_displayFormats[(int)FormatType.Date]);
			}
		}
		public string TimeWorked
		{
			get
			{
				var span = m_end - m_start;
				return $"Worked : {span.Hours}:{span.Minutes}";
			}
		}
		public string PersonalNotes { get; set; }
		public string ImageUrl 
		{ 
			get
			{
				var workedTime = m_end - m_start;
				int ordinatyTime = 8;
				int flux = ordinatyTime - workedTime.Hours;

				if (flux < 0)
				{
					return "overtime.png";
				}
				else if (flux == 0)
				{
					return "normal.png";
				}
				else
				{
					return "early.jpg";
				}
			}
		}
	}
}