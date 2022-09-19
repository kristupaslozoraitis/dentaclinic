# dentaclinic
## Sistemos paskirtis
- Sistema bus skirta patogesnei vizito registracijai pas odontologą. Sistemoje egzistuos 3 rolės – tai yra odontologas, registruotas vartotojas bei svečias. Svečias turės galimybę tik peržiūrėti laisvus vizitų laikus. Norėdamas užsiregistruoti į vizitą, asmuo pirmiausiai turės užsiregistruoti sistemoje. Odontologas matys visas registracijas, kokios yra įvykdytos.
##	Funkciniai reikalavimai
Neregistruotas sistemos naudotojas galės:

-	Peržiūrėti laisvus vizitų laikus pas specialistus.
-	Užsiregistruoti sistemoje.

Registruotas sistemos naudotojas galės:

-	Prisijungti prie sistemos
-	Registruotis vizitui pas pasirinktą specialistą.
-	Matyti savo vizitų istoriją.
-	Palikti atsiliepimą po apsilankymo.
-	Atšaukti savo vizitą.
-	Pakeisti vizito laiką.
-	Peržiūrėti specialisto teikiamas paslaugas.

Odontologas sistemoje galės:

-	Matyti artėjančius vizitus.
-	Tvirtinti vizitų registracijas.
-	Nurodyti laisvus laikus vizitui.
-	Pridėti savo teikiamas paslaugų pasirinkimus.
-	Pašalinti savo teikiamų paslaugų pasirinkimu 

##	Sistemos architektūra
Sistemai kurti bus naudojamos šios technologijos:
- FrontEnd daliai React framework,
- BackEnd daliai .NET 6 Core,
- Duomenų bazė MySql
