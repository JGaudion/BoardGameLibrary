﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BoardGameLibrary.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class BoardGameLibraryDatabaseEntities : DbContext
    {
        public BoardGameLibraryDatabaseEntities()
            : base("name=BoardGameLibraryDatabaseEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Game> Games { get; set; }
        public virtual DbSet<Expansion> Expansions { get; set; }
        public virtual DbSet<Keyword> Keywords { get; set; }
        public virtual DbSet<Play> Plays { get; set; }
        public virtual DbSet<Request> Requests { get; set; }
        public virtual DbSet<UserOpinion> UserOpinions { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
    }
}
