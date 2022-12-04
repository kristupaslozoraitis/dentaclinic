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

## Vartotojo sąsajos langai

### Pagrindinis langas
![image](https://user-images.githubusercontent.com/65513238/205515565-81c07c9f-e03b-4b0e-87b4-86eacf4c9f5a.png)
### Prisijungimo forma
![image](https://user-images.githubusercontent.com/65513238/205515574-26278149-f551-4938-9180-203368cefb2f.png)
### Registracijos formos 1 žingsnis
![image](https://user-images.githubusercontent.com/65513238/205515578-b31bc959-706f-40ba-9efd-a8dc44d22d4c.png)
### Registracijos formos 2 žingsnis
![image](https://user-images.githubusercontent.com/65513238/205515585-c1d2652d-14c7-4225-8eb8-1adeb0c71f9e.png)
### Vizitų istorijos langas
![image](https://user-images.githubusercontent.com/65513238/205515608-7376c61c-4dc9-4cc6-9c8e-cfd36e89e39e.png)
### Laisvų vizitų langas
![image](https://user-images.githubusercontent.com/65513238/205515618-b1d04bff-e19f-4f4d-a856-26528fb64d63.png)
### Paciento kortelės langas
![image](https://user-images.githubusercontent.com/65513238/205515623-1a1ed97d-b569-4afd-80e6-90f26785ca97.png)
### Odontologo vizitų administravimo langas
![image](https://user-images.githubusercontent.com/65513238/205515653-76084234-fdd8-41b7-8531-ecaf83875289.png)
### Paslaugų redagavimo langas
![image](https://user-images.githubusercontent.com/65513238/205515666-d4dd9c8b-01f8-4031-8c17-5d9a44d3273e.png)
### Naujos paslaugos forma
![image](https://user-images.githubusercontent.com/65513238/205515676-36dd6078-627e-4d89-b09b-bca2092f7bc8.png)
### Naujo vizito sukūrimo forma
![image](https://user-images.githubusercontent.com/65513238/205515681-0837d4dc-4c77-4e70-af76-f9eacdda755d.png)


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

## IŠVADA
- Projektas realizuotas pagal aprašytus funkcinius reikalavimus. Visas funkcionalumas veikia kaip ir priklauso. Žinoma yra ir taisytinų dalykų - geresnis responsive layout, nukreipimų į specifinius puslapius pasibaigus sesijos galiojimui tobulinimas

