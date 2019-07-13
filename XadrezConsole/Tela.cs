using System;
using System.Collections.Generic;
using System.Text;
using XadrezConsole.EntitiesTabuleiro;

namespace XadrezConsole
{
    class Tela
    {
        public static void ImprimirTabuleiro(Tabuleiro tabuleiro)
        {
            for (int linha = 0; linha < tabuleiro.Linhas; linha++)
            {
                for (int coluna = 0; coluna < tabuleiro.Colunas; coluna++)
                {
                    if (tabuleiro.PecaPosition(linha, coluna) != null)
                        Console.Write(tabuleiro.PecaPosition(linha, coluna) + " ");
                    else
                        Console.Write("- ");
                }
                Console.WriteLine();
            }
        }
    }
}
