using LifeQuality.Core.StandartsInfo;
using LifeQuality.Core.StandartsInfo.Blood;
using LifeQuality.Core.StandartsInfo.Stool;
using LifeQuality.Core.StandartsInfo.Urine;
using LifeQuality.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeQuality.Core.Services.Interfaces
{
    public interface IAnalysisAdapter
    {
        AnalysisCheckResult CheckBloodAnalysis(BloodRecord analysis, BloodStandart standart);

        AnalysisCheckResult CheckUrineAnalysis(UrineRecord analysis, UrineStandart standart);

        AnalysisCheckResult CheckStoolAnalysis(StoolRecord analysis, StoolStandart standart);
    }
}
