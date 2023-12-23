DROP TABLE evidencija CASCADE CONSTRAINTS;

CREATE TABLE evidencija (
	id VARCHAR(32 CHAR) NOT NULL,
	datum VARCHAR (20),
	status VARCHAR (20),
    izvestaj VARCHAR (50)
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

