# User Hash

Aplikace pro správu uživatelů v rámci administrace, která umožňuje přidávat nové uživatele a měnit jejich hesla. Uživatelé a jejich data jsou uloženy v XML souboru.

## Funkce aplikace

- **Přidání nového uživatele**: Můžete přidat nového uživatele, který bude mít výchozí roli `User` a prázdné heslo (hash hesla bude nastaven až při změně hesla).
- **Změna hesla**: Umožňuje změnit heslo pro vybraného uživatele. Heslo je bezpečně uloženo jako Base64 hash.
- **Zobrazení uživatelů**: Seznam uživatelů je zobrazen v `ListBox`, přístup k němu má pouze administrátor.
- **Výběr uživatele**: Po výběru uživatele můžete měnit jeho heslo.
- **Odhlášení**: Umožňuje přepínat uživatele.

## Struktura XML souboru

Aplikace používá XML soubor pro uložení uživatelských dat. Struktura souboru vypadá takto:

```xml
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
</Users>
```

## Vysvětlení polí

- **Username**: Uživatelské jméno.
- **HashedPassword**: Heslo uživatele, uložené jako Base64 hash.
- **Role**: Role uživatele (`User`, `Admin`).

## Požadavky

- .NET Framework (Windows Forms aplikace)
- Visual Studio nebo jiný IDE pro práci s .NET aplikacemi
