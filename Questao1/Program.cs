﻿using System;
using System.Collections.Generic;

namespace Questao1
{
    class Program
    {
        //////////// QUESTÃO 1.  ///////////////  
        //Problema da mochila - Genético

        private static int valorMax = 0;
        private static readonly int capacidadeMochila = 20;
        //private static readonly int numDeGeracoes = 10;
        private static readonly int percentagemMax = 1000;
        private static readonly int percentagemMin = 0;
        private static readonly int percentagemDeMutacao = 5;
        private static readonly int tamanhaDaPopulacaoInicial = 20;
        private static readonly int numeroDeGeracoes = 1000;
        private static Random randomizar = new Random();
        static void Main(string[] args)
        {
            Item[] itens = {new Item(1,1),new Item(3,2),new Item(5,6),new Item(5,6),new Item(4,3),
                            new Item(1,1),new Item(3,2),new Item(5,6),new Item(5,6),new Item(4,3),
                            new Item(1,1),new Item(4,3),new Item(5,6),new Item(2,2),new Item(4,3),
                            new Item(5,6),new Item(4,3),new Item(5,6),new Item(2,2),new Item(4,3)};

            List<TipoItem> itemQuantidade = itemQuntudadeSort(itens);
            // System.Console.WriteLine($"Valor maximo: {valorMax}");
            // foreach (var item in itemQuantidade)
            // {
            //     System.Console.WriteLine($"Valor: {item.Tipo.Valor} Peso de um item:{item.Tipo.Peso}"+
            //                             $" Qantidade {item.Quntidade} Peso total: {item.peso()} Valor total: {item.valor()}");
            // }
            List<TipoItem> mochila = mochilaGeneticaGerar(itemQuantidade);
            System.Console.WriteLine($"Valor maximo: {valorMax}");
            int valorMochilaFinal = 0;
            int pesoMochilaFinal = 0;
            foreach (var item in mochila)
            {
                System.Console.WriteLine($"Valor do item: {item.Tipo.Valor} - Peso do item:{item.Tipo.Peso} - " +
                                        $"Qantidade: {item.Quntidade} - Peso total: {item.peso()} - Valor total: {item.valor()}");
                valorMochilaFinal += item.valor();
                pesoMochilaFinal += item.peso();
            }
            System.Console.WriteLine($"Peso total Mochila:{pesoMochilaFinal}");
            System.Console.WriteLine($"Valor total Mochila:{valorMochilaFinal}");
        }

