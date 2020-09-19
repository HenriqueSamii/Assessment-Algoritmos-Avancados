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
        private static readonly int numDeGeracoes = 10;
        private static readonly int percentagemMax = 1000;
        private static readonly int percentagemMin = 0;
        private static readonly int percentagemDeMutacao = 5;
        private static readonly int tamanhaDaPopulacaoInicial = 20;
        private static readonly int numeroDeGeracoes = 1000;
        static void Main(string[] args)
        {
            Item[] itens = {new Item(1,1),new Item(3,2),new Item(5,6),new Item(5,6),new Item(4,3),
                            new Item(1,1),new Item(3,2),new Item(5,6),new Item(5,6),new Item(4,3),
                            new Item(1,1),new Item(4,3),new Item(5,6),new Item(2,2),new Item(4,3),
                            new Item(5,6),new Item(4,3),new Item(5,6),new Item(2,2),new Item(4,3)};

            List<TipoItem> itemQuantidade = itemQuntudadeSort(itens);
            List<Item> mochila = mochilaGenetica(itemQuantidade);
            int pessoTotal = 0;
            int valorTotal = 0;
            System.Console.WriteLine("Mocila Com os itens:");
            foreach (var item in mochila)
            {
                pessoTotal += item.Peso;
                valorTotal += item.Valor;
                System.Console.WriteLine("-Valor " + item.Valor + " com o peso de " + item.Peso);
            }
            System.Console.WriteLine("Valor Total: " + valorTotal);
            System.Console.WriteLine("Peso Total: " + pessoTotal);
        }

        private static List<Item> mochilaGenetica(List<TipoItem> itemQuantidade)
        {
            int geracao = 0;
            List<List<TipoItem>> pais = new List<List<TipoItem>>();
            List<TipoItem> resultadoTipoItem = mochilaGeneticaGerar(itemQuantidade, geracao, pais);
            List<Item> resultado = new List<Item>();
            foreach (var tipoItem in resultadoTipoItem)
            {
                for (int i = 0; i < tipoItem.Quntidade; i++)
                {
                    resultado.Add(tipoItem.Tipo);
                }
            }
            return resultado;
        }

        private static List<TipoItem> mochilaGeneticaGerar(List<TipoItem> itemQuantidade, int geracao, List<List<TipoItem>> pais)
        {
            List<List<TipoItem>> populacaoInicial = pais;

            /// Gerar população inicial  
            for (int i = 0; i < 20 - populacaoInicial.Count; i++)
            {
                List<TipoItem> holderCromosoma = new List<TipoItem>();
                int holderPeso = -1;
                while (holderPeso < 0 || holderPeso > capacidadeMochila)
                {
                    holderCromosoma.Clear();
                    foreach (var item in itemQuantidade)
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
                populacaoInicial.Add(holderCromosoma);
            }

            ///Ordenar população  
            populacaoInicial = ordenarPopulacao(populacaoInicial);
            int popOrgMax = 0;
            foreach (var item in populacaoInicial[populacaoInicial.Count - 1])
            {
                popOrgMax += item.valor();
            }
            if (popOrgMax == valorMax)
            {
                return populacaoInicial[populacaoInicial.Count - 1];
            }

            ///Seleção 
            List<int> indexMelhores = new List<int>();
            List<int> indexPiores = new List<int>();

            int medadePopulacao = populacaoInicial.Count / 2;
            int metadeDaMetade = medadePopulacao / 2;
            var a = new Random();
            while (indexPiores.Count != metadeDaMetade)
            {
                int index = a.Next(0, medadePopulacao - 1);
                if (!indexPiores.Contains(index))
                {
                    indexPiores.Add(index);
                }
            }

            while (indexMelhores.Count != medadePopulacao - metadeDaMetade)
            {
                int index = a.Next(medadePopulacao, populacaoInicial.Count - 1);
                if (!indexMelhores.Contains(index))
                {
                    indexMelhores.Add(index);
                }
            }

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
                //genesTestados ver se ultrapasoou a quandtidade de possiveis poissibilidades e nao encontrou solucao
                //List<int> genesTestados = new List<int>();
                int numeroDeTentativas = 0;
                while (true)
                {
                    int pesoGen1 = 0;
                    int pesoGen2 = 0;
                    var gene1 = new List<TipoItem>(populacaoInicial[indexMelhores[i]]);
                    var gene2 = new List<TipoItem>(populacaoInicial[indexPiores[i]]);
                    int crossIndex1 = a.Next(0, gene1.Count - 1);
                    int crossIndex2 = a.Next(0, gene2.Count - 1);
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
                        populacaoInicial[indexMelhores[i]] = gene1;
                        populacaoInicial[indexPiores[i]] = gene2;
                        break;
                    }
                    if (numeroDeTentativas == 50)
                    {
                        break;
                    }
                    numeroDeTentativas++;
                }
            }
            ///Mutação 

            // Random random = new Random();
            // int randomNumber = random.Next(0, 1000);

            if (geracao < numDeGeracoes)
            {
                return mochilaGeneticaGerar(itemQuantidade, geracao, pais);
            }
            return null;
        }

        private static List<List<TipoItem>> ordenarPopulacao(List<List<TipoItem>> populacaoInicial)
        {
            List<List<TipoItem>> retorno = new List<List<TipoItem>>();
            foreach (var listaDeTipoItem in populacaoInicial)
            {
                if (retorno.Count == 0)
                {
                    retorno.Add(listaDeTipoItem);
                }
                else
                {
                    int valorHolder = 0;
                    foreach (var item in listaDeTipoItem)
                    {
                        valorHolder += item.valor();
                    }
                    for (int i = 0; i <= retorno.Count; i++)
                    {
                        if (retorno.Count == i)
                        {
                            retorno.Add(listaDeTipoItem);
                        }
                        else
                        {
                            int valorHolderTemp = 0;
                            foreach (var item in retorno[i])
                            {
                                valorHolderTemp += item.valor();
                            }
                            if (valorHolderTemp > valorHolder)
                            {
                                retorno.Insert(i, listaDeTipoItem);
                            }
                        }
                    }
                }
            }
            return retorno;
        }

        private static List<TipoItem> itemQuntudadeSort(Item[] itens)
        {
            List<TipoItem> itemQuantidade = new List<TipoItem>();
            foreach (var item in itens)
            {
                valorMax += item.Valor;
                int indexItemQuantidade = -1;
                for (int i = 0; i < itemQuantidade.Count - 1; i++)
                {
                    if (itemQuantidade[i].Tipo == item)
                    {
                        indexItemQuantidade = i;
                        break;
                    }
                }
                if (indexItemQuantidade > -1)
                {
                    itemQuantidade[indexItemQuantidade].Quntidade++;
                }
                else
                {
                    itemQuantidade.Add(new TipoItem(item));
                }
            }
            return itemQuantidade;
        }
    }
}