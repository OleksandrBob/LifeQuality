using CSharpFunctionalExtensions;
using LifeQuality.Core.Dto;
using LifeQuality.Core.Services.Interfaces;
using LifeQuality.DAL.Model;
using LifeQuality.Server.Comands.Analysis;
using MediatR;

namespace LifeQuality.Server.Comands.Patient;

public class GetPatientAnalysis : IRequest<Result<List<AnalysisDto>, string>>
{
    public int PatientId { get; set; }

    public class Handler : IRequestHandler<GetPatientAnalysis, Result<List<AnalysisDto>, string>>
    {
        private readonly IPatientService _patientService;

        public Handler(IPatientService patientService)
        {
            _patientService = patientService;
        }

        public async Task<Result<List<AnalysisDto>, string>> Handle(GetPatientAnalysis request, CancellationToken cancellationToken)
        {
            var analysisResult = await _patientService.GetPatientAnalysis(request.PatientId);

            if (!analysisResult.Any())
            {
                return Result.Failure<List<AnalysisDto>, string>("Analysis not found");
            }


            var result = analysisResult.Select(a => new AnalysisDto
            {
                Id = a.Id,
                PatientId = a.PatientId,
                AnalysisType = a.AnalysisType,
                AnalysisDate = a.AnalysisDate,
                LaboratoryName = a.LaboratoryName,
            }).ToList();

            return result;
        }
    }
}

