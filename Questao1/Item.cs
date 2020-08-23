namespace Questao1
{
    public class Item
    {
        public int Valor { get; set; } 
        public int Peso { get; set; }
        public Item(int valor, int peso)
        {
            this.Valor = valor;
            this.Peso = peso;
        }
    }
}