        private static List<TipoItem> mochilaGeneticaGerar(List<TipoItem> itemQuantidadeMaxima)
        {
            int geracao = 0;
            List<List<TipoItem>> pais = new List<List<TipoItem>>();
            List<List<TipoItem>> geracoesActuais = new List<List<TipoItem>>();
            while (geracao < numeroDeGeracoes)//numeroDeGeracoes  //1
            {
                /// Gerar população inicial
                geracoesActuais = new List<List<TipoItem>>(pais);
                int LopCriacao = tamanhaDaPopulacaoInicial;
                if (geracoesActuais.Count > 0)
                {
                    LopCriacao -= geracoesActuais.Count;
                }
                //System.Console.WriteLine(geracoesActuais.Count);
                for (int i = 0; i < LopCriacao; i++)
                {
                    List<TipoItem> holderCromosoma = new List<TipoItem>();
                    int holderPeso = capacidadeMochila + 1;
                    while (holderPeso > capacidadeMochila)
                    {
                        holderCromosoma.Clear();
                        foreach (var item in itemQuantidadeMaxima)
                        {
                            Random random = new Random();
                            int novaQuantidade = random.Next(0, item.Quntidade);
                            holderCromosoma.Add(new TipoItem(item.Tipo, novaQuantidade));
                        }
                        holderPeso = 0;

                        foreach (var item in holderCromosoma)
                        {
                            holderPeso += item.peso();
                        }
                    }
                    geracoesActuais.Add(holderCromosoma);
                    ////////////////////   test /////////////////////////////
                    // int holderPessoStack = 0;
                    // int holderValorStack = 0;
                    // foreach (var item in holderCromosoma)
                    // {
                    //     holderPessoStack += item.peso();
                    //     holderValorStack += item.valor();
                    // }
                    // System.Console.WriteLine($"item criado- valor: {holderValorStack} - peso: {holderPessoStack} - count actual {geracoesActuais.Count}");
                    ///////////////////////////////////////////////////////////
                }
                /// ///////////////////////////////////////////
                /// Ordenar população  
                geracoesActuais = ordenarPopulacao(geracoesActuais);
                int totalValorItemMaior = 0;
                foreach (var item in geracoesActuais[geracoesActuais.Count - 1])
                {
                    totalValorItemMaior += item.valor();
                }
                if (totalValorItemMaior == valorMax)
                {
                    return geracoesActuais[geracoesActuais.Count - 1];
                }
                /// ///////////////////////////
                ///Seleção 
                List<int> indexMelhores = new List<int>();
                List<int> indexPiores = new List<int>();

                int medadePopulacao = geracoesActuais.Count / 2;
                int metadeDaMetade = medadePopulacao / 2;

                while (indexPiores.Count != metadeDaMetade)
                {
                    int index = randomizar.Next(0, medadePopulacao - 1);
                    if (!indexPiores.Contains(index))
                    {
                        indexPiores.Add(index);
                    }
                }

                while (indexMelhores.Count != medadePopulacao - metadeDaMetade)
                {
                    int index = randomizar.Next(medadePopulacao, geracoesActuais.Count - 1);
                    if (!indexMelhores.Contains(index))
                    {
                        indexMelhores.Add(index);
                    }
                }
                /// /////////////////////////////////
                ///Crossover 
                int quantidadeDeCrossovers = 0;
                if (indexMelhores.Count < indexPiores.Count)
                {
                    quantidadeDeCrossovers = indexMelhores.Count;
                }
                else
                {
                    quantidadeDeCrossovers = indexPiores.Count;
                }
                for (int i = 0; i < quantidadeDeCrossovers; i++)
                {
                    int numeroDeTentativas = 0;
                    while (true)
                    {
                        int pesoGen1 = 0;
                        int pesoGen2 = 0;
                        var gene1 = new List<TipoItem>(geracoesActuais[indexMelhores[i]]);
                        var gene2 = new List<TipoItem>(geracoesActuais[indexPiores[i]]);
                        int crossIndex1 = randomizar.Next(0, gene1.Count - 1);
                        int crossIndex2 = randomizar.Next(0, gene2.Count - 1);
                        TipoItem tipoItemHolder1 = new TipoItem(gene1[crossIndex1].Tipo, gene1[crossIndex1].Quntidade);
                        TipoItem tipoItemHolder2 = new TipoItem(gene2[crossIndex1].Tipo, gene2[crossIndex1].Quntidade);
                        gene1[crossIndex1] = tipoItemHolder2;
                        gene2[crossIndex1] = tipoItemHolder1;
                        if (crossIndex1 != crossIndex2)
                        {
                            TipoItem tipoItemHolder3 = new TipoItem(gene1[crossIndex2].Tipo, gene1[crossIndex2].Quntidade);
                            TipoItem tipoItemHolder4 = new TipoItem(gene2[crossIndex2].Tipo, gene2[crossIndex2].Quntidade);
                            gene1[crossIndex2] = tipoItemHolder4;
                            gene2[crossIndex2] = tipoItemHolder3;
                        }

                        for (int x = 0; x < gene2.Count; x++)
                        {
                            pesoGen1 += gene1[x].peso();
                            pesoGen2 += gene2[x].peso();
                        }

                        if (pesoGen1 < capacidadeMochila && pesoGen2 < capacidadeMochila)
                        {
                            geracoesActuais[indexMelhores[i]] = gene1;
                            geracoesActuais[indexPiores[i]] = gene2;
                            break;
                        }
                        if (numeroDeTentativas == 50)
                        {
                            break;
                        }
                        numeroDeTentativas++;
                    }
                }
                /// ///////////////////////////////////////////

                /// ///////////////////////////////////////////
                ///Mutação 
                List<int> indexDeMutacao = new List<int>();
                while (indexDeMutacao.Count != medadePopulacao)
                {
                    int index = randomizar.Next(0, geracoesActuais.Count - 1);
                    if (!indexDeMutacao.Contains(index))
                    {
                        indexDeMutacao.Add(index);
                    }
                }
                foreach (var index in indexDeMutacao)
                {
                    //Random random = new Random();
                    int percentagemMutacao = randomizar.Next(percentagemMin, percentagemMax);
                    if (percentagemMutacao <= percentagemDeMutacao)
                    {
                        while (true)
                        {
                            int pesoMutacao = 0;
                            var geneMutado = new List<TipoItem>(geracoesActuais[index]);
                            int itemMutadoIndex = randomizar.Next(0, geneMutado.Count - 1);
                            int quantidadeNova = randomizar.Next(0, itemQuantidadeMaxima[itemMutadoIndex].Quntidade);
                            TipoItem geneMutadoTipoItem = new TipoItem(itemQuantidadeMaxima[itemMutadoIndex].Tipo, quantidadeNova);
                            geneMutado[itemMutadoIndex] = geneMutadoTipoItem;
                            foreach (var item in geneMutado)
                            {
                                pesoMutacao += item.peso();
                            }
                            if (pesoMutacao <= capacidadeMochila)
                            {
                                geracoesActuais[index] = geneMutado;
                                break;
                            }
                        }
                    }
                }
                geracoesActuais = ordenarPopulacao(geracoesActuais);
                pais = geracoesActuais.GetRange((geracoesActuais.Count / 2), (geracoesActuais.Count / 2));
                ////////////////////   test /////////////////////////////
                // foreach (var gera in geracoesActuais)
                // {
                //     int holderPessoStack = 0;
                //     int holderValorStack = 0;
                //     foreach (var item in gera)
                //     {
                //         holderPessoStack += item.peso();
                //         holderValorStack += item.valor();
                //     }
                //     System.Console.WriteLine($"item criado- valor: {holderValorStack} - peso: {holderPessoStack} - count actual {geracoesActuais.Count}");
                // }
                // System.Console.WriteLine();
                ///////////////////////////////////////////////////////////
                geracao++;
            }
            return geracoesActuais[geracoesActuais.Count - 1];
        }
        private static List<List<TipoItem>> ordenarPopulacao(List<List<TipoItem>> arr)
        {
            if (arr.Count <= 1)
            {
                return arr;
            }
            int mid = arr.Count / 2;

            var l = arr.GetRange(0, mid);
            var r = arr.GetRange(mid, arr.Count - mid);

            l = ordenarPopulacao(l);
            r = ordenarPopulacao(r);
            return merge(l, r);
        }

