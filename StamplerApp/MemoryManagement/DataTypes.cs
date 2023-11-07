namespace MemoryManagement
{
	public class DateEntry
	{
		public string Date { get; set; }
		public string ShiftStart { get; set; }
		public string ShiftEnd { get; set; }
		public string TimeWorked { get; set; }
		public string PersonalNotes { get; set; }
		public string ImageUrl { get; set; }

		public DateEntry(string fromString)
		{
			// 2023-11-07;08:00.00;16:30.00;8 0;Notes here;normal.png
			var test = fromString.Split(';');
			Date = test[0];
			ShiftStart = test[1];
			ShiftEnd = test[2];
			TimeWorked = test[3];	
			PersonalNotes = test[4];
			ImageUrl = test[5];

			var strTime = TimeWorked.Split(" ");
			int timeWorked = Int32.Parse(strTime[0]);

			int ordinatyTime = 8;
			int flux = ordinatyTime - timeWorked;

			if (flux < 0)
			{
				ImageUrl = "overtime.png";
			}
			else if (flux == 0 )
			{
				ImageUrl = "normal.png";
			}
			else
			{
				ImageUrl = "early.jpg";
			}

		}

		public DateEntry()
		{
			Date = "";
			ShiftStart = "";
			ShiftEnd = "";
			TimeWorked = "";
			PersonalNotes = "";
			ImageUrl = "";
		}
		public string AsBundledString()
		{
			return Date + ";" + ShiftStart + ";" + ShiftEnd + ";" + TimeWorked + ";" + PersonalNotes + ";" + ImageUrl;
		}
	}
}