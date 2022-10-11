CREATE TABLE licitations (
    pk_licitation uniqueidentifier NOT NULL,
	organ_name text NOT NULL,
	organ_document text NOT NULL,
	notice text NOT NULL,
	object text NOT NULL,
	value decimal NOT NULL,
	status int NOT NULL,
	opening_date DateTime NOT NULL,
	link text NOT NULL,
	create_date DateTime NOT NULL
   CONSTRAINT pk_licitation  PRIMARY KEY (pk_licitation)
);