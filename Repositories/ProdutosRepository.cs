using AprendendoAPIComPersistencia.Models;
using AprendendoAPIComPersistencia.ViewModels;

namespace AprendendoAPIComPersistencia.Repositories
{
    public class ProdutosRepository : RepositoryBase
    {

        public ProdutosRepository()
        {
        }

        public static List<Produto> ConsultarTodos()
        {
            var produtos = new List<Produto>();

            var respostaSql = Select("SELECT * FROM Produtos;");
            while (respostaSql.Read())
            {
                Produto produto = new Produto()
                {
                    Id = respostaSql.GetInt64(0),
                    Descricao = respostaSql.GetString(1),
                    Categoria = respostaSql.GetString(2)
                };
                produtos.Add(produto);
            }
            respostaSql.Close();

            return produtos;
        }

        public static List<ProdutosComFornecedoresViewModel> ConsultarProdutosComFornecedores()
        {
            var produtos = new List<ProdutosComFornecedoresViewModel>();

            var respostaSql = 
                Select(@"SELECT p.*, f.* FROM Produtos p
                            LEFT JOIN FornecedoresProdutos fp ON p.Id = fp.IdProduto
                            LEFT JOIN Fornecedores f ON fp.IdFornecedor = f.Id;");
            while (respostaSql.Read())
            {
                var produtoComFornecedor = new ProdutosComFornecedoresViewModel()
                {
                    IdProduto = respostaSql.GetInt64(0),
                    Descricao = respostaSql.GetString(1),
                    Categoria = respostaSql.GetString(2),
                    IdFornecedor = respostaSql.GetInt64(3),
                    Nome = respostaSql.GetString(4)
                };
                produtos.Add(produtoComFornecedor);
            }
            respostaSql.Close();

            return produtos;
        }

        public static Produto ConsultarPorId(long id)
        {
            var respostaSql = Select($"SELECT * FROM Produtos WHERE Id = {id}");
            if (respostaSql.Read())
            {
                var produto = new Produto()
                {
                    Id = respostaSql.GetInt64(0),
                    Descricao = respostaSql.GetString(1),
                    Categoria = respostaSql.GetString(2)
                };
                respostaSql.Close();
                return produto;
            }
            
            return new Produto();
        }

        public static Produto ConsultarPorDescricao(String nome)
        {
            var respostaSql = Select($"SELECT * FROM Produtos WHERE Descricao like '%{nome}%'");
            if (respostaSql.Read())
            {
                var produto = new Produto()
                {
                    Id = respostaSql.GetInt64(0),
                    Descricao = respostaSql.GetString(1),
                    Categoria = respostaSql.GetString(2)
                };
                respostaSql.Close();
                return produto;
            }

            return new Produto();
        }

        public static int Cadastrar(Produto produto)
        {
            return Update(
                $@"INSERT INTO Produtos (Descricao, Categoria) VALUES
                    ('{produto.Descricao}', '{produto.Categoria}')");
        }

        public static int Alterar(Produto produto)
        {
            return Update(
                $@"UPDATE Produtos SET
                    Descricao = '{produto.Descricao}',
                    Categoria = '{produto.Categoria}'
                    WHERE Id = {produto.Id}");
        }

        public static int Excluir(long id)
        {
            return Update($"DELETE FROM Produtos WHERE Id = {id}");
        }
    }
}
