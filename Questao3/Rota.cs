namespace Questao3
{
    public class Rota
    {
        public int Peso { get; set; }
        public Caminho LocalDeChegada { get; set; }
        public Rota()
        {
        }
        public Rota(int peso,Caminho localDeChegada)
        {
            this.Peso = peso;
            this.LocalDeChegada = localDeChegada;
        }
    }
}