using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldCupData.Model
{
    public class Weather
    {
        public string Humidity { get; set; }
        public string TempCelsius { get; set; }
        public string TempFarenheit { get; set; }
        public string WindSpeed { get; set; }
        public string Description { get; set; }
    }
}
