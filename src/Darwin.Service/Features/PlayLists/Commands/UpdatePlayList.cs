﻿using Darwin.Core.BaseDto;
using Darwin.Core.Entities;
using Darwin.Core.RepositoryCore;
using Darwin.Core.UnitofWorkCore;
using Darwin.Model.Request.PlayLists;
using Darwin.Model.Response.PlayLists;
using Darwin.Service.Common;
using Mapster;

namespace Darwin.Service.Features.PlayLists.Commands;

public static class UpdatePlayList
{
    public record Command(Guid id, UpdatePlayListRequest Model, string creatorId) : ICommand<DarwinResponse<UpdatedPlayListResponse>>;

    public class CommandHandler(IGenericRepository<PlayList> _playListRepository) 
        : ICommandHandler<Command, DarwinResponse<UpdatedPlayListResponse>>
    {
        public async Task<DarwinResponse<UpdatedPlayListResponse>> Handle(Command request, CancellationToken cancellationToken)
        {
            var hasList= await _playListRepository.GetAsync(x=>x.Id==request.id && x.Creator.Id==request.creatorId);
            if (hasList is null)
                return DarwinResponse<UpdatedPlayListResponse>.Fail("Liste bulunamadı.", 404);

            if (!hasList.IsFavorite)
                hasList.IsUsable = request.Model.IsUsable;
 
            hasList.Name = request.Model.Name;
            hasList.Description = request.Model.Description;
            hasList.IsPublic = request.Model.IsPublic;

            await _playListRepository.UpdateAsync(hasList);

            return DarwinResponse<UpdatedPlayListResponse>.Success(hasList.Adapt<UpdatedPlayListResponse>());
        }
    }
}
