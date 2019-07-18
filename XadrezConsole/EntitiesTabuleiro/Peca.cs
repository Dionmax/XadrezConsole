namespace XadrezConsole.EntitiesTabuleiro
{
    abstract class Peca
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
            => QuantidadeMovivemtos++;

        public void DecrementarMovimento()
            => QuantidadeMovivemtos--;

        public bool ExisteMovimentosPossiveis()
        {
            bool[,] matriz = MovimentosPossiveis();

            foreach (bool posicaoPossivel in matriz)
                if (posicaoPossivel)
                    return true;

            return false;
        }

        public bool MovimentoPossivel(Posicao posicao)
            => MovimentosPossiveis()[posicao.Linha, posicao.Coluna];

        public abstract bool[,] MovimentosPossiveis();
    }
}
