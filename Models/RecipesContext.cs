using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Recipes.Seed;

namespace Recipes.Models;

public partial class RecipesContext : DbContext
{
    public RecipesContext()
    {
    }

    public RecipesContext(DbContextOptions<RecipesContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Ingredient> Ingredients { get; set; }

    public virtual DbSet<Recipe> Recipes { get; set; }

    public virtual DbSet<RecipeIngredient> RecipeIngredients { get; set; }

    public virtual DbSet<RecipeStep> RecipeSteps { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Ingredient>(entity =>
        {
            entity.Property(e => e.Title).HasMaxLength(255);
        });

        modelBuilder.Entity<Recipe>(entity =>
        {
            entity.ToTable("Recipe");

            entity.Property(e => e.Category).HasMaxLength(255);
            entity.Property(e => e.Complexity).HasMaxLength(255);
            entity.Property(e => e.CreatedDate).HasColumnType("date");
            entity.Property(e => e.ImageUrl).HasMaxLength(255);
            entity.Property(e => e.PreparationTime).HasMaxLength(255);
            entity.Property(e => e.Title).HasMaxLength(255);
        });

        modelBuilder.Entity<RecipeIngredient>(entity =>
        {
            entity.HasOne(d => d.Ingredient).WithMany(p => p.RecipeIngredients)
                .HasForeignKey(d => d.IngredientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RecipeIngredients_Ingredients");

            entity.HasOne(d => d.Recipe).WithMany(p => p.RecipeIngredients)
                .HasForeignKey(d => d.RecipeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RecipeIngredients_Recipe");
        });

        modelBuilder.Entity<RecipeStep>(entity =>
        {
            entity.Property(e => e.ImageUrl).HasMaxLength(255);

            entity.HasOne(d => d.Recipe).WithMany(p => p.RecipeSteps)
                .HasForeignKey(d => d.RecipeId)
                .HasConstraintName("FK_RecipeSteps_Recipe");
        });

        OnModelCreatingPartial(modelBuilder);

        modelBuilder.Seed();
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
