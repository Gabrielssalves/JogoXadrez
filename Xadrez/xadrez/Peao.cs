using System;
using tabuleiro;

namespace xadrez {
    class Peao : PecaXadrez {

        private PartidaXadrez partida;

        public Peao(Tabuleiro tabuleiro, Cor cor, PartidaXadrez partida) : base(tabuleiro, cor) {
            this.partida = partida;
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

                //#Jogada especial en passant Esquerda
                if(posicao.linha == 3) {
                    Posicao esquerda = new Posicao(posicao.linha, posicao.coluna - 1);
                    if(tabuleiro.verificaVagaPosicao(esquerda) && podeMover(esquerda, false) && tabuleiro.peca(esquerda) == partida.vulneravelEnPassant) {
                        mat[esquerda.linha - 1 , esquerda.coluna] = true;
                    }
                    Posicao direita = new Posicao(posicao.linha, posicao.coluna + 1);
                    if (tabuleiro.verificaVagaPosicao(direita) && podeMover(direita, false) && tabuleiro.peca(direita) == partida.vulneravelEnPassant) {
                        mat[direita.linha - 1 , direita.coluna] = true;
                    }
                }
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

                //#Jogada especial en passant Direita
                if (posicao.linha == 4) {
                    Posicao esquerda = new Posicao(posicao.linha, posicao.coluna - 1);
                    if (tabuleiro.verificaVagaPosicao(esquerda) && podeMover(esquerda, false) && tabuleiro.peca(esquerda) == partida.vulneravelEnPassant) {
                        mat[esquerda.linha + 1, esquerda.coluna] = true;
                    }
                    Posicao direita = new Posicao(posicao.linha, posicao.coluna + 1);
                    if (tabuleiro.verificaVagaPosicao(direita) && podeMover(direita, false) && tabuleiro.peca(direita) == partida.vulneravelEnPassant) {
                        mat[direita.linha + 1, direita.coluna] = true;
                    }
                }
            }

            return mat;
        }
    }
}
