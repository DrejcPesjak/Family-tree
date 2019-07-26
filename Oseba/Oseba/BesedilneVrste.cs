using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public abstract class BesedilneVrste
{
    private string besedilo;

    protected string Besedilo { get; set; }

    public void PripniNaKonec(string dodatek)
    {
        Besedilo += dodatek; 
    }
}


public class ZivljenskaZgodba : BesedilneVrste
{
    private string naslov;

    public ZivljenskaZgodba()
    {
        Naslov = Besedilo = "";
    }

    public ZivljenskaZgodba(string naslov, string besedilo)
    {
        this.Naslov= naslov;
        this.Besedilo = besedilo;
    }

    public static bool operator ==(ZivljenskaZgodba prva, ZivljenskaZgodba druga)
    {
        if (prva.Besedilo == druga.Besedilo && prva.Naslov == druga.Naslov)
            return true;
        else
            return false;
    }

    public static bool operator !=(ZivljenskaZgodba prva, ZivljenskaZgodba druga)
    {
        if (prva == druga) return false;
        else return true;
    }

    public string Naslov
    {
        get { return naslov; }
        set { naslov = value; }
    }
}
public class Zivljenjepis : BesedilneVrste
{
    public enum Tip { splosno, delodajalec };
    private Tip tipZivPis;
    private string delodajalec;

    public Zivljenjepis()
    {
        tipZivPis = Tip.splosno;
        delodajalec = "";
    }

    public Zivljenjepis(Tip tip, string delodajalec)
    {
        this.tipZivPis = tip;
        this.Delodajalec = delodajalec;
    }


    //<summary>
    //Za katerega delodajalca je namenjen življenjepis
    //</summary>  
    public string Delodajalec
    {
        get { return delodajalec; }
        set { if (this.tipZivPis == Tip.delodajalec) delodajalec = value; }
    }    
}



public class Opomba : BesedilneVrste
{    
    private string tema;


    // <summary>
    // Glede česa je opomba: zakon(ločitev...), slabi zapisi v rodovniku itd.
    // </summary>
    public string Tema
    {
        get { return tema; }
        set { tema = value; }
    }
}