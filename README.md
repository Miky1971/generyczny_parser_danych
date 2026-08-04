## Uruchomienie w terminalu:
dotnet run

Serwer (Kestrel) wystartuje na porcie 5091 (zdefiniowanym w `Properties/launchSettings.json`,
profil `http`) - dokładny adres pojawi się też w konsoli po `dotnet run`.

## Testowanie w drugim terminalu:
curl -X POST http://localhost:5091/api/v1/parse-content -H "Content-Type: application/json" -d "{\"type\":\"CSV\" \"content\":\"bmF6d2Esd2llawpKYW4sMzAKQW5uYSwyNQ==\"}"

lub:
curl -X POST http://localhost:5091/api/v1/parse-content -H "Content-Type: application/json" -d "{\"type\":\"INTERNAL_JSON\",\"content\":\"W3sibmF6d2EiOiJKYW4iLCJ3aWVrIjozMH0seyJuYXp3YSI6IkFubmEiLCJ3aWVrIjoyNX1d\"}"

Pole `content` to Base64 z tekstu:
nazwa,wiek
Jan,30
Anna,25

lub:
{"nazwa":"Jan","wiek":30},{"nazwa":"Anna","wiek":25}

Oczekiwana odpowiedź json:
{"status":"Success","qtyRows":2,"totalRows":2,"data":[{"nazwa":"Jan","wiek":"30"},{"nazwa":"Anna","wiek":"25"}]}
