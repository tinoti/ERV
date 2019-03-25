using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ERV.Model
{
    class User
    {

		private string id;

		public string Id
		{
			get { return id; }
			set
			{
				//Connect user and user button
				id = value;
				Button.id = id;
				
			}
		}




		public string Name { get; set; }
		public StackPanel StackPanel { get; set; }
		public TextBlock TextBlock { get; set; }
		public List<CheckBox> CheckBoxList { get; set; }
		public Button1 Button { get; set; }
		public List<Date> Dates = new List<Date>();
		
		public void CreateProperties()
		{

			//Create TextBlock
			TextBlock = new TextBlock();
			TextBlock.Margin = new Thickness(0, 0, 0, 4);
			TextBlock.FontWeight = FontWeights.Bold;
			TextBlock.Text = Name;
			TextBlock.HorizontalAlignment = HorizontalAlignment.Center;

			//Create checkboxes
			CheckBoxList = CreateCheckBoxes();

			//Create Button
			Button = new Button1();
			Button.Margin = new Thickness(0, 3, 0, 3);
			Button.Content = "Ukloni";

			//Create stack panel
			StackPanel = new StackPanel();
			StackPanel.Margin = new Thickness(5);
			StackPanel.Children.Add(TextBlock);
			
			foreach (CheckBox checkBox in CheckBoxList)
			{
				StackPanel.Children.Add(checkBox);
			}

			StackPanel.Children.Add(Button);
		}


		private List<CheckBox> CreateCheckBoxes()
		{
			List<CheckBox> CheckBoxList = new List<CheckBox>();

			//First checkbox
			CheckBox CheckBoxOne = new CheckBox();
			CheckBoxOne.Content = "Pitaj za godišnji";
			CheckBoxOne.Margin = new Thickness(0, 3, 0, 3);
			CheckBoxOne.Name = "vacation";

			//Second checkbox
			CheckBox CheckBoxTwo = new CheckBox();
			CheckBoxTwo.Content = "Pitaj za put";
			CheckBoxTwo.Margin = new Thickness(0, 3, 0, 3);
			CheckBoxTwo.Name = "trip";

			//Third checkbox
			CheckBox CheckBoxThree = new CheckBox();
			CheckBoxThree.Content = "Pitaj za bolovanje";
			CheckBoxThree.Margin = new Thickness(0, 3, 0, 3);
			CheckBoxThree.Name = "sick";

			//Add to list
			CheckBoxList.Add(CheckBoxOne);
			CheckBoxList.Add(CheckBoxTwo);
			CheckBoxList.Add(CheckBoxThree);

			return CheckBoxList;
		}

		public void FillListWithDates(DateTime SelectedDate)
		{
			int NumberOfDays = DateTime.DaysInMonth(SelectedDate.Year, SelectedDate.Month);
			
			for(int i = 1; i <= NumberOfDays; i++)
			{
				DateTime Day = new DateTime(SelectedDate.Year, SelectedDate.Month, i);
				Date date = new Date(Day);		

				Dates.Add(date);
			}

		}




	}

	class Button1 : Button
	{
		public string id;
	}
}
