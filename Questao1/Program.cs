using System;
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
            List<TipoItem> pais = new List<TipoItem>{};
            List<TipoItem> resultadoTipoItem = mochilaGeneticaGerar(itemQuantidade,geracao,pais);
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

        private static List<TipoItem> mochilaGeneticaGerar(List<TipoItem> itemQuantidade, int geracao, List<TipoItem> pais)
        {
            //int tamanhaDaPopulacaoInicial = 20;
            //int tamanhaDaPopulacaoAposCruzamento = 30;
            // Random random = new Random();
            // int randomNumber = random.Next(0, 1000);
            throw new NotImplementedException();
        }

        private static List<TipoItem> itemQuntudadeSort(Item[] itens)
        {
            List<TipoItem> itemQuantidade = new List<TipoItem>();
            foreach (var item in itens)
            {
                valorMax += item.Valor;
                int indexItemQuantidade = -1;
                for (int i = 0; i < itemQuantidade.Count-1; i++)
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

// Indivíduo: Vetor de 6 posições (genes) onde cada gene vale 0 ou 1.
// Cálculo da aptidão: por indivíduo. A aptidão é o somatório dos valores dos genes.
// Tamanho da população inicial: 20
// Tamanho da população após cruzamento: 30
// Tamanho da população após seleção: 20 – os mais aptos, sem critério de desempate

// Regra de mutação:
// Dados os novos indivíduos formados, selecione-os aleatoriamente e até 50% dos novos indivíduos, para serem modificados.
//  A probabilidade de mutação é de 0,5%. Se ocorrer mutação, até 3 genes do indivíduo podem sofrer mutação. 
// A quantidade de genes e quais devem ser escolhidos aleatoriamente (figura c.)

// Regra de ordenação da população:
// Mantenha a população, sempre ao fim do processo de cálculo de aptidão, ordenada, em um vetor.
//  Use um método eficiente de ordenação que tenha no máximo O(n log n) de complexidade de pior caso.