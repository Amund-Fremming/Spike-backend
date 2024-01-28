# Spike

- [Figma](https://www.figma.com/file/oBgpl8HkiowbkUFe6HchFL/Untitled?node-id=0%3A1&mode=dev)
- [Frontend](https://github.com/Amund-Fremming/Spike-frontend)

### Kort om appen

- Super enkel og liten spørsmål applikasjon.
- Her man man opprette og bli med i spill og legge til spørsmål
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

- Mulighet for å lagre spill, slik at man har en feed med spill fra community. Her skal man kunne stemme opp eller ned på et spill, der de med høyest vurderingen ligger på toppen
- Flere bevegende elementer i bakgrunn som et romskip eller en meteor som flyr i bakgrunn av planeten
- Mer annimasjoner på mascot figurene
- Når et spill opprettes og startes burde man ikke kunne legge til spørsmål i dette spillet lenger, kanskje se på mulighet for å bytte host om man faller ut

- **Fyll ut når ferdig**

# trenger

- GetGameQuestionsById
- AddQuestionToGame
- GetNumberOfQuestions
- GetGameById
- GetPublicGames
- StartGame
- PublishGame
- InkrementGameVote
- DecrementGameVote
