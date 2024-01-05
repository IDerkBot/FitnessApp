using System.Windows.Media;
using FitnessApp.Core;
using FitnessApp.Core.Interfaces;
using FitnessApp.Models;
using LiveCharts;
using LiveCharts.Helpers;
using LiveCharts.Wpf;

namespace FitnessApp.Wpf.Implementation;

public class ChartCreator : IChartCreator
{
    public SeriesCollection GetRating()
    {
        return new SeriesCollection
        {
            new ColumnSeries
            {
                Title = "Рейтинг",
                Values = Global.Database.GetAppRatingValues().AsChartValues()
            }
        };
    }

    public SeriesCollection GetWeight(double calcIdealWeight, Person selectedPerson)
    {
        return new SeriesCollection
        {
            new LineSeries
            {
                Title = "Ideal Weight",
                Values = Enumerable.Repeat(calcIdealWeight, 10).AsChartValues(),
                PointGeometry = null,
                Fill = Brushes.Transparent,
                Stroke = Brushes.ForestGreen,
                StrokeDashArray = new DoubleCollection { 3 },
            },

            new LineSeries
            {
                Title = "Weight",
                Values = Global.Database.GetWeightValues(selectedPerson.User.Id).AsChartValues(),
                // LabelPoint = _ => $"{Global.Database.GetWeightDates(CurrentPerson.User.Id)}"
            },

            new LineSeries
            {
                Title = "Target Weight",
                Values = Enumerable.Repeat(selectedPerson.TargetWeight, 10).AsChartValues(),
                PointGeometry = null,
                Fill = Brushes.Transparent,
                Stroke = Brushes.Red,
                StrokeDashArray = new DoubleCollection { 3 },
            }
        };
    }
}