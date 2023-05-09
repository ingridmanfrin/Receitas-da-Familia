using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ReceitasDeFamilia.Repositories.Entities;

public partial class ReceitasDeFamiliaDbContext : DbContext
{
    public ReceitasDeFamiliaDbContext()
    {
    }

    public ReceitasDeFamiliaDbContext(DbContextOptions<ReceitasDeFamiliaDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CategoriasReceita> CategoriasReceita { get; set; }

    public virtual DbSet<Familia> Familias { get; set; }

    public virtual DbSet<Favorito> Favoritos { get; set; }

    public virtual DbSet<Receita> Receitas { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=ITLNB109;Database=db_receitas_de_familia;Trusted_Connection=True;Trust Server Certificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CategoriasReceita>(entity =>
        {
            entity.HasKey(e => e.IdCategoria);

            entity.ToTable("CATEGORIAS_RECEITA");

            entity.Property(e => e.IdCategoria).HasColumnName("id_categoria");
            entity.Property(e => e.DataAlteracao)
                .HasColumnType("datetime")
                .HasColumnName("data_alteracao");
            entity.Property(e => e.DataCriacao)
                .HasColumnType("datetime")
                .HasColumnName("data_criacao");
            entity.Property(e => e.FoiDeletado)
                .IsRequired()
                .HasDefaultValueSql("('0')")
                .HasColumnName("foi_deletado");
            entity.Property(e => e.Nome)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nome");
            entity.Property(e => e.UsuarioAlteracao)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("usuario_alteracao");
            entity.Property(e => e.UsuarioCriacao)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("usuario_criacao");
        });

        modelBuilder.Entity<Familia>(entity =>
        {
            entity.HasKey(e => e.IdFamilia);

            entity.ToTable("FAMILIAS");

            entity.Property(e => e.IdFamilia).HasColumnName("id_familia");
            entity.Property(e => e.DataAlteracao)
                .HasColumnType("datetime")
                .HasColumnName("data_alteracao");
            entity.Property(e => e.DataCriacao)
                .HasColumnType("datetime")
                .HasColumnName("data_criacao");
            entity.Property(e => e.Descricao)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("descricao");
            entity.Property(e => e.FoiDeletado)
                .IsRequired()
                .HasDefaultValueSql("('0')")
                .HasColumnName("foi_deletado");
            entity.Property(e => e.Foto)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("foto");
            entity.Property(e => e.Nome)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nome");
            entity.Property(e => e.UsuarioAlteracao)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("usuario_alteracao");
            entity.Property(e => e.UsuarioCriacao)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("usuario_criacao");
        });

        modelBuilder.Entity<Favorito>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("FAVORITOS");

            entity.Property(e => e.DataAlteracao)
                .HasColumnType("datetime")
                .HasColumnName("data_alteracao");
            entity.Property(e => e.DataCriacao)
                .HasColumnType("datetime")
                .HasColumnName("data_criacao");
            entity.Property(e => e.FoiDeletado)
                .IsRequired()
                .HasDefaultValueSql("('0')")
                .HasColumnName("foi_deletado");
            entity.Property(e => e.IdReceita).HasColumnName("id_receita");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.UsuarioAlteracao)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("usuario_alteracao");
            entity.Property(e => e.UsuarioCriacao)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("usuario_criacao");

            entity.HasOne(d => d.IdReceitaNavigation).WithMany()
                .HasForeignKey(d => d.IdReceita)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FAVORITOS_fk0");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany()
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FAVORITOS_fk1");
        });

        modelBuilder.Entity<Receita>(entity =>
        {
            entity.HasKey(e => e.IdReceita).HasName("PK_USUARIO");

            entity.ToTable("RECEITAS");

            entity.Property(e => e.IdReceita).HasColumnName("id_receita");
            entity.Property(e => e.CriadorReceita)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("criador_receita");
            entity.Property(e => e.DataAlteracao)
                .HasColumnType("datetime")
                .HasColumnName("data_alteracao");
            entity.Property(e => e.DataCriacao)
                .HasColumnType("datetime")
                .HasColumnName("data_criacao");
            entity.Property(e => e.FoiDeletado)
                .IsRequired()
                .HasDefaultValueSql("('0')")
                .HasColumnName("foi_deletado");
            entity.Property(e => e.IdCategoria).HasColumnName("id_categoria");
            entity.Property(e => e.IdFamilia).HasColumnName("id_familia");
            entity.Property(e => e.InformacoesAdicionais)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("informacoes_adicionais");
            entity.Property(e => e.Ingredientes)
                .IsUnicode(false)
                .HasColumnName("ingredientes");
            entity.Property(e => e.ModoPreparo)
                .IsUnicode(false)
                .HasColumnName("modo_preparo");
            entity.Property(e => e.Nome)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nome");
            entity.Property(e => e.Rendimento)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("rendimento");
            entity.Property(e => e.TempoPreparoMin).HasColumnName("tempo_preparo_min");
            entity.Property(e => e.UsuarioAlteracao)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("usuario_alteracao");
            entity.Property(e => e.UsuarioCriacao)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("usuario_criacao");

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.Receita)
                .HasForeignKey(d => d.IdCategoria)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("RECEITAS_fk0");

            entity.HasOne(d => d.IdFamiliaNavigation).WithMany(p => p.Receita)
                .HasForeignKey(d => d.IdFamilia)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("RECEITAS_fk1");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario);

            entity.ToTable("USUARIO");

            entity.HasIndex(e => e.CodigoValidacaoEmail, "UQ__USUARIO__0038993E7BD4D65C").IsUnique();

            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.CodigoValidacaoEmail).HasColumnName("codigo_validacao_email");
            entity.Property(e => e.DataAlteracao)
                .HasColumnType("datetime")
                .HasColumnName("data_alteracao");
            entity.Property(e => e.DataCriacao)
                .HasColumnType("datetime")
                .HasColumnName("data_criacao");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.EmailValidado)
                .IsRequired()
                .HasDefaultValueSql("('0')")
                .HasColumnName("email_validado");
            entity.Property(e => e.FoiDeletado)
                .IsRequired()
                .HasDefaultValueSql("('0')")
                .HasColumnName("foi_deletado");
            entity.Property(e => e.Nome)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nome");
            entity.Property(e => e.Salt)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("salt");
            entity.Property(e => e.Senha)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("senha");
            entity.Property(e => e.UsuarioAlteracao)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("usuario_alteracao");
            entity.Property(e => e.UsuarioCriacao)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("usuario_criacao");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
