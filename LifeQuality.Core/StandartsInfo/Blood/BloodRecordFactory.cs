using LifeQuality.Core.StandartsInfo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeQuality.Core.StandartsInfo.Blood;

public class BloodRecordFactory : IRecordFactory
{
    public IRecord CreateRecord()
    {
        return new BloodRecord();
    }
}