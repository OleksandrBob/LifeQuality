using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeQuality.Core.StandartsInfo.Urine;

public class UrineStandart
{
    public (double, double) pH { get; set; }

    public (double, double) Protein { get; set; }

    public (double, double) Glucose { get; set; }

    public (double, double) Microorganisms { get; set; }
}

