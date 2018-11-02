using System;
using tabuleiro;

namespace xadrez {
    class Cavalo : PecaXadrez {
        public Cavalo(Tabuleiro tabuleiro, Cor cor) : base(tabuleiro, cor) {

        }

        public override string ToString() {
            return "C";
        }

        public override bool[,] movimentosPossiveis() {
            bool[,] mat = new bool[tabuleiro.linha, tabuleiro.coluna];
            Posicao pos = new Posicao(0, 0);
            pos.defineValores(posicao.linha, posicao.coluna);

            for (int i = -2; i < 3; i++) {
                for (int j = -2; j < 3; j++) {
                    if ((i != 0 && j != 0) && (i + j != (2*i) && i + j != (0))) {
                        schemaValidarMovimento(i, j, pos, mat, 0, true);
                    } 
                }
            }

            return mat;
        }
    }
}
