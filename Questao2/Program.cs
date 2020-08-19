using System;
using System.Collections.Generic;

namespace Questao2
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] moedas = {50,25,10,5,1};
            int troco = 96;

            //0. Versão iterativo e guloso
            /*List<int> trocoMoedas = trocoEmMoedas(troco,moedas);
            foreach (var item in trocoMoedas)
            {
                System.Console.Write(item + " ");
            }
            System.Console.WriteLine("");*/
            // 1.Versão recursiva e gulosa
            string trocoMoedasRecusivo = trocoEmMoedasRecusivo(troco,moedas,0);
            System.Console.WriteLine(trocoMoedasRecusivo);

            // 2.Versão programação dinâmica
        }

        private static string trocoEmMoedasRecusivo(int troco, int[] moedas, int posicao)
        {
            if (troco == 0)
            {
                return "";
            }
            while (troco-moedas[posicao] < 0)
            {
                posicao++;
            }
            return moedas[posicao]+ " " + trocoEmMoedasRecusivo(troco-moedas[posicao], moedas, posicao);
        }

        private static List<int> trocoEmMoedas(int troco, int[] moedas)
        {
            var moedarRetorno = new List<int>();
            int moedaIndex = 0;
            while (troco != 0)
            {
                if (troco - moedas[moedaIndex] < 0)
                {
                    moedaIndex++;
                }
                else
                {
                    moedarRetorno.Add(moedas[moedaIndex]);
                    troco -= moedas[moedaIndex];
                }
            }
            return moedarRetorno;
        }
    }
}
// QUESTÃO 2.  

// Suponha que você seja um programador para um fabricante de máquinas de venda automática. Sua empresa quer racionalizar o esforço, distribuindo o menor número possível de moedas como troco para cada transação. Suponha que um cliente coloque uma nota de 1 real e compre um item por 37 centavos. Qual é o menor número de moedas que você pode usar para dar o troco? A resposta é: cinco moedas – uma de 50 centavos, uma de 10 centavos e três de 1 centavo. Como chegamos à resposta de cinco moedas? Começamos com a maior moeda disponível (50 centavos) e usamos o maior número possível de moedas. Depois, passamos para o próximo valor de moeda (25 centavos) e usamos o maior número possível. Este seria um algoritmo guloso, e funcionaria bem com moedas de real, dólar, euro.

// Todavia, seu projeto está sendo desenvolvido para a distante nação de Algoríthia, onde há moedas de 1, 5, 10, 25 centavos e... 21 centavos. E, neste caso, nosso algoritmo guloso falha: há uma possibilidade de resolver o troco com 3 moedas, se usarmos 3 moedas de 21 centavos.   

//     1.Crie a versão recursiva e gulosa deste algoritmo. Lembre-se de utilizar uma variável que contenha a lista de valores possíveis de moedas (por exemplo, [1, 5, 10, 25, 50]). Informe a complexidade do algoritmo.
//     2.Crie uma versão deste algoritmo em programação dinâmica. Você pode ilustrar a tabela construída para o exemplo citado acima como forma de elucidar melhor sua solução, caso desejar. Informe a complexidade do algoritmo.
