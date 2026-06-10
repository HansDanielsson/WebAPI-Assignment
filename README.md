# WebAPI-Assignment

Advanced programming assignment in #C at MedieInstitutet



\# Inlämningsuppgift



Detta projekt är startpunkten för din inlämingsuppgift. Ni behöver skapa ett eget api som är skyddat med hjälp av api-nycklar. Ni skall definiera resurser (data) som skall kunna skickas från ert api efter att en användare gör ett korrekt anrop och inkluderar sin api-nyckel. 



\## Användarregistrering

\- En användare ska kunna registrera sig i systemet.

\- Användaren ska kunna logga in.

\- Du väljer inloggningsstrategi:

&#x20; - Individuella konton (med e-post och lösenord)



\## API-nyckel

\- Efter registrering och inloggning ska användaren kunna begära en API-nyckel.

\- API-nyckeln ska sparas i databasen och kopplas till användaren.



\## Skyddade API-slutpunkter med CRUD-funktionalitet

\- Du ska skapa minst en resurs (t.ex. recept, sportresultat, speldata, personliga anteckningar) som användaren kan hantera via CRUD:

&#x20; - Create – Lägga till data.

&#x20; - Read – Hämta data.

&#x20; - Update – Ändra befintlig data.

&#x20; - Delete – Ta bort data.

\- Alla CRUD-operationer ska kräva giltig API-nyckel.

\- API-nyckeln ska skickas med i anropet och valideras innan data returneras eller ändras.



\## Databas

\- Du väljer:

&#x20; - Entity Framework + SQL



\## Betygsättning

Denna uppgift bedöms med IG (icke godkänd), G (godkänd) och VG (Väl Godkänt).



\### För godkänt (G) krävs:

\- Användare kan registrera sig och logga in.

\- Användare kan begära och få en API-nyckel.

\- CRUD-funktionalitet finns för vald resurs och är skyddad med API-nyckel.

\- API-nyckeln valideras korrekt vid varje anrop.

\- Databasen fungerar enligt vald lösning (SQL med EF).

\- Ni använder kontroller som endpoints och hanterar logiken i dessa.

\- Korrekta svarskoder skickas från ditt API.



\### För Väl godkänt (VG) krävs:

\- Samtliga punkter från G

\- Ni har valt en komplex struktur av data att returnera och använder er av DTO:er för att begränsa informationen.

\- Ni använder designmönster med tjänster och repositories

\- Ni använder korrekt validering och har skapat minst en egen validering (custom validation).

\- En fungerande Swagger



\## Inlämning

\- En länk till ett GitHub-repo på itslearning

\- Bifoga en README.md som beskriver:

&#x20; - Hur projektet startas.

&#x20; - Hur man registrerar en användare och får en API-nyckel.

&#x20; - Exempel på anrop till de skyddade CRUD-slutpunkterna.



