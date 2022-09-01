using AprendendoAPIComPersistencia.Models;
using AprendendoAPIComPersistencia.Repositories;
using Microsoft.AspNetCore.Cors;
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

        [HttpGet]
        [Route("{id}")]
        public ActionResult ConsultarProduto(long id)
        {
            var produto = ProdutosRepository.ConsultarPorId(id);
            if (produto.Id == 0)
                return NotFound($"Não foi encontrado produto com o id {id}");
            return Ok(produto);
        }

        [HttpGet]
        [Route("Descricao/{nome}")]
        public ActionResult ConsultarPorDescricao(string nome)
        {
            var produto = ProdutosRepository.ConsultarPorDescricao(nome);
            if (produto.Id == 0)
                return NotFound($"Não foi encontrado produto com o nome {nome}");
            return Ok(produto);
        }

        [HttpGet]
        [Route("ProdutosComFornecedores")]
        public ActionResult ConsultarProdutosComFornecedores()
        {
            var produtos = ProdutosRepository.ConsultarProdutosComFornecedores();
            return Ok(produtos);
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
