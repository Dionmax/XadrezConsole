using XadrezConsole.EntitiesTabuleiro;

namespace XadrezConsole.Xadrez
{
    class Peao : Peca
    {
        public Peao(Tabuleiro tabuleiro, Cor cor) : base(tabuleiro, cor) { }

        private bool ExisteInimigo(Posicao posicao)
        {
            Peca peca = Tabuleiro.PecaPosition(posicao);
            return peca != null && peca.Cor != Cor;
        }

        private bool Livre(Posicao posicao)
            => Tabuleiro.PecaPosition(posicao) == null;

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] matriz = new bool[Tabuleiro.Linhas, Tabuleiro.Colunas];

            Posicao posicaoAux = new Posicao(0, 0);

            //O peão possui movimentos especificos, e se move diferente dependendo da cor.
            if (Cor == Cor.Branca)
            {
                posicaoAux.DefinirValores(Posicao.Linha - 1, Posicao.Coluna);
                if (Tabuleiro.PosicaoValida(posicaoAux) && Livre(posicaoAux))
                    matriz[posicaoAux.Linha, posicaoAux.Coluna] = true;

                posicaoAux.DefinirValores(Posicao.Linha - 2, Posicao.Coluna);
                if (Tabuleiro.PosicaoValida(posicaoAux) && Livre(posicaoAux) && QuantidadeMovivemtos == 0)
                    matriz[posicaoAux.Linha, posicaoAux.Coluna] = true;

                posicaoAux.DefinirValores(Posicao.Linha - 1, Posicao.Coluna - 1);
                if (Tabuleiro.PosicaoValida(posicaoAux) && ExisteInimigo(posicaoAux))
                    matriz[posicaoAux.Linha, posicaoAux.Coluna] = true;

                posicaoAux.DefinirValores(Posicao.Linha - 1, Posicao.Coluna + 1);
                if (Tabuleiro.PosicaoValida(posicaoAux) && ExisteInimigo(posicaoAux))
                    matriz[posicaoAux.Linha, posicaoAux.Coluna] = true;
            }
            else
            {
                posicaoAux.DefinirValores(Posicao.Linha + 1, Posicao.Coluna);
                if (Tabuleiro.PosicaoValida(posicaoAux) && Livre(posicaoAux))
                    matriz[posicaoAux.Linha, posicaoAux.Coluna] = true;

                posicaoAux.DefinirValores(Posicao.Linha + 2, Posicao.Coluna);
                if (Tabuleiro.PosicaoValida(posicaoAux) && Livre(posicaoAux) && QuantidadeMovivemtos == 0)
                    matriz[posicaoAux.Linha, posicaoAux.Coluna] = true;

                posicaoAux.DefinirValores(Posicao.Linha + 1, Posicao.Coluna - 1);
                if (Tabuleiro.PosicaoValida(posicaoAux) && ExisteInimigo(posicaoAux))
                    matriz[posicaoAux.Linha, posicaoAux.Coluna] = true;

                posicaoAux.DefinirValores(Posicao.Linha + 1, Posicao.Coluna + 1);
                if (Tabuleiro.PosicaoValida(posicaoAux) && ExisteInimigo(posicaoAux))
                    matriz[posicaoAux.Linha, posicaoAux.Coluna] = true;
            }

            return matriz;
        }

        public override string ToString()
            => "P";
    }
}
