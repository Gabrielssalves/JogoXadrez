using tabuleiro;
namespace xadrez {
    class Bispo : PecaXadrez {

        public Bispo(Tabuleiro tabuleiro, Cor cor) : base(tabuleiro, cor) {

        }

        public override string ToString() {
            return "B";
        }

        public override bool[,] movimentosPossiveis() {
            bool[,] mat = new bool[tabuleiro.linha, tabuleiro.coluna];
            Posicao pos = new Posicao(0, 0);
            pos.defineValores(posicao.linha, posicao.coluna);

            for (int i = -1; i < 2; i++) {
                for (int j = -1; j < 2; j++) {
                    if (i != 0) {
                        schemaValidarMovimento(i, j, pos, mat, 99, true);
                    }
                }
            }
            return mat;
        }
    }
}