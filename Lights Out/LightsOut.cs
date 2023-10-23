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
    public partial class LightsOut : UserControl
    {
        //Lista lineare che è la griglia
        private List<Button> Griglia = new List<Button>();
        public LightsOut()
        {
            InitializeComponent();
        }

        private void LightsOut_Load(object sender, EventArgs e)
        {
            //All'avvio dello user control la griglia va creata
            CreaGriglia();
            //Dopo la creazione della griglia vano fatte le mosse
            MosseRandom();
        }

        //Crea la griglia
        private void CreaGriglia()
        {
            //In base alle dimensioni dello user control il programma sa di quanto fare la griglia
            for (int i = 0; i < this.Height / 50; i++)
            {
                for (int j = 0; j < this.Width / 50; j++)
                {
                    //Aggiunge il bottone alla lista dell bottone
                    Griglia.Add(new Button());

                    //Inizializza l'ultimo bottone aggiunto
                    Griglia[Griglia.Count - 1].Size = new Size(50, 50);
                    Griglia[Griglia.Count - 1].Location = new Point(j * 50, i * 50);
                    Griglia[Griglia.Count - 1].BackColor = Color.White;
                    //Salvo il numero del bottone nel suo testo
                    Griglia[Griglia.Count - 1].Text = Convert.ToString(Griglia.Count - 1);
                    Griglia[Griglia.Count - 1].ForeColor = Color.White;

                    Griglia[Griglia.Count - 1].Click += Dynamic_Buttom_Click;

                    //Aggiungo il bottone allo user control
                    this.Controls.Add(Griglia[Griglia.Count - 1]);
                }
            }
        }

        //Gestisce il click dei bottoni
        private void Dynamic_Buttom_Click(object sender, EventArgs e)
        {
            //Se si ha già vinto non permetter di fare altre mosse
            if (ControlloVittoria())
                return;
            Button button = (Button)sender;
            AreaInteressata(Convert.ToInt32(button.Text));
            //Controlla se si ha vinto
            if (ControlloVittoria())
                MessageBox.Show("Hai Vinto");
        }

        //Si occupa di far cambiare colore al bottone premuto e a quelli attorno
        private void AreaInteressata(int index)
        {
            InvertiColore(Convert.ToInt32(index));
            if (ControlloOut(Convert.ToInt32(index), false))
                InvertiColore(Convert.ToInt32(index) - 1);
            if (ControlloOut(Convert.ToInt32(index), true))
                InvertiColore(Convert.ToInt32(index) + 1);
            InvertiColore(Convert.ToInt32(index) - (this.Width / 50));
            InvertiColore(Convert.ToInt32(index) + (this.Width / 50));
        }

        private void InvertiColore(int index)
        {
            //Se il bottone è fuori dal range da eccezione e non fa nulla
            try
            {
                //Inverte i colori del testo e del bottone
                if (Griglia[index].BackColor == Color.White)
                {
                    Griglia[index].BackColor = Color.Black;
                    Griglia[index].ForeColor = Color.Black;
                }
                else
                {
                    Griglia[index].BackColor = Color.White;
                    Griglia[index].ForeColor = Color.White;
                }
            }
            catch (ArgumentOutOfRangeException)
            {

            }
        }

        //Controlla che il bottone premuto sia sul lato
        private bool ControlloOut(int i, bool segno)
        {
            /*Visto che è una lista lineare se clicco un pultasante basta che faccio cambiare il colore 
             * del pulsante a sinistra e a destra, ma ciò può portare a far cambiare anche quello della 
             * riga sopra sul bordo destro se il bottone premuto è al bordo di sinistra.*/

            int aux, a, b;
            if (segno)
            {
                aux = -1;
                a = 1;
                b = this.Height / 50;
            }
            else
            {
                aux = 0;
                a = 0;
                b = this.Height / 50 - 1;
            }

            for(int j = a; j <= b; j++)
            {
                /*Se il num del bottone è un membro della tabellina della larghezza dello user control / 50
                 * vuol dire che è sul bordo sinistro, se invece è un membro della tabellina ma -1 allora è 
                 * sul bordo destro(in questi due case non deve cambiare il colore del successivo (se a destra)
                 * o del precedente (se a sinistra))*/

                if (i == (this.Width / 50) * j + aux)
                    return false;
            }

            return true;
        }

        private bool ControlloVittoria()
        {
            //Se tutti bottoni sono neri allora si vince
            for(int i = 0; i < Griglia.Count; i++)
                if (Griglia[i].BackColor == Color.White)
                    return false;
            return true;
        }

        private void MosseRandom()
        {
            Random r = new Random();
            for (int i = 0; i < (this.Height / 50) * (this.Width / 50) - 1; i++)
                //Preme un bottone random
                AreaInteressata(r.Next(0, Griglia.Count));
        }
    }
}
