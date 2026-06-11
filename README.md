# WebAPI-Assignment

Advanced programming assignment in #C at MedieInstitutet

\# Inlämningsuppgift

Detta är ett system som ska hantera anteckningar. Man kan gruppera anteckningar i olika kategorier.

Det finns 2 färdiga användare
\admin@test.com, Lösen: Admin123!
\hans@test.com, Lösen: Test123!


Test först med att logga in med en färdig användare:
\Body:
{
    "email" : "admin@test.com",
    "password" : "Admin123!"
}

/login

Nya användare registreras på /register
\Body:
{
    "email" : "newuser@test.com",
    "password" : "Pass123!"
}

\---

Hämta API-nyckel för att sätta en header-variabel: x-api-key
\/user/userapikey

\---

Det finns 3 st kategorier: Snabb, Hemma och Borta

Det finns 3 st anteckningar
\Titel: Mat			Notering: Dags att köpa mat	K: Snabb
\Titel: Saker		Notering: Att köpa					K: Hemma
\Titel: Kläder		Notering: Blå byxor					K: Borta

\##Hantering av kategorier:
\Get:
\Lista alla: /categories
\En: /categories/{id}

\Post:
\Lägga till: /categories
\{ "name" : "Namn på kategori" }

Lägger till en notering: /categories/{id}/notes/{Notering-id}

\Put:
\Ändra: /categories/{id}
{ "name" : "Uppdatera namnet" }

\Del:
\Ta bort: /categories/{id}

\##Hantering av anteckningar
\Get:
\Lista all: /notes
\En: /notes/{id}

\Post:
\Lägga till: /notes
\{ "title" : "Titel på noteringen", "content" : "Innehåll", "categoryid" : "{Kategori-id}" }

\Ändra kategori på noteringen: /notes/{id}/category/{Kategori-id}

\Put:
\Ändra: /notes/{id}
\{ "title" : "Titel på noteringen", "content" : "Innehåll", "categoryid" : "{Kategori-id}" }

\Del:
\Ta bort: /notes/{id}

\Regler:
\Man kan bara ändra/ta bort/använda sina egna poster som man har skapat.
\Man får förvisso se alla anda men bara förändra sina egna!

När man testar får man vara extra noga med alla nycklar/id
