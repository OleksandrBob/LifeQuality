using LifeQuality.Core.StandartsInfo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeQuality.Core.StandartsInfo.Stool;

public class StoolRecord : IRecord
{
    public double Mucus { get; set; }

    public double Blood { get; set; }

    public double WhiteBloodCells { get; set; }

    public double Microorganisms { get; set; }
}
