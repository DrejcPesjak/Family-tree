using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace DruzinskoDrevoWinForm
{
    public partial class Form1 : Form
    {
        public Oseba[] druzina = new Oseba[100];    //omejitev na combobox-u  
        private string filename;
        //Osebek risalniObjekt = new Osebek();

        public Form1()
        {            
            InitializeComponent();
            this.splitContainer1.Panel1.AutoScroll = true;
            //risalniObjekt.Click += new EventHandler(this.osebek_onClick);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;   //ob spremembi velikosti forme, panel2 ne spremeni velikosti
            this.splitContainer1.Panel2MinSize = 280;                                   //minimalna velikost drugega panela, v pxl
        }

        private void btnProfilnaSlika_Click(object sender, EventArgs e)     //spremeni profilno sliko osebe
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files|*.BMP;*.GIF;*.JPG;*.JPEG;*.PNG";
            DialogResult dr = ofd.ShowDialog();

            if (dr == DialogResult.OK)
            {
                filename = ofd.FileName;
                pictureBoxProfilna.Image = Image.FromFile(filename);
            }
        }

        private void btnDodajOsebo_Click(object sender, EventArgs e)
        {
            int stevilkaOsebe; int visinaTry;
            string destFilename = "";
            

            if(lblIDosebe.Text=="empty")        //nova oseba
            {
                
                if(pictureBoxProfilna.Image != null)        //shrani navedeno sliko (zbrano s strani uporabnika) v podmapo te aplikacije
                {
                    if (Directory.Exists(Path.GetDirectoryName(Application.ExecutablePath) + "\\Slike") == false)
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(Application.ExecutablePath) + "\\Slike");
                    } 
                    //pot do aplikacije + relativna pot do podmape + "Oseba" + zaporedna številka osebe + končnica slike
                    destFilename = Path.GetDirectoryName(Application.ExecutablePath) + "\\Slike\\" + "Oseba" + Array.IndexOf(druzina, null)  + Path.GetExtension(filename);
                    System.IO.File.Copy(filename, destFilename, true);
                }


                if(checkBoxSpol.Checked)
                {
                    if (dateTimePickerSmrt.Checked)     //pokojni
                    {                        
                        Moski a = new Moski(txtBoxIme.Text, txtBoxPriimek.Text, txtBoxVzdevek.Text, txtBoxHisnoIme.Text, dateTimePickerRojstvo.Value, dateTimePickerSmrt.Value,
                                            txtBoxKrajRoj.Text, txtBoxKrajSmrti.Text, int.TryParse(txtBoxVisina.Text, out visinaTry) ? visinaTry : visinaTry, txtBoxEmail.Text, txtBoxTelefon.Text, destFilename != "" ? new Bitmap(destFilename) : null);

                        druzina[Array.IndexOf(druzina, null)] = a;
                    }
                    else        //še živi
                    {
                        Moski a = new Moski(txtBoxIme.Text, txtBoxPriimek.Text, txtBoxVzdevek.Text, txtBoxHisnoIme.Text, dateTimePickerRojstvo.Value, txtBoxKrajRoj.Text,
                                            int.TryParse(txtBoxVisina.Text, out visinaTry) ? visinaTry : visinaTry, txtBoxEmail.Text, txtBoxTelefon.Text, destFilename != "" ? new Bitmap(destFilename) : null);

                        druzina[Array.IndexOf(druzina, null)] = a;
                    }
                }
                else
                {
                    if(dateTimePickerSmrt.Checked)  //preminula
                    {
                        Zenska a = new Zenska(txtBoxIme.Text, txtBoxPriimek.Text, txtBoxVzdevek.Text, txtBoxDekPriimek.Text, dateTimePickerRojstvo.Value, dateTimePickerSmrt.Value,
                                            txtBoxKrajRoj.Text, txtBoxKrajSmrti.Text, int.TryParse(txtBoxVisina.Text, out visinaTry) ? visinaTry : visinaTry, txtBoxEmail.Text, txtBoxTelefon.Text, destFilename != "" ? new Bitmap(destFilename) : null);

                        druzina[Array.IndexOf(druzina, null)] = a;
                    }
                    else        //še živeča
                    {
                        Zenska a = new Zenska(txtBoxIme.Text, txtBoxPriimek.Text, txtBoxVzdevek.Text, txtBoxDekPriimek.Text, dateTimePickerRojstvo.Value, txtBoxKrajRoj.Text,
                                                int.TryParse(txtBoxVisina.Text, out visinaTry) ? visinaTry : visinaTry, txtBoxEmail.Text, txtBoxTelefon.Text, destFilename != "" ? new Bitmap(destFilename) : null);

                        druzina[Array.IndexOf(druzina, null)] = a;
                    }
                }

                if (comboBoxSoprog.SelectedIndex > -1)
                {
                    druzina[Array.IndexOf(druzina, null)-1].Soprog = druzina[comboBoxSoprog.SelectedIndex];
                    druzina[comboBoxSoprog.SelectedIndex].Soprog = druzina[Array.IndexOf(druzina, null)-1];
                }
                if (comboBoxOce.SelectedIndex > -1) druzina[Array.IndexOf(druzina, null) - 1].Oce = druzina[comboBoxOce.SelectedIndex];
                if (comboBoxMati.SelectedIndex > -1) druzina[Array.IndexOf(druzina, null) - 1].Mati = druzina[comboBoxMati.SelectedIndex];

            }
            else if(Int32.TryParse(lblIDosebe.Text, out stevilkaOsebe))     //že obstoječa oseba
            {
                if (pictureBoxProfilna.Image != null)        //shrani navedeno sliko (zbrano s strani uporabnika) v podmapo te aplikacije
                {
                    if (Directory.Exists(Path.GetDirectoryName(Application.ExecutablePath) + "\\Slike") == false)
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(Application.ExecutablePath) + "\\Slike");
                    }
                    //pot do aplikacije + relativna pot do podmape + "Oseba" + zaporedna številka osebe + končnica slike
                    destFilename = Path.GetDirectoryName(Application.ExecutablePath) + "\\Slike\\" + "Oseba" + stevilkaOsebe + Path.GetExtension(filename);
                    try { System.IO.File.Copy(filename, destFilename, true); }
                    catch { }
                }

                druzina[stevilkaOsebe].Ime = txtBoxIme.Text;
                druzina[stevilkaOsebe].Priimek = txtBoxPriimek.Text;
                druzina[stevilkaOsebe].Vzdevek = txtBoxVzdevek.Text;
                druzina[stevilkaOsebe].Datum_rojstva = dateTimePickerRojstvo.Value;
                druzina[stevilkaOsebe].Kraj_rojstva = txtBoxKrajRoj.Text;

                if (dateTimePickerSmrt.Checked)
                {
                    druzina[stevilkaOsebe].Datum_smrti = dateTimePickerSmrt.Value;
                    druzina[stevilkaOsebe].Kraj_smrti = txtBoxKrajSmrti.Text;
                }

                druzina[stevilkaOsebe].Visina = int.TryParse(txtBoxVisina.Text, out visinaTry) ? visinaTry : visinaTry;
                druzina[stevilkaOsebe].Email = txtBoxEmail.Text;
                druzina[stevilkaOsebe].Telefon = txtBoxTelefon.Text;
                druzina[stevilkaOsebe].ProfilnaSlika = destFilename != "" ? new Bitmap(destFilename) : null;

                if (checkBoxSpol.Checked) ((Moski)druzina[stevilkaOsebe]).Hisno_ime = txtBoxHisnoIme.Text;
                else ((Zenska)druzina[stevilkaOsebe]).Dekliski_priimek = txtBoxDekPriimek.Text;


                if (comboBoxSoprog.SelectedIndex > 1)
                {
                    druzina[stevilkaOsebe].Soprog = druzina[comboBoxSoprog.SelectedIndex];
                    druzina[comboBoxSoprog.SelectedIndex].Soprog = druzina[stevilkaOsebe];
                }
                if (comboBoxOce.SelectedIndex > 1) druzina[stevilkaOsebe].Oce = druzina[comboBoxOce.SelectedIndex];
                if (comboBoxMati.SelectedIndex > 1) druzina[stevilkaOsebe].Mati = druzina[comboBoxMati.SelectedIndex];
            }
            else
            {
                //napaka
                MessageBox.Show("Osebe niste dodali / spremembe niso bile shranjene!");
            }

            lblIDosebe.Text = "empty";
            checkBoxSpol.Checked = true;
            txtBoxDekPriimek.Text = txtBoxEmail.Text = txtBoxHisnoIme.Text = txtBoxIme.Text = txtBoxKrajRoj.Text = txtBoxKrajSmrti.Text = txtBoxTelefon.Text = txtBoxVisina.Text = txtBoxVzdevek.Text = txtBoxPriimek.Text = "";
            pictureBoxProfilna.Image = null;
            napolni_DropDown();
            
            splitContainer1_Panel1_Paint();
        }

        private void checkBoxSpol_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBoxSpol.Checked)
            {
                //moški
                lblHisnoIme.Visible = txtBoxHisnoIme.Visible = txtBoxHisnoIme.Enabled = true;
                lblDekPriimek.Visible = txtBoxDekPriimek.Visible = txtBoxDekPriimek.Enabled = false;
            }
            else
            {
                //ženska
                lblHisnoIme.Visible = txtBoxHisnoIme.Visible = txtBoxHisnoIme.Enabled = false;
                lblDekPriimek.Visible = txtBoxDekPriimek.Visible = txtBoxDekPriimek.Enabled = true;
            }
        }       
 
        private void napolni_DropDown()
        {
            comboBoxSoprog.Items.Clear();
            comboBoxOce.Items.Clear();
            comboBoxMati.Items.Clear();


            foreach(Oseba oseba in druzina)
            {
                if (oseba != null)
                {
                    comboBoxSoprog.Items.Add(oseba.Ime + " " + oseba.Priimek);
                    comboBoxOce.Items.Add(oseba.Ime + " " + oseba.Priimek);
                    comboBoxMati.Items.Add(oseba.Ime + " " + oseba.Priimek);
                }
            }
        }

        private void izbrisi_lable()
        {            
            this.splitContainer1.Panel1.Controls.Clear();
        }

        private void splitContainer1_Panel1_Paint()  //izris družinskega drevesa
        {
            izbrisi_lable();

            if (druzina[0] != null)
            {
                Oseba najstarejsi = druzina[0];     
                foreach (Oseba oseba in druzina)     //iskanje najstarejšega para
                {
                    if (oseba != null)
                    {
                        if (oseba.GetType() == typeof(Moski))
                        {
                            if (oseba.Oce == null && oseba.Mati == null)
                            {
                                if (oseba.Soprog != null)
                                {
                                    if (oseba.Soprog.Oce == null && oseba.Soprog.Mati == null)
                                    {
                                        najstarejsi = oseba;
                                        narisi_Vrstico(new List<Oseba>() { najstarejsi }, new List<int>() { 0 }, 132 / 2 + 15);
                                    }
                                }
                            }
                        }
                    }
                }

                List<Oseba> seznamStari = new List<Oseba>() { najstarejsi };    //trenutna vrstica
                List<Oseba> seznamNovi = new List<Oseba>();                     //naslednja vrstica - otroci prejšnje
                List<int> otrociVDruzini = new List<int>();                     //razdelitev otrok po družinah v vrstici
                int stetjeY = 1;                                                //vertikala generacije (vrstice)

                while (seznamStari.Count > 0)
                {
                    foreach (Oseba oseba in seznamStari)
                    {
                        if (oseba.seznamOtrok(druzina).Count > 0)       //verjetno popravit - stric nima otrok v otrociVDruzini.Add(0)
                        {
                            seznamNovi.AddRange(oseba.seznamOtrok(druzina));
                            otrociVDruzini.Add(oseba.seznamOtrok(druzina).Count);
                        }                        
                    }

                    int ySredina = (2 * 132) * stetjeY + 132 / 2 + 15;
                    if(otrociVDruzini.Count == 0) break;        //izhod ko ni več otrok
                    narisi_Vrstico(seznamNovi, otrociVDruzini, ySredina);
                    seznamStari.Clear();
                    seznamStari.AddRange(seznamNovi);    
                    seznamNovi.Clear();
                    otrociVDruzini.Clear();
                    stetjeY++;
                }
            }
        }

        private void narisi_Vrstico(List<Oseba> seznamVrstice, List<int> otrociVDruzini, int ySredina)
        {
            Graphics graficniPanel1 = Graphics.FromHwnd(this.splitContainer1.Panel1.Handle);
            int stOseb = seznamVrstice.Count;
            for(int i = 0; i < stOseb; i++)     //nariše osebe
            {
                if(stOseb % 2 == 0 && stOseb > 0)   //sodo število oseb v vrstici
                {
                    int xSredina = this.splitContainer1.Panel1.Width / 2 - 25 - (230 * stOseb / 2) - (50 * (stOseb / 2 - 1)) + 46 + (280 * i);                    
                    narisi_Osebo(xSredina, ySredina, seznamVrstice[i]);

                    narisi_Crto(xSredina, ySredina - 132, xSredina, ySredina - 66);     //črta iz posameznika

                    if(seznamVrstice[i].Soprog != null)
                    {
                        int xSredinaSoproga = xSredina + (3 * 46);
                        narisi_Osebo(xSredinaSoproga, ySredina, seznamVrstice[i].Soprog);
                        narisi_Crto(xSredina + 46, ySredina, xSredinaSoproga - 46, ySredina);    //poročna črta

                        if(seznamVrstice[i].seznamOtrok(druzina).Count > 0) //če imata otroke
                        {
                            narisi_Crto(xSredina + 46 + 23, ySredina, xSredinaSoproga - 46 - 23, ySredina + 132);                            
                        }
                    }
                }

                else if (stOseb > 0)       //liho število oseb v vrstici
                {
                    int xSredina = this.splitContainer1.Panel1.Width / 2 - 23 - 92 - (280 + 50) * (stOseb / 2) + 46 + (280 * i);
                    narisi_Osebo(xSredina, ySredina, seznamVrstice[i]);
                    
                    //najstarejši - otrociVDruzini = 0
                    if(otrociVDruzini[0] != 0) narisi_Crto(xSredina, ySredina - 132, xSredina, ySredina - 66);      //črta iz posameznika
                    
                    if (seznamVrstice[i].Soprog != null)
                    {
                        int xSredinaSoproga = xSredina + (3 * 46);
                        narisi_Osebo(xSredinaSoproga, ySredina, seznamVrstice[i].Soprog);
                        narisi_Crto(xSredina + 46, ySredina, xSredinaSoproga - 46, ySredina);          //poročna črta

                        if (seznamVrstice[i].seznamOtrok(druzina).Count > 0) //če imata otroke
                        {
                            narisi_Crto(xSredina + 46 + 23, ySredina, xSredinaSoproga - 46 - 23, ySredina + 132);                            
                        }
                    }
                }
            }

            int zeIzrisanih = 0, xZacetekCrte = 0, xKonecCrte;
            foreach(int stOtrok in otrociVDruzini)             //risanje sorodstvene črte (bratje in sestre)
            {
                if (stOtrok > 0 && stOseb > 0)
                {
                    if (stOseb % 2 == 0)   //sodo število oseb v vrstici
                    {
                        xZacetekCrte = this.splitContainer1.Panel1.Width / 2 - 25 - (230 * stOseb / 2) - (50 * (stOseb / 2 - 1)) + 46 + (280 * zeIzrisanih);
                    }
                    else                   //liho število oseb v vrstici
                    {
                        xZacetekCrte = this.splitContainer1.Panel1.Width / 2 - 23 - 92 - (280 + 50) * (stOseb / 2) + 46 + (280 * zeIzrisanih);
                    }
                }

                xKonecCrte = xZacetekCrte + 280 * (stOtrok-1);
                if (stOtrok == 1) xKonecCrte += 69;
                narisi_Crto(xZacetekCrte, ySredina - 132, xKonecCrte, ySredina - 132);                
                zeIzrisanih += stOtrok;
            }
        }

        private void narisi_Osebo(int xSredina, int ySredina, Oseba oseba)
        {
            if (xSredina - 46 < this.splitContainer1.Panel1.AutoScrollPosition.X)
            {
                foreach (Control c in this.splitContainer1.Panel1.Controls)
                {
                    c.Left += -(xSredina - 46);
                }
            }
            
            Osebek risalniObjekt = new Osebek();
            risalniObjekt.Parent = splitContainer1.Panel1;
            risalniObjekt.Name = "Oseba" + Array.IndexOf(druzina, oseba);
            risalniObjekt.Location = new Point(xSredina - 46, ySredina - 66);
            risalniObjekt.NovaOseba = oseba;
            risalniObjekt.Click += new EventHandler(this.osebek_onClick);            

            this.splitContainer1.Panel1.Controls.Add(risalniObjekt);
            risalniObjekt.Invalidate();
            this.splitContainer1.Panel1.Invalidate();            
        }

        private void narisi_Crto(int xZacetek, int yZacetek, int xKonec, int yKonec)
        {
            Crtica povezovalnaCrta = new Crtica(xZacetek, yZacetek, xKonec, yKonec);
            povezovalnaCrta.Location = new Point(xZacetek, yZacetek);
            povezovalnaCrta.Size = new System.Drawing.Size(xKonec - xZacetek+2, yKonec - yZacetek+2);
            povezovalnaCrta.Parent = splitContainer1.Panel1;

            this.splitContainer1.Panel1.Controls.Add(povezovalnaCrta);
            povezovalnaCrta.Invalidate();
            this.splitContainer1.Panel1.Invalidate();
        }        

        private void osebek_onClick(Object sender, EventArgs e)
        {
            int index = int.Parse(((Osebek)sender).Name.Replace("Oseba", ""));//vzameš stran "Oseba" in potem maš index v druzini
            lblIDosebe.Text = index.ToString();//Array.IndexOf(druzina, oseba).ToString();

            txtBoxIme.Text = druzina[index].Ime;
            txtBoxPriimek.Text = druzina[index].Priimek;
            txtBoxVzdevek.Text = druzina[index].Vzdevek;
            dateTimePickerRojstvo.Value = druzina[index].Datum_rojstva;
            txtBoxKrajRoj.Text = druzina[index].Kraj_rojstva;

            if (druzina[index].Datum_smrti != default(DateTime))
            {
                dateTimePickerSmrt.Value = druzina[index].Datum_smrti;
                txtBoxKrajSmrti.Text = druzina[index].Kraj_smrti;
                dateTimePickerSmrt.Checked = true;
            }

            txtBoxVisina.Text = druzina[index].Visina.ToString();
            txtBoxEmail.Text = druzina[index].Email;
            txtBoxTelefon.Text = druzina[index].Telefon;

            if (druzina[index] is Moski)
            {
                txtBoxHisnoIme.Text = ((Moski)druzina[index]).Hisno_ime;
                checkBoxSpol.Checked = true;
            }
            else
            {
                txtBoxDekPriimek.Text = ((Zenska)druzina[index]).Dekliski_priimek;
                checkBoxSpol.Checked = false;
            }

            if (druzina[index].Soprog != null) comboBoxSoprog.SelectedIndex = Array.IndexOf(druzina, druzina[index].Soprog);
            if (druzina[index].Oce != null) comboBoxOce.SelectedIndex = Array.IndexOf(druzina, druzina[index].Oce);
            if (druzina[index].Mati != null) comboBoxMati.SelectedIndex = Array.IndexOf(druzina, druzina[index].Mati);

            if (druzina[index].ProfilnaSlika != null) pictureBoxProfilna.Image = druzina[index].ProfilnaSlika;
        }
    }


