﻿namespace Darwin.Domain.ResponseModels.Contents;

public class GetContentResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Lyrics { get; set; }
    public string ImageUrl { get; set; }
    public bool IsUsable { get; set; }
}
