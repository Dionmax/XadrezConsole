using XadrezConsole.EntitiesTabuleiro;

namespace XadrezConsole.Xadrez
{
    class Peao : Peca
    {
        private PartidaDeXadrez _partida;
        public Peao(Tabuleiro tabuleiro, Cor cor, PartidaDeXadrez partida) : base(tabuleiro, cor)
        {
            _partida = partida;
        }

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
                {
                    matriz[posicaoAux.Linha, posicaoAux.Coluna] = true;

                    posicaoAux.DefinirValores(Posicao.Linha - 2, Posicao.Coluna);
                    if (Tabuleiro.PosicaoValida(posicaoAux) && Livre(posicaoAux) && QuantidadeMovivemtos == 0)
                        matriz[posicaoAux.Linha, posicaoAux.Coluna] = true;
                }

                posicaoAux.DefinirValores(Posicao.Linha - 1, Posicao.Coluna - 1);
                if (Tabuleiro.PosicaoValida(posicaoAux) && ExisteInimigo(posicaoAux))
                    matriz[posicaoAux.Linha, posicaoAux.Coluna] = true;

                posicaoAux.DefinirValores(Posicao.Linha - 1, Posicao.Coluna + 1);
                if (Tabuleiro.PosicaoValida(posicaoAux) && ExisteInimigo(posicaoAux))
                    matriz[posicaoAux.Linha, posicaoAux.Coluna] = true;

                // #MovimentoEspecial En Passant Brancas
                if (Posicao.Linha == 3)
                {
                    Posicao posicaoEsquerda = new Posicao(Posicao.Linha, Posicao.Coluna - 1);
                    if (Tabuleiro.PosicaoValida(posicaoEsquerda) && ExisteInimigo(posicaoEsquerda)
                        && Tabuleiro.PecaPosition(posicaoEsquerda) == _partida.VulneravelEnPassant)
                        matriz[posicaoEsquerda.Linha - 1, posicaoEsquerda.Coluna] = true;

                    Posicao posicaoDireita = new Posicao(Posicao.Linha, Posicao.Coluna + 1);
                    if (Tabuleiro.PosicaoValida(posicaoDireita) && ExisteInimigo(posicaoDireita)
                        && Tabuleiro.PecaPosition(posicaoDireita) == _partida.VulneravelEnPassant)
                        matriz[posicaoDireita.Linha - 1, posicaoDireita.Coluna] = true;
                }
            }
            else
            {
                posicaoAux.DefinirValores(Posicao.Linha + 1, Posicao.Coluna);
                if (Tabuleiro.PosicaoValida(posicaoAux) && Livre(posicaoAux))
                {
                    matriz[posicaoAux.Linha, posicaoAux.Coluna] = true;

                    posicaoAux.DefinirValores(Posicao.Linha + 2, Posicao.Coluna);
                    if (Tabuleiro.PosicaoValida(posicaoAux) && Livre(posicaoAux) && QuantidadeMovivemtos == 0)
                        matriz[posicaoAux.Linha, posicaoAux.Coluna] = true;
                }

                posicaoAux.DefinirValores(Posicao.Linha + 1, Posicao.Coluna - 1);
                if (Tabuleiro.PosicaoValida(posicaoAux) && ExisteInimigo(posicaoAux))
                    matriz[posicaoAux.Linha, posicaoAux.Coluna] = true;

                posicaoAux.DefinirValores(Posicao.Linha + 1, Posicao.Coluna + 1);
                if (Tabuleiro.PosicaoValida(posicaoAux) && ExisteInimigo(posicaoAux))
                    matriz[posicaoAux.Linha, posicaoAux.Coluna] = true;

                // #MovimentoEspecial En Passant Pretas
                if (Posicao.Linha == 4)
                {
                    Posicao posicaoEsquerda = new Posicao(Posicao.Linha, Posicao.Coluna - 1);
                    if (Tabuleiro.PosicaoValida(posicaoEsquerda) && ExisteInimigo(posicaoEsquerda)
                        && Tabuleiro.PecaPosition(posicaoEsquerda) == _partida.VulneravelEnPassant)
                        matriz[posicaoEsquerda.Linha + 1, posicaoEsquerda.Coluna] = true;

                    Posicao posicaoDireita = new Posicao(Posicao.Linha, Posicao.Coluna + 1);
                    if (Tabuleiro.PosicaoValida(posicaoDireita) && ExisteInimigo(posicaoDireita)
                        && Tabuleiro.PecaPosition(posicaoDireita) == _partida.VulneravelEnPassant)
                        matriz[posicaoDireita.Linha + 1, posicaoDireita.Coluna] = true;
                }
            }

            return matriz;
        }

        public override string ToString()
            => "P";
    }
}
