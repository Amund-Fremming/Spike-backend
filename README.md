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

## Backend

- Må hente votes til bruker når games displayes slik at votes can lastes.
  1. Hente votes med gameId:
     - Alle kort vil spamme apiet
     - Må iterere gjennom alle voters for å finne om bruker har stemt i hvert card
  2. Hente games med sine votes som ett objekt
     - Utrolig mye data som må renderes i frontend
     - Må iterere gjennom alle voters for å finne om bruker har stemt i hvert card
  3. Oppdatere en count når votes registreres
     - Mye transaksjoner, muligens bottleneck
     - Risikerer noen votes i,ke blir registrert, men ytelsen er bedre (Kasnkje verdt det)
     - Bruke enkelt API for å finne ut om bruker har stemt.

## Ytelsesoptimalisering

- Når spill opprettes, bruk spinner, og gjør det umulig å spamme apiet.
- Legg til transaksjoner så ikke samme spill kan lages av to

## Finishing touches

- Hvis ikke kontakt med backend, alert om ikke tilkoblet nettverk!
- Ny splash screen som er lik og like stor som loading bilde

- Søk sorteres etter rating
- Input validering
- Refaktorer frontend
- Refaktorer backend og legg til error handling!!

## Fix before appstore submit

- Expo doctor (npx)
