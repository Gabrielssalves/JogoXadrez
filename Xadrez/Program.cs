using System;
using tabuleiro;
using xadrez;

namespace Xadrez {
    class Program {
        static void Main(string[] args) {

            try {
                PartidaXadrez partida = new PartidaXadrez();

                while (!partida.terminada) {
                    try {
                        Console.Clear();
                        Tela.imprimirTabuleiro(partida.tabuleiro);
                        Console.WriteLine();
                        Console.WriteLine("Turno: " + partida.turno);
                        Console.WriteLine("Aguardando jogada: " + partida.jogadorAtual);


                        Console.Write("Origem:");
                        Posicao origem = Tela.lerPosicaoXadrez().toPosicao();
                        partida.validarPosicaoOrigem(origem);
                        bool[,] posicoesPossiveis = partida.tabuleiro.peca(origem).movimentosPossiveis();                        
                        Console.Clear();
                        Tela.imprimirTabuleiro(partida.tabuleiro, posicoesPossiveis);

                        Console.Write("Destino:");
                        Posicao destino = Tela.lerPosicaoXadrez().toPosicao();
                        partida.validarPosicaoDestino(origem, destino);
                        partida.realizaJogada(origem, destino);
                    }
                    catch(TabuleiroException e) {
                        Console.WriteLine(e.Message);
                        Console.ReadLine();
                    }

                }


            }
            catch(TabuleiroException e) {
                Console.WriteLine(e.Message);
            }


            
            Console.ReadLine();
        }
    }
}
