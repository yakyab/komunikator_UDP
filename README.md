# Komunikator UDP

Prosty komunikator, który umożliwia wymianę wiadomości przy użyciu protokołu UDP.

## Funkcje

- Wysyłanie i odbieranie wiadomości w czasie rzeczywistym.
- Możliwość wprowadzania adresu IP odbiorcy oraz portów dla nasłuchiwania i docelowego.
- Dynamiczna aktualizacja listy wiadomości w interfejsie użytkownika.
- Obsługa błędów, w tym błędnych adresów IP oraz zajętych portów.

## Stack Technologiczny

- **Język:** C#
- **Framework:** .NET Framework
- **Interfejs:** WPF (Windows Presentation Foundation)
- **Komunikacja sieciowa:** UDP (przy użyciu klasy `UdpClient`)
- **Architektura:** MVVM (Model-View-ViewModel)

## Instalacja

1. Sklonuj to repozytorium na swój lokalny komputer.
2. Otwórz projekt w Visual Studio.
3. Zbuduj projekt i uruchom go, korzystając z opcji "Uruchom" w Visual Studio.

## Użytkowanie

1. Po uruchomieniu aplikacji wprowadź adres IP odbiorcy, port nasłuchiwania i port docelowy.
2. Kliknij przycisk "Zatwierdź", aby nawiązać połączenie UDP.
3. Wprowadź wiadomość w odpowiednim polu tekstowym i kliknij "Wyślij", aby wysłać wiadomość do odbiorcy.
4. Odbierane wiadomości pojawią się na liście poniżej pola tekstowego.
