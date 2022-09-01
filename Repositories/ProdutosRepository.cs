using AprendendoAPIComPersistencia.Models;

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
