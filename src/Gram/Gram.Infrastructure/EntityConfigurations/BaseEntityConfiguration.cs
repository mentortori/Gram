﻿using Gram.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gram.Infrastructure.EntityConfigurations
{
    internal class BaseEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : class, IEntity
    {
        public void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(m => m.Id);
            builder.Property(m => m.RowVersion)
                .IsRowVersion()
                .IsRequired();
        }
    }
}
