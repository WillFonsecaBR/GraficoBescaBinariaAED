/**
 * AED - TRABALHO PRATICO 2 - PESQUISA BINÁRIA
 * PROFESSOR - ÁLISSON
 * INTEGRANTES: BERNARDO, GUILHERME BARBOSA, LARYSSA FERNANDA, PEDRO OCT´AVIO, WELLINGTON, WILLIAN ALVES.
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace SegundoTrabalho___AED
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            grafico.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;// Modelo os pontos da curva do grafico.
        }
        public bool BuscaBI(int num, int[] V) //Busca Binária
        {
            int inicio = 0, fim = V.Length - 1, meio = (inicio + fim) / 2;

            while (inicio <= fim && V[meio] != num)
            {
                if (num > V[meio])
                {
                    inicio = meio + 1;
                }
                else
                {
                    fim = meio - 1;
                }
                meio = (inicio + fim) / 2;
            }

            if (V[meio] == num)
                return true;
            else
                return false;
        }

        public void QuickSortRecursivo(int[] vetor, int primeiro, int ultimo) //Método pra ordenar o vetor
        {

            int baixo, alto, meio, pivo, repositorio;
            baixo = primeiro;
            alto = ultimo;
            meio = (int)((baixo + alto) / 2);

            pivo = vetor[meio];

            while (baixo <= alto)
            {
                while (vetor[baixo] < pivo)
                    baixo++;
                while (vetor[alto] > pivo)
                    alto--;
                if (baixo < alto)
                {
                    repositorio = vetor[baixo];
                    vetor[baixo++] = vetor[alto];
                    vetor[alto--] = repositorio;
                }
                else
                {
                    if (baixo == alto)
                    {
                        baixo++;
                        alto--;
                    }
                }
            }

            if (alto > primeiro)
                QuickSortRecursivo(vetor, primeiro, alto);
            if (baixo < ultimo)
                QuickSortRecursivo(vetor, baixo, ultimo);
        }

        public void preencher(int[] V) //Preencher o vetor
        {
            Random r = new Random();

            for (int i = 0; i < V.Length; i++)
            {
                V[i] = r.Next(10000, 100000);
            }
        }
        public void Executar() // Inicia o procedimento de preenchimento, ordenação busca e contagem de tempo.
        {
            Stopwatch sw = new Stopwatch(); //Salva o tempo de execução
            TimeSpan tempo; //Variável para receber o tempo de execução
                            

            int[] x; // Vetor onde sera adicionado os elementos.

            bool aux3; // Se o elemento esta ou não no vetor 

            string aux4; // Variavel que guardará o resultado da condição feita para saber se o elemento esta no vetor.

            // Gera um elemento aleatorio para pesquisa no vetor.
            Random r = new Random();
            int aux = r.Next(10000, 100000);

            double aux2;

            for (int i = 10000; i <= 100000; i = i + 1000) //Preenche e faz a busca de vetores de 10.000 a 100.000 posições
            {
                x = new int[i];
                preencher(x);

                QuickSortRecursivo(x, 0, x.Length - 1);// Ordena o vetor antes da pesquisa.

                sw.Start(); //Começa a contar o tempo
                aux3 = BuscaBI(aux, x); // executa a busca
                sw.Stop(); //Para de contar o tempo

                tempo = sw.Elapsed;//Recebe o tempo de execução

                aux2 = Convert.ToDouble(tempo.TotalSeconds); // converte o tempo

                // Condição para saber se o elemento esta ou não no vetor.
                if (aux3 == true)
                {
                    aux4 = "Sim";
                }
                else
                {
                    aux4 = "Não";
                }

                //Exibe as informações de cada etapa do processo no list box.
                listarProcesso.Items.Add("=============================");
                listarProcesso.Items.Add("Quantidade elementos : " + i);
                listarProcesso.Items.Add("Elemento Procurado: " + aux);
                listarProcesso.Items.Add("Elemento encontrado? " + aux4);
                listarProcesso.Items.Add("Tempo decorrido: " + aux2);

                //insere os elementos nos eixos do grafico.
                grafico.Series[0].Points.AddXY(i, aux2);
                grafico.Update(); // atualiza os graficos
            }
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            btnPesquisar.Visible = false; // Faz o botão ficar invisivel.
            btnFinalizar.Visible = false;
            Executar(); // Executa a função de inicialização do sistema. 
            btnFinalizar.Visible = true;
        }

        private void btnFinalizar_Click(object sender, EventArgs e)
        {
            Close(); // Fecha o programa
        }
    }
}
