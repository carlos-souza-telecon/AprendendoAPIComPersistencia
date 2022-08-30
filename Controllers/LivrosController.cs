using AprendendoAPIComPersistencia.Models;
using Microsoft.AspNetCore.Mvc;

namespace AprendendoAPIComPersistencia.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LivrosController : ControllerBase
    {
        public static List<Livro> livros = new List<Livro>()
        {
            new Livro()
            {
                Titulo = "Harry Potter e a Pedra Filosofal",
                Autor = "J. K. Rowling",
                AnoLancamento = 1997
            },
            new Livro()
            {
                Titulo = "Harry Potter e a Câmara Secreta",
                Autor = "J. K. Rowling",
                AnoLancamento = 1998
            }
        };

        public LivrosController()
        {

        }

        [HttpGet]
        public ActionResult<List<Livro>> ConsultarLivros()
        {
            return livros;
        }

        [HttpGet]
        [Route("{numeroLivro}")]
        public ActionResult<Livro> ConsultarLivro(int numeroLivro)
        {
            return Ok(livros[numeroLivro]);
        }

        [HttpPost]
        public ActionResult CadastrarLivro(Livro novoLivro)
        {
            if (novoLivro.Titulo.Equals("Indefinido") ||
                novoLivro.Titulo.Length < 1)
                return BadRequest("Título não definido!");

            if (novoLivro.Autor.Length < 1)
                return BadRequest("Autor não informado!");

            if (novoLivro.AnoLancamento < 1000)
                return BadRequest("Ano inválido!");

            livros.Add(novoLivro);
            return Ok($"Livro {novoLivro.Titulo} cadastrado!");
        }

        [HttpDelete]
        [Route("{numeroLivro}")]
        public ActionResult ExcluirLivro(int numeroLivro)
        {
            if (numeroLivro < 0 ||
                numeroLivro >= livros.Count)
                return BadRequest("Não foi encontrado o livro informado!");

            livros.RemoveAt(numeroLivro);

            return Ok($"Livro {numeroLivro} excluído com sucesso!");
        }

        [HttpPut]
        [Route("{numeroLivro}")]
        public ActionResult AlterarLivro(int numeroLivro, Livro livroAlterado)
        {
            if (numeroLivro < 0 ||
                numeroLivro >= livros.Count)
                return BadRequest("Não foi encontrado o livro informado!");

            if (livroAlterado.Titulo.Equals("Indefinido") ||
                livroAlterado.Titulo.Length < 1)
                return BadRequest("Título não definido!");

            if (livroAlterado.Autor.Length < 1)
                return BadRequest("Autor não informado!");

            if (livroAlterado.AnoLancamento < 1000)
                return BadRequest("Ano inválido!");

            livros[numeroLivro] = livroAlterado;
            return Ok($"Livro {numeroLivro} alterado com sucesso!");
        }
    }
}
