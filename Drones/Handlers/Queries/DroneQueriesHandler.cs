using AutoMapper;
using Drones.Dtos.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Drones.Entities.Drone;

namespace Drones.Handlers.Queries
{
    public class DroneQueriesHandler :
        IRequestHandler<CheckMedicationByDroneRequest, IEnumerable<MedicationResponse>>,
        IRequestHandler<CheckAvailablesDronesForLoadingRequest, IEnumerable<DroneResponse>>,
        IRequestHandler<CheckBatteryLevelForDroneRequest, CheckBatteryLevelForDroneResponse>

    {
        private readonly IMapper _mapper;
        private readonly ApiDbContext _context;

        public DroneQueriesHandler(IMapper mapper, ApiDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<IEnumerable<MedicationResponse>> Handle(CheckMedicationByDroneRequest request, CancellationToken cancellationToken)
        {
            var drone =
                await
                _context
                .Drones
                .FirstOrDefaultAsync(x => x.SerialNumber.Equals(request.SerialNumber));

            if (drone is null)
                throw new ArgumentException(
                    "There is no registered drone with the provided serial number");

            return _mapper.Map<IEnumerable<MedicationResponse>>(drone.Medications);
        }

        public async Task<IEnumerable<DroneResponse>> Handle(CheckAvailablesDronesForLoadingRequest request, CancellationToken cancellationToken)
        {
            decimal minBatteryCapacity = 0.25M;
            var availableDrones =
                await
                _context
                .Drones
                .Where(x => x.State != StateLevel.Idle && 
                            x.Medications.Sum(y => y.Weight) < x.WeightLimit &&
                            x.BatteryCapacity >= minBatteryCapacity)
                .ToListAsync();

            return _mapper.Map<IEnumerable<DroneResponse>>(availableDrones);
        }

        public async Task<CheckBatteryLevelForDroneResponse> Handle(CheckBatteryLevelForDroneRequest request, CancellationToken cancellationToken)
        {
            var drone =
                await
                _context
                .Drones
                .FirstOrDefaultAsync(x => x.SerialNumber.Equals(request.SerialNumber));

            if (drone is null)
                throw new ArgumentException(
                    "There is no registered drone with the provided serial number");

            return _mapper.Map<CheckBatteryLevelForDroneResponse>(drone);
        }

    }
}
