## Uruchomienie lokalne

w terminalu:
dotnet run

wynik:
1) w drugim terminalu:
curl http://localhost:5091/

2) w przeglądarce:
http://localhost:5091/

Serwer (Kestrel) wystartuje na porcie 5091 (zdefiniowanym w `Properties/launchSettings.json`,
profil `http`) - dokładny adres pojawi się też w konsoli po `dotnet run`.

