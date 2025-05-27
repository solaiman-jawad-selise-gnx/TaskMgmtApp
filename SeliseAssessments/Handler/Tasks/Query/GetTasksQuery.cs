using AutoMapper;
using MediatR;
using SeliseAssessments.Model;

namespace SeliseAssessments.Handler.Tasks.Query;
public class GetTasksQuery : IRequest<IEnumerable<TaskItemDto>>
{
    public TaskQueryParameters Parameters { get; set; }

    public GetTasksQuery(TaskQueryParameters parameters)
    {
        Parameters = parameters;
    }
}

public class GetTasksQueryHandler : IRequestHandler<GetTasksQuery, IEnumerable<TaskItemDto>>
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public GetTasksQueryHandler(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TaskItemDto>> Handle(GetTasksQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Tasks
            .Include(t => t.AssignedToUser)
            .Include(t => t.CreatedByUser)
            .Include(t => t.Team)
            .AsQueryable();

        var p = request.Parameters;

        if (p.Status.HasValue)
            query = query.Where(t => t.Status == p.Status);

        if (p.AssignedToUserId.HasValue)
            query = query.Where(t => t.AssignedToUserId == p.AssignedToUserId);

        if (p.TeamId.HasValue)
            query = query.Where(t => t.TeamId == p.TeamId);

        if (p.DueDate.HasValue)
            query = query.Where(t => t.DueDate.Date == p.DueDate.Value.Date);

        query = query
            .OrderBy(t => t.DueDate)
            .Skip((p.Page - 1) * p.PageSize)
            .Take(p.PageSize);

        var result = await query.ToListAsync(cancellationToken);
        return _mapper.Map<List<TaskItemDto>>(result);
    }
}