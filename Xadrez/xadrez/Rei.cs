using tabuleiro;

namespace xadrez {
    class Rei : PecaXadrez {

        private PartidaXadrez partida;

        public Rei(Tabuleiro tabuleiro, Cor cor, PartidaXadrez partida) : base (tabuleiro, cor) {
            this.partida = partida;
        }

        public override string ToString() {
            return "R";
        }

        private bool testeTorreParaRoque(Posicao posicao) {
            Peca peca = tabuleiro.peca(posicao);
            return peca != null && peca is Torre && peca.cor == cor && peca.qtdeMovimentos == 0;
        }

        public override bool[,] movimentosPossiveis() {
            bool[,] mat = new bool[tabuleiro.linha, tabuleiro.coluna];
            Posicao pos = new Posicao(0, 0);
            pos.defineValores(posicao.linha, posicao.coluna);

            for (int i = -1; i < 2; i++) {
                for (int j = -1; j < 2; j++) {
                    schemaValidarMovimento(i, j, pos, mat, 0, true);
                }
            }

            // #Jogada especial Roque
            if(qtdeMovimentos == 0 && !partida.xeque) {
                //#Jogada Especial roque pequeno
                Posicao posicaoRoqueP = new Posicao(posicao.linha, posicao.coluna + 3);
                if (testeTorreParaRoque(posicaoRoqueP)) {
                    Posicao newPosRei = new Posicao(posicao.linha, posicao.coluna + 2);
                    Posicao newPosTorre = new Posicao(posicao.linha, posicao.coluna + 1);
                    if(tabuleiro.peca(newPosRei) == null && tabuleiro.peca(newPosTorre) == null) {
                        mat[posicao.linha, posicao.coluna + 2] = true;
                    }
                }
                // #Jogada especial Roque Grande
                Posicao posicaoRoqueG = new Posicao(posicao.linha, posicao.coluna - 4);
                if (testeTorreParaRoque(posicaoRoqueP)) {
                    Posicao newPosRei = new Posicao(posicao.linha, posicao.coluna - 2);
                    Posicao newPosTorre = new Posicao(posicao.linha, posicao.coluna - 1);
                    Posicao posLivre = new Posicao(posicao.linha, posicao.coluna - 3);
                    if (tabuleiro.peca(newPosRei) == null && tabuleiro.peca(newPosTorre) == null && tabuleiro.peca(posLivre) == null) {
                        mat[posicao.linha, posicao.coluna - 2] = true;
                    }
                }
            }
            return mat;
        }
    }
}
