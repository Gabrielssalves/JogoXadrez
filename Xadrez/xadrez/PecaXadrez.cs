using System;
using tabuleiro;

namespace xadrez {
    abstract class PecaXadrez : Peca {

        public PecaXadrez(Tabuleiro tabuleiro, Cor cor) : base(tabuleiro, cor) {

        }

        protected bool podeMover(Posicao posicao) {
            Peca peca = tabuleiro.peca(posicao);
            return peca == null || peca.cor != this.cor;
        }

        protected void schemaValidarMovimento(int variacaoLinha, int variacaoColuna, Posicao pos, bool[,] mat, int QtdeMaximaRecursao) {

            pos.defineValores(pos.linha + variacaoLinha, pos.coluna + variacaoColuna);
            if (tabuleiro.verificaValoresPosicao(pos) && podeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;
                if ((tabuleiro.peca(pos) == null || tabuleiro.peca(pos).cor == this.cor) && (QtdeMaximaRecursao != 0)) {
                    schemaValidarMovimento(variacaoLinha, variacaoColuna, pos, mat, QtdeMaximaRecursao--);
                }
            }
            pos.defineValores(posicao.linha, posicao.coluna);
        }
    }
}
