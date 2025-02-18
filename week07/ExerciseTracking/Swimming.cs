using System;
namespace ExerciseTracking
{
    public class Swimming : Activity
    {
        private int _laps;
        private const double LapDistanceInKm = 50.0 / 1000.0;
        private const double KmToMilesConversion = 0.62;

        public Swimming(DateTime date, int minutes, int laps) : base(date, minutes)
        {
            _laps = laps;
        }

        public override double GetDistance()
        {
            return _laps * LapDistanceInKm * KmToMilesConversion;
        }

        public override double GetSpeed()
        {
            return (GetDistance() / GetMinutes()) * 60;
        }

        public override double GetPace()
        {
            return GetMinutes() / GetDistance();
        }
    }
}