using XadrezConsole.EntitiesTabuleiro;

namespace XadrezConsole.Xadrez
{
    class Rei : Peca
    {
        public Rei(Tabuleiro tabuleiro, Cor cor) : base(tabuleiro, cor) { }

        public override string ToString()
            => "R";
    }
}
