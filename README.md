# Spike

- [Figma](https://www.figma.com/file/oBgpl8HkiowbkUFe6HchFL/Untitled?node-id=0%3A1&mode=dev)

### Kort om appen

- Super enkel og liten spørsmål applikasjon.
- Her man man opprette, bli med i spill, legge til spørsmål og publisere spillet sitt.
- Har brukt ASP.NET som backend som hostes hos Azure
- React native som frontend

### Fokus

- Få appen på Appstore
- Håndtere race conditions

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

# Spike - TODO

## Frontened

- Legge til deviceId på spill useEffect med AsyncAtorage

- Generer ikoner for nivå
- Legge til vote knapper
- Display prosent rating (Kanskje farger som indikerer hvor bra det er på skriften, gul, grønn, rød?)
- Burde lyuse den voten du har avgitt

- Scrolling evig eller sider

- Legge til en plass der hvor spillere kan anngi iconet til spillet sitt
- Koble opp søk

# Backend

- fullfør søk api
- Set max antall Games som hentes per søk, også første!
- Fiks \_context bruk til repos i Controllere

## Ytelsesoptimalisering

- Legg til spinner der lasting kan ta tid
- Fiks glitch loading på mascot

- Legg til transaksjoner så ikke samme spill kan lages av to

## Finishing touches

- Input validering!
- Refaktorer frontend
- Refaktorer backend og legg til error handling!!

## Fix before appstore submit

- Expo doctor (npx)
