 using System;
using tabuleiro;

namespace xadrez {
    class Torre : Peca {

        public Torre(Tabuleiro tabuleiro, Cor cor) : base(tabuleiro, cor) {

        }

        public override string ToString() {
            return "T";
        }

        private bool podeMover(Posicao posicao) {
            Peca peca = tabuleiro.peca(posicao);
            return peca == null || peca.cor != this.cor;
        }

        private void schemaValidarMovimento(int variacaoLinha, int variacaoColuna, Posicao pos, bool[,] mat) {

            pos.defineValores(pos.linha + variacaoLinha, pos.coluna + variacaoColuna);
            if (tabuleiro.verificaValoresPosicao(pos) && podeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;                
                if (tabuleiro.peca(pos) != null && tabuleiro.peca(pos).cor != this.cor) {
                    return;
                }
                schemaValidarMovimento(variacaoLinha, variacaoColuna, pos, mat);
            }
            pos.defineValores(posicao.linha, posicao.coluna);    
        }

        public override bool[,] movimentosPossiveis() {
            bool[,] mat = new bool[tabuleiro.linha, tabuleiro.coluna];
            Posicao pos = new Posicao(0, 0);
            pos.defineValores(posicao.linha, posicao.coluna);

            schemaValidarMovimento(1, 0, pos, mat);
            schemaValidarMovimento(0, 1, pos, mat);
            schemaValidarMovimento(-1, 0, pos, mat);
            schemaValidarMovimento(0, -1, pos, mat);

            return mat;
        }
    }
}
