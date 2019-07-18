using XadrezConsole.EntitiesTabuleiro;

namespace XadrezConsole.Xadrez
{
    class Cavalo : Peca
    {
        public Cavalo(Tabuleiro tabuleiro, Cor cor) : base(tabuleiro, cor) { }

        private bool PodeMover(Posicao posicao)
        {
            Peca peca = Tabuleiro.PecaPosition(posicao);
            return peca == null || peca.Cor != Cor;
        }
        public override bool[,] MovimentosPossiveis()
        {
            bool[,] matriz = new bool[Tabuleiro.Linhas, Tabuleiro.Colunas];

            Posicao posicaoAux = new Posicao(0, 0);

            for (int i = -2; i <= 2; i += 4)
            {
                for (int j = -1; j <= 1; j += 2)
                {
                    posicaoAux.DefinirValores(Posicao.Linha + i, Posicao.Coluna + j);

                    if (Tabuleiro.PosicaoValida(posicaoAux) && PodeMover(posicaoAux))
                        matriz[posicaoAux.Linha, posicaoAux.Coluna] = true;

                    posicaoAux.DefinirValores(Posicao.Linha + j, Posicao.Coluna + i);

                    if (Tabuleiro.PosicaoValida(posicaoAux) && PodeMover(posicaoAux))
                        matriz[posicaoAux.Linha, posicaoAux.Coluna] = true;
                }
            }
            return matriz;
        }

        public override string ToString()
           => "C";
    }
}
