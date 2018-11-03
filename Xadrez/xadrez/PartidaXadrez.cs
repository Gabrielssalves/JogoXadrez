using System;
using System.Collections.Generic;
using tabuleiro;

namespace xadrez {
    class PartidaXadrez {

        public Tabuleiro tabuleiro { get; private set; }
        public int turno { get; private set; }
        public Cor jogadorAtual { get; private set; }
        public bool terminada { get; private set; }
        private HashSet<Peca> pecas;
        private HashSet<Peca> capturadas;
        public bool xeque { get; private set; }
        public Peca vulneravelEnPassant { get; private set; }

        public PartidaXadrez() {
            tabuleiro = new Tabuleiro(8, 8);
            turno = 1;
            jogadorAtual = Cor.Branca;
            terminada = false;
            vulneravelEnPassant = null;
            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();
            gerarPecas();
        }

        public Peca executaMovimento (Posicao origem, Posicao destino) {
            Peca peca = tabuleiro.retirarPeca(origem);
            peca.incrementarQtdeMovimentos();
            Peca pecaCapturada = tabuleiro.retirarPeca(destino);
            tabuleiro.colocarPeca(peca, destino);
            if(pecaCapturada != null) {
                capturadas.Add(pecaCapturada);
            }

            //#Jogada Especial Roque Pequeno
            if(peca is Rei && destino.coluna == origem.coluna + 2) {
                Posicao origemTorre = new Posicao(origem.linha, origem.coluna + 3);
                Posicao destinoTorre = new Posicao(origem.linha, origem.coluna + 1);
                Peca torre = tabuleiro.retirarPeca(origemTorre);
                torre.incrementarQtdeMovimentos();
                tabuleiro.colocarPeca(torre, destinoTorre);
            }
            //#Jogada Especial Roque Grade
            if (peca is Rei && destino.coluna == origem.coluna - 2) {
                Posicao origemTorre = new Posicao(origem.linha, origem.coluna - 4);
                Posicao destinoTorre = new Posicao(origem.linha, origem.coluna - 1);
                Peca torre = tabuleiro.retirarPeca(origemTorre);
                torre.incrementarQtdeMovimentos();
                tabuleiro.colocarPeca(torre, destinoTorre);
            }
            //#Jogada Especial En Passant
            if(peca is Peao) {
                if(origem.coluna != destino.coluna && pecaCapturada == null) {
                    Posicao posPeaoCapturado;
                    if(peca.cor == Cor.Branca) {
                        posPeaoCapturado = new Posicao(destino.linha + 1, destino.coluna);
                    }
                    else {
                        posPeaoCapturado = new Posicao(destino.linha - 1, destino.coluna);
                    }
                    pecaCapturada = tabuleiro.retirarPeca(posPeaoCapturado);
                    capturadas.Add(pecaCapturada);
                }
            }
            return pecaCapturada;
        }

        public void desfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada) {
            Peca peca = tabuleiro.retirarPeca(destino);
            peca.decrementarQtdeMovimentos();
            if(pecaCapturada != null) {
                tabuleiro.colocarPeca(pecaCapturada, destino);
                capturadas.Remove(pecaCapturada);
            }
            tabuleiro.colocarPeca(peca, origem);

            //#Jogada Especial Roque Pequeno
            if (peca is Rei && destino.coluna == origem.coluna + 2) {
                Posicao origemTorre = new Posicao(origem.linha, origem.coluna + 3);
                Posicao destinoTorre = new Posicao(origem.linha, origem.coluna + 1);
                Peca torre = tabuleiro.retirarPeca(destinoTorre);
                torre.decrementarQtdeMovimentos();
                tabuleiro.colocarPeca(torre, origemTorre);
            }
            //#Jogada Especial Roque Grade
            if (peca is Rei && destino.coluna == origem.coluna - 2) {
                Posicao origemTorre = new Posicao(origem.linha, origem.coluna - 4);
                Posicao destinoTorre = new Posicao(origem.linha, origem.coluna - 1);
                Peca torre = tabuleiro.retirarPeca(destinoTorre);
                torre.decrementarQtdeMovimentos();
                tabuleiro.colocarPeca(torre, origemTorre);
            }
            //#Jogada Especial En Passant
            if (peca is Peao) {
                if (origem.coluna != destino.coluna && pecaCapturada == vulneravelEnPassant) {
                    Peca PeaoCapturado = tabuleiro.retirarPeca(destino);
                    Posicao posPeaoCapturado;
                    if (peca.cor == Cor.Branca) {
                        posPeaoCapturado = new Posicao(3, destino.coluna);
                    }
                    else {
                        posPeaoCapturado = new Posicao(4, destino.coluna);
                    }
                    pecaCapturada = tabuleiro.retirarPeca(posPeaoCapturado);
                    capturadas.Remove(pecaCapturada);
                }
            }

        }

