
namespace tabuleiro {
    class Tabuleiro {
        public int linha { get; set; }
        public int coluna { get; set; }
        private Peca[,] pecas;

        public Tabuleiro(int linha, int coluna) {
            this.linha = linha;
            this.coluna = coluna;
            pecas = new Peca[linha, coluna];
        }

        public Peca peca (int linha, int coluna) {
            return pecas[linha, coluna];
        }

        public Peca peca(Posicao posicao) {
            return pecas[posicao.linha, posicao.coluna];
        }


        public void colocarPeca(Peca peca, Posicao posicao) {
            if(verificaVagaPosicao(posicao)) {
                throw new TabuleiroException("Já existe uma peça nesta posição!");
            }
            pecas[posicao.linha, posicao.coluna] = peca;
            peca.posicao = posicao; 
        }

        public Peca retirarPeca (Posicao posicao) {
            if(peca(posicao) == null) {
                return null;
            }
            Peca aux = peca(posicao);
            aux.posicao = null;
            pecas[posicao.linha, posicao.coluna] = null;
            return aux;
        }

        public bool verificaValoresPosicao(Posicao posicao) {
            if (posicao.linha < 0 || posicao.linha >= linha || posicao.coluna < 0 || posicao.coluna >= coluna) {
                return false;
            }

            return true;
        }

        public bool verificaVagaPosicao(Posicao posicao) {
            validaPosicao(posicao);
            return peca(posicao) != null;
        }

        public void validaPosicao(Posicao posicao) {
            if (!verificaValoresPosicao(posicao)) {
                throw new TabuleiroException("Posição inválida!");
            }
        }
    }
}
