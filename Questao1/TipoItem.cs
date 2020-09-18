namespace Questao1
{
    public class TipoItem
    {
        public Item Tipo { get; set; }
        public int Quntidade { get; set; }
        public TipoItem(Item tipo)
        {
            this.Tipo = tipo;
            this.Quntidade = 1;
        }
        public TipoItem(Item tipo, int quantidade)
        {
            this.Tipo = tipo;
            this.Quntidade = quantidade;
        }
        public int peso() {
            return (this.Quntidade*this.Tipo.Peso);
        }
        public int valor() {
            return (this.Quntidade*this.Tipo.Valor);
        }
    }
}