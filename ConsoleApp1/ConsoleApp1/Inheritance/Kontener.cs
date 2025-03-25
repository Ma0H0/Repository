namespace ConsoleApp1.Inheritance;

public class Kontener
{
    public double waga_l { get; set; } = 0;
    public double wysokosc { get; set; }
    public double waga_k { get; set; }
    public double glebokosc { get; set; }
    public double max_lad { get; set; }
    public char rodzaj { get; set; }
    public string numer_seryjny { get; set; }
    public static int numer { get; set; } = 1;
    
    public static bool ladunek { get; set; } = false;
    public Kontener(int Wysokosc,int Waga_k,int Glebokosc,int Max_lad)
    {
        wysokosc = Wysokosc;
        waga_k = Waga_k;
        glebokosc = Glebokosc;
        max_lad = Max_lad;
        

    }
    
    public virtual void Oproznij()
    {
        waga_l = 0;
        ladunek = false;
    }

    public virtual void Zaladuj(int masa)
    {
        if (masa > max_lad)
        {
            OverfillException error = new OverfillException("Za duży ładunek");
        }
        else if (ladunek == false)
        {
            waga_l = masa;
            ladunek = true;
        }
        else
        {
            Console.WriteLine("Kontener ma już ładunek");
        }
    }

    public virtual void Info()
    {
        Console.WriteLine("Kontener: " + numer_seryjny);
        Console.WriteLine(" Wysokosc: " + wysokosc);
        Console.WriteLine(" Waga kontenera: " + waga_k);
        Console.WriteLine(" Glebokosc: " + glebokosc);
        Console.WriteLine(" Maksymalna Waga Ładunku: " + max_lad);
        Console.WriteLine(" Waga ładunku: " + waga_l);
        
        
    }
}