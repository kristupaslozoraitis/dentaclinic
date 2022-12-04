# DentaClinic
## Sistemos paskirtis
- Sistema bus skirta patogesnei vizito registracijai pas odontologą. Sistemoje egzistuos 3 rolės – tai yra odontologas, registruotas vartotojas bei svečias. Svečias turės galimybę tik užsiregistruoti norėdamas matyti turinį. Norėdamas užsiregistruoti į vizitą, asmuo pirmiausiai turės užsiregistruoti sistemoje. Odontologas matys visas registracijas, kokios yra įvykdytos.
##	Funkciniai reikalavimai
Neregistruotas sistemos naudotojas galės:

-	Užsiregistruoti sistemoje.

Registruotas sistemos naudotojas galės:

-	Prisijungti prie sistemos
-	Matyti laisvus vizitų laikus.
-	Registruotis vizitui pas pasirinktą specialistą.
-	Matyti savo vizitų istoriją.
-	Palikti atsiliepimą po apsilankymo.
-	Atšaukti savo vizitą.

Odontologas sistemoje galės:

-	Matyti artėjančius vizitus.
-	Sukurti naujus vizitus.
-	Pridėti savo teikiamas paslaugų pasirinkimus.
-	Pašalinti savo teikiamų paslaugų pasirinkimu 

##	Sistemos architektūra
Sistemai kurti bus naudojamos šios technologijos:
- FrontEnd daliai React framework,
- BackEnd daliai .NET 6 Core,
- Duomenų bazė MsSQL

## Api specifikacija

### Auth
- POST "/api/register" skirtas vartotojo registracijai
- POST "/api/login" skirtas vartotojo prisijungimui
- GET "/api/me" skirtas gauti rolių sąrašą iš serverio

### Feedback
- GET "/api/v1/patientCards/{patientCardId}/visits/{visitId}/feedbacks" skirtas gauti atsiliepimus apie vizitą
- POST "/api/v1/patientCards/{patientCardId}/visits/{visitId}/feedbacks" skirtas sukurti atsiliepimą vizitui
- GET "/api/v1/patientCards/{patientCardId}/visits/{visitId}/feedbacks/{id}" skirtas gauti atsiliepimą pagal jo id
- PUT "/api/v1/patientCards/{patientCardId}/visits/{visitId}/feedbacks/{id}" skirtas atnaujinti atsiliepimą pagal jo id
- DELETE "/api/v1/patientCards/{patientCardId}/visits/{visitId}/feedbacks/{id}" skirtas ištrinti atsiliepimą pagal jo id

### FreeVisit
- GET "/api/v1/freeVisits" skirtas gauti sąrašą vizitų, kurie yra laisvi
- POST "/api/v1/freeVisits" skirtas sukurti laisvą vizitą
- GET "/api/v1/freeVisits/admin" skirtas administratoriui/odontologui gauti sąrašą visų savo kurtų vizitų
- GET "/api/v1/freeVisits/{id}" skirtas gauti tam tikrą laisvą vizitą pagal jo id
- PUT "/api/v1/freeVisits/{id}" skirtas atnaujinti laisvo vizito informaciją
- DELETE "/api/v1/freeVisits/{id}" skirtas ištrinti laisvą vizitą

### PatientCard
- GET "/api/v1/patientsCards" skirtas gauti sąrašą pacientų kortelių
- POST "/api/v1/patientsCards" skirtas paciento kortelės sukūrimui
- GET "/api/v1/patientsCards/{id}" skirtas gauti paciento kortelę pagal kortelės id
- PUT "/api/v1/patientsCards/{id}" skirtas atnaujinti paciento kortelės informaciją
- DELETE "/api/v1/patientsCards/{id}" skirtas ištrinti paciento kortelę

### Service
- GET "/api/v1/services" skirtas gauti visų paslaugų sąrašui
- POST "/api/v1/services" skirtas paslaugos sukūrimui
- PUT "/api/v1/services" skirtas paslaugos atnaujinimui pagal paslaugos id
- GET "/api/v1/services/{id}" skirtas gauti paslaugą pagal jos id
- DELETE "/api/v1/services/{id}" skirtas paslaugos ištrynimui

### Visit
- GET "/api/v1/patientCards/{patientCardId}/visits" skirtas užregistruotų vizitų sąrašui gauti
- POST "/api/v1/patientCards/{patientCardId}/visits" skirtas užregistruoto vizito sukūrimui
- GET "/api/v1/patientCards/{patientCardId}/visits/{id}" skirtas užregistruoto vizito gavimui pagal jo id
- PUT "/api/v1/patientCards/{patientCardId}/visits/{id}" skirtas užregistruoto vizito atnaujinimui
- DELETE "/api/v1/patientCards/{patientCardId}/visits/{id}" skirtas užregistruoto vizito atšaukimui