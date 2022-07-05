using System;
using System.Collections.Generic;
using System.Text;

namespace Lesson1_DAL
{
    public class DefaultSettings
    {
        public double PricePerMonth { get; set; } = 100;
        public double PriceForLost { get; set; } = 500;
        public double PriceForDamaged { get; set; } = 200;
        public double PriceForDelayed { get; set; } = 50;
        public int DaysForRent { get; set; } = 30;
    }
}
