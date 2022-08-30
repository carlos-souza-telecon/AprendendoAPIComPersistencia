using AprendendoAPIComPersistencia.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace AprendendoAPIComPersistencia.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProdutosController : ControllerBase
    {
        private readonly ILogger<ProdutosController> _logger;

        public ProdutosController(ILogger<ProdutosController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<List<Produto>> ConsultarProdutos()
        {
            List<Produto> produtos = new List<Produto>();

            string connString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=aprendendoapi;Persist Security Info=True;User ID=aprendendoapi;Password=aprendendoapi;Integrated Security=false";
            SqlConnection conn = new SqlConnection(connString);

            try
            {
                _logger.LogInformation("Abrindo conexão com o banco...");
                conn.Open();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao conectar com o banco:\n{ex.Message}");
                return Problem($"Erro ao conectar com o banco:\n{ex.Message}");
            }

            SqlCommand command = new SqlCommand("SELECT * FROM Produtos;", conn);
            SqlDataReader respostaSql = command.ExecuteReader();

            while(respostaSql.Read())
            {
                Produto produto = new Produto()
                {
                    Id = respostaSql.GetInt64(0),
                    Descricao = respostaSql.GetString(1),
                    Categoria = respostaSql.GetString(2)
                };
                produtos.Add(produto);
            }

            return produtos;
        }

    }
}
