DROP TABLE evidencija CASCADE CONSTRAINTS;

CREATE TABLE Evidencija (
    ID varchar(255),
    Datum Date,
    Status varchar(255),
    Opis varchar(255)
);

insert into evidencija 
values ('12',TO_DATE('12.07.1972', 'DD.MM.YYYY'),
'ACTIVE','mrzim ovo');

insert into evidencija 
values ('12',TO_DATE('12.07.1972', 'DD.MM.YYYY'),
'ACTIVE','mrzim ovo');

insert into evidencija 
values ('12',TO_DATE('12.07.1972', 'DD.MM.YYYY'),
'ACTIVE','mrzim ovo');

