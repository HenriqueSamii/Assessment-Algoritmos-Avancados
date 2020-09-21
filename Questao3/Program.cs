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
                                                        10000);
                if (inicioNome == caminho.NomeCaminho)
                {
                    novoItemTabela.MinimaDistancia = inicio;
                }
                tabela.Add(novoItemTabela);
            }
            while (todosCaminhos.Count != 0)
            {
                int valorMinimoDeCaminhoNaoVisitado = infinitade;
                for (int i = 0; i < tabela.Count; i++)
                {
                    for (int j = 0; j < todosCaminhos.Count; j++)
                    {
                        if (tabela[i].NomeDoVector == todosCaminhos[j].NomeCaminho
                            &&
                            tabela[i].MinimaDistancia < valorMinimoDeCaminhoNaoVisitado)
                        {
                            // System.Console.WriteLine($"{valorMinimoDeCaminhoNaoVisitado} {i} {j}");
                            valorMinimoDeCaminhoNaoVisitado = tabela[i].MinimaDistancia;
                            // System.Console.WriteLine(valorMinimoDeCaminhoNaoVisitado);
                            foreach (var rota in tabela[i].Vetor.Rotas)
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
                                //System.Console.WriteLine(rota.LocalDeChegada.NomeCaminho +" index tablea " + indexTabelaMendorDistancia);
                                int minDistanciaTeste = rota.Peso + valorMinimoDeCaminhoNaoVisitado;
                                //System.Console.WriteLine(minDistanciaTeste);
                                //System.Console.WriteLine(tabela[indexTabelaMendorDistancia].MinimaDistancia);
                                if (minDistanciaTeste > tabela[indexTabelaMendorDistancia].MinimaDistancia)
                                {
                                    System.Console.WriteLine(tabela[indexTabelaMendorDistancia].MinimaDistancia);
                                    tabela[indexTabelaMendorDistancia].MinimaDistancia = minDistanciaTeste;
                                    System.Console.WriteLine(tabela[indexTabelaMendorDistancia].MinimaDistancia);
                                    tabela[indexTabelaMendorDistancia].NomeDoVectorAnterior = tabela[i].Vetor.NomeCaminho;
                                    tabela[indexTabelaMendorDistancia].VetorAnterior = tabela[i].Vetor;
                                }
                            }
                            todosCaminhos.RemoveAt(j);

                            ///break os dois for loops
                            i = tabela.Count;
                            j = todosCaminhos.Count;
                        }
                    }
                }
            }
            /// /////
            // string nomeActual = pontoFinalNome;
            // int indexRotaFinal = tabela.FindIndex(x => x.NomeDoVector == nomeActual);
            // int minimaDistanciaCW = tabela[indexRotaFinal].MinimaDistancia;
            // while(minimaDistanciaCW > -1){
            //     int distancia = 0;
            //     string caminho = inicioNome;
            //     if (minimaDistanciaCW != 0)
            //     {
            //         distancia = tabela[indexRotaFinal].MinimaDistancia;
            //         caminho = tabela[indexRotaFinal].NomeDoVector;
            //         nomeActual = tabela[indexRotaFinal].NomeDoVectorAnterior;
            //         indexRotaFinal = tabela.FindIndex(x => x.NomeDoVector == nomeActual);
            //         minimaDistanciaCW = tabela[indexRotaFinal].MinimaDistancia;
            //     }
            //     System.Console.WriteLine($" --> Ponto: {caminho} - Distancia: {distancia}");
            // }
            foreach (var itemTabela in tabela)
            {
                System.Console.WriteLine($" --> Ponto: {itemTabela.NomeDoVector} --- Distancia: {itemTabela.MinimaDistancia} --- Anterior: {itemTabela.NomeDoVectorAnterior}");
            }
        }
    }
}
