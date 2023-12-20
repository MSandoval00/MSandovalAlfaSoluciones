using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DL;

public partial class MsandovalAlfaSolucionesContext : DbContext
{
    public MsandovalAlfaSolucionesContext()
    {
    }

    public MsandovalAlfaSolucionesContext(DbContextOptions<MsandovalAlfaSolucionesContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Alumno> Alumnos { get; set; }

    public virtual DbSet<AlumnoBeca> AlumnoBecas { get; set; }

    public virtual DbSet<Beca> Becas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-G7UVJH5; Database= MSandovalAlfaSoluciones; TrustServerCertificate=True;Trusted_Connection=True; User ID=sa; Password=pass@word1;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Alumno>(entity =>
        {
            entity.HasKey(e => e.IdAlumno).HasName("PK__Alumno__460B474030689174");

            entity.ToTable("Alumno");

            entity.HasIndex(e => e.Email, "UQ__Alumno__A9D105344DD9B4C0").IsUnique();

            entity.Property(e => e.Email)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(250)
                .IsUnicode(false);
        });

        modelBuilder.Entity<AlumnoBeca>(entity =>
        {
            entity.HasKey(e => e.IdAlumnoBeca).HasName("PK__AlumnoBe__E458C38E531CB241");

            entity.ToTable("AlumnoBeca");

            entity.HasOne(d => d.IdAlumnoNavigation).WithMany(p => p.AlumnoBecas)
                .HasForeignKey(d => d.IdAlumno)
                .HasConstraintName("FK__AlumnoBec__IdAlu__21B6055D");

            entity.HasOne(d => d.IdBecaNavigation).WithMany(p => p.AlumnoBecas)
                .HasForeignKey(d => d.IdBeca)
                .HasConstraintName("FK__AlumnoBec__IdBec__22AA2996");
        });

        modelBuilder.Entity<Beca>(entity =>
        {
            entity.HasKey(e => e.IdBeca).HasName("PK__Beca__23D228E0C7C69168");

            entity.ToTable("Beca");

            entity.Property(e => e.Tipo)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
