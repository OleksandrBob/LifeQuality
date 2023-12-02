using CSharpFunctionalExtensions;
using LifeQuality.Core.Services.Interfaces;
using LifeQuality.Core.StandartsInfo;
using LifeQuality.DAL.Model;
using MediatR;

namespace LifeQuality.Server.Comands.Analysis;

public class GetAnalysisCheckedQuery : IRequest<Result<AnalysisCheckResult, string>>
{
    public int AnalysisId { get; set; }

    public Gender gender { get; set; }

    public Region region { get; set; }

    public AgeRange ageRange { get; set; }

    public HeightRange heightRange { get; set; }

    public class Handler : IRequestHandler<GetAnalysisCheckedQuery, Result<AnalysisCheckResult, string>>
    {
        private readonly IAnalysisService _analysisService;

        public Handler(IAnalysisService analysisService)
        {
            _analysisService = analysisService;
        }

        public async Task<Result<AnalysisCheckResult, string>> Handle(GetAnalysisCheckedQuery request,
            CancellationToken cancellationToken)
        {
            var analysis = await _analysisService.GetAnalysisById(request.AnalysisId);

            if (analysis is null)
            {
                return Result.Failure<AnalysisCheckResult, string>("Analysis with this Id is not found");
            }

            var standart = await _analysisService.GetStandartByParameters(
                analysis.AnalysisType,
                request.ageRange,
                request.gender,
                request.heightRange,
                request.region);

            var checkResult = _analysisService.CheckAnalysisDueToStandart(analysis, standart);

            return Result.Success<AnalysisCheckResult, string>(checkResult);
        }
    }
}
