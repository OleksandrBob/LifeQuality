using LifeQuality.Core.StandartsInfo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeQuality.Core.StandartsInfo.Urine;

public class UrineRecord : IRecord
{
    public double pH { get; set; }

    public double Protein { get; set; }

    public double Glucose { get; set; }

    public double Microorganisms { get; set; }
}
