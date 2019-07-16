using XadrezConsole.EntitiesTabuleiro;

namespace XadrezConsole.Xadrez
{
    class Rei : Peca
    {
        public Rei(Tabuleiro tabuleiro, Cor cor) : base(tabuleiro, cor) { }

        private bool PodeMover(Posicao posicao)
        {
            Peca peca = Tabuleiro.PecaPosition(posicao);
            return peca == null || peca.Cor != Cor;
        }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] matriz = new bool[Tabuleiro.Linhas, Tabuleiro.Colunas];

            Posicao posicaoAux = new Posicao(0, 0);

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    posicaoAux.DefinirValores(Posicao.Linha + i, Posicao.Coluna + j);

                    if (Tabuleiro.PosicaoValida(posicaoAux) && PodeMover(posicaoAux))
                        matriz[posicaoAux.Linha, posicaoAux.Coluna] = true;
                }
            }

            return matriz;
        }

        public override string ToString()
            => "R";

    }
}
