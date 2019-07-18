using XadrezConsole.EntitiesTabuleiro;

namespace XadrezConsole.Xadrez
{
    class Bispo : Peca
    {
        public Bispo(Tabuleiro tabuleiro, Cor cor) : base(tabuleiro, cor) { }

        private bool PodeMover(Posicao posicao)
        {
            Peca peca = Tabuleiro.PecaPosition(posicao);
            return peca == null || peca.Cor != Cor;
        }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] matriz = new bool[Tabuleiro.Linhas, Tabuleiro.Colunas];

            Posicao posicaoAux = new Posicao(0, 0);

            for (int i = -1; i <= 1; i += 2)
            {
                for (int j = -1; j <= 1; j += 2)
                {
                    posicaoAux.DefinirValores(Posicao.Linha + i, Posicao.Coluna + j);

                    while (Tabuleiro.PosicaoValida(posicaoAux) && PodeMover(posicaoAux))
                    {
                        matriz[posicaoAux.Linha, posicaoAux.Coluna] = true;

                        if (Tabuleiro.PecaPosition(posicaoAux) != null && Tabuleiro.PecaPosition(posicaoAux).Cor != Cor)
                            break;

                        posicaoAux.Linha += i;
                        posicaoAux.Coluna += j;
                    }
                }
            }
            return matriz;
        }

        public override string ToString()
           => "B";
    }
}
