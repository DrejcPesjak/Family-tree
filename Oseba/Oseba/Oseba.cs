using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Drawing;

public abstract class Oseba
{
    private string ime, priimek, vzdevek;
    private DateTime datum_rojstva, datum_smrti;
    private string kraj_rojstva, kraj_smrti;
    private int visina;     //v centimetrih
    private string email, telefon;      //email: neki@neki.neki     telefon: XXX-XXX-XXX
    private Oseba oce;
    private Oseba mati;    
    public List<BesedilneVrste> besedila = new List<BesedilneVrste>();
    private Bitmap profilnaSlika;
    private Oseba soprog;
        
        
    //lahko pošleš prazen niz/0/null
    public Oseba(string ime, string priimek, string vzdevek, DateTime datum_rojstva, DateTime datum_smrti, string kraj_rojstva, string kraj_smrti, int visina, string email, string telefon, Bitmap slika)
    {
        this.Ime = ime;
        this.Priimek = priimek;
        this.Vzdevek = vzdevek;
        this.Datum_rojstva = datum_rojstva;
        this.Datum_smrti = datum_smrti;
        this.Kraj_rojstva = kraj_rojstva; 
        this.Kraj_smrti = kraj_smrti;
        this.Visina = visina;
        this.Email = email;
        this.Telefon = telefon;
        this.ProfilnaSlika = slika;
    }
    public Oseba(string ime, string priimek, string vzdevek, int danDR, int mesecDR, int letoDR, int danDS, int mesecDS, int letoDS, string kraj_rojstva, string kraj_smrti, int visina, string email, string telefon, Bitmap slika)
    {
        this.Ime = ime;
        this.Priimek = priimek;
        this.Vzdevek = vzdevek;
        this.Datum_rojstva = new DateTime(letoDR,mesecDR,danDR);
        this.Datum_smrti = new DateTime(letoDS,mesecDS,letoDS);
        this.Kraj_rojstva = kraj_rojstva;
        this.Kraj_smrti = kraj_smrti;
        this.Visina = visina;
        this.Email = email;
        this.Telefon = telefon;
        this.ProfilnaSlika = slika;
    }
    public Oseba(string ime, string priimek, string vzdevek, DateTime datum_rojstva, string kraj_rojstva, int visina, string email, string telefon, Bitmap slika)
    {
        this.Ime = ime;
        this.Priimek = priimek;
        this.Vzdevek = vzdevek;
        this.Datum_rojstva = datum_rojstva;
        this.Kraj_rojstva = kraj_rojstva;
        this.Visina = visina;
        this.Email = email;
        this.Telefon = telefon;
        this.ProfilnaSlika = slika;
    }

    public void doddajBesedilo(BesedilneVrste besedilo)
    {
        besedila.Add(besedilo);
    }
    public bool odstraniBesedilo(BesedilneVrste besedilo)
    {
        if (besedila.Contains(besedilo))
        {
            besedila.RemoveAt(besedila.IndexOf(besedilo));
            return true;
        }
        else
            return false;
    }

    public List<Oseba> seznamOtrok(Oseba[] seznamVseh)
    {
        List<Oseba> seznamOtrok = new List<Oseba>();
        foreach(Oseba oseba in seznamVseh)
        {
            if (oseba != null)
            {
                if (oseba.Oce != null)
                    if (oseba.Oce == this)
                        seznamOtrok.Add(oseba);
                if (oseba.Mati != null)
                    if (oseba.Mati == this)
                        seznamOtrok.Add(oseba);
            }
        }

        return seznamOtrok;
    }

    public override string ToString()
    {
        string oseba = this.Ime + this.Priimek + "(" + this.Vzdevek + ")\n Rojen: " +
                        (this.Datum_rojstva).ToShortDateString() + ", " + this.Kraj_rojstva + "\nUmrl:" + (this.Datum_smrti).ToShortDateString() + ", " + this.Kraj_smrti + "\nEmail: " +
                        this.Email + "\nTelefon: " + this.Telefon;
        
        return base.ToString();
    }
    
    //primerjanje višine dveh oseb -- preoblaganje operatorjev
    public static bool operator >(Oseba a, Oseba b)
    {
        if (a.Visina > b.Visina) return true;
        else return false;
    }
    public static bool operator <(Oseba a, Oseba b)
    {
        return b > a;
    }

