using System.Collections.Generic;

namespace Questao3
{
    public class Caminho
    {
        public string NomeCaminho { get; set; }
        public List<Rota> Rotas { get; set; }
        public Caminho(string nomeCaminho)
        {
            this.NomeCaminho = nomeCaminho;
            this.Rotas = new List<Rota>();
        }
        public Caminho()
        {
            
        }
    }
}