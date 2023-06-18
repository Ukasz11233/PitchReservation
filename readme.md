# Aplikacja do rezerwacji boisk do siatkówki

### Autor: Łukasz Chmielewski (grupa pt 9.45)

## Opis
Celem aplikacji było stworzenie prostego interfejsu webowego w którym mamy możliwość rezerwacji boisk do siatkówki, oraz wystawianie im opini.  
Mamy dostępne cztery zakładki:  
- Gracze - możemy dodawać w niej graczy którzy następnie mogą rezerwować boiska, oraz wystawiać opinie
- Boiska - dodajemy podstawowe informacje o boisku
- Opinie - dodajemy opinie, przypisująć odpowiedniego gracza oraz boisko
- Rezerwacje - dodajemy rezerwacje do boiska
- Informacje - zawiera informacje o rezerwacjach które są anulowane, które odbywają się dzisiaj lub które mają status w trakcie.  
Dodatkowo, niemożliwe jest usunięcie gracza lub boiska, jeśli posiada ono referencję do innej tabeli.  
Boisko musi być rezerwowane na conajmniej godzinę.

## Jak uruchmoic aplikację:
Wystarczy, że w terminalu podamy komendę: __dotenet run__ 

W razie niepowodzeń, zalecam usunięcie katalogu _Migrations_ oraz uruchomienie kolejno komend:
__dotnet ef migrations add InitialCreate__  
__dotnet ef database update__  
__dotnet run__

