﻿using System;
using tabuleiro;

namespace xadrez {
    class PartidaXadrez {

        public Tabuleiro tabuleiro { get; private set; }
        private int turno;
        private Cor jogadorAtual;
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

        private void gerarPecas() {
            tabuleiro.colocarPeca(new Torre(tabuleiro, Cor.Preta), new PosicaoXadrez(1, 'c').toPosicao());
            tabuleiro.colocarPeca(new Rei(tabuleiro, Cor.Branca), new PosicaoXadrez(7, 'c').toPosicao());
            tabuleiro.colocarPeca(new Cavalo(tabuleiro, Cor.Preta), new PosicaoXadrez(7, 'g').toPosicao());
        }


    }
}
