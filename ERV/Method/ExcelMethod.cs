using ERV.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Excel = Microsoft.Office.Interop.Excel;

namespace ERV.Method
{
	class ExcelMethod
	{
		//Initializes the excel app, creates workbook and worksheet
		public void Initialize(out Excel.Workbook xlWorkBook, out Excel.Worksheet xlWorkSheet)
		{

			Excel.Application xlApp = new Excel.Application();


			if (xlApp == null)
			{
				MessageBox.Show("Excel is not properly installed!");
				System.Windows.Application.Current.Shutdown();
			}


			xlWorkBook = xlApp.Workbooks.Add("");
			xlWorkSheet = xlWorkBook.ActiveSheet;

		}

		//Formats the excels sheets
		public void Format(Excel.Worksheet xlWorkSheet)
		{
			xlWorkSheet.StandardWidth = 3.33;

			//Second colum is date, its width is greater so it can fit
			xlWorkSheet.Columns[2].ColumnWidth = 11;

			//Set the first row (the row in which the static text is) height, it only needs to be set on the first column to affect the entire row
			xlWorkSheet.Range["A1"].RowHeight = 165;

			//Formats the first block of static text
			xlWorkSheet.Range["A1", "F1"].Merge();
			FormatHelper(xlWorkSheet, "A1", "A1", 12, "Times New Roman", Excel.XlVAlign.xlVAlignCenter, Excel.XlHAlign.xlHAlignLeft);

			//Formats the second block of static text
			xlWorkSheet.Range["G1", "O1"].Merge();
			FormatHelper(xlWorkSheet, "G1", "G1", 6, "Times New Roman", Excel.XlVAlign.xlVAlignCenter, Excel.XlHAlign.xlHAlignLeft);

			//Formats the third block of static text
			xlWorkSheet.Range["P1", "X1"].Merge();
			FormatHelper(xlWorkSheet, "P1", "P1", 6, "Times New Roman", Excel.XlVAlign.xlVAlignCenter, Excel.XlHAlign.xlHAlignLeft);

			//Last row is a bit higher
			xlWorkSheet.Range["A35"].RowHeight = 19.5;

			//Sets the borders of the sheet to be thin
			xlWorkSheet.Range["A1", "X35"].Borders.Weight = Excel.XlBorderWeight.xlThin;

			//Sets the second row border to hairline and its height to be smaller so it separates the static text from the rest
			xlWorkSheet.Range["A2", "X2"].Borders.Weight = Excel.XlBorderWeight.xlHairline;
			xlWorkSheet.Range["A2"].RowHeight = 12.75;
			xlWorkSheet.Range["A3"].RowHeight = 12.00;

			//Formats the row numbering
			FormatHelper(xlWorkSheet, "A4", "X34", 10, "Arial", Excel.XlVAlign.xlVAlignTop, Excel.XlHAlign.xlHAlignCenter);

			//Formats the third row
			FormatHelper(xlWorkSheet, "C3", "X3", 7, "Arial", Excel.XlVAlign.xlVAlignCenter, Excel.XlHAlign.xlHAlignCenter);
			//First two columns of the third row
			FormatHelper(xlWorkSheet, "A3", "B3", 10, "Times New Roman", Excel.XlVAlign.xlVAlignCenter, Excel.XlHAlign.xlHAlignCenter);

			//Formats the last row
			FormatHelper(xlWorkSheet, "C35", "X35", 6, "Arial", Excel.XlVAlign.xlVAlignTop, Excel.XlHAlign.xlHAlignCenter);
			//First two columns of the last row
			FormatHelper(xlWorkSheet, "A35", "B35", 10, "Times New Roman", Excel.XlVAlign.xlVAlignTop, Excel.XlHAlign.xlHAlignCenter);
			xlWorkSheet.Rows.Range["A35", "B35"].Font.Bold = true;

		}

		//R1 and R2 are ranges of cells and rows, FontSize, FontName, VAling and HAling changes the font size, font name, vertical and horizontal align of the selected range
		public void FormatHelper(Excel.Worksheet xlWorkSheet, string R1, string R2, int FontSize, string FontName, Excel.XlVAlign VAlign, Excel.XlHAlign HAlign)
		{
			xlWorkSheet.Range[R1, R2].Font.Size = FontSize;
			xlWorkSheet.Range[R1, R2].Font.Name = FontName;
			xlWorkSheet.Range[R1, R2].Cells.VerticalAlignment = VAlign;
			xlWorkSheet.Range[R1, R2].Cells.HorizontalAlignment = HAlign;
		}

