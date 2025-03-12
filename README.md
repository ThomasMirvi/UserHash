# User Hash
Aplikace pro správu uživatelů v rámci administrace, která umožňuje přidávat nové uživatele, měnit jejich hesla a upravovat jejich role. Uživatelé a jejich data jsou uloženy v XML souboru.
Funkce aplikace
Přidání nového uživatele: Můžete přidat nového uživatele, který bude mít výchozí roli User a prázdné heslo (hash hesla bude nastaven až při změně hesla).
Změna hesla: Umožňuje změnit heslo pro vybraného uživatele. Heslo je bezpečně uloženo jako Base64 hash.
Zobrazení uživatelů: Seznam uživatelů je zobrazen v ListBox, kde jsou uvedeny uživatelské jméno a jejich role.
Výběr uživatele: Po výběru uživatele v seznamu můžete zobrazit jeho údaje (např. uživatelské jméno a roli).
Odhlášení: Umožňuje administrátorovi se odhlásit a přejít zpět na přihlašovací formulář.

Funkce aplikace
Přidání nového uživatele: Můžete přidat nového uživatele, který bude mít výchozí roli User a prázdné heslo (hash hesla bude nastaven až při změně hesla).
Změna hesla: Umožňuje změnit heslo pro vybraného uživatele. Heslo je bezpečně uloženo jako Base64 hash.
Zobrazení uživatelů: Seznam uživatelů je zobrazen v ListBox, kde jsou uvedeny uživatelské jméno a jejich role.
Výběr uživatele: Po výběru uživatele v seznamu můžete zobrazit jeho údaje (např. uživatelské jméno a roli).
Odhlášení: Umožňuje administrátorovi se odhlásit a přejít zpět na přihlašovací formulář.

Struktura XML souboru
Aplikace používá XML soubor pro uložení uživatelských dat. Struktura souboru vypadá takto:
<?xml version="1.0" encoding="utf-8"?>
<Users>
  <User>
    <Username>admin</Username>
    <HashedPassword>YWRtaW4=</HashedPassword>
    <Role>Admin</Role>
  </User>
  <User>
    <Username>user</Username>
    <HashedPassword>dXNlcg==</HashedPassword>
    <Role>User</Role>
  </User>
  <User>
    <Username>pepa</Username>
    <HashedPassword>YWFh</HashedPassword>
    <Role>User</Role>
  </User>
  <User>
    <Username>tomas</Username>
    <HashedPassword></HashedPassword>
    <Role>User</Role>
  </User>
</Users>

