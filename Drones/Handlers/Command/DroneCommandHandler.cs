using AutoMapper;
using Drones.Dtos.Command;
using MediatR;

namespace Drones.Handlers.Command
{
    public class DroneCommandHandler:
        IRequestHandler<CreateModifyDroneRequest, CreateModifyDroneResponse>
    {
        private readonly IMapper _mapper;
        private readonly ApiDbContext _context;

        public DroneCommandHandler(IMapper mapper, ApiDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public Task<CreateModifyDroneResponse> Handle(CreateModifyDroneRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
