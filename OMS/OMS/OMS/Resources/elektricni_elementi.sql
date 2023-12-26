CREATE TABLE elektricni_element (
	id VARCHAR(32) NOT NULL,
	naziv VARCHAR (30),
	tip VARCHAR (20),
    geolokacija VARCHAR (50),
    naponski_nivo VARCHAR (20)
);

INSERT INTO elektricni_element (id, naziv, tip, geolokacija, naponski_nivo)
VALUES ('1', 'kuvalo', '123', 'subotica', 'nizak nivo');

INSERT INTO elektricni_element (id, naziv, tip, geolokacija, naponski_nivo)
VALUES ('2', 'toster', '456', 'novi sad', 'srednji nivo');

INSERT INTO elektricni_element (id, naziv, tip, geolokacija, naponski_nivo)
VALUES ('3', 'sporet', '789', 'beograd', 'visoki nivo');