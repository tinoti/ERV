using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ERV.Model;

namespace ERV
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		List<User> ListOfUserObject = new List<User>();

		//Custom properties for calendar
		private void SelectMonthCalendar_Loaded(object sender, RoutedEventArgs e)
		{
			//Default selected month is the previous month
			SelectMonthCalendar.DisplayDate = DateTime.Now.AddMonths(-1);
		}

		//Overides the default display mode for calendar, so that when the user clicks on months it selects it and stays in year mode
		private void SelectMonthCalendar_DisplayModeChanged(object sender, CalendarModeChangedEventArgs e)
		{
			//If the display mode should go to month (meaning the user clicked the month), revert it back to year mode
			if (SelectMonthCalendar.DisplayMode == CalendarMode.Month)
			{
				SelectMonthCalendar.DisplayMode = CalendarMode.Year;
			}
		}

		//Overides the default calendar behaviour. The highlight stays on the clicked month and changes only when you click another month
		private void SelectMonthCalendar_PreviewMouseUp(object sender, MouseButtonEventArgs e)
		{
			//Sets the mouse capture to null before it gets to child element (thats why we use PreviewMouseUp instead of MouseUp) so it can't highlight anything and the highlight stays
			//on the clicked month
			if (Mouse.Captured is System.Windows.Controls.Primitives.CalendarItem)
			{
				Mouse.Capture(null);
			}
		}

		//Add default pathway (desktop) to textbox. 
		private void SaveLocationTextBox_Loaded(object sender, RoutedEventArgs e)
		{	
			SaveLocationTextBox.Text = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
		}

		//Promts the user to select the folder and writes it in textbox
		private void SaveLocationButton_Click(object sender, RoutedEventArgs e)
		{
			//Create new folder browser dialog and show it
			System.Windows.Forms.FolderBrowserDialog folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
			System.Windows.Forms.DialogResult result = folderBrowserDialog.ShowDialog();

			if (result == System.Windows.Forms.DialogResult.OK)
			{
				//Write it in textbox
				SaveLocationTextBox.Text = folderBrowserDialog.SelectedPath;

				////Set the cursor to end of text in textbox (it needs to be in focus for that)
				SaveLocationTextBox.Focus();
				SaveLocationTextBox.SelectionStart = SaveLocationTextBox.Text.Length;
			}
		}

		//Creates and positions the user on the grid
		private void Window_Loaded(object sender, RoutedEventArgs e)
		{

			//Create users and add them to list
			List<string> UsersList = new List<string>();
			UsersList.Add("Saša Ruškov");
			UsersList.Add("Valentino Tomašić");
			UsersList.Add("Melita Bosek");
			UsersList.Add("Saša Uzelac");


			int i = 0;
			int ColumnCounter = 0;
			int RowCounter = 0;

			//Create and position each user on the grid
			foreach (string user in UsersList)
			{
				User User = new User();
				ListOfUserObject.Add(User);

				User.Name = user;

				//Create the label, checkboxes and remove button
				User.CreateProperties();

				//i 3 and i 6 means the grid columns are full and the user needs to be displayed in the new row, so it updates row and set the column counter back to 0
				if (i == 3 || i == 6)
				{
					RowCounter++;
					ColumnCounter = 0;
				}

				//Add the user to the grid
				UserGrid.Children.Add(User.StackPanel);

				//Position the created user on the grid
				Grid.SetColumn(User.StackPanel, ColumnCounter);
				Grid.SetRow(User.StackPanel, RowCounter);

				
				ColumnCounter++;
				i++;

			}
		}



		
		//Starts the writing process
		private void Button_Click(object sender, RoutedEventArgs e)
		{

			//foreach (User user in ListOfUserObject)
			//{
			//	foreach (CheckBox checkBox in user.CheckBoxList)
			//	{
			//		if ((bool)checkBox.IsChecked)
			//		{
			//			MessageBox.Show(user.Name + " checked: " + checkBox.Name);
			//		}
			//	}
			//}


		}
	}
}
