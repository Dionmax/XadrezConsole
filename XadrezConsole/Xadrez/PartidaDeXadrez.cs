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
        public bool JogadorEmXeque { get; private set; }

        private HashSet<Peca> pecas;
        private HashSet<Peca> capturadas;

        public PartidaDeXadrez()
        {
            TabuleiroPartida = new Tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Branca;
            Terminada = false;
            JogadorEmXeque = false;
            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();

            ColocarPecas();
        }

        public Peca ExecutarMovimento(Posicao origem, Posicao destino)
        {
            Peca peca = TabuleiroPartida.RetirarPeca(origem);
            peca.IncrementarMovimento();
            Peca PecaCapturada = TabuleiroPartida.RetirarPeca(destino);
            TabuleiroPartida.ColocarPeca(peca, destino);

            if (PecaCapturada != null)
                capturadas.Add(PecaCapturada);

            // #MovimentoEspecial Roque maior
            if (peca is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao origemTorre = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao destinoTorre = new Posicao(origem.Linha, origem.Coluna - 1);

                Peca torre = TabuleiroPartida.RetirarPeca(origemTorre);
                torre.IncrementarMovimento();
                TabuleiroPartida.ColocarPeca(torre, destinoTorre);
            }

            // #MovimentoEspecial Roque menor
            if (peca is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao origemTorre = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao destinoTorre = new Posicao(origem.Linha, origem.Coluna + 1);

                Peca torre = TabuleiroPartida.RetirarPeca(origemTorre);
                torre.IncrementarMovimento();
                TabuleiroPartida.ColocarPeca(torre, destinoTorre);
            }

            return PecaCapturada;
        }

        public void DesfazerMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
        {
            Peca peca = TabuleiroPartida.RetirarPeca(destino);

            peca.DecrementarMovimento();

            if (pecaCapturada != null)
            {
                TabuleiroPartida.ColocarPeca(pecaCapturada, destino);
                capturadas.Remove(pecaCapturada);
            }

            // #MovimentoEspecial Roque maior (revertendo)
            if (peca is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao origemTorre = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao destinoTorre = new Posicao(origem.Linha, origem.Coluna - 1);

                Peca torre = TabuleiroPartida.RetirarPeca(destinoTorre);
                torre.DecrementarMovimento();
                TabuleiroPartida.ColocarPeca(torre, origemTorre);
            }

            // #MovimentoEspecial Roque menor (revertendo)
            if (peca is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao origemTorre = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao destinoTorre = new Posicao(origem.Linha, origem.Coluna + 1);

                Peca torre = TabuleiroPartida.RetirarPeca(destinoTorre);
                torre.DecrementarMovimento();
                TabuleiroPartida.ColocarPeca(torre, origemTorre);
            }

            TabuleiroPartida.ColocarPeca(peca, origem);
        }

        public void RealizarJogada(Posicao origem, Posicao destino)
        {
            Peca pecaCapturada = ExecutarMovimento(origem, destino);

            if (EstaEmXeque(JogadorAtual))
            {
                DesfazerMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroException("Você não pode se colocar em xeque!");
            }

            JogadorEmXeque = (EstaEmXeque(CorAdversaria(JogadorAtual)))
                ? true : false;

            if (TesteXequemate(CorAdversaria(JogadorAtual)))
                Terminada = true;
            else
            {
                Turno++;
                MudarCorJogador();
            }
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
            if (!TabuleiroPartida.PecaPosition(origem).MovimentoPossivel(destino))
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

        private Peca ReiEmJogo(Cor cor)
        {
            foreach (Peca peca in PecasEmJogo(cor))
                if (peca is Rei)
                    return peca;

            throw new TabuleiroException("Era para ter um rei de cada cor nesse tabuleiro O.O");
        }

        private Cor CorAdversaria(Cor cor)
            => (cor == Cor.Branca) ? Cor.Preta : Cor.Branca;

        public bool EstaEmXeque(Cor cor)
        {
            Peca rei = ReiEmJogo(cor);

            if (rei == null)
                throw new TabuleiroException($"Cade o rei da cor #{cor}?");

            foreach (Peca peca in PecasEmJogo(CorAdversaria(cor)))
            {
                bool[,] matriz = peca.MovimentosPossiveis();
                if (matriz[rei.Posicao.Linha, rei.Posicao.Coluna])
                    return true;
            }

            return false;
        }

        public bool TesteXequemate(Cor cor)
        {
            if (!JogadorEmXeque)
                return false;

            foreach (Peca peca in PecasEmJogo(cor))
            {
                bool[,] matriz = peca.MovimentosPossiveis();

                for (int linha = 0; linha < TabuleiroPartida.Linhas; linha++)
                    for (int coluna = 0; coluna < TabuleiroPartida.Colunas; coluna++)
                        if (matriz[linha, coluna])
                        {
                            Posicao origem = peca.Posicao;
                            Posicao destino = new Posicao(linha, coluna);

                            Peca pecaCapturada = ExecutarMovimento(origem, destino);
                            bool testeXeque = EstaEmXeque(cor);
                            DesfazerMovimento(origem, destino, pecaCapturada);

                            if (!testeXeque)
                                return false;
                        }
            }

            return true;
        }

        public void ColocarNovaPeca(char coluna, int linha, Peca peca)
        {
            TabuleiroPartida.ColocarPeca(peca, new PosicaoXadrez(coluna, linha).ToPosition());
            pecas.Add(peca);
        }

        public void ColocarPecas()
        {
            ColocarNovaPeca('e', 1, new Rei(TabuleiroPartida, Cor.Branca, this));
            ColocarNovaPeca('d', 1, new Dama(TabuleiroPartida, Cor.Branca));
            ColocarNovaPeca('a', 1, new Torre(TabuleiroPartida, Cor.Branca));
            ColocarNovaPeca('b', 1, new Cavalo(TabuleiroPartida, Cor.Branca));
            ColocarNovaPeca('g', 1, new Cavalo(TabuleiroPartida, Cor.Branca));
            ColocarNovaPeca('h', 1, new Torre(TabuleiroPartida, Cor.Branca));
            ColocarNovaPeca('c', 1, new Bispo(TabuleiroPartida, Cor.Branca));
            ColocarNovaPeca('f', 1, new Bispo(TabuleiroPartida, Cor.Branca));

            ColocarNovaPeca('e', 8, new Rei(TabuleiroPartida, Cor.Preta, this));
            ColocarNovaPeca('d', 8, new Dama(TabuleiroPartida, Cor.Preta));
            ColocarNovaPeca('a', 8, new Torre(TabuleiroPartida, Cor.Preta));
            ColocarNovaPeca('h', 8, new Torre(TabuleiroPartida, Cor.Preta));
            ColocarNovaPeca('b', 8, new Cavalo(TabuleiroPartida, Cor.Preta));
            ColocarNovaPeca('g', 8, new Cavalo(TabuleiroPartida, Cor.Preta));
            ColocarNovaPeca('c', 8, new Bispo(TabuleiroPartida, Cor.Preta));
            ColocarNovaPeca('f', 8, new Bispo(TabuleiroPartida, Cor.Preta));

            ColocarNovaPeca('d', 2, new Peao(TabuleiroPartida, Cor.Branca));
            ColocarNovaPeca('e', 2, new Peao(TabuleiroPartida, Cor.Branca));
            ColocarNovaPeca('a', 2, new Peao(TabuleiroPartida, Cor.Branca));
            ColocarNovaPeca('h', 2, new Peao(TabuleiroPartida, Cor.Branca));
            ColocarNovaPeca('b', 2, new Peao(TabuleiroPartida, Cor.Branca));
            ColocarNovaPeca('g', 2, new Peao(TabuleiroPartida, Cor.Branca));
            ColocarNovaPeca('c', 2, new Peao(TabuleiroPartida, Cor.Branca));
            ColocarNovaPeca('f', 2, new Peao(TabuleiroPartida, Cor.Branca));

            ColocarNovaPeca('d', 7, new Peao(TabuleiroPartida, Cor.Preta));
            ColocarNovaPeca('e', 7, new Peao(TabuleiroPartida, Cor.Preta));
            ColocarNovaPeca('a', 7, new Peao(TabuleiroPartida, Cor.Preta));
            ColocarNovaPeca('h', 7, new Peao(TabuleiroPartida, Cor.Preta));
            ColocarNovaPeca('b', 7, new Peao(TabuleiroPartida, Cor.Preta));
            ColocarNovaPeca('g', 7, new Peao(TabuleiroPartida, Cor.Preta));
            ColocarNovaPeca('c', 7, new Peao(TabuleiroPartida, Cor.Preta));
            ColocarNovaPeca('f', 7, new Peao(TabuleiroPartida, Cor.Preta));
        }
    }
}
