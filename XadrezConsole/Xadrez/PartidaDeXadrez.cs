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

        private HashSet<Peca> pecas;
        private HashSet<Peca> capturadas;

        public PartidaDeXadrez()
        {
            TabuleiroPartida = new Tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Branca;
            Terminada = false;
            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();

            ColocarPecas();
        }

        public void ExecutarMovimento(Posicao origem, Posicao destino)
        {
            Peca peca = TabuleiroPartida.RetirarPeca(origem);
            peca.IncrementarMovimento();
            Peca PecaCapturada = TabuleiroPartida.RetirarPeca(destino);
            TabuleiroPartida.ColocarPeca(peca, destino);

            if (PecaCapturada != null)
                capturadas.Add(PecaCapturada);
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

        public HashSet<Peca> PecasCapturadas(Cor cor)
        {
            HashSet<Peca> conjuntoPecas = new HashSet<Peca>();

            foreach (Peca pecaCapturada in capturadas)
                if (pecaCapturada.Cor == cor)
                    conjuntoPecas.Add(pecaCapturada);

            return conjuntoPecas;
        }

        public HashSet<Peca> PecasEmJogo(Cor cor)
        {
            HashSet<Peca> conjuntoPecas = new HashSet<Peca>();

            foreach (Peca pecaEmJogo in pecas)
                if (pecaEmJogo.Cor == cor)
                    conjuntoPecas.Add(pecaEmJogo);

            conjuntoPecas.ExceptWith(PecasCapturadas(cor));

            return conjuntoPecas;
        }

        public void ColocarNovaPeca(char coluna, int linha, Peca peca)
        {
            TabuleiroPartida.ColocarPeca(peca, new PosicaoXadrez(coluna, linha).ToPosition());
            pecas.Add(peca);
        }

        public void ColocarPecas()
        {
            ColocarNovaPeca('a', 1, new Torre(TabuleiroPartida, Cor.Branca));
            ColocarNovaPeca('h', 1, new Torre(TabuleiroPartida, Cor.Branca));
            ColocarNovaPeca('a', 8, new Torre(TabuleiroPartida, Cor.Preta));
            ColocarNovaPeca('h', 8, new Torre(TabuleiroPartida, Cor.Preta));

            ColocarNovaPeca('d', 1, new Rei(TabuleiroPartida, Cor.Branca));
            ColocarNovaPeca('d', 8, new Rei(TabuleiroPartida, Cor.Preta));
        }
    }
}
