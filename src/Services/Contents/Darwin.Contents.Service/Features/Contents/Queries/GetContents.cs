﻿using Darwin.Contents.Core.AbstractServices;
using Darwin.Contents.Core.Dtos.Responses.Content;
using Darwin.Contents.Service.Common;
using Darwin.Shared.Dtos;

namespace Darwin.Application.Features.Contents.Queries;

public static class GetContents
{
    public record Query() : IQuery<DarwinResponse<List<GetContentResponse>>>
    //, ICacheableQuery
    {
        //public string CachingKey => "GetContents";
        //public double CacheTime => 0.5;
    }

    public class QueryHandler : IQueryHandler<Query, DarwinResponse<List<GetContentResponse>>>
    {
        private readonly IContentService _contentService;

        public QueryHandler(IContentService contentService)
        {
            _contentService = contentService;
        }

        public async Task<DarwinResponse<List<GetContentResponse>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var getContentsResponse = await _contentService.GetAllAsync();

            return DarwinResponse<List<GetContentResponse>>.Success(getContentsResponse);
        }
    }
}
