# Spis treści
+ [O projekcie](#o-projekcie)
+ [Uruchomienie](#uruchomienie)
+ [Korzystanie z aplikacji](#korzystanie-z-aplikacji)
# O projekcie
Aplikacja agreguje odczyty czujników, pozwalając na aktywne monitorowanie ich w jednym miejscu.

# Uruchomienie
+ Pobierz i rozpakuj archiwum dostępne w sekcji releases.
+ Edytuj plik dbconfig.json aby dostosować parametry połączenia z bazą danych.
+ Uruchom główną aplikację - dla centrum jest to ta z dopiskiem '.Desktop'.

# Korzystanie z aplikacji
## Centrum czujników
Tak długo, jak aplikacja jest uruchomiona, będzie zbierać odczyty zgodnie ze swoją konfiguracją i wysyłać je do bazy danych.
### Wyświetlanie stanu czujników
Po uruchomieniu aplikacji wyświetla się lista monitorowanych czujników. Odczyty są odświeżane na bieżąco.

### Dodawanie nowego czujnika
Naciśnij "+" w prawym górnym rogu ekranu czujników

Wypełnij pola odpowiednimi danymi i zapisz.
+ **Nazwa czujnika** - pozwala rozpoznać czujnik na liście;
+ **Ścieżka** - lokalizacja programu pobierającego aktualny odczyt czujnika. Program ten powinien podawać odczyt na główne wyjście, przykładowy kod znajduje się w folderze samples;
+ **Zakres pomiaru** - wykorzystywany do graficznego przedstawienia odczytu;
+ **Interwał** - odstęp czasu pomiędzy kolejnymi wywołaniami programu pobierającego odczyt.
### Edycja istniejącego czujnika
+ Naciśnij przycisk "..." przy odpowiednim czujniku.
+ Wprowadź pożądane zmiany we właściwościach czujnika, następnie zatwierdź przyciskiem "Zapisz".

## Czytnik czujników
Czytnik nie zapewnia możliwości interakcji, ale umożliwia obserwację stanu wszystkich czujników znajdujących sie w bazie danych, nawet
