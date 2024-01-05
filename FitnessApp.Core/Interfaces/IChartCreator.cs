using FitnessApp.Models;
using LiveCharts;

namespace FitnessApp.Core.Interfaces;

public interface IChartCreator
{
    SeriesCollection GetRating();
    SeriesCollection GetWeight(double calcIdealWeight, Person selectedPerson);
}