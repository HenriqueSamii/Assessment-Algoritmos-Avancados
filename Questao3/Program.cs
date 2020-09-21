using System;
using System.Collections.Generic;

namespace Questao3
{
    class Program
    {
        private static readonly int infinitade = int.MaxValue;
        private static readonly int inicio = 0;
        private static readonly string inicioNome = "A";
        static void Main(string[] args)
        {
            Caminho a = new Caminho("A");
            Caminho b = new Caminho("B");
            Caminho c = new Caminho("C");
            Caminho d = new Caminho("D");
            Caminho e = new Caminho("E");
            Caminho f = new Caminho("F");

            a.Rotas = new List<Rota>{new Rota(1,b),new Rota(1,c),new Rota(2,d)};
            b.Rotas = new List<Rota>{new Rota(5,e),new Rota(1,d)};
            c.Rotas = new List<Rota>{new Rota(1,b),new Rota(1,c),new Rota(1,d)};
            d.Rotas = new List<Rota>{new Rota(1,b),new Rota(1,c),new Rota(1,d)};
            e.Rotas = new List<Rota>{new Rota(1,b),new Rota(1,c),new Rota(1,d)};
            f.Rotas = new List<Rota>{new Rota(1,b),new Rota(1,c),new Rota(1,d)};

            List<Caminho> todosCaminhos = new List<Caminho>{a,b,c,d,e,f};
            List<ItemTabela> Tabela = new List<ItemTabela>();
            foreach (var caminho in todosCaminhos)
            {
                ItemTabela novoItemTabela = new ItemTabela(caminho.NomeCaminho,
                                                        caminho,
                                                        infinitade);
                if (inicioNome == caminho.NomeCaminho)
                {
                    novoItemTabela.MinimaDistancia = inicio;
                }
            }
        }
    }
}
