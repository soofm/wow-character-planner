using MediatR;

namespace CharacterPlanner.Application.Profile.Queries;

public class GetProfileQuery : IRequest
{

}

public class GetProfileQueryHandler : AsyncRequestHandler<GetProfileQuery>
{
    protected override Task Handle(GetProfileQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}