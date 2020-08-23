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
    }
}