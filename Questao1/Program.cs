using System;
using System.Collections.Generic;

namespace Questao1
{
    class Program
    {
        //////////// QUESTÃO 1.  ///////////////  
        //Problema da mochila - Genético

        private static int valorMax = 0;
        private static int capacidadeMochila = 20;
        private static int numDeGeracoes = 10;
        
        private static readonly int percentagemMax = 1000;
        
        private static readonly int percentagemMin = 0;
        private static int percentagemDeMutacao = 5;
        static void Main(string[] args)
        {
            Item[] itens = {new Item(1,1),new Item(3,2),new Item(5,6),new Item(5,6),new Item(4,3),
                            new Item(1,1),new Item(3,2),new Item(5,6),new Item(5,6),new Item(4,3),
                            new Item(1,1),new Item(4,3),new Item(5,6),new Item(2,2),new Item(4,3),
                            new Item(5,6),new Item(4,3),new Item(5,6),new Item(2,2),new Item(4,3)};

            List<TipoItem> itemQuantidade = itemQuntudadeSort(itens);
            List<Item> mochila = mochilaGenetica(itemQuantidade);
        }

        private static List<Item> mochilaGenetica(List<TipoItem> itemQuantidade)
        {
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