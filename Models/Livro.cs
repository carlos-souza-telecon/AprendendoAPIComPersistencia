namespace AprendendoAPIComPersistencia.Models
{
    public class Livro
    {
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public int AnoLancamento { get; set; }

        public Livro()
        {
            Titulo = "Indefinido";
            Autor = "Desconhecido";
            AnoLancamento = -1;
        }

    }
}
