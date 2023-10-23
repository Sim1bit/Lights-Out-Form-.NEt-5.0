using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lights_Out
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private bool Crea = true;
        private LightsOut LightsOutGame = new LightsOut();

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //anche se true alla seconda volta che si ripete esce sempre (se la griglia già esiste, altrimenti alla prima) 
            while (true) 
            {
                //Se la griglia non esiste allora la crea, altri menti la distrugge e la ricrea
                if (Crea) 
                {
                    //Per evitare di mettere caratteri
                    try
                    {
                        //Per assicurarsi che si rispettino le dimensioni ed evitare di fare griglie troppo grandi
                        if (Convert.ToInt32(toolStripTextBox1.Text) > 15 || Convert.ToInt32(toolStripTextBox1.Text) < 1 || Convert.ToInt32(toolStripTextBox2.Text) > 15 || Convert.ToInt32(toolStripTextBox2.Text) < 1)
                        {
                            MessageBox.Show("Dimensioni Errate devono essere da 1 a 15");
                            toolStripTextBox1.Text = "";
                            toolStripTextBox2.Text = "";
                        }
                        else
                        {
                            //Se le dimensioni sono accettabili crea uno user control dalle dimensioni indicate dall'utente tramite le textbox del menù strip

                            LightsOutGame = new LightsOut();
                            LightsOutGame.Size = new Size(Convert.ToInt32(toolStripTextBox1.Text) * 50, Convert.ToInt32(toolStripTextBox2.Text) * 50);
                            LightsOutGame.Location = new Point(12, 52);

                            this.Controls.Add(LightsOutGame);

                            Crea = false;
                        }
                    }
                    catch (FormatException Err)
                    {
                        MessageBox.Show(Err.Message);
                        toolStripTextBox1.Text = "";
                        toolStripTextBox2.Text = "";
                    }
                    return;
                }
                else
                {
                    this.Controls.Remove(LightsOutGame);
                    Crea = true;
                }
            }
        }

        private void endToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Distrugge la griglia se si preme il bottene end del menù strip
            if (!Crea)
            {
                this.Controls.Remove(LightsOutGame);
                Crea = true;
            }
        }
    }
}
