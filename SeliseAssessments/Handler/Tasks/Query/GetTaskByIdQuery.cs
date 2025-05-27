using AutoMapper;
using MediatR;

namespace SeliseAssessments.Handler.Tasks.Query;
public record GetTaskByIdQuery(int Id) : IRequest<TaskItemDto>;

public class GetTaskByIdQueryHandler : IRequestHandler<GetTaskByIdQuery, TaskItemDto>
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public GetTaskByIdQueryHandler(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<TaskItemDto> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
    {
        var task = await _context.Tasks
            .Include(t => t.AssignedToUser)
            .Include(t => t.CreatedByUser)
            .Include(t => t.Team)
            .FirstOrDefaultAsync(t => t.Id == request.Id);

        return task == null ? null : _mapper.Map<TaskItemDto>(task);
    }
}