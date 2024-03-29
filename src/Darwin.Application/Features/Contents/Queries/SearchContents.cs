﻿using Darwin.Application.Common;
using Darwin.Application.Services;
using Darwin.Domain.BaseDto;
using Darwin.Domain.ResponseModels.Contents;
using FluentValidation;

namespace Darwin.Application.Features.Contents.Queries;

public static class SearchContents
{
    public record Query(string SearchText) : IQuery<DarwinResponse<List<SearchContentResponse>>>;
    public class QueryHandler : IQueryHandler<Query, DarwinResponse<List<SearchContentResponse>>>
    {
        private readonly IContentService _contentService;

        public QueryHandler(IContentService contentService)
        {
            _contentService = contentService;
        }

        public async Task<DarwinResponse<List<SearchContentResponse>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var searchContentsResponse= await _contentService.SearchAsync(request.SearchText);
            return DarwinResponse<List<SearchContentResponse>>.Success(searchContentsResponse);
        }
    }

    public class SearchMusicsQueryValidator : AbstractValidator<Query>
    {
        public SearchMusicsQueryValidator()
        {
            RuleFor(x => x.SearchText)
                .NotNull()
                .NotEmpty()
                .Length(3, 25);
        }
    }
}


