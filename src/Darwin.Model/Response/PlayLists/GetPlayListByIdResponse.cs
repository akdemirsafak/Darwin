﻿using Darwin.Model.Response.Musics;

namespace Darwin.Model.Response.PlayLists;

public class GetPlayListByIdResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsUsable { get; set; }
    public bool IsPublic { get; set; }
    public virtual IList<GetMusicResponse> Musics { get; set; }
}
