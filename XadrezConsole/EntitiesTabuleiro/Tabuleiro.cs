namespace XadrezConsole.EntitiesTabuleiro
{
    class Tabuleiro
    {
        public int Linhas { get; set; }

        public int Colunas { get; set; }

        private Peca[,] Pecas;

        public Tabuleiro(int linhas, int colunas)
        {
            Linhas = linhas;
            Colunas = colunas;
            Pecas = new Peca[linhas, colunas];
        }

        public Peca PecaPosition(int linha, int coluna)
            => Pecas[linha, coluna];

        public Peca PecaPosition(Posicao posicao)
            => Pecas[posicao.Linha, posicao.Coluna];

        public void ColocarPeca(Peca peca, Posicao posicao)
        {
            if (ExistePeca(posicao))
                throw new TabuleiroException("Já existe uma peça nessa posição!");

            Pecas[posicao.Linha, posicao.Coluna] = peca;
            peca.Posicao = posicao;
        }

        public bool PosicaoValida(Posicao posicao)
        {
            if (posicao.Linha < 0 || posicao.Linha >= Linhas || posicao.Coluna < 0 || posicao.Coluna >= Colunas)
                return false;

            return true;
        }

        public void ValidarPosicao(Posicao posicao)
        {
            if (!PosicaoValida(posicao))
                throw new TabuleiroException("Posição Inválida!");
        }

        public bool ExistePeca(Posicao posicao)
        {
            ValidarPosicao(posicao);

            return PecaPosition(posicao) != null;
        }
    }
}
