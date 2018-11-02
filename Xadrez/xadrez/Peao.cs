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


            if (this.cor == Cor.Branca) {
                if (qtdeMovimentos > 0) {
                    schemaValidarMovimento(-1, 0, pos, mat, 0, true);
                }
                else { 
                    schemaValidarMovimento(-1, 0, pos, mat, 1, true);
                }
                schemaValidarMovimento(-1, 1, pos, mat, 0,false);
                schemaValidarMovimento(-1, -1, pos, mat, 0, false);
            }
            else {
                if (qtdeMovimentos > 0) {
                    schemaValidarMovimento(1, 0, pos, mat, 0, true);
                }
                else {
                    schemaValidarMovimento(1, 0, pos, mat, 1, true);
                }
                schemaValidarMovimento(1, 1, pos, mat, 0, false);
                schemaValidarMovimento(1, -1, pos, mat, 0, false);
            }
            return mat;
        }
    }
}
