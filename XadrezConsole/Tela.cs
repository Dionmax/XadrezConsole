using System;
using System.Collections.Generic;
using XadrezConsole.EntitiesTabuleiro;
using XadrezConsole.Xadrez;

namespace XadrezConsole
{
    class Tela
    {
        public static void ImprimirPartida(PartidaDeXadrez partida)
        {
            Console.Clear();
            Tela.ImprimirTabuleiro(partida.TabuleiroPartida);
            Console.WriteLine();
            ImprimirPecasCapturadas(partida);
            Console.WriteLine();
            Console.WriteLine($"Turno da partida: {partida.Turno}");

            if (!partida.Terminada)
            {
                Console.WriteLine($"Esperando a vez: {partida.JogadorAtual}");

                if (partida.JogadorEmXeque)
                    Console.WriteLine("Xeque!");
            }
            else
            {
                Console.WriteLine("Xeque mate!");
                Console.WriteLine($"Vencedor: {partida.JogadorAtual}");
            }
        }

        public static void ImprimirPecasCapturadas(PartidaDeXadrez partida)
        {
            Console.WriteLine("Peças capturadas: ");
            Console.Write("Brancas: ");
            ImprimirConjunto(partida.PecasCapturadas(Cor.Branca));
            Console.Write("\nPretas: ");
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            ImprimirConjunto(partida.PecasCapturadas(Cor.Preta));
            Console.ForegroundColor = aux;
        }

        public static void ImprimirConjunto(HashSet<Peca> conjuntoPecas)
        {
            Console.Write("[");
            foreach (Peca peca in conjuntoPecas)
                Console.Write(peca + " ");

            Console.Write("]");
        }

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
                    Console.BackgroundColor = (PosicoesPossiveis[linha, coluna]) ? CorAlterada : CorOriginal;

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
