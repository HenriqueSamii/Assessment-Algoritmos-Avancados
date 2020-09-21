namespace Questao3
{
    public class ItemTabela
    {
        public string NomeDoVector { get; set; }
        public Caminho Vetor { get; set; }
        public int MinimaDistancia { get; set; }
        public string NomeDoVectorAnterior { get; set; }
        public Caminho VetorAnterior { get; set; }
        public ItemTabela(string nomeDoVector,Caminho vetor,int minimaDistancia)
        {
            this.NomeDoVector = nomeDoVector;
            this.Vetor = vetor;
            this.MinimaDistancia = minimaDistancia;
        }
        public ItemTabela()
        {
            
        }
    }
}