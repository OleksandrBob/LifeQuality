using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeQuality.Core.StandartsInfo.Stool;

public class StoolStandart
{
    public (double, double) Mucus { get; set; }

    public (double, double) Blood { get; set; }

    public (double, double) WhiteBloodCells { get; set; }

    public (double, double) Microorganisms { get; set; }
}