using System;
using System.Collections.Generic;
using System.Text;
using XadrezConsole.EntitiesTabuleiro;

namespace XadrezConsole.Xadrez
{
    class PartidaDeXadrez
    {
        public Tabuleiro TabuleiroPartida { get; private set; }
        public int Turno { get; private set; }
        public Cor JogadorAtual { get; private set; }
        public bool Terminada { get; private set; }

        public PartidaDeXadrez()
        {
            TabuleiroPartida = new Tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Branca;
            Terminada = false;

            ColocarPecas();
        }

        public void ExecutarMovimento(Posicao origem, Posicao destino)
        {
            Peca peca = TabuleiroPartida.RetirarPeca(origem);
            peca.IncrementarMovimento();
            Peca PecaCapturada = TabuleiroPartida.RetirarPeca(destino);
            TabuleiroPartida.ColocarPeca(peca, destino);
        }

        public void RealizarJogada(Posicao origem, Posicao destino)
        {
            ExecutarMovimento(origem, destino);
            Turno++;
            MudarCorJogador();
        }

        public void ValidarPosicaoOrigem(Posicao posicao)
        {
            if (TabuleiroPartida.PecaPosition(posicao) == null)
                throw new TabuleiroException("Não existe peça na posição de origem escolhida!");

            if (JogadorAtual != TabuleiroPartida.PecaPosition(posicao).Cor)
                throw new TabuleiroException("A peça escolhida não é sua!");

            if (!TabuleiroPartida.PecaPosition(posicao).ExisteMovimentosPossiveis())
                throw new TabuleiroException("Não há movimentos possiveis para a peça escolhidas!");
        }

        public void ValidarPosicaoDestino(Posicao origem, Posicao destino)
        {
            if (!TabuleiroPartida.PecaPosition(origem).PodeMoverPara(destino))
                throw new TabuleiroException("Posição de destino inválida!");
        }

        private void MudarCorJogador()
        {
            if (JogadorAtual == Cor.Branca)
                JogadorAtual = Cor.Preta;
            else
                JogadorAtual = Cor.Branca;
        }

        public void ColocarPecas()
        {
            TabuleiroPartida.ColocarPeca(new Torre(TabuleiroPartida, Cor.Branca), new PosicaoXadrez('a', 1).ToPosition());
            TabuleiroPartida.ColocarPeca(new Torre(TabuleiroPartida, Cor.Branca), new PosicaoXadrez('h', 1).ToPosition());
            TabuleiroPartida.ColocarPeca(new Torre(TabuleiroPartida, Cor.Preta), new PosicaoXadrez('a', 8).ToPosition());
            TabuleiroPartida.ColocarPeca(new Torre(TabuleiroPartida, Cor.Preta), new PosicaoXadrez('h', 8).ToPosition());

            TabuleiroPartida.ColocarPeca(new Rei(TabuleiroPartida, Cor.Branca), new PosicaoXadrez('d', 2).ToPosition());
            TabuleiroPartida.ColocarPeca(new Rei(TabuleiroPartida, Cor.Preta), new PosicaoXadrez('d', 7).ToPosition());
        }
    }
}
