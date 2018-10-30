using System;
using tabuleiro;
using xadrez;

namespace Xadrez {
    class Tela {
        
        public static void imprimirTabuleiro(Tabuleiro tabuleiro) {

            for(int i = 0; i<tabuleiro.linha; i++) {
                Console.Write(8 - i + " ");
                for (int j = 0; j < tabuleiro.coluna; j++) {
                    Tela.imprimirPeca(tabuleiro.peca(i, j));
                }
                Console.WriteLine();
            }
            Console.WriteLine("  A  B  C  D  E  F  G  H  ");
            Console.WriteLine();
        }

        public static void imprimirTabuleiro(Tabuleiro tabuleiro, bool [,] posicoesPossiveis) {

            ConsoleColor fundoOriginal = Console.BackgroundColor;
            ConsoleColor fundoAlterado = ConsoleColor.DarkGray;

            for (int i = 0; i < tabuleiro.linha; i++) {
                Console.BackgroundColor = fundoOriginal;
                Console.Write(8 - i + " ");
                for (int j = 0; j < tabuleiro.coluna; j++) {
                    if (posicoesPossiveis[i, j]) {
                        Console.BackgroundColor = fundoAlterado;
                    }
                    else {
                        Console.BackgroundColor = fundoOriginal;
                    }
                    Tela.imprimirPeca(tabuleiro.peca(i, j));
                }
                Console.WriteLine();
            }
            Console.BackgroundColor = fundoOriginal;
            Console.WriteLine("  A  B  C  D  E  F  G  H  ");
            Console.WriteLine();
        }

        public static PosicaoXadrez lerPosicaoXadrez() {
            string posicao = Console.ReadLine();
            Console.Write("Coluna:");
            char coluna = posicao[0];
            int linha = int.Parse(posicao[1] + "");            
            return new PosicaoXadrez(linha, coluna); 
        }

        public static void imprimirPeca(Peca peca) {

            if (peca == null) {
                Console.Write("— ");
            }
            else {
                if (peca.cor == Cor.Branca) {
                    Console.Write(peca + " ");
                }
                else {
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(peca + " ");
                    Console.ForegroundColor = aux;
                }
            }
            Console.Write(" ");
        }
    }
}
