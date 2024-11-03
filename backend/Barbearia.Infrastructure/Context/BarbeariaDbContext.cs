// Infrastructure/Context/BarbeariaDbContext.cs
using Barbearia.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Barbearia.Domain.Entities;

public class BarbeariaDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int,
    IdentityUserClaim<int>, IdentityUserRole<int>, IdentityUserLogin<int>,
    IdentityRoleClaim<int>, IdentityUserToken<int>>
{   
    // CTOR
    public BarbeariaDbContext(DbContextOptions<BarbeariaDbContext> options)
        : base(options)
    {
    }

    // DB SET
    public DbSet<Servico> Servicos { get; set; }
    public DbSet<Agendamento> Agendamentos { get; set; }


    // OVERRIDE
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Agendamento>(entity =>
        {
            entity.HasKey(e => e.Id);

            // Relacionamento com Cliente
            entity.HasOne(e => e.Cliente)
                .WithMany()
                .HasForeignKey(e => e.ClienteId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relacionamento muitos-para-muitos com Servico
            entity.HasMany(e => e.Servicos)
                .WithMany()
                .UsingEntity(
                    "AgendamentosServicos",
                    l => l.HasOne(typeof(Servico)).WithMany().HasForeignKey("ServicoId"),
                    r => r.HasOne(typeof(Agendamento)).WithMany().HasForeignKey("AgendamentoId"),
                    j =>
                    {
                        j.HasKey("AgendamentoId", "ServicoId");
                        j.ToTable("AgendamentosServicos");
                    });

            // Propriedades
            entity.Property(e => e.DataHorario)
                .IsRequired();

            entity.Property(e => e.Status)
                .IsRequired();

            entity.Property(e => e.ObservacaoCliente)
                .HasMaxLength(500);

            entity.Property(e => e.ObservacaoBarbeiro)
                .HasMaxLength(500);

            entity.Property(e => e.DataSolicitacao)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

            entity.Property(e => e.Ativo)
                .IsRequired()
                .HasDefaultValue(true);

            // Índices
            entity.HasIndex(e => e.DataHorario);
            entity.HasIndex(e => e.Status);
            entity.HasIndex(e => e.ClienteId);
        });

        // Configurar nomes das tabelas do Identity
        builder.Entity<ApplicationUser>(entity =>
        {
            entity.ToTable("Usuarios");
            entity.Property(e => e.NomeCompleto)
                  .IsRequired()
                  .HasMaxLength(100);
            entity.Property(e => e.Status)
                  .HasDefaultValue(true);
            entity.Property(e => e.DataCadastro)
                  .HasDefaultValueSql("GETDATE()");
            entity.Property(e => e.TipoUsuario)
                  .IsRequired();
        });

        builder.Entity<ApplicationRole>(entity =>
        {
            entity.ToTable("Perfis");
            entity.Property(e => e.Descricao)
                  .HasMaxLength(200);
        });

        builder.Entity<IdentityUserRole<int>>(entity =>
        {
            entity.ToTable("UsuariosPerfis");
        });

        builder.Entity<IdentityUserClaim<int>>(entity =>
        {
            entity.ToTable("UsuariosClaims");
        });

        builder.Entity<IdentityUserLogin<int>>(entity =>
        {
            entity.ToTable("UsuariosLogins");
        });

        builder.Entity<IdentityRoleClaim<int>>(entity =>
        {
            entity.ToTable("PerfisRoles");
        });

        builder.Entity<IdentityUserToken<int>>(entity =>
        {
            entity.ToTable("UsuariosTokens");
        });

        // Seed dos roles
        builder.Entity<ApplicationRole>().HasData(
            new ApplicationRole
            {
                Id = 1,
                Name = "Admin",
                NormalizedName = "ADMIN",
                Descricao = "Administrador do sistema",
                Status = true,
                ConcurrencyStamp = Guid.NewGuid().ToString()
            },
            new ApplicationRole
            {
                Id = 2,
                Name = "Cliente",
                NormalizedName = "CLIENTE",
                Descricao = "Cliente da barbearia",
                Status = true,
                ConcurrencyStamp = Guid.NewGuid().ToString()
            }
        );

        builder.Entity<Servico>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Nome)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.Descricao)
                .HasMaxLength(200);

            entity.Property(e => e.Status)
                .HasDefaultValue(true);

            entity.Property(e => e.DataCadastro)
                .HasDefaultValueSql("GETDATE()");
        });
    }
}