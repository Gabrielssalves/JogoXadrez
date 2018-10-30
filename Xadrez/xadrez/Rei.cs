using tabuleiro;
namespace xadrez {
    class Rei : Peca {

        public Rei(Tabuleiro tabuleiro, Cor cor) : base (tabuleiro, cor) {

        }

        public override string ToString() {
            return "R";
        }

        private bool podeMover(Posicao posicao) {
            Peca peca = tabuleiro.peca(posicao);
            return peca == null || peca.cor != this.cor;
        }

        private void schemaValidarMovimento(int variacaoLinha, int variacaoColuna, bool[,] mat) {
            Posicao pos = new Posicao(0, 0);
            pos.defineValores(variacaoLinha, variacaoColuna);
            if(tabuleiro.verificaValoresPosicao(pos) && podeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;
            }
        }

        public override bool[,] movimentosPossiveis() {
            bool[,] mat = new bool[tabuleiro.linha, tabuleiro.coluna];
            
            for (int i = -1; i<2; i++) {
                for(int j = -1; j<2; j++) {
                    schemaValidarMovimento(posicao.linha + i, posicao.coluna + j, mat);
                }
            }
            return mat;
        }
    }
}
