﻿using System;
using System.Collections.Generic;
using System.Text;
using XadrezConsole.EntitiesTabuleiro;

namespace XadrezConsole.Xadrez
{
    class PartidaDeXadrez
    {
        public Tabuleiro Tabuleiro { get; private set; }
        private int _Turno;
        private Cor _JogadorAtual;

        public PartidaDeXadrez()
        {
            Tabuleiro = new Tabuleiro(8, 8);
            _Turno = 1;
            _JogadorAtual = Cor.Branca;
        }

        public void ColocarPecas()
        {
            Tabuleiro.ColocarPeca(new Torre(Tabuleiro, Cor.Branca), new PosicaoXadrez('a', 1).ToPosition());
            Tabuleiro.ColocarPeca(new Torre(Tabuleiro, Cor.Branca), new PosicaoXadrez('h', 1).ToPosition());
        }

        public void ExecutarMovimento(Posicao origem, Posicao destino)
        {
            Peca peca = Tabuleiro.RetirarPeca(origem);
            peca.IncrementarMovimento();
            Peca PecaCapturada = Tabuleiro.RetirarPeca(destino);
            Tabuleiro.ColocarPeca(peca, destino);
        }
    }
}
