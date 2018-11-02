using System;
using tabuleiro;
using xadrez;
using System.Collections.Generic;

namespace Xadrez {
    class Tela {
        

        public static void imprimirPartida(PartidaXadrez partida) {
            imprimirTabuleiro(partida.tabuleiro);
            Console.WriteLine();
            imprimirPecasCapturadas(partida);

            Console.WriteLine();
            Console.WriteLine("Turno: " + partida.turno);
            if (!partida.terminada) {
                Console.Write("Aguardando jogada: ");
                ConsoleColor aux = Console.ForegroundColor;
                if (partida.jogadorAtual == Cor.Preta) {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                }
                Console.WriteLine(partida.jogadorAtual);
                Console.ForegroundColor = aux;
                Console.WriteLine();
                if (partida.xeque) {
                    Console.WriteLine("XEQUE!");
                }
            }
            else {
                Console.WriteLine("XEQUEMATE!");
                Console.WriteLine("Vencedor: " + partida.jogadorAtual);
            }
            
        }

        public static void imprimirPecasCapturadas(PartidaXadrez partida) {
            Console.WriteLine("Peças Capturadas: ");
            Console.Write("Brancas: ");
            imprimirConjunto(partida.pecasCapturadas(Cor.Branca));
            Console.WriteLine();
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Pretas: ");
            imprimirConjunto(partida.pecasCapturadas(Cor.Preta));
            Console.ForegroundColor = aux;
            Console.WriteLine();
        }

        public static void imprimirConjunto(HashSet<Peca> conjunto) {
            Console.Write("[");
            foreach(Peca x in conjunto) {
                Console.Write(x + " ");
            }
            Console.Write("]");
        }
        public static void imprimirTabuleiro(Tabuleiro tabuleiro) {

            for(int i = 0; i<tabuleiro.linha; i++) {
                Console.Write(8 - i + " ");
                for (int j = 0; j < tabuleiro.coluna; j++) {
                    Tela.imprimirPeca(tabuleiro.peca(i, j));
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a  b  c  d  e  f  g  h  ");
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
            Console.WriteLine("  a  b  c  d  e  f  g  h  ");
            Console.WriteLine();
        }

        public static PosicaoXadrez lerPosicaoXadrez() {
            string posicao = Console.ReadLine();
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
