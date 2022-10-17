CREATE TABLE licitations (
    pk_licitation uniqueidentifier NOT NULL,
	organ_name varchar(max) NOT NULL,
	organ_document varchar(max) NOT NULL,
	notice varchar(max) NOT NULL,
	object varchar(max) NOT NULL,
	value decimal NOT NULL,
	status int NOT NULL,
	opening_date DateTime NOT NULL,
	link varchar(max) NOT NULL,
	create_date DateTime NOT NULL
   CONSTRAINT pk_licitation  PRIMARY KEY (pk_licitation)
);