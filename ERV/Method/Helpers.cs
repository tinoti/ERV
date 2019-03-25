using ERV.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ERV.Method
{
	class Helpers
	{
		//Load user into a list from a text file
		public void LoadUsersInList(List<string> UsersList)
		{
			string FullPath = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\..\Users.txt"));
			string line;
			System.IO.StreamReader file = new System.IO.StreamReader(FullPath);
			while ((line = file.ReadLine()) != null)
			{
				string[] name = line.Split(',');
				UsersList.Add(name[0]);
			}
			file.Close();
		}

		//Create User
		public User CreateUser(string user)
		{

			User User = new User();
			User.Name = user;

			//Create the label, checkboxes and remove button
			User.CreateProperties();




			return User;
		}

		//Add user to the grid
		public void AddUserToGrid(User User, Grid UserGrid, ref int i, ref int RowCounter, ref int ColumnCounter)
		{
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
		}

		//Add "add user" button after the last user
		public void AddNewUserButton(Grid UserGrid, int RowCounter, int ColumnCounter)
		{
			Button Button = new Button1();
			Button.Margin = new Thickness(3, 9, 3, 9);
			Button.Content = "Dodaj korisnika";
			UserGrid.Children.Add(Button);
			Grid.SetColumn(Button, ColumnCounter);
			Grid.SetRow(Button, RowCounter);
			Button.VerticalAlignment = VerticalAlignment.Bottom;
		}

	}
}
