﻿using System.ComponentModel.DataAnnotations;

namespace Darwin.Core.Entities;

public class Category : BaseEntity
{
    public Category()
    {
        Musics = new HashSet<Music>();
    }

    [Required, MinLength(3), MaxLength(64)]
    public string Name { get; set; }
    public string? ImageUrl { get; set; }
    public bool IsUsable { get; set; }
    public virtual ICollection<Music> Musics { get; set; }
}