﻿using Darwin.Core.BaseDto;
using Darwin.Core.Entities;
using Darwin.Core.RepositoryCore;
using Darwin.Core.UnitofWorkCore;
using Darwin.Model.Common;
using Darwin.Service.Common;

namespace Darwin.Service.Musics.Commands.Delete;

public class DeleteMusicCommand : ICommand<DarwinResponse<NoContent>>
{
    public Guid Id { get; }

    public DeleteMusicCommand(Guid id)
    {
        Id = id;
    }

    public class Handler : ICommandHandler<DeleteMusicCommand, DarwinResponse<NoContent>>
    {
        private readonly IGenericRepository<Music> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IGenericRepository<Music> repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<DarwinResponse<NoContent>> Handle(DeleteMusicCommand request, CancellationToken cancellationToken)
        {
            var existMusic= await _repository.GetAsync(x=>x.Id==request.Id);
            if (existMusic == null)
                return DarwinResponse<NoContent>.Fail("");
            existMusic.IsUsable = false;
            existMusic.DeletedAt = DateTime.UtcNow.Ticks;
            await _repository.UpdateAsync(existMusic);
            await _unitOfWork.CommitAsync();
            return DarwinResponse<NoContent>.Success(204);
        }
    }
}
