# Spike

- [Figma](https://www.figma.com/file/oBgpl8HkiowbkUFe6HchFL/Untitled?node-id=0%3A1&mode=dev)

### Kort om appen

- Super enkel og liten spørsmål applikasjon.
- Her man man opprette, bli med i spill, legge til spørsmål og publisere spillet sitt.
- Har brukt ASP.NET som backend som hostes hos Azure.
- React native som frontend.

### Fokus

- Få appen på Appstore.
- Håndtere race conditions.

### Hva har jeg lært

- Hvordan jeg setter opp utviklingsmiljlø med Azure og github actions
- Utvikle frontend med react native
- Bruk av SignalR for live oppdateringer
- Bruk av transactions for å håndtere race conditions
- Lage gjenbrukbare komponenter for å minimere kode

### Hva skal jeg gjøre annerledes neste gang

- Bruke mer tid i planleggingen og få fastslått noen flere generiske komponenter
- Lære å burke Figma mer effektivt og riktig

### Ting som skal legges til

- Flere bevegende elementer i bakgrunn som et romskip eller en meteor som flyr i bakgrunn av planeten
- Mer annimasjoner på mascot figurene

<hr>

### Hvorfor tar det lang tid å hente games?

1. Vi kaller på Game i datbasen etter spill
2. For hvert spill, kaller vi på Voters i databasen
3. For hvert spill, kaller vi på voters i databasen
4. Så sorterer vi spillene.

### Løsning?

1. Ikke kalkuler votes i GetGames men gjør det etter hver vote
2. Lagre brukers votes i frontend, så slipper man å legge disse til i backend O(n) vs O(1);
3. Sorter i getGames Linq
