using XadrezConsole.EntitiesTabuleiro;

namespace XadrezConsole.Xadrez
{
    class PosicaoXadrez
    {
        public char Coluna { get; set; }

        public int Linha { get; set; }

        public PosicaoXadrez(char coluna, int linha)
        {
            Coluna = coluna;
            Linha = linha;
        }

        public Posicao ToPosition()
            => new Posicao(8 - Linha, Coluna - 'a');

        public override string ToString()
            => "" + Coluna + Linha;
    }
}
