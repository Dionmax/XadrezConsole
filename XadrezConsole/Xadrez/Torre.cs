using XadrezConsole.EntitiesTabuleiro;

namespace XadrezConsole.Xadrez
{
    class Torre : Peca
    {
        public Torre(Tabuleiro tabuleiro, Cor cor) : base(tabuleiro, cor) { }

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
                posicaoAux.DefinirValores(Posicao.Linha + i, Posicao.Coluna);
                while (Tabuleiro.PosicaoValida(posicaoAux) && PodeMover(posicaoAux))
                {
                    matriz[posicaoAux.Linha, posicaoAux.Coluna] = true;

                    if (Tabuleiro.PecaPosition(posicaoAux) != null && Tabuleiro.PecaPosition(posicaoAux).Cor != Cor)
                        break;

                    posicaoAux.Linha += i;
                }

                posicaoAux.DefinirValores(Posicao.Linha, Posicao.Coluna + i);
                while (Tabuleiro.PosicaoValida(posicaoAux) && PodeMover(posicaoAux))
                {
                    matriz[posicaoAux.Linha, posicaoAux.Coluna] = true;

                    if (Tabuleiro.PecaPosition(posicaoAux) != null && Tabuleiro.PecaPosition(posicaoAux).Cor != Cor)
                        break;

                    posicaoAux.Coluna += i;
                }
            }

            return matriz;
        }

        public override string ToString()
            => "T";
    }
}