//***********nov control za vsako osebo************************************************
    public class Osebek : Control
    {
        private Oseba novaOseba;

        public Oseba NovaOseba
        {
            get { return novaOseba; }
            set { novaOseba = value; }
        }

        public Osebek()
        {
            this.Width = 92;
            this.Height = 132;
        }

        /*protected override void OnClick(EventArgs e)
        {            
            
            //this.Parent.Parent.GetChildAtPoint(new Point(right))
            //base.OnClick(e);
        }*/

        protected override void OnPaint(PaintEventArgs e)
        {
            if(novaOseba.ProfilnaSlika == null)     //če ni slike nariše prazen kvadrat
                e.Graphics.DrawRectangle(new Pen(Color.Black, 3), new Rectangle(new Point(0, 0), new Size(91, 117)));    
            else
                e.Graphics.DrawImage(novaOseba.ProfilnaSlika, new RectangleF(0, 0, 92, 119));     
      
            e.Graphics.DrawString(novaOseba.Ime + " " + novaOseba.Priimek, 
                                  new System.Drawing.Font("Microsoft Sans Serif", 8), 
                                  new System.Drawing.SolidBrush(System.Drawing.Color.Black), 
                                  new Point(0, 120));
             
            base.OnPaint(e);        
        }
    }

    public class Crtica : Control
    {
        int xZacetek, yZacetek, xKonec, yKonec;

        public Crtica(int xZacetek, int yZacetek, int xKonec, int yKonec)
        {
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.BackColor = Color.Transparent;
            this.xZacetek = xZacetek; 
            this.yZacetek = yZacetek;
            this.xKonec = xKonec;
            this.yKonec = yKonec;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.DrawLine(new Pen(Color.Black, 1), new Point(0, 0), new Point(xKonec-xZacetek, yKonec-yZacetek));
            base.OnPaint(e);
        }
    }
}
