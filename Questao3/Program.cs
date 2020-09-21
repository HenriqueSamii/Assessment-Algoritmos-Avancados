using System;
using System.Collections.Generic;

namespace Questao3
{
    class Program
    {
        private static readonly int infinitade = int.MaxValue;
        private static readonly int inicio = 0;
        private static readonly string inicioNome = "A";
        private static readonly string pontoFinalNome = "F";
        static void Main(string[] args)
        {
            Caminho a = new Caminho("A");
            Caminho b = new Caminho("B");
            Caminho c = new Caminho("C");
            Caminho d = new Caminho("D");
            Caminho e = new Caminho("E");
            Caminho f = new Caminho("F");

            a.Rotas = new List<Rota> { new Rota(3, b), new Rota(1, c), new Rota(6, d) };
            b.Rotas = new List<Rota> { new Rota(5, e), new Rota(1, d) };
            c.Rotas = new List<Rota> { new Rota(1, b), new Rota(1, d) };
            d.Rotas = new List<Rota> { new Rota(1, e), new Rota(1, f) };
            e.Rotas = new List<Rota> { new Rota(5, f) };
            f.Rotas = new List<Rota>();

            List<Caminho> todosCaminhos = new List<Caminho> { a, b, c, d, e, f };
            List<ItemTabela> tabela = new List<ItemTabela>();
            foreach (var caminho in todosCaminhos)
            {
                ItemTabela novoItemTabela = new ItemTabela(caminho.NomeCaminho,
                                                        caminho,
                                                        infinitade);
                if (inicioNome == caminho.NomeCaminho)
                {
                    novoItemTabela.MinimaDistancia = inicio;
                }
                tabela.Add(novoItemTabela);
            }
            while (todosCaminhos.Count != 0)
            {
                int indexTabelaHulder = 0;
                int indexTodosCaminhosAindaPorPassarHolder = 0;
                int valorMinimoDeCaminhoNaoVisitado = infinitade;
                for (int indexTabela = 0; indexTabela < tabela.Count; indexTabela++)
                {
                    for (int indexTodosCaminhosAindaPorPassar = 0; indexTodosCaminhosAindaPorPassar < todosCaminhos.Count; indexTodosCaminhosAindaPorPassar++)
                    {
                        if (tabela[indexTabela].NomeDoVector == todosCaminhos[indexTodosCaminhosAindaPorPassar].NomeCaminho
                            &&
                            tabela[indexTodosCaminhosAindaPorPassar].MinimaDistancia < valorMinimoDeCaminhoNaoVisitado)
                        {
                            //System.Console.WriteLine($"{valorMinimoDeCaminhoNaoVisitado} {i} {j}");
                            valorMinimoDeCaminhoNaoVisitado = tabela[indexTabela].MinimaDistancia;
                            indexTabelaHulder = indexTabela;
                            indexTodosCaminhosAindaPorPassarHolder = indexTodosCaminhosAindaPorPassar;
                            break;
                        }
                    }
                }
                //System.Console.WriteLine($"{valorMinimoDeCaminhoNaoVisitado} {i} {j}");
                valorMinimoDeCaminhoNaoVisitado = tabela[indexTabelaHulder].MinimaDistancia;
                //System.Console.WriteLine(valorMinimoDeCaminhoNaoVisitado);
                foreach (var rota in tabela[indexTabelaHulder].Vetor.Rotas)
                {
                    int indexTabelaMendorDistancia = 0;
                    foreach (var item in tabela)
                    {
                        if (rota.LocalDeChegada.NomeCaminho == item.NomeDoVector)
                        {
                            break;
                        }
                        indexTabelaMendorDistancia++;
                    }
                    int minDistanciaTeste = rota.Peso + valorMinimoDeCaminhoNaoVisitado;
                    if (minDistanciaTeste < tabela[indexTabelaMendorDistancia].MinimaDistancia)
                    {
                        tabela[indexTabelaMendorDistancia].MinimaDistancia = minDistanciaTeste;
                        tabela[indexTabelaMendorDistancia].NomeDoVectorAnterior = tabela[indexTabelaHulder].Vetor.NomeCaminho;
                        tabela[indexTabelaMendorDistancia].VetorAnterior = tabela[indexTabelaHulder].Vetor;
                    }
                }
                todosCaminhos.RemoveAt(indexTodosCaminhosAindaPorPassarHolder);
            }
            /// /////
            System.Console.WriteLine("Tabela de Percursos Mais Rapidos");
            foreach (var itemTabela in tabela)
            {
                System.Console.WriteLine($" --> Ponto: {itemTabela.NomeDoVector} --- Distancia Total: {itemTabela.MinimaDistancia} --- Anterior: {itemTabela.NomeDoVectorAnterior}");
            }

            //////
            System.Console.WriteLine("\nPercurso Mais Rapido para " + pontoFinalNome);
            //////
            string nomeActual = pontoFinalNome;
            int indexRotaFinal = tabela.FindIndex(x => x.NomeDoVector == nomeActual);
            int minimaDistanciaCW = tabela[indexRotaFinal].MinimaDistancia;
            while (minimaDistanciaCW > 0)
            {
                int distancia = 0;
                string caminho = inicioNome;
                distancia = tabela[indexRotaFinal].MinimaDistancia;
                caminho = tabela[indexRotaFinal].NomeDoVector;
                nomeActual = tabela[indexRotaFinal].NomeDoVectorAnterior;
                indexRotaFinal = tabela.FindIndex(x => x.NomeDoVector == nomeActual);
                minimaDistanciaCW = tabela[indexRotaFinal].MinimaDistancia;
                System.Console.WriteLine($" --> Ponto: {caminho} - Distancia Total: {distancia}");
            }
            System.Console.WriteLine($" --> Ponto: {inicioNome} - Distancia Total: {inicio}");
        }
    }
}
