namespace XadrezConsole.EntitiesTabuleiro
{
    class Tabuleiro
    {
        public int Linhas { get; set; }

        public int Colunas { get; set; }

        private Peca[,] _pecas;

        public Tabuleiro(int linhas, int colunas)
        {
            Linhas = linhas;
            Colunas = colunas;
            _pecas = new Peca[linhas, colunas];
        }

        public Peca PecaPosition(int linha, int coluna)
            => _pecas[linha, coluna];

        public Peca PecaPosition(Posicao posicao)
            => _pecas[posicao.Linha, posicao.Coluna];

        public void ColocarPeca(Peca peca, Posicao posicao)
        {
            if (ExistePeca(posicao))
                throw new TabuleiroException("Já existe uma peça nessa posição!");

            _pecas[posicao.Linha, posicao.Coluna] = peca;
            peca.Posicao = posicao;
        }

        public Peca RetirarPeca(Posicao posicao)
        {
            //Casas vazias são atribuidas "null". Obs: Poderia se atribuir "vazio" com outro método.
            if (PecaPosition(posicao) == null)
                return null;

            Peca aux = PecaPosition(posicao);
            aux.Posicao = null;
            _pecas[posicao.Linha, posicao.Coluna] = null;
            return aux;
        }

        public bool PosicaoValida(Posicao posicao)
            => (posicao.Linha < 0 || posicao.Linha >= Linhas || posicao.Coluna < 0 || posicao.Coluna >= Colunas) 
            ? false : true;

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
