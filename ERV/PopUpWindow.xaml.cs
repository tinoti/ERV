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
using System.Windows.Shapes;

namespace ERV
{
	/// <summary>
	/// Interaction logic for PopUpWindow.xaml
	/// </summary>
	public partial class PopUpWindow : Window
	{
		public PopUpWindow()
		{
			InitializeComponent();
		}

		private void PopUpCalendar_Loaded(object sender, RoutedEventArgs e)
		{
			//Default selected month is the previous month
			PopUpCalendar.DisplayDate = DateTime.Now.AddMonths(-1);
		}

		private void PopUpButton_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}
	}
}
