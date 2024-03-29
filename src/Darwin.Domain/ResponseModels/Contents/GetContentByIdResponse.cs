﻿using Darwin.Domain.ResponseModels.Categories;
using Darwin.Domain.ResponseModels.Moods;

namespace Darwin.Domain.ResponseModels.Contents;

public class GetContentByIdResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Lyrics { get; set; }
    public string ImageUrl { get; set; }
    public bool IsUsable { get; set; }
    public virtual IList<GetMoodResponse> Moods { get; set; }
    public virtual IList<GetCategoryResponse> Categories { get; set; }
}