    //spremeni podatke osebe - večina ob napačnem vpisu
    public string Ime
    {
        get { return ime; }
        set { if(!value.Any(char.IsDigit)) ime = value; }
    }
    public string Priimek
    {
        get { return priimek; }
        set { if (!value.Any(char.IsDigit)) priimek = value; }
    }
    public string Vzdevek
    {
        get { return vzdevek; }
        set { vzdevek = value; }
    }
    public DateTime Datum_rojstva
    {
        get { return datum_rojstva; }
        set { datum_rojstva = value; }
    }
    public DateTime Datum_smrti
    {
        get { return datum_smrti; }
        set { datum_smrti = value; }
    }
    public string Kraj_rojstva
    {
        get { return kraj_rojstva; }
        set { kraj_rojstva = value; }
    }
    public string Kraj_smrti
    {
        get { return kraj_smrti; }
        set { kraj_smrti = value; }
    }    
    public int Visina
    {
        get { return visina; }
        set { if(value>20 && value<300) visina = value; }
    }
    public string Telefon
    {
        /*https://msdn.microsoft.com/en-us/library/e7f5w83z(v=vs.110).aspx   --Regex.Replace(string input,string pattern,string replacement)*/

        get { return telefon; }
        set 
        { 
            if(value.All(char.IsDigit))
            {
                //0038631416465 -13
                //38631416465   -11
                //031416465     -9
                switch (value.Length)
                {
                    case 13:
                        telefon = Regex.Replace(value, @"(\d{5})(\d{2})(\d{3})(\d{3})", "0$2-$3-$4");
                        break;
                    case 11:
                        telefon = Regex.Replace(value, @"(\d{3})(\d{2})(\d{3})(\d{3})", "0$2-$3-$4");
                        break;
                    case 9:
                        telefon = Regex.Replace(value, @"(\d{3})(\d{3})(\d{3})", "$1-$2-$3");
                        break;
                }
            }
        }
    }
    public string Email
    {
        get { return email; }
        set 
        {
            string expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            if (Regex.IsMatch(value, expresion)) email = value;
        }
    }
    public Bitmap ProfilnaSlika
    {
        get { return profilnaSlika; }
        set { profilnaSlika = value; }
    }
    public Oseba Oce
    {
        get { return oce; }
        set { oce = value; }
    }
    public Oseba Mati
    {
        get { return mati; }
        set { mati = value; }
    }
    public Oseba Soprog
    {
        get { return soprog; }
        set { soprog = value; }
    }
}

public class Moski : Oseba
{
    private string hisno_ime;    //Pr Zdôvənk, Pr Potočjek

    public Moski(string ime, string priimek, string vzdevek, string hisno_ime, DateTime datum_rojstva, DateTime datum_smrti, string kraj_rojstva, string kraj_smrti, int visina, string email, string telefon, Bitmap slika)
        : base(ime, priimek, vzdevek, datum_rojstva, datum_smrti, kraj_rojstva, kraj_smrti, visina, email, telefon, slika)
    {
        this.Hisno_ime = hisno_ime;
    }
    public Moski(string ime, string priimek, string vzdevek, string hisno_ime, int danDR, int mesecDR, int letoDR, int danDS, int mesecDS, int letoDS, string kraj_rojstva, string kraj_smrti, int visina, string email, string telefon, Bitmap slika)
        : base(ime,  priimek,  vzdevek,  danDR,  mesecDR,  letoDR,  danDS,  mesecDS,  letoDS,  kraj_rojstva,  kraj_smrti,  visina,  email,  telefon,  slika)
    {
        this.Hisno_ime = hisno_ime;
    }
    public Moski(string ime, string priimek, string vzdevek, string hisno_ime, DateTime datum_rojstva, string kraj_rojstva, int visina, string email, string telefon, Bitmap slika)
        : base(ime, priimek, vzdevek, datum_rojstva, kraj_rojstva, visina, email, telefon, slika)
    {
        this.Hisno_ime = hisno_ime;
    }

    public override string ToString()
    {
        return "Hišno ime: " + Hisno_ime + "\n" + base.ToString();         
    }

    public string Hisno_ime
    {
        get { return hisno_ime; }
        set { hisno_ime = value; }
    }
}

public class Zenska : Oseba
{
    private string dekliski_priimek;
    

    public Zenska(string ime, string priimek, string vzdevek, string dekliski_priimek, DateTime datum_rojstva, DateTime datum_smrti, string kraj_rojstva, string kraj_smrti, int visina, string email, string telefon, Bitmap slika)
        : base(ime, priimek, vzdevek, datum_rojstva, datum_smrti, kraj_rojstva, kraj_smrti, visina, email, telefon, slika)
    {
        this.Dekliski_priimek = dekliski_priimek;
    }
    public Zenska(string ime, string priimek, string vzdevek, string dekliski_priimek, int danDR, int mesecDR, int letoDR, int danDS, int mesecDS, int letoDS, string kraj_rojstva, string kraj_smrti, int visina, string email, string telefon, Bitmap slika)
        : base(ime,  priimek,  vzdevek,  danDR,  mesecDR,  letoDR,  danDS,  mesecDS,  letoDS,  kraj_rojstva,  kraj_smrti,  visina,  email,  telefon,  slika)
    {
        this.Dekliski_priimek = dekliski_priimek;
    }
    public Zenska(string ime, string priimek, string vzdevek, string dekliski_priimek, DateTime datum_rojstva, string kraj_rojstva, int visina, string email, string telefon, Bitmap slika)
        : base(ime, priimek, vzdevek, datum_rojstva, kraj_rojstva, visina, email, telefon, slika)
    {
        this.Dekliski_priimek = dekliski_priimek;
    }


    public override string ToString()
    {
        return "Dekliški priimek: " + Dekliski_priimek + "\n" + base.ToString();
    }

    public string Dekliski_priimek
    {
        get { return dekliski_priimek; }
        set { dekliski_priimek = value; }
    }    
}

