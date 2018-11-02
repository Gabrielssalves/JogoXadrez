using System;
using tabuleiro;

namespace xadrez {
    abstract class PecaXadrez : Peca {

        public PecaXadrez(Tabuleiro tabuleiro, Cor cor) : base(tabuleiro, cor) {

        }

        protected bool podeMover(Posicao posicao, bool aceitaPosVazia) {
            Peca peca = tabuleiro.peca(posicao);
            if (aceitaPosVazia) {
                return peca == null || peca.cor != this.cor;
            }
            else {
                if (peca == null) {
                    return false;
                }
                else {
                    return peca.cor != this.cor;
                }
            }
        }

        protected void schemaValidarMovimento(int variacaoLinha, int variacaoColuna, Posicao pos, bool[,] mat, int QtdeMaximaRecursao, bool aceitaPosVazia) {

            pos.defineValores(pos.linha + variacaoLinha, pos.coluna + variacaoColuna);
            if (tabuleiro.verificaValoresPosicao(pos) && podeMover(pos, aceitaPosVazia)) {
                mat[pos.linha, pos.coluna] = true;
                if ((tabuleiro.peca(pos) == null || tabuleiro.peca(pos).cor == this.cor) && (QtdeMaximaRecursao != 0)) {
                    schemaValidarMovimento(variacaoLinha, variacaoColuna, pos, mat, --QtdeMaximaRecursao, aceitaPosVazia);
                }
            }
            pos.defineValores(posicao.linha, posicao.coluna);
        }
    }
}
