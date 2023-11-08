﻿using Darwin.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Darwin.Infrastructure.DbContexts;

public class DarwinDbContext : IdentityDbContext<AppUser, AppRole, string>
{
    public DarwinDbContext(DbContextOptions<DarwinDbContext> options) : base(options)
    {

    }
    public DbSet<Content> Contents { get; set; }
    public DbSet<PlayList> PlayLists { get; set; }
    public DbSet<AgeRate> AgeRates { get; set; }
    public DbSet<Mood> Moods { get; set; }
    public DbSet<Category> Categories { get; set; }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Content>().Navigation(c => c.AgeRate).AutoInclude();
        builder.Entity<PlayList>().Navigation(c => c.Contents).AutoInclude();
        builder.Entity<Content>().Navigation(x => x.Categories).AutoInclude();
        builder.Entity<Content>().Navigation(x => x.Moods).AutoInclude();

        base.OnModelCreating(builder);
    }
}
