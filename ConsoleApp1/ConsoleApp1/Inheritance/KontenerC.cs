namespace ConsoleApp1.Inheritance;

public class KontenerC : Kontener
{
    
    public double temperature { set; get; }
    public KontenerC(int Wysokosc, int Waga_k, int Glebokosc, int Max_lad) : base(Wysokosc, Waga_k, Glebokosc, Max_lad)
    {
        rodzaj = 'C';
        numer_seryjny = "KON-" + rodzaj+"-"+ numer;
        numer += 1;
    }

    public void settemperature(double temperature)
    {
        this.temperature = temperature;
    } 
    public virtual void Zaladuj(int masa,double temp)
    {
        if (temperature >= temp)
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
    }
}