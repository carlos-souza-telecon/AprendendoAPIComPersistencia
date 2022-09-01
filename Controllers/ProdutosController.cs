using AprendendoAPIComPersistencia.Models;
using AprendendoAPIComPersistencia.Repositories;
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
            return ProdutosRepository.ConsultarTodos();
        }

        [HttpPost]
        public ActionResult CadastrarProduto(Produto produto)
        {
            int resultados = ProdutosRepository.Cadastrar(produto);
            return Ok($"{resultados} produto(s) cadastrado(s).");
        }

        [HttpPut]
        public ActionResult AlterarProduto(Produto produto)
        {
            int resultados = ProdutosRepository.Alterar(produto);
            return Ok($"{resultados} produto(s) alterado(s).");
        }

        [HttpDelete]
        public ActionResult ExcluirProduto(long id)
        {
            int resultados = ProdutosRepository.Excluir(id);
            return Ok($"{resultados} produto(s) excluído(s).");
        }

    }
}
