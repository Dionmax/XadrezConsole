using XadrezConsole.EntitiesTabuleiro;

namespace XadrezConsole.Xadrez
{
    class Rei : Peca
    {
        private PartidaDeXadrez partida;
        public Rei(Tabuleiro tabuleiro, Cor cor, PartidaDeXadrez partida) : base(tabuleiro, cor)
        {
            this.partida = partida;
        }

        private bool PodeMover(Posicao posicao)
        {
            Peca peca = Tabuleiro.PecaPosition(posicao);
            return peca == null || peca.Cor != Cor;
        }

        private bool TestarTorreParaRoque(Posicao posicao)
        {
            Peca peca = Tabuleiro.PecaPosition(posicao);

            return peca != null && peca is Torre && peca.Cor == Cor && peca.QuantidadeMovivemtos == 0;
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


            if (QuantidadeMovivemtos == 0 && !partida.JogadorEmXeque)
            {
                // #MovimentoEspecial Roque menor
                Posicao posicaoTorre1 = new Posicao(Posicao.Linha, Posicao.Coluna + 3);
                if (TestarTorreParaRoque(posicaoTorre1))
                {
                    Posicao posicaoAdjacente1 = new Posicao(Posicao.Linha, Posicao.Coluna + 1);
                    Posicao posicaoAdjacente2 = new Posicao(Posicao.Linha, Posicao.Coluna + 2);

                    if ((Tabuleiro.PecaPosition(posicaoAdjacente1) == null
                        && Tabuleiro.PecaPosition(posicaoAdjacente2) == null))
                        matriz[Posicao.Linha, Posicao.Coluna + 2] = true;
                }

                // #MovimentoEspecial Roque maior
                Posicao posicaoTorre2 = new Posicao(Posicao.Linha, Posicao.Coluna - 4);
                if (TestarTorreParaRoque(posicaoTorre2))
                {
                    Posicao posicaoAdjacente1 = new Posicao(Posicao.Linha, Posicao.Coluna - 1);
                    Posicao posicaoAdjacente2 = new Posicao(Posicao.Linha, Posicao.Coluna - 2);
                    Posicao posicaoAdjacente3 = new Posicao(Posicao.Linha, Posicao.Coluna - 3);

                    if (Tabuleiro.PecaPosition(posicaoAdjacente1) == null
                        && Tabuleiro.PecaPosition(posicaoAdjacente2) == null
                        && Tabuleiro.PecaPosition(posicaoAdjacente3) == null)
                        matriz[Posicao.Linha, Posicao.Coluna - 2] = true;
                }
            }

            return matriz;
        }

        public override string ToString()
            => "R";

    }
}
