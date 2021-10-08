using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DataContext : DbContext
    {
        //Construtor para definir como será conectado com o BD
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        //Lista de propriedades que vão definir quais classes vão virar tabelas.
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
    }
}