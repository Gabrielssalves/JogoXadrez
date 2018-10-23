using System;
using tabuleiro;

namespace Xadrez {
    class Program {
        static void Main(string[] args) {

            Tabuleiro tabuleiro = new Tabuleiro(8, 8);


            Tela.imprimirTabuleiro(tabuleiro);
            Console.ReadLine();
        }
    }
}
