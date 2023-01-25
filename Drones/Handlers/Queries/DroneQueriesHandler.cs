using AutoMapper;
using Drones.Dtos.Queries;
using MediatR;

namespace Drones.Handlers.Queries
{
    public class DroneQueriesHandler:
        IRequestHandler<CheckMedicationByDroneRequest, CheckMedicationByDroneResponse>,
        IRequestHandler<CheckAvailablesDronesForLoadingRequest, CheckAvailablesDronesForLoadingResponse>,
        IRequestHandler<CheckBatteryLevelForDroneRequest, CheckBatteryLevelForDroneResponse>,

    {
        private readonly IMapper _mapper;
        private readonly ApiDbContext _context;

        public DroneQueriesHandler(IMapper mapper, ApiDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public Task<CheckMedicationByDroneResponse> Handle(CheckMedicationByDroneRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<CheckAvailablesDronesForLoadingResponse> Handle(CheckAvailablesDronesForLoadingRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<CheckBatteryLevelForDroneResponse> Handle(CheckBatteryLevelForDroneRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
