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

            schemaValidarMovimento( 1, 1, pos, mat,99);
            schemaValidarMovimento( 1, -1, pos, mat,99);
            schemaValidarMovimento(-1, 1, pos, mat,99);
            schemaValidarMovimento(-1, -1, pos, mat,99);

            return mat;
        }
    }
}