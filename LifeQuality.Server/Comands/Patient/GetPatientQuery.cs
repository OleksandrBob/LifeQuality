using CSharpFunctionalExtensions;
using LifeQuality.Core;
using LifeQuality.Core.Dto;
using LifeQuality.Core.Services.Interfaces;
using MediatR;

namespace LifeQuality.Server.Comands.Patient;

public class GetPatientQuery : IRequest<Result<List<PatientDto>, string>>
{
    //Should be read from the user JWT. For now let it be passed as a parameter
    public int DoctorId { get; set; }

    public string SearchQuery { get; set; }

    public class Handler : IRequestHandler<GetPatientQuery, Result<List<PatientDto>, string>>
    {
        private readonly IPatientService _patientService;

        public Handler(IPatientService patientService)
        {
            _patientService = patientService;
        }

        public async Task<Result<List<PatientDto>, string>> Handle(GetPatientQuery request,
            CancellationToken cancellationToken)
        {
            var patient = await _patientService.GetPatientsBy(request.DoctorId, request.SearchQuery);

            if (!patient?.Any() ?? true)
            {
                return Result.Failure<List<PatientDto>, string>("Patient not fount");
            }

            var patientDtos = patient.Select(p => new PatientDto
            {
                Id = p.Id,
                Name = p.Name,
                Email = p.Email,
                Surname = p.Surname,
                PhoneNumber = p.PhoneNumber,
            }).ToList();

            return Result.Success<List<PatientDto>, string>(patientDtos);
        }
    }
}
