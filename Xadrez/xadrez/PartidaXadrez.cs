using System;
using tabuleiro;

namespace xadrez {
    class PartidaXadrez {

        public Tabuleiro tabuleiro { get; private set; }
        public int turno { get; private set; }
        public Cor jogadorAtual { get; private set; }
        public bool terminada { get; private set; }

        public PartidaXadrez() {
            tabuleiro = new Tabuleiro(8, 8);
            turno = 1;
            jogadorAtual = Cor.Branca;
            gerarPecas();
        }

        public void executaMovimento (Posicao origem, Posicao destino) {
            Peca p = tabuleiro.retirarPeca(origem);
            p.incrementarQtdeMovimentos();
            Peca pecaCapturada = tabuleiro.retirarPeca(destino);
            tabuleiro.colocarPeca(p, destino);
        }

        public void realizaJogada (Posicao origem, Posicao destino) {
            executaMovimento(origem, destino);
            turno++;
            mudaJogador();
        }

        private void mudaJogador() {
            if (jogadorAtual == Cor.Branca) {
                jogadorAtual = Cor.Preta;
            }
            else {
                jogadorAtual = Cor.Branca;
            }
        }

        public void validarPosicaoOrigem(Posicao posicao) {
            if(tabuleiro.peca(posicao) == null){
                throw new TabuleiroException("Não existe peça na posição de origem!");
            }
            if(jogadorAtual != tabuleiro.peca(posicao).cor) {
                throw new TabuleiroException("Você deve movimentas apenas as peças da cor:  " + jogadorAtual + "!");
            }
            if (!tabuleiro.peca(posicao).existeMovimentosPossiveis()) {
                throw new TabuleiroException("Não há movimentos possíveis para a peça " + tabuleiro.peca(posicao) + "!");
            }
        }

        public void validarPosicaoDestino(Posicao origem, Posicao destino) {
            if (!tabuleiro.peca(origem).podeMoverPara(destino)) {
                throw new TabuleiroException("Posição de destino inválida!");
            }
        }

        private void gerarPecas() {
            tabuleiro.colocarPeca(new Torre(tabuleiro, Cor.Preta), new PosicaoXadrez(8, 'a').toPosicao());
            tabuleiro.colocarPeca(new Torre(tabuleiro, Cor.Preta), new PosicaoXadrez(8, 'h').toPosicao());
            tabuleiro.colocarPeca(new Cavalo(tabuleiro, Cor.Preta), new PosicaoXadrez(8, 'b').toPosicao());
            tabuleiro.colocarPeca(new Cavalo(tabuleiro, Cor.Preta), new PosicaoXadrez(8, 'g').toPosicao());
            tabuleiro.colocarPeca(new Bispo(tabuleiro, Cor.Preta), new PosicaoXadrez(8, 'c').toPosicao());
            tabuleiro.colocarPeca(new Bispo(tabuleiro, Cor.Preta), new PosicaoXadrez(8, 'f').toPosicao());
            tabuleiro.colocarPeca(new Rei(tabuleiro, Cor.Preta), new PosicaoXadrez(8, 'd').toPosicao());
            tabuleiro.colocarPeca(new Rainha(tabuleiro, Cor.Preta), new PosicaoXadrez(8, 'e').toPosicao());

            for (int i = 0; i < 8; i++) {                
                tabuleiro.colocarPeca(new Peao(tabuleiro, Cor.Preta), new PosicaoXadrez(7, Convert.ToChar('a' + i) ).toPosicao());
            }

            tabuleiro.colocarPeca(new Torre(tabuleiro, Cor.Branca), new PosicaoXadrez(1, 'a').toPosicao());
            tabuleiro.colocarPeca(new Torre(tabuleiro, Cor.Branca), new PosicaoXadrez(1, 'h').toPosicao());
            tabuleiro.colocarPeca(new Cavalo(tabuleiro, Cor.Branca), new PosicaoXadrez(1, 'b').toPosicao());
            tabuleiro.colocarPeca(new Cavalo(tabuleiro, Cor.Branca), new PosicaoXadrez(1, 'g').toPosicao());
            tabuleiro.colocarPeca(new Bispo(tabuleiro, Cor.Branca), new PosicaoXadrez(1, 'c').toPosicao());
            tabuleiro.colocarPeca(new Bispo(tabuleiro, Cor.Branca), new PosicaoXadrez(1, 'f').toPosicao());
            tabuleiro.colocarPeca(new Rei(tabuleiro, Cor.Branca), new PosicaoXadrez(1, 'd').toPosicao());
            tabuleiro.colocarPeca(new Rainha(tabuleiro, Cor.Branca), new PosicaoXadrez(1, 'e').toPosicao());

            for (int i = 0; i < 8; i++) {
                tabuleiro.colocarPeca(new Peao(tabuleiro, Cor.Branca), new PosicaoXadrez(2, Convert.ToChar('a' + i)).toPosicao());
            }


        }


    }
}
