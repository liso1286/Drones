using AutoMapper;
using Drones.Dtos.Command;
using Drones.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Drones.Entities.Drone;

namespace Drones.Handlers.Command
{
    public class DroneCommandHandler :
        IRequestHandler<CreateModifyDroneRequest, CreateModifyDroneResponse>,
        IRequestHandler<LoadDroneRequest, LoadDroneResponse>
    {
        private readonly IMapper _mapper;
        private readonly ApiDbContext _context;

        public DroneCommandHandler(IMapper mapper, ApiDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<CreateModifyDroneResponse> Handle(CreateModifyDroneRequest request, CancellationToken cancellationToken)
        {
            ValidateParams(request);

            var drone = await
                _context
                .Drones
                .FirstOrDefaultAsync(x => x.SerialNumber.Equals(request.SerialNumber)) ??
                _mapper.Map<Drone>(request);

            drone = _mapper.Map(request, drone);

            if (drone.State != StateLevel.Idle)
                throw new Exception($"Drone is actually {drone.State}. " +
                                    $"Only idle drones can be modified");

            if (drone.Id < 1)
            {
                if (_context.Drones.Count() > 9)
                    throw new Exception("The maximum number of the fleet has been reached");
                drone = _context.Drones.Add(drone).Entity;
            }
            else drone = _context.Drones.Update(drone).Entity;

            await _context.SaveChangesAsync();
            return _mapper.Map<CreateModifyDroneResponse>(drone);
        }

        private static void ValidateParams(CreateModifyDroneRequest request)
        {
            if (string.IsNullOrEmpty(request.SerialNumber))
                throw new Exception("Serial can't be null or empty");
            else if (request.SerialNumber.Length > 100)
                throw new Exception("Serial number length must be less than 100");
            else if (request.WeightLimit > 500)
                throw new Exception("The limit weight must not exceed 500");
        }

        public async Task<LoadDroneResponse> Handle(LoadDroneRequest request, CancellationToken cancellationToken)
        {
            var drone =
                await
                _context
                .Drones
                .FirstOrDefaultAsync(x => x.SerialNumber.Equals(request.DroneSerialNumber));

            if (drone is null)
                throw new ArgumentException(
                    "There is no registered drone with the provided serial number");

            drone.CreateAddMedications(
                request.Code,
                request.Name,
                request.Weight,
                string.Empty);

            drone = _context.Update(drone).Entity;
            await _context.SaveChangesAsync();
            var medicationResponse =
                _mapper.Map<HashSet<MedicationItem>>(drone.Medications);

            return new LoadDroneResponse
            {
                DroneSerialNumber = request.DroneSerialNumber,
                Medications = medicationResponse
            };
        }
    }
}
