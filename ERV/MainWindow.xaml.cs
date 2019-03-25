using System;
using System.Collections.Generic;
using System.IO;
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
using ERV.Method;
//using System.Windows.Shapes;
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
		List<string> UsersList = new List<string>();
		Helpers Helpers = new Helpers();

		//Custom properties for calendar
		private void SelectMonthCalendar_Loaded(object sender, RoutedEventArgs e)
		{
			//Default selected month is the previous month
			SelectMonthCalendar.DisplayDate = DateTime.Now.AddMonths(-1);
			SelectMonthCalendar.SelectedDate = DateTime.Now.AddMonths(-1);
		}

		//Overides the default display mode for calendar, so that when the user clicks on months it selects it and stays in year mode
		private void SelectMonthCalendar_DisplayModeChanged(object sender, CalendarModeChangedEventArgs e)
		{
			//If the display mode should go to month (meaning the user clicked the month), revert it back to year mode
			if (SelectMonthCalendar.DisplayMode == CalendarMode.Month)
			{
				SelectMonthCalendar.DisplayMode = CalendarMode.Year;
				SelectMonthCalendar.SelectedDate = SelectMonthCalendar.DisplayDate;

				//MessageBox.Show(SelectMonthCalendar.SelectedDate.Value.Month.ToString());
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

		//Perform actions when the windows loads
		private void Window_Loaded(object sender, RoutedEventArgs e)
		{

					

			//Create users and add them to list
			Helpers.LoadUsersInList(UsersList);
			
			int i = 0;
			int ColumnCounter = 0;
			int RowCounter = 0;

			//Create and add each user to the grid
			foreach (string user in UsersList)
			{

				//Create user
				User User = Helpers.CreateUser(user);

				//Set user id
				User.Id = i.ToString();

				//Add remove user button event
				User.Button.Click += new RoutedEventHandler(OnButtonClick);

				//Add user to list
				ListOfUserObject.Add(User);

				//Add user to the grid
				Helpers.AddUserToGrid(User, UserGrid, ref i, ref RowCounter, ref ColumnCounter);
					
				ColumnCounter++;
				i++;

			}

			//Add the "add user" button after the last user
			Helpers.AddNewUserButton(UserGrid, RowCounter, ColumnCounter, i);

			

		}


		//Event for the remove user button
		public void OnButtonClick(object sender, RoutedEventArgs e)
		{
			//cast sender as button
			Button1 Button = sender as Button1;

			//Find the user with the same id as the button
			User User = ListOfUserObject.FirstOrDefault(b => b.Id == Button.id);

			//Full path to the text file that stores all the users
			string FullPath = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\..\Users.txt"));

			//Create new temporary file
			var tempFile = Path.GetTempFileName();

			//Read all the lines and keep those that are not the current user name
			var linesToKeep = File.ReadLines(FullPath).Where(l => l != User.Name);

			//Write all lines to temp file
			File.WriteAllLines(tempFile, linesToKeep);

			//Delete the old file and write the temp file to the old file location
			File.Delete(FullPath);
			File.Move(tempFile, FullPath);

			//Reload window
			System.Windows.Forms.Application.Restart();
			System.Windows.Application.Current.Shutdown();
		}

		
		//Starts the writing process
		private void Button_Click(object sender, RoutedEventArgs e)
		{
			


			//Check what checkboxes are ticked in every user
			foreach (User user in ListOfUserObject)
			{
				//Fill user with the days from the selected month
				user.FillListWithDates((DateTime)SelectMonthCalendar.SelectedDate);



				//Vacation handler
				if ((bool)user.CheckBoxList.FirstOrDefault(b => b.Name == "vacation").IsChecked)
				{
					//Display calendar
					PopUpWindow PopUpWindow = new PopUpWindow();

					PopUpWindow.Owner = App.Current.MainWindow;
					GrayRectangle.Visibility = Visibility.Visible;
					
					PopUpWindow.ShowDialog();
					GrayRectangle.Visibility = Visibility.Hidden;
					var dates = PopUpWindow.PopUpCalendar.SelectedDates;


					foreach(DateTime date in dates)
					{
						foreach(Date userDate in user.Dates)
						{
							if(date == userDate.CurrentDate)
							{
								userDate.Vacation = true;
							}
						}
					}

					MessageBox.Show("a");


				}

			
			}


		}

	}
}
