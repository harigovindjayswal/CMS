using Application.Interfaces;
using Application.MasterData.CaseTypes.DTO;
using Domain.AppEntities;
using MediatR;
using Persistence.Context;

namespace Application.MasterData.CaseTypes.Commands;

public class CreateCaseType
{
    public class Command : IRequest<int>
    {
        public required CaseTypeDto CaseType { get; set; }
    }

    public class Handler(CmsContext context, IUserAccessor userAccessor) : IRequestHandler<Command, int>
    {
        public async Task<int> Handle(Command request, CancellationToken cancellationToken)
        {
            var entity = new CaseTypeMst
            {
                TypeName = request.CaseType.TypeName.Trim(),
                IsActive = request.CaseType.IsActive,
                CreatedBy = userAccessor.GetUserId(),
                CreatedDate = DateTime.Now
            };

            context.CaseTypes.Add(entity);
            await context.SaveChangesAsync(cancellationToken);
            return entity.CaseTypeId;
        }
    }
}

