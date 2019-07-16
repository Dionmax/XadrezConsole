﻿using System;
using XadrezConsole.EntitiesTabuleiro;
using XadrezConsole.Xadrez;

namespace XadrezConsole
{
    class Tela
    {
        public static void ImprimirTabuleiro(Tabuleiro tabuleiro)
        {
            for (int linha = 0; linha < tabuleiro.Linhas; linha++)
            {
                Console.Write(8 - linha + " ");

                for (int coluna = 0; coluna < tabuleiro.Colunas; coluna++)
                    ImprimirPeca(tabuleiro.PecaPosition(linha, coluna));

                Console.WriteLine();
            }

            Console.WriteLine("  a b c d e f g h");
        }

        public static void ImprimirTabuleiro(Tabuleiro tabuleiro, bool[,] PosicoesPossiveis)
        {
            ConsoleColor CorOriginal = Console.BackgroundColor;
            ConsoleColor CorAlterada = ConsoleColor.DarkGray;

            for (int linha = 0; linha < tabuleiro.Linhas; linha++)
            {
                Console.Write(8 - linha + " ");

                for (int coluna = 0; coluna < tabuleiro.Colunas; coluna++)
                {
                    if (PosicoesPossiveis[linha, coluna])
                        Console.BackgroundColor = CorAlterada;
                    else
                        Console.BackgroundColor = CorOriginal;

                    ImprimirPeca(tabuleiro.PecaPosition(linha, coluna));
                    Console.BackgroundColor = CorOriginal;
                }

                Console.WriteLine();
            }

            Console.WriteLine("  a b c d e f g h");

        }

        public static void ImprimirPeca(Peca peca)
        {
            if (peca == null)
                Console.Write("-");
            else
            {

                if (peca.Cor == Cor.Branca)
                    Console.Write(peca);
                else
                {
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(peca);
                    Console.ForegroundColor = aux;
                }
            }
            Console.Write(" ");
        }

        public static PosicaoXadrez LerPosicaoXadrez()
        {
            string leituraPosicao = Console.ReadLine();
            char coluna = leituraPosicao[0];
            int linha = int.Parse(leituraPosicao[1] + "");

            return new PosicaoXadrez(coluna, linha);
        }
    }
}
