namespace ConsoleApp1.Inheritance;

public class KontenerG : Kontener, IHazardNotifier 
{
    public char rodzaj { set; get; } = 'G';
    
    public KontenerG(int Wysokosc, int Waga_k, int Glebokosc, int Max_lad) : base(Wysokosc, Waga_k, Glebokosc, Max_lad)
    {
        rodzaj = 'G';
        numer_seryjny = "KON-" + rodzaj+"-"+ numer;
        numer += 1;
    }
    public virtual void Oproznij()
    {
        waga_l = 0 + waga_l * 0.05;
        ladunek = false;
    }  
    public void SendMessage(string message)
    {
        Console.WriteLine(message + this.numer_seryjny );
    }    
}