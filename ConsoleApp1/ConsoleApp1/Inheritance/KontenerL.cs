namespace ConsoleApp1.Inheritance;

public class KontenerL : Kontener, IHazardNotifier 
{
    public bool niebezpiecznyładunek { get; set; }
    public char rodzaj { set; get; } = 'L';
    public KontenerL(int Wysokosc, int Waga_k, int Glebokosc, int Max_lad) : base(Wysokosc, Waga_k, Glebokosc, Max_lad)
    {
        rodzaj = 'L';
        numer_seryjny = "KON-" + rodzaj+"-"+ numer;
        numer += 1;
    }

    public virtual void Zaladuj(int masa,bool niebie)
    {
        niebezpiecznyładunek = niebie;
        if (niebie && masa > max_lad * 0.5)
        {
            SendMessage("Nastąpiła próba wykonania niebezpiecznej operacji w kontenerze: ");
            throw new OverfillException("Za duży ładunek"); 
            
        }
        if (!niebie && masa > max_lad*0.9)
        {
            SendMessage("Nastąpiła próba wykonania niebezpiecznej operacji w kontenerze: ");
            throw new OverfillException("Za duży ładunek");
            
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

    public void SendMessage(string message)
    {
        Console.WriteLine(message + this.numer_seryjny );
    }
}