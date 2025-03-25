using ConsoleApp1.Inheritance;

namespace ConsoleApp1;

public class Statek
{
public double predkosc { get; set; }
public int l_kontener { get; set; }
public double max_waga_k { get; set; }
public List<Kontener> kontenery { get; set; } = new List<Kontener>();
public int o_l_kontener { get; set; } = 0;

public Statek(double predkosc, int l_kontener, double max_waga_k)
{
    this.predkosc = predkosc;
    this.l_kontener = l_kontener;
    this.max_waga_k = max_waga_k;
}

public void zaladuj_kontener(Kontener k)
{
    if (o_l_kontener  == 0)
    {
        kontenery.Add(k);
        o_l_kontener++;
    }
    else
    {
        if (l_kontener < kontenery.Count())
        {
            kontenery.Add(k);
            o_l_kontener++;
        }
        else
        {
            Console.WriteLine("Statek jest pełny");
        }
    }
}
public void zaladuj_kontener(List<Kontener> kon)
{
    int a = l_kontener - kon.Count;
    if (l_kontener != kontenery.Count && a >= 0)
    {
        foreach (Kontener k in kon)
        {
            kontenery.Add(k);
            o_l_kontener++;
        }
    }
    else
    { 
        Console.WriteLine("Nie można dodać takiej liczby kontenerów na statek");
    }
}

public void usun_kontener(Kontener k)
{
    foreach (Kontener k1 in kontenery)
    {
        if (k.numer_seryjny == k1.numer_seryjny)
        {
            kontenery.Remove(k1);
            o_l_kontener--;
            return;
        }
    }

}

public void zamień_kontenery(Kontener k1, Kontener k2)
{
    foreach (Kontener k3 in kontenery)
    {
        if (k3.numer_seryjny == k1.numer_seryjny)
        {
            kontenery[kontenery.IndexOf(k3)] = k2;
            return;
        }
    }
    
}

public void przenieś_kontener(Kontener k, Statek s)
{
    if (s.kontenery.Count != s.l_kontener)
    {
        kontenery.Remove(k);
        s.kontenery.Add(k);
    }
    
}

public void info()
{   
    Console.WriteLine("Statek Info:");
    Console.WriteLine(" Predkość: " + predkosc);
    Console.WriteLine(" Kontener: " + l_kontener);
    Console.WriteLine(" Max Waga Ładunku:" + max_waga_k);
    Console.WriteLine(" Lista Kontenerów:");
    foreach (Kontener k in kontenery)
    {
        Console.WriteLine("     " + k.numer_seryjny);
        
    }
    
}

}