using CSharpFunctionalExtensions;
using LifeQuality.Core.Dto;
using LifeQuality.Core.Services.Interfaces;
using LifeQuality.DAL.Model;
using MediatR;

namespace LifeQuality.Server.Comands.Analysis;

public class AddAnalysisQuery : IRequest<Result<int, string>>
{
    public int PatientId { get; set; }
    public string LaboratoryName { get; set; }

    public AnalysisType AnalysisType { get; set; }

    public DateTime AnalysisDate { get; set; }
    public string Data { get; set; }

    public class Handler : IRequestHandler<AddAnalysisQuery, Result<int, string>>
    {
        private readonly IAnalysisService _analysisService;

        public Handler(IAnalysisService analysisService)
        {
            _analysisService = analysisService;
        }

        public async Task<Result<int, string>> Handle(AddAnalysisQuery request,
            CancellationToken cancellationToken)
        {
            var addAnalysis =
                await _analysisService.AddNewAnalysis(request.PatientId, request.LaboratoryName, request.AnalysisType, request.AnalysisDate, request.Data);

            if (addAnalysis == 0)
            {
                return Result.Failure<int, string>("Analysis not added, make sure patient with provided id exists");
            }

            return addAnalysis;
        }
    }
}