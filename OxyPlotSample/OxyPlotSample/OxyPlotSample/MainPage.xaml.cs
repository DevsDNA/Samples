namespace OxyPlotSample
{
	using OxyPlot;
	using OxyPlot.Axes;
	using OxyPlot.Series;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;
	using Xamarin.Forms;

	public partial class MainPage : ContentPage
	{
		private DateTime selectedDate;
		private Dictionary<DateTime, double> clients;
		private PlotModel modelYear;
		private PlotModel modelMonth;
		private DateTimeAxis yearAxis;

		public MainPage()
		{
			selectedDate = new DateTime(2021,12,1);
			modelYear = new PlotModel();
			modelMonth = new PlotModel();

			InitializeComponent();
			BtnLeft.Command = new Command<string>(ChangeDateCommandExecute);
			BtnRigth.Command = new Command<string>(ChangeDateCommandExecute);
		}

		protected override async void OnAppearing()
		{
			base.OnAppearing();

			await GetData();
			FillChartYear();
			FillChartMonth();
		}

		protected override void OnDisappearing()
		{
			base.OnDisappearing();

			yearAxis.AxisChanged -= Axis_AxisChanged;
		}

		private async Task GetData()
		{
			Random random = new Random();

			await Task.Delay(3000);
			clients = new Dictionary<DateTime, double>();

			for (int month = 1; month <= 12; month++)
			{
				int daysInMonth = DateTime.DaysInMonth(2021, month);
				for (int day = 1; day <= daysInMonth; day++)
				{
					clients.Add(new DateTime(2021, month, day), random.Next(0, 3000));
				}
			}
		}

		private void ChangeDateCommandExecute(string args)
		{
			if (string.IsNullOrEmpty(args))
				throw new ArgumentNullException(nameof(args));
			else if (args == "+")
				selectedDate = selectedDate.AddMonths(1);
			else if (args == "-")
				selectedDate = selectedDate.AddMonths(-1);
			FillChartMonth();
		}

		private void FillChartYear()
		{
			if (yearAxis != null)
				yearAxis.AxisChanged -= Axis_AxisChanged;
			modelYear = new PlotModel
			{
				PlotType = PlotType.XY,
				Title = "Año completo"
			};


			foreach (int year in clients.Keys.Select(d => d.Year).Distinct())
			{
				foreach (IGrouping<int, DateTime> monthValues in clients.Keys.Where(d => d.Year == year).GroupBy(d => d.Month))
				{
					LineSeries serie = new LineSeries();
					serie.MarkerType = MarkerType.Circle;

					var list = clients.Where(c => c.Key.Year == year && c.Key.Month == monthValues.Key)
									  .Select(c => new DataPoint(DateTimeAxis.ToDouble(c.Key), c.Value));

					serie.Points.AddRange(list);
					serie.Title = $"{monthValues.Key}-{year}";
					
					modelYear.Series.Add(serie);
				}
			}

			yearAxis = new DateTimeAxis
			{
				Position = AxisPosition.Bottom
			};
			yearAxis.IntervalType = DateTimeIntervalType.Days;
			yearAxis.AxisChanged += Axis_AxisChanged;
			yearAxis.StringFormat = "dd/MM/yy";
			modelYear.Axes.Add(yearAxis);
			modelYear.Padding = new OxyThickness(10);
			Device.BeginInvokeOnMainThread(() => OxyPlotClientsYear.Model = modelYear);
		}

		private void Axis_AxisChanged(object sender, AxisChangedEventArgs e)
		{
			if (e.ChangeType == AxisChangeTypes.Zoom)
				DoLogic();
			else if (e.ChangeType != AxisChangeTypes.Zoom)
				return;
		}

		private void DoLogic()
		{
			// Do logic
		}

		private void FillChartMonth()
		{
			modelMonth = new PlotModel
			{
				PlotType = PlotType.XY,
				Title = "Por meses"
			};


			LineSeries serie = new LineSeries();
			serie.MarkerType = MarkerType.Circle;

			var list = clients.Where(c => c.Key.Year == selectedDate.Year && c.Key.Month == selectedDate.Month)
							  .Select(c => new DataPoint(DateTimeAxis.ToDouble(c.Key), c.Value));

			serie.Points.AddRange(list);
			serie.Title = $"{selectedDate.Month}-{selectedDate.Year}";
			modelMonth.Series.Add(serie);


			DateTimeAxis axis = new DateTimeAxis
			{
				Position = AxisPosition.Bottom
			};
			axis.StringFormat = "dd/MM";
			modelMonth.Axes.Add(axis);
			modelMonth.Padding = new OxyThickness(10);
			Device.BeginInvokeOnMainThread(() => OxyPlotClientsForMonthYear.Model = modelMonth);
		}
	}
}
