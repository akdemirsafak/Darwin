﻿using Darwin.Core.BaseDto;
using Darwin.Core.Entities;
using Darwin.Core.RepositoryCore;
using Darwin.Core.UnitofWorkCore;
using Darwin.Model.Common;
using Darwin.Service.Common;
using FluentValidation;

namespace Darwin.Service.Features.Contents.Commands;

public static class DeleteContent
{
    public record Command(Guid Id) : ICommand<DarwinResponse<NoContent>>;

    public class CommandHandler : ICommandHandler<Command, DarwinResponse<NoContent>>
    {
        private readonly IGenericRepository<Content> _contentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CommandHandler(IGenericRepository<Content> contentRepository, IUnitOfWork unitOfWork)
        {
            _contentRepository = contentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<DarwinResponse<NoContent>> Handle(Command request, CancellationToken cancellationToken)
        {
            var existMusic = await _contentRepository.GetAsync(x => x.Id == request.Id);
            if (existMusic == null)
                return DarwinResponse<NoContent>.Fail("");
            existMusic.IsUsable = false;
            existMusic.DeletedAt = DateTime.UtcNow.Ticks;
            await _contentRepository.UpdateAsync(existMusic);
            await _unitOfWork.CommitAsync();
            return DarwinResponse<NoContent>.Success(204);
        }
    }


    public class DeleteMusicCommandValidator : AbstractValidator<Command>
    {
        public DeleteMusicCommandValidator()
        {
            RuleFor(x => x.Id).NotNull();
        }
    }
}
