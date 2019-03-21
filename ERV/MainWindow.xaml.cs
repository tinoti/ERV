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

		//Custom properties for calendar
		private void SelectMonthCalendar_Loaded(object sender, RoutedEventArgs e)
		{
			//Default selected month is the previouse month
			SelectMonthCalendar.DisplayDate = DateTime.Now.AddMonths(-1);
		}

		//Overides the default display mode, so that when the user clicks on a months it selects it and stays in year mode
		private void SelectMonthCalendar_DisplayModeChanged(object sender, CalendarModeChangedEventArgs e)
		{
			//If the display mode should go to month (meaning the user clicked the month), revert it back to year mode
			if (SelectMonthCalendar.DisplayMode == CalendarMode.Month)
			{
				SelectMonthCalendar.DisplayMode = CalendarMode.Year;
			}
		}

		//Overides the default behaviour. The highlight stays on the clicked month and changes only when you click another month
		private void SelectMonthCalendar_PreviewMouseUp(object sender, MouseButtonEventArgs e)
		{
			//Sets the mouse capture to null before it gets to child element (thats why we use PreviewMouseUp instead of MouseUp) so it can't highlight anything and the highlight stays
			//on the clicked month
			if (Mouse.Captured is System.Windows.Controls.Primitives.CalendarItem)
			{
				Mouse.Capture(null);
			}
		}
	}
}