        private static List<List<TipoItem>> merge(List<List<TipoItem>> l, List<List<TipoItem>> r)
        {
            var ret = new List<List<TipoItem>>();
            while (l.Count > 0 || r.Count > 0)
            {
                if (l.Count == 0)
                {
                    ret.Add(r[0]);
                    r.RemoveAt(0);
                }
                else if (r.Count == 0)
                {
                    ret.Add(l[0]);
                    l.RemoveAt(0);
                }
                else
                {
                    int totalTipoItem1 = 0;
                    int totalTipoItem2 = 0;
                    foreach (var item in l[0])
                    {
                        totalTipoItem1 += item.valor();
                    }
                    foreach (var item in r[0])
                    {
                        totalTipoItem2 += item.valor();
                    }
                    if (totalTipoItem1 <= totalTipoItem2)
                    {
                        ret.Add(l[0]);
                        l.RemoveAt(0);
                    }
                    else
                    {
                        ret.Add(r[0]);
                        r.RemoveAt(0);
                    }
                }

            }
            return ret;
        }

        private static List<TipoItem> itemQuntudadeSort(Item[] itens)
        {
            List<TipoItem> itemQuantidade = new List<TipoItem>();
            foreach (var item in itens)
            {
                valorMax += item.Valor;
                bool itemExiste = false;
                for (int i = 0; i < itemQuantidade.Count; i++)
                {
                    if (itemQuantidade[i].Tipo.Peso == item.Peso && itemQuantidade[i].Tipo.Valor == item.Valor)
                    {
                        itemQuantidade[i].Quntidade += 1;
                        itemExiste = true;
                    }
                }
                if (!itemExiste)
                {
                    itemQuantidade.Add(new TipoItem(item));
                }
            }
            return itemQuantidade;
        }
        // private static List<List<TipoItem>> ordenarPopulacao(List<List<TipoItem>> populacaoInicial)
        // {
        //     List<List<TipoItem>> retorno = new List<List<TipoItem>>(populacaoInicial);
        //     List<TipoItem> temp = new List<TipoItem>();
        //     for (int i = 0; i < retorno.Count; i++)
        //     {
        //         for (int j = 0; j < retorno.Count - 1; j++)
        //         {
        //             int totalTipoItem1 = 0;
        //             int totalTipoItem2 = 0;
        //             foreach (var item in retorno[j])
        //             {
        //                 totalTipoItem1 += item.valor();
        //             }
        //             foreach (var item in retorno[j + 1])
        //             {
        //                 totalTipoItem2 += item.valor();
        //             }

        //             if (totalTipoItem1 > totalTipoItem2)
        //             {
        //                 temp = retorno[j];
        //                 retorno[j] = retorno[j + 1];
        //                 retorno[j + 1] = temp;
        //             }
        //         }
        //     }
        //     return retorno;
        // }
    }
}