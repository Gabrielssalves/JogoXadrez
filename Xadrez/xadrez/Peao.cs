using System;
using tabuleiro;

namespace xadrez {
    class Peao : PecaXadrez {

        public Peao(Tabuleiro tabuleiro, Cor cor) : base(tabuleiro, cor) {

        }

        public override string ToString() {
            return "p";
        }

        public override bool[,] movimentosPossiveis() {
            bool[,] mat = new bool[tabuleiro.linha, tabuleiro.coluna];
            Posicao pos = new Posicao(0, 0);
            pos.defineValores(posicao.linha, posicao.coluna);

            schemaValidarMovimento(1, 0, pos, mat, 0);
            schemaValidarMovimento(0, 1, pos, mat, 0);
            schemaValidarMovimento(-1, 0, pos, mat, 0);
            schemaValidarMovimento(0, -1, pos, mat, 0);

            return mat;
        }
    }
}
