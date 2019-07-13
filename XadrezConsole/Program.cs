﻿using System;
using XadrezConsole.EntitiesTabuleiro;
using XadrezConsole.Xadrez;

namespace XadrezConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Tabuleiro tabuleiro = new Tabuleiro(8, 8);

            tabuleiro.ColocarPeca(new Rei(tabuleiro,Cor.Preta), new Posicao(0,0)); ;

            Tela.ImprimirTabuleiro(tabuleiro);
        }
    }
}