﻿using System.ComponentModel.DataAnnotations;

namespace Darwin.Core.Entities;

public class Music:BaseEntity
{
    [Required, MinLength(3), MaxLength(64)]
    public string Name { get; set; }
    public string ImageUrl { get; set; }
    public bool IsUsable { get; set; }
    public virtual IList<MusicCategory> Categories { get; set; }
    public virtual IList<MusicMood> Moods { get; set; }
}