        public void realizaJogada (Posicao origem, Posicao destino) {
            Peca pecaCapturada = executaMovimento(origem, destino);
            if (estaEmXeque(jogadorAtual)) {
                desfazMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroException("Você não pode se colocar em xeque!");
            }

            Peca peca = tabuleiro.peca(destino);

            //Jogada especial promoção
            if (peca is Peao) {
                if ((peca.cor == Cor.Branca && destino.linha == 0) || (peca.cor == Cor.Preta && destino.linha == 7)) {
                    peca = tabuleiro.retirarPeca(destino);
                    pecas.Remove(peca);
                    Peca dama = new Rainha(tabuleiro, peca.cor);
                    tabuleiro.colocarPeca(dama, destino);
                    pecas.Add(dama);
                }
            }
            if (estaEmXeque(adversaria(jogadorAtual))) {
                xeque = true;
            }
            else {
                xeque = false;
            }

            if (testeXequemate(adversaria(jogadorAtual))) {
                terminada = true;
            }
            else {
                turno++;
                mudaJogador();
            }

            
            // # Jogada especial en passant

            if (peca is Peao && (destino.linha == origem.linha - 2 || destino.linha == origem.linha + 2)) {
                vulneravelEnPassant = peca;
            }
            else {
                vulneravelEnPassant = null;
            }
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

        public void colocarNovaPeca (int linha, char coluna, Peca peca) {
            tabuleiro.colocarPeca(peca, new PosicaoXadrez(linha, coluna).toPosicao());
            pecas.Add(peca);
        }

        private void gerarPecas() {
            colocarNovaPeca(8, 'a', new Torre(tabuleiro, Cor.Preta));
            colocarNovaPeca(8, 'h', new Torre(tabuleiro, Cor.Preta));
            colocarNovaPeca(8, 'b', new Cavalo(tabuleiro, Cor.Preta));
            colocarNovaPeca(8, 'g', new Cavalo(tabuleiro, Cor.Preta));
            colocarNovaPeca(8, 'c', new Bispo(tabuleiro, Cor.Preta));
            colocarNovaPeca(8, 'f', new Bispo(tabuleiro, Cor.Preta));
            colocarNovaPeca(8, 'd', new Rainha(tabuleiro, Cor.Preta));
            colocarNovaPeca(8, 'e', new Rei(tabuleiro, Cor.Preta, this));

            for (int i = 0; i < 8; i++) {
                colocarNovaPeca(7, Convert.ToChar('a' + i), new Peao(tabuleiro, Cor.Preta, this));
            }

            colocarNovaPeca(1, 'a', new Torre(tabuleiro, Cor.Branca));
            colocarNovaPeca(1, 'h', new Torre(tabuleiro, Cor.Branca));
            colocarNovaPeca(1, 'b', new Cavalo(tabuleiro, Cor.Branca));
            colocarNovaPeca(1, 'g', new Cavalo(tabuleiro, Cor.Branca));
            colocarNovaPeca(1, 'c', new Bispo(tabuleiro, Cor.Branca));
            colocarNovaPeca(1, 'f', new Bispo(tabuleiro, Cor.Branca));
            colocarNovaPeca(1, 'd', new Rainha(tabuleiro, Cor.Branca));
            colocarNovaPeca(1, 'e', new Rei(tabuleiro, Cor.Branca, this));

            for (int i = 0; i < 8; i++) {
                colocarNovaPeca(2, Convert.ToChar('a' + i), new Peao(tabuleiro, Cor.Branca, this));
            }
        }

        public HashSet<Peca> pecasCapturadas(Cor cor) {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach(Peca x in capturadas) {
                if (x.cor == cor) {
                    aux.Add(x);
                }
            }
            return aux;
        }

        public HashSet<Peca> pecasEmJogo(Cor cor) {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in pecas) {
                if (x.cor == cor) {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(pecasCapturadas(cor));
            return aux;
        }

        private Cor adversaria(Cor cor) {
            if(cor == Cor.Branca) {
                return Cor.Preta;
            }
            else {
                return Cor.Branca;
            }
        }

        private Peca rei(Cor cor) {
            foreach(Peca x in pecasEmJogo(cor)) {
                if (x is Rei) {
                    return x;
                }
            }
            return null;
        }

        public bool estaEmXeque(Cor cor) {
            Peca R = rei(cor);
            if(R == null) {
                throw new TabuleiroException("Não existe rei da cor " + cor + " no tabuleiro!");
            }

            foreach (Peca x in pecasEmJogo(adversaria(cor))) {
                bool[,] mat = x.movimentosPossiveis();
                if (mat[R.posicao.linha, R.posicao.coluna]) {
                    return true;
                }
            }
            return false;
        }

        public bool testeXequemate(Cor cor) {
            if (!estaEmXeque(cor)) {
                return false;
            }
            foreach (Peca x in pecasEmJogo(cor)) {
                bool[,] mat = x.movimentosPossiveis();
                for (int i = 0; i < tabuleiro.linha; i++) {
                    for (int j = 0; j < tabuleiro.coluna; j++) {
                        if (mat[i, j]) {
                            Posicao origem = x.posicao;
                            Posicao destino = new Posicao(i, j);
                            Peca pecaCapturada = executaMovimento(origem, destino);
                            bool testeXeque = estaEmXeque(cor);
                            desfazMovimento(origem, destino, pecaCapturada);
                            if (!testeXeque) {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

    }
}
