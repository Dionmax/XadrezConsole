namespace XadrezConsole.EntitiesTabuleiro
{
    class Peca
    {
        public Posicao Posicao { get; set; }

        public Cor Cor { get; set; }

        public int QuantidadeMovivemtos { get; protected set; }

        public Tabuleiro Tabuleiro { get; protected set; }

        public Peca(Tabuleiro tabuleiro, Cor cor)
        {
            Posicao = null;
            Cor = cor;
            QuantidadeMovivemtos = 0;
            Tabuleiro = tabuleiro;
        }

        public void IncrementarMovimento()
        {
            QuantidadeMovivemtos++;
        }
    }
}
