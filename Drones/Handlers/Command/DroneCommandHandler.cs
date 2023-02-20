using AutoMapper;
using Drones.Dtos.Command;
using Drones.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

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
            var drone = await CreateModifyValidation(request);
            drone = _mapper.Map(request, drone);

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

        public async Task<LoadDroneResponse> Handle(LoadDroneRequest request, CancellationToken cancellationToken)
        {
            var drone = await
                _context
                .Drones
                .FirstOrDefaultAsync(x => x.SerialNumber.Equals(request.DroneSerialNumber));

            drone = LoadingValidation(request, drone);

            drone.State = Drone.StateLevel.Loading;
            drone = _context.Update(drone).Entity;
            await _context.SaveChangesAsync();

            drone.CreateAddMedications(
                request.Code,
                request.Name,
                request.Weight,
                request.ImagePath);

            drone.State = Drone.StateLevel.Loaded;
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

        private async Task<Drone> CreateModifyValidation(CreateModifyDroneRequest request)
        {
            if (string.IsNullOrEmpty(request.SerialNumber))
                throw new Exception("Serial can't be null or empty");
            else if (request.SerialNumber.Length > 100)
                throw new Exception("Serial number length must be less than 100");
            else if (request.WeightLimit > 500)
                throw new Exception("The limit weight must not exceed 500");

            var drone = await
                _context
                .Drones
                .FirstOrDefaultAsync(x => x.SerialNumber.Equals(request.SerialNumber)) ??
                _mapper.Map<Drone>(request);

            var totalWight = drone.Medications is null ? 0 : drone.Medications.Sum(x => x.Weight);

            if (drone.State == Drone.StateLevel.Loading && totalWight > 0)
                throw new Exception("Drones while carrying medications cant'n change their parameters");
            else if (drone.State == Drone.StateLevel.Idle && totalWight > 0)
                throw new Exception("To put the drone in available state use the Deactivate service");
            else if (request.WeightLimit < totalWight)
                throw new Exception("The maximum load capacity must exceed the actual weight");

            return drone;
        }

        private static Drone LoadingValidation(LoadDroneRequest request, Drone? drone)
        {
            if (drone is null)
                throw new ArgumentException(
                    "There is no registered drone with the provided serial number");
            var totalWeight = drone.Medications.Sum(x => x.Weight) + request.Weight;
            var removeWheight = totalWeight - drone.WeightLimit;
            if (totalWeight > drone.WeightLimit)
                throw new ArgumentException(
                    $"The weight of the medication exceeds the carrying capacity. " +
                    $"Please remove {removeWheight} g.");
            if (drone.BatteryCapacity < 0.25M)
                throw new Exception("The drone cannot fly with the battery level below 25%");

            var validName =
                 request.Name.All(x => Char.IsLetterOrDigit(x) ||
                                  x.Equals('-') ||
                                  x.Equals('_'));

            var validCode = request.Name.All(x => (Char.IsLetter(x) &&
                                                   Char.IsUpper(x)) ||
                                             x.Equals('_') ||
                                             Char.IsNumber(x));
            if (!validName)
                throw new Exception("The name of the medicine can only contain letters, numbers and the characters \"-\" and \"_\"");
            if (!validCode)
                throw new Exception("The name of the medicine can only contain upper letters, numbers and the character \"_\"");
            return drone;
        }
    }
}
