CREATE TABLE licitations_links (
pk_licitations_links uniqueidentifier NOT NULL,
link varchar(max) NOT NULL,
status int NOT NULL
constraint pk_licitations_links primary key (pk_licitations_links)
);