		//Adds static text to the sheet
		public void AddStaticText(Excel.Worksheet xlWorkSheet, string Name, DateTime SelectedDate)
		{

			var month = CultureInfo.GetCultureInfo("hr-HR").DateTimeFormat.GetMonthName(SelectedDate.Month);
			var year = SelectedDate.Year;
			//Add first block of static text
			xlWorkSheet.Range["A1"].Value = "EVIDENCIJA O RADNOM \nVREMENU RADNIKA \nza mjesec: " + char.ToUpper(month[0]) + month.Substring(1) + "/" + year + "\nPoslodavac:\nRepro-grav d.o.o.\nOrg. jedinica:\n1\nIme i prezime radnika:\n" + Name;

			xlWorkSheet.Range["A1"].Characters[49, month.Length + 6].Font.Bold = true;
			xlWorkSheet.Range["A1"].Characters[49 + month.Length + 6 + 12, 18].Font.Bold = true;
			//Add second block of static text
			xlWorkSheet.Range["G1"].Value = "LEGENDA:\n1. datum rada\n2. početak rada\n3. završetak rada\n4. vrijeme i sati zastoja, prekida rada i slično do kojega je došlo krivnjom poslodavca ili uslijed drugih okolnosti za koje radnik nije odgovoran\n5. ukupno dnevno radno vrijeme u satima te od toga sati:\n5.a - rada noću,\n5.b - prekovremenog rada\n6. sati rada u preraspodijeljenom radnom vremenu i razdoblje preraspodijeljenog radnog vremena, SD - slobodan dan po osnovi raspodijele (nije propisano Pravilnikom)\n7. sati rada\n7.a nedjeljom\n7.b blagdanom\n7.c neradni dan utvrđenim posebnim propisom";

			//Add third block of static text
			xlWorkSheet.Range["P1"].Value = "IB - izostanak s rada po osnovi blagdana ili neradnih dana utvrđenih posebnim propisom\n(nije propisano Pravilnikom)\n8. sati provedeni na službenom putu\n9. sati terenskog rada\n10. sati pripravnosti te sati rada po pozivu\n11. sati korištenja godišnjeg odmora\n12 sati privremene nesposobnosti za rad (bolovanje)\n13. vrijeme rodiljnog, roditeljskog dopusta ili korištenja drugih prava sukladno posebnom propisu\n14. sati plaćenog dopusta\n15. sati neplaćenog dopusta\n16. sati nenazočnosti u tijeku dnevnog rasporeda radnog vremena, odobrene ili neodobrene od poslodavca\n17. sati provedeni u štrajku\n18. sati isključenja s rada(lockout)";

			//Adds row numbering
			int i = 1;
			foreach (Excel.Range row in xlWorkSheet.Range["A4", "A34"])
			{
				row.Value = i;
				i++;
			}

			//Adds column numbering
			xlWorkSheet.Range["A3"].Value = "r.br";
			xlWorkSheet.Range["B3"].Value = "1. datum";
			xlWorkSheet.Range["C3"].Value = "2";
			xlWorkSheet.Range["D3"].Value = "3";
			xlWorkSheet.Range["E3"].Value = "4";
			xlWorkSheet.Range["F3"].Value = "5";
			xlWorkSheet.Range["G3"].Value = "5.a";
			xlWorkSheet.Range["H3"].Value = "5.b";
			xlWorkSheet.Range["I3"].Value = "6";
			xlWorkSheet.Range["J3"].Value = "7";
			xlWorkSheet.Range["K3"].Value = "7.a";
			xlWorkSheet.Range["L3"].Value = "7.b";
			xlWorkSheet.Range["M3"].Value = "7.c";
			i = 8;
			foreach (Excel.Range row in xlWorkSheet.Range["N3", "X3"])
			{
				row.Value = i;
				i++;
			}

			//Adds last row static text (UKUPNO and FORMULAS)
			xlWorkSheet.Range["A35"].Value = "-";
			xlWorkSheet.Range["B35"].Value = "UKUPNO:";
			xlWorkSheet.Range["F35"].Formula = "=SUM(F4:F34)";
			xlWorkSheet.Range["J35"].Formula = "=SUM(J4:J34)";
			xlWorkSheet.Range["L35"].Formula = "=SUM(L4:L34)";
			xlWorkSheet.Range["M35"].Formula = "= SUM(M4:M34)";
			xlWorkSheet.Range["N35"].Formula = "=SUM(N4:N34)";
			xlWorkSheet.Range["Q35"].Formula = "=SUM(Q4:Q34)";
			xlWorkSheet.Range["R35"].Formula = "=SUM(R4:R34)";

		}

		//Write data to excel
		public void WriteData(List<Date> Dates, Excel.Worksheet xlWorkSheet)
		{

			int i = 4;
			string WorkingHours;
			string Vacation; // Q
			string Trip; // N
			string Sick; // R
			string WorkingDayHours; //J
			foreach (Date Date in Dates)
			{
				//Write the date
				string DateField = "B" + i;			
				xlWorkSheet.Range[DateField].Value = Date.CurrentDate.ToString("dd.MM.yy");

				

				//Check for saturday or sunday
				if(Date.CurrentDate.DayOfWeek.ToString() == "Saturday" ||Date.CurrentDate.DayOfWeek.ToString() == "Sunday")
				{
					i++;
					continue;
				}

				//Write working hours
				WorkingHours = "F" + i;
				xlWorkSheet.Range[WorkingHours].Value = "8";

				//Check for vacation, trip or sick
				if (Date.Vacation == true)
				{
					Vacation = "Q" + i;
					xlWorkSheet.Range[Vacation].Value = "8";
					i++;
					continue;
				}
				else if (Date.Trip == true)
				{
					Trip = "N" + i;
					xlWorkSheet.Range[Trip].Value = "8";
					i++;
					continue;
				}
				else if (Date.Sick == true)
				{
					Sick = "R" + i;
					xlWorkSheet.Range[Sick].Value = "8";
					i++;
					continue;
				}

				//if no vacation, trip or sick is checked then write the normal 8 to 16 working hour
				string StartingHour = "C" + i;
				string EndingHour = "D" + i;
				WorkingDayHours = "J" + i;
				xlWorkSheet.Range[StartingHour].Value = "8";
				xlWorkSheet.Range[EndingHour].Value = "16";
				xlWorkSheet.Range[WorkingDayHours].Value = "8";

				i++;


			}
		}


	}
}
