using CSharpFunctionalExtensions;
using LifeQuality.Core.Dto;
using LifeQuality.Core.Services.Interfaces;
using LifeQuality.DAL.Model;
using MediatR;

namespace LifeQuality.Server.Comands.Analysis;

public class GetAnalysisQuery : IRequest<Result<List<AnalysisDto>, string>>
{
    public int UserId { get; set; }

    public AnalysisType AnalysisType { get; set; }

    public bool SortByDateDescending { get; set; }

    public class Handler : IRequestHandler<GetAnalysisQuery, Result<List<AnalysisDto>, string>>
    {
        private readonly IAnalysisService _analysisService;

        public Handler(IAnalysisService analysisService)
        {
            _analysisService = analysisService;
        }

        public async Task<Result<List<AnalysisDto>, string>> Handle(GetAnalysisQuery request,
            CancellationToken cancellationToken)
        {
            var analysis =
                await _analysisService.GetUserAnalysis(request.UserId, request.AnalysisType,
                    request.SortByDateDescending);

            if (!analysis?.Any() ?? true)
            {
                return Result.Failure<List<AnalysisDto>, string>("Analysis not fount");
            }

            return analysis.Select(a => new AnalysisDto()
            {
                Id = a.Id,
                PatientId = a.PatientId,
                AnalysisType = a.AnalysisType,
                AnalysisDate = a.AnalysisDate,
                LaboratoryName = a.LaboratoryName,
            }).ToList();
        }
    }
}