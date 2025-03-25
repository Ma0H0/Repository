
using ConsoleApp1;
using ConsoleApp1.Inheritance;

{
    KontenerC c = new KontenerC(10, 20, 30, 40);// Stworzenie kontenera
    c.settemperature(10); // Ustawienie Temperatury
    c.Zaladuj(5,9);// Załadowanie ładunku do kontenera
    c.Info();//Wypisanie informacji o kontenerze 
    Statek s1 = new Statek(5,6,100);// Utworzenie statku
    s1.zaladuj_kontener(c); // Załadowanie kontenera na statek
    s1.info();// Wypisanie informacji statku
    Statek s2 = new Statek(5,4,100);
    s1.przenieś_kontener(c,s2);// Przeniesienie kontenera pomiędzy statkami
    s1.info();
    s2.info();
    KontenerG g1 = new KontenerG(10, 20, 30, 40);
    KontenerG g2 = new KontenerG(10, 20, 30, 40);
    KontenerG g3 = new KontenerG(10, 20, 30, 40);
    List<Kontener> Lista_Kontenerów = new List<Kontener>();
    Lista_Kontenerów.Add(g1);
    Lista_Kontenerów.Add(g2);
    Lista_Kontenerów.Add(g3);
    s1.zaladuj_kontener(Lista_Kontenerów);// Załadowanie listy kontenerów
    s1.info();
    s1.usun_kontener(g3);// Usunięcie kontenere ze statku
    c.Oproznij();// Rozładowanie kontenera
    s1.zamień_kontenery(g2,g3);// Zamiana kontenerów
    s1.info();
    
}
