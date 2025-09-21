# Spis treści
+ [[#O projekcie]]
+ [[#Instalacja]]
+ [[#Korzystanie z aplikacji]]
+ [[#TODO]]
+ [[#Wykorzystane narzędzia]]
# O projekcie
Aplikacja agreguje odczyty czujników, pozwalając na aktywne monitorowanie ich w jednym miejscu.

# Instalacja
+ Ściągnij repozytorium
+ Opublikuj projekt .Desktop (pozostałe platformy nie są zaimplementowane) jako zależny od platformy
+ Przenieś wygenerowane pliki na docelowe urządzenie

# Korzystanie z aplikacji
## Wyświetlanie stanu czujników
Po uruchomieniu aplikacji wyświetla się lista monitorowanych czujników.

## Dodawanie nowego czujnika
Naciśnij "+" w prawym górnym rogu ekranu czujników

Wypełnij pola odpowiednimi danymi i zapisz.
+ **Nazwa czujnika** - pozwala rozpoznać czujnik na liście
+ **Ścieżka** - lokalizacja programu pobierającego aktualny odczyt czujnika
+ **Zakres pomiaru** - wykorzystywany do graficznego przedstawienia odczytu
+ **Interwał** - odstęp czasu pomiędzy kolejnymi wywołaniami programu pobierającego odczyt.
## Edycja istniejącego czujnika
Naciśnij przycisk "..." przy odpowiednim czujniku

Wprowadź pożądane zmiany we właściwościach czujnika, następnie zatwierdź przyciskiem "Zapisz".

## Plik konfiguracyjny
Każda zmiana w konfiguracji czujników jest automatycznie zapisywana w pliku JSON. Przy uruchomieniu aplikacja szuka tego pliku i, jeśli istnieje, ładuje zapisaną w nim listę czujników.

# TODO
+ Przesyłanie odczytów pomiędzy urządzeniami
+ Możliwość ustawiania progów i wysyłania ostrzeżeń o ich przekroczeniu

# Wykorzystane narzędzia
Avalonia
