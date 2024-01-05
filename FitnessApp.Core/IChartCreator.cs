using FitnessApp.Models;
using LiveCharts;

namespace FitnessApp.Core;

public interface IChartCreator
{
    SeriesCollection GetRating();
    SeriesCollection GetWeight(Func<double> calcIdealWeight, Person selectedPerson);
}