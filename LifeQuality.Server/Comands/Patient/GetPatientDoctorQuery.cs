using CSharpFunctionalExtensions;
using LifeQuality.Core.Dto;
using LifeQuality.Core.Services.Interfaces;
using LifeQuality.DAL.Model;
using MediatR;

namespace LifeQuality.Server.Comands.Patient;

public class GetPatientDoctorQuery : IRequest<Result<Doctor>>
{
    public int PatientId { get; set; }

    public class Handler : IRequestHandler<GetPatientDoctorQuery, Result<Doctor>>
    {
        private readonly IPatientService _patientService;

        public Handler(IPatientService patientService)
        {
            _patientService = patientService;
        }

        public async Task<Result<Doctor>> Handle(GetPatientDoctorQuery request,
            CancellationToken cancellationToken)
        {
            var doctor = await _patientService.GetPatientDoctor(request.PatientId);

            if (doctor == null)
            {
                return Result.Failure<Doctor>("Doctor not fount");
            }

            var result = new Doctor
            {
                Id = doctor.Id,
                Name = doctor.Name,
                Email = doctor.Email,
                Surname = doctor.Surname
            };

            return Result.Success<Doctor>(result);
        }
    }
}
