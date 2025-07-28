using Renova.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Renova.Persistence
{
    public class RenovaDbContext : DbContext
    {
        public RenovaDbContext(DbContextOptions<RenovaDbContext> options) : base(options) { }

        public DbSet<UsuarioModel> Usuario { get; set; }
        public DbSet<ClienteModel> Cliente { get; set; }
        public DbSet<TelefoneModel> Telefone { get; set; }
        public DbSet<LojaModel> Loja { get; set; }
        public DbSet<ContasAPagarModel> ContasAPagar { get; set; }
        public DbSet<ContasAReceberModel> ContasAReceber { get; set; }
        public DbSet<ProdutoModel> Produto { get; set; }
        public DbSet<ProdutoMovimentacaoModel> ProdutoMovimentacao { get; set; }
        public DbSet<MovimentacaoModel> Movimentacao { get; set; }
        public DbSet<CorProdutoModel> CorProduto { get; set; }
        public DbSet<TipoProdutoModel> TipoProduto { get; set; }
        public DbSet<TamanhoProdutoModel> TamanhoProduto { get; set; }
        public DbSet<MarcaProdutoModel> MarcaProduto { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Usuario
            modelBuilder.Entity<UsuarioModel>(entity =>
            {
                entity.ToTable("Usuario");
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Id).ValueGeneratedOnAdd();
                entity.Property(p => p.Email).HasMaxLength(200).IsRequired();
                entity.Property(p => p.Nome).HasMaxLength(100).IsRequired();
                entity.Property(p => p.SenhaHash).HasMaxLength(256).IsRequired();
                entity.HasIndex(p => p.Email).IsUnique();

                entity.HasMany(p => p.Clientes)
                      .WithOne(p => p.Usuario)
                      .HasForeignKey(p => p.UsuarioId)
                      .OnDelete(DeleteBehavior.NoAction);

                entity.HasMany(p => p.Telefones)
                      .WithOne(p => p.Usuario)
                      .HasForeignKey(p => p.UsuarioId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(p => p.Lojas)
                        .WithOne(p => p.Usuario)
                        .HasForeignKey(p => p.UsuarioId)
                        .OnDelete(DeleteBehavior.NoAction);
            });

            // Cliente
            modelBuilder.Entity<ClienteModel>(entity =>
            {
                entity.ToTable("Cliente");
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Id).ValueGeneratedOnAdd();
                entity.Property(p => p.Referencia).IsRequired();
                entity.Property(p => p.Nome).HasMaxLength(100).IsRequired();
                entity.Property(p => p.UsuarioId).IsRequired();

                entity.HasOne(p => p.Usuario)
                      .WithMany(p => p.Clientes)
                      .HasForeignKey(p => p.UsuarioId)
                      .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(p => p.Loja)
                      .WithMany(p => p.Clientes)
                      .HasForeignKey(p => p.LojaId)
                      .OnDelete(DeleteBehavior.NoAction);

                entity.HasMany(p => p.ProdutosFornecidos)
                        .WithOne(p => p.Fornecedor)
                        .HasForeignKey(p => p.FornecedorId)
                        .OnDelete(DeleteBehavior.NoAction);
            });

            // Telefone
            modelBuilder.Entity<TelefoneModel>(entity =>
            {
                entity.ToTable("Telefone");
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Id).ValueGeneratedOnAdd();
                entity.Property(p => p.DDI).HasMaxLength(3);
                entity.Property(p => p.DDD).HasMaxLength(3);
                entity.Property(p => p.Numero).HasMaxLength(15).IsRequired();
                entity.Property(p => p.Whatsapp).IsRequired().HasDefaultValue(false);
                entity.Property(p => p.UsuarioId).IsRequired();

                entity.HasOne(p => p.Usuario)
                      .WithMany(p => p.Telefones)
                      .HasForeignKey(p => p.UsuarioId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Loja
            modelBuilder.Entity<LojaModel>(entity =>
            {
                entity.ToTable("Loja");
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Id).ValueGeneratedOnAdd();
                entity.Property(p => p.Nome).HasMaxLength(100).IsRequired();
                entity.Property(p => p.UsuarioId).IsRequired();

                entity.HasOne(p => p.Usuario)
                      .WithMany(p => p.Lojas)
                      .HasForeignKey(p => p.UsuarioId)
                      .OnDelete(DeleteBehavior.NoAction);

                entity.HasMany(p => p.Clientes)
                        .WithOne(p => p.Loja)
                        .HasForeignKey(p => p.LojaId)
                        .OnDelete(DeleteBehavior.NoAction);

                entity.HasMany(p => p.Produtos)
                        .WithOne(p => p.Loja)
                        .HasForeignKey(p => p.LojaId)
                        .OnDelete(DeleteBehavior.NoAction);

                entity.HasMany(p => p.Cores)
                        .WithOne(p => p.Loja)
                        .HasForeignKey(p => p.LojaId)
                        .OnDelete(DeleteBehavior.NoAction);

                entity.HasMany(p => p.Tamanhos)
                        .WithOne(p => p.Loja)
                        .HasForeignKey(p => p.LojaId)
                        .OnDelete(DeleteBehavior.NoAction);

                entity.HasMany(p => p.Marcas)
                        .WithOne(p => p.Loja)
                        .HasForeignKey(p => p.LojaId)
                        .OnDelete(DeleteBehavior.NoAction);

                entity.HasMany(p => p.Tipos)
                        .WithOne(p => p.Loja)
                        .HasForeignKey(p => p.LojaId)
                        .OnDelete(DeleteBehavior.NoAction);

                entity.HasMany(p => p.Movimentacoes)
                        .WithOne(p => p.Loja)
                        .HasForeignKey(p => p.LojaId)
                        .OnDelete(DeleteBehavior.NoAction);

                entity.HasMany(p => p.ContasAPagar)
                        .WithOne(p => p.Loja)
                        .HasForeignKey(p => p.LojaId)
                        .OnDelete(DeleteBehavior.NoAction);

                entity.HasMany(p => p.ContasAReceber)
                        .WithOne(p => p.Loja)
                        .HasForeignKey(p => p.LojaId)
                        .OnDelete(DeleteBehavior.NoAction);

                entity.HasMany(p => p.ProdutoMovimentacoes)
                        .WithOne(p => p.Loja)
                        .HasForeignKey(p => p.LojaId)
                        .OnDelete(DeleteBehavior.NoAction);
            });

            // ContasAPagar
            modelBuilder.Entity<ContasAPagarModel>(entity =>
            {
                entity.ToTable("ContasAPagar");
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Id).ValueGeneratedOnAdd();
                entity.Property(p => p.Valor).HasColumnType("decimal(18,2)").IsRequired();
                entity.Property(p => p.Status).HasMaxLength(20).IsRequired();
                entity.Property(p => p.OriginalId).IsRequired(false);
                entity.Property(p => p.NumParcela).IsRequired();
                entity.Property(p => p.TotParcela).IsRequired();
                entity.Property(p => p.DataVencimento).IsRequired();
                entity.Property(p => p.DataPagamento).IsRequired(false);
                entity.Property(p => p.MovimentacaoId).IsRequired();
                entity.Property(p => p.LojaId).IsRequired();

                entity.HasOne(p => p.Original)
                      .WithMany()
                      .HasForeignKey(p => p.OriginalId)
                      .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(p => p.Movimentacao)
                      .WithMany(p => p.ContasAPagar)
                      .HasForeignKey(p => p.MovimentacaoId)
                      .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(p => p.Loja)
                        .WithMany(p => p.ContasAPagar)
                        .HasForeignKey(p => p.LojaId)
                        .OnDelete(DeleteBehavior.NoAction);
            });

            // ContasAReceber
            modelBuilder.Entity<ContasAReceberModel>(entity =>
            {
                entity.ToTable("ContasAReceber");
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Id).ValueGeneratedOnAdd();
                entity.Property(p => p.Valor).HasColumnType("decimal(18,2)").IsRequired();
                entity.Property(p => p.Status).HasMaxLength(20).IsRequired();
                entity.Property(p => p.OriginalId).IsRequired(false);
                entity.Property(p => p.NumParcela).IsRequired();
                entity.Property(p => p.TotParcela).IsRequired();
                entity.Property(p => p.DataVencimento).IsRequired();
                entity.Property(p => p.DataPagamento).IsRequired(false);
                entity.Property(p => p.MovimentacaoId).IsRequired();
                entity.Property(p => p.LojaId).IsRequired();

                entity.HasOne(p => p.Original)
                      .WithMany()
                      .HasForeignKey(p => p.OriginalId)
                      .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(p => p.Movimentacao)
                      .WithMany(p => p.ContasAReceber)
                      .HasForeignKey(p => p.MovimentacaoId)
                      .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(p => p.Loja)
                        .WithMany(p => p.ContasAReceber)
                        .HasForeignKey(p => p.LojaId)
                        .OnDelete(DeleteBehavior.NoAction);
            });

            // Produto
            modelBuilder.Entity<ProdutoModel>(entity =>
            {
                entity.ToTable("Produto");
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Id).ValueGeneratedOnAdd();
                entity.Property(p => p.Referencia).IsRequired();
                entity.Property(p => p.Preco).HasColumnType("decimal(18,4)").IsRequired();
                entity.Property(p => p.Status).HasMaxLength(20).IsRequired();
                entity.Property(p => p.Descricao).HasMaxLength(200).IsRequired();
                entity.Property(p => p.DataEntrada).IsRequired();
                entity.Property(p => p.FornecedorId).IsRequired();
                entity.Property(p => p.CorId).IsRequired();
                entity.Property(p => p.TamanhoId).IsRequired();
                entity.Property(p => p.MarcaId).IsRequired();
                entity.Property(p => p.TipoId).IsRequired();
                entity.Property(p => p.LojaId).IsRequired();

                entity.HasOne(p => p.Fornecedor)
                      .WithMany(p => p.ProdutosFornecidos)
                      .HasForeignKey(p => p.FornecedorId)
                      .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(p => p.Cor)
                        .WithMany(c => c.Produtos)
                        .HasForeignKey(p => p.CorId)
                        .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(p => p.Tamanho)
                        .WithMany(t => t.Produtos)
                        .HasForeignKey(p => p.TamanhoId)
                        .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(p => p.Marca)
                        .WithMany(m => m.Produtos)
                        .HasForeignKey(p => p.MarcaId)
                        .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(p => p.Tipo)
                        .WithMany(t => t.Produtos)
                        .HasForeignKey(p => p.TipoId)
                        .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(p => p.Loja)
                        .WithMany(l => l.Produtos)
                        .HasForeignKey(p => p.LojaId)
                        .OnDelete(DeleteBehavior.NoAction);

                entity.HasMany(p => p.Movimentacoes)
                        .WithOne(pm => pm.Produto)
                        .HasForeignKey(pm => pm.ProdutoId)
                        .OnDelete(DeleteBehavior.NoAction);
            });

            // ProdutoMovimentacao
            modelBuilder.Entity<ProdutoMovimentacaoModel>(entity =>
            {
                entity.ToTable("ProdutoMovimentacao");
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Id).ValueGeneratedOnAdd();
                entity.Property(p => p.Valor).HasColumnType("decimal(18,4)").IsRequired();
                entity.Property(p => p.ProdutoId).IsRequired();
                entity.Property(p => p.MovimentacaoId).IsRequired();
                entity.Property(p => p.LojaId).IsRequired();

                entity.HasOne(p => p.Produto)
                      .WithMany(p => p.Movimentacoes)
                      .HasForeignKey(p => p.ProdutoId)
                      .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(p => p.Movimentacao)
                        .WithMany(m => m.ProdutoMovimentacoes)
                        .HasForeignKey(p => p.MovimentacaoId)
                        .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(p => p.Loja)
                        .WithMany(l => l.ProdutoMovimentacoes)
                        .HasForeignKey(p => p.LojaId)
                        .OnDelete(DeleteBehavior.NoAction);
            });

            // Movimentacao
            modelBuilder.Entity<MovimentacaoModel>(entity =>
            {
                entity.ToTable("Movimentacao");
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Id).ValueGeneratedOnAdd();
                entity.Property(p => p.Data).IsRequired();
                entity.Property(p => p.Tipo).IsRequired();
                entity.Property(p => p.LojaId).IsRequired();

                entity.HasOne(p => p.Loja)
                      .WithMany(l => l.Movimentacoes)
                      .HasForeignKey(p => p.LojaId)
                      .OnDelete(DeleteBehavior.NoAction);

                entity.HasMany(p => p.ProdutoMovimentacoes)
                        .WithOne(pm => pm.Movimentacao)
                        .HasForeignKey(pm => pm.MovimentacaoId)
                        .OnDelete(DeleteBehavior.NoAction);

                entity.HasMany(p => p.ContasAPagar)
                        .WithOne(cp => cp.Movimentacao)
                        .HasForeignKey(cp => cp.MovimentacaoId)
                        .OnDelete(DeleteBehavior.NoAction);

                entity.HasMany(p => p.ContasAReceber)
                        .WithOne(cr => cr.Movimentacao)
                        .HasForeignKey(cr => cr.MovimentacaoId)
                        .OnDelete(DeleteBehavior.NoAction);

                entity.HasMany(p => p.ProdutoMovimentacoes)
                        .WithOne(pm => pm.Movimentacao)
                        .HasForeignKey(pm => pm.MovimentacaoId)
                        .OnDelete(DeleteBehavior.NoAction);
            });

            // CorProduto
            modelBuilder.Entity<CorProdutoModel>(entity =>
            {
                entity.ToTable("CorProduto");
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Id).ValueGeneratedOnAdd();
                entity.Property(p => p.Valor).HasMaxLength(100).IsRequired();
                entity.Property(p => p.LojaId).IsRequired();

                entity.HasOne(p => p.Loja)
                      .WithMany(l => l.Cores)
                      .HasForeignKey(p => p.LojaId)
                      .OnDelete(DeleteBehavior.NoAction);

                entity.HasMany(p => p.Produtos)
                        .WithOne(p => p.Cor)
                        .HasForeignKey(p => p.CorId)
                        .OnDelete(DeleteBehavior.NoAction);

            });

            // TipoProduto
            modelBuilder.Entity<TipoProdutoModel>(entity =>
            {
                entity.ToTable("TipoProduto");
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Id).ValueGeneratedOnAdd();
                entity.Property(p => p.Valor).HasMaxLength(50).IsRequired();
                entity.Property(p => p.LojaId).IsRequired();

                entity.HasOne(p => p.Loja)
                      .WithMany(l => l.Tipos)
                      .HasForeignKey(p => p.LojaId)
                      .OnDelete(DeleteBehavior.NoAction);

                entity.HasMany(p => p.Produtos)
                        .WithOne(p => p.Tipo)
                        .HasForeignKey(p => p.TipoId)
                        .OnDelete(DeleteBehavior.NoAction);
            });

            // TamanhoProduto
            modelBuilder.Entity<TamanhoProdutoModel>(entity =>
            {
                entity.ToTable("TamanhoProduto");
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Id).ValueGeneratedOnAdd();
                entity.Property(p => p.Valor).HasMaxLength(50).IsRequired();
                entity.Property(p => p.LojaId).IsRequired();

                entity.HasOne(p => p.Loja)
                      .WithMany(l => l.Tamanhos)
                      .HasForeignKey(p => p.LojaId)
                      .OnDelete(DeleteBehavior.NoAction);

                entity.HasMany(p => p.Produtos)
                        .WithOne(p => p.Tamanho)
                        .HasForeignKey(p => p.TamanhoId)
                        .OnDelete(DeleteBehavior.NoAction);
            });

            // MarcaProduto
            modelBuilder.Entity<MarcaProdutoModel>(entity =>
            {
                entity.ToTable("MarcaProduto");
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Id).ValueGeneratedOnAdd();
                entity.Property(p => p.Valor).HasMaxLength(100).IsRequired();
                entity.Property(p => p.LojaId).IsRequired();

                entity.HasOne(p => p.Loja)
                      .WithMany(l => l.Marcas)
                      .HasForeignKey(p => p.LojaId)
                      .OnDelete(DeleteBehavior.NoAction);

                entity.HasMany(p => p.Produtos)
                        .WithOne(p => p.Marca)
                        .HasForeignKey(p => p.MarcaId)
                        .OnDelete(DeleteBehavior.NoAction);
            });
        }

    }
}
