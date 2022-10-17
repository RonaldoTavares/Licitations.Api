CREATE TABLE 
organs (
    pk_organ uniqueidentifier NOT NULL,
	organ_name varchar(max) NOT NULL,
	organ_document varchar(max) NOT NULL,
	active int NOT NULL
   CONSTRAINT pk_organ  PRIMARY KEY (pk_organ)
);

CREATE TABLE 
search_constants (
    pk_search_constant uniqueidentifier NOT NULL,
	fk_organ uniqueidentifier NOT NULL,
	constant varchar(max) NOT NULL,
	type int NOT NULL
   CONSTRAINT pk_search_constant  PRIMARY KEY (pk_search_constant),
   CONSTRAINT fk_organ  FOREIGN KEY (fk_organ) REFERENCES organs (pk_organ)
);

insert into organs(pk_organ, organ_name, organ_document, active) values ('7375E9C4-5D74-4213-A78D-4A3462F03948', 'DER RJ', '28521870000125',1)
insert into search_constants(pk_search_constant, fk_organ, constant, type) values (NEWID(), '7375E9C4-5D74-4213-A78D-4A3462F03948', 'REF', 6)
insert into search_constants(pk_search_constant, fk_organ, constant, type) values (NEWID(), '7375E9C4-5D74-4213-A78D-4A3462F03948', 'TIPO', 7)
insert into search_constants(pk_search_constant, fk_organ, constant, type) values (NEWID(), '7375E9C4-5D74-4213-A78D-4A3462F03948', 'OBJETO', 2)
insert into search_constants(pk_search_constant, fk_organ, constant, type) values (NEWID(), '7375E9C4-5D74-4213-A78D-4A3462F03948', 'ORÇAMENTO OFICIAL: ', 3)
insert into search_constants(pk_search_constant, fk_organ, constant, type) values (NEWID(), '7375E9C4-5D74-4213-A78D-4A3462F03948', 'ORÇAMENTO OFICIAL: R$ ', 0)
insert into search_constants(pk_search_constant, fk_organ, constant, type) values (NEWID(), '7375E9C4-5D74-4213-A78D-4A3462F03948', ' (', 1)
insert into search_constants(pk_search_constant, fk_organ, constant, type) values (NEWID(), '7375E9C4-5D74-4213-A78D-4A3462F03948', 'DATA DA LICITAÇÃO: ', 4)
insert into search_constants(pk_search_constant, fk_organ, constant, type) values (NEWID(), '7375E9C4-5D74-4213-A78D-4A3462F03948', ' às', 5)
insert into search_constants(pk_search_constant, fk_organ, constant, type) values (NEWID(), '7375E9C4-5D74-4213-A78D-4A3462F03948', '//body', 8)
insert into search_constants(pk_search_constant, fk_organ, constant, type) values (NEWID(), '7375E9C4-5D74-4213-A78D-4A3462F03948', 'An error occurred on the server when processing the URL.', 9)
insert into search_constants(pk_search_constant, fk_organ, constant, type) values (NEWID(), '7375E9C4-5D74-4213-A78D-4A3462F03948', 'https://www.der.rj.gov.br/licitacao_completo.asp?ident=', 10)
insert into search_constants(pk_search_constant, fk_organ, constant, type) values (NEWID(), '7375E9C4-5D74-4213-A78D-4A3462F03948', '0', 11)

ALTER TABLE organs ADD last_licitation int NOT NULL DEFAULT 0;
update organs set last_licitation = 1500 where pk_organ = '7375E9C4-5D74-4213-A78D-4A3462F03948'

insert into organs(pk_organ, organ_name, organ_document, active, last_licitation) values ('8af68c2b-234a-438f-b869-b92bedaa5d6f', 'DER MG', '17309790000194',1, 2989)
insert into search_constants(pk_search_constant, fk_organ, constant, type) values (NEWID(), '8af68c2b-234a-438f-b869-b92bedaa5d6f', '6', 11)
insert into search_constants(pk_search_constant, fk_organ, constant, type) values (NEWID(), '8af68c2b-234a-438f-b869-b92bedaa5d6f', 'edital ', 6)
insert into search_constants(pk_search_constant, fk_organ, constant, type) values (NEWID(), '8af68c2b-234a-438f-b869-b92bedaa5d6f', ' Detalhes', 7)
insert into search_constants(pk_search_constant, fk_organ, constant, type) values (NEWID(), '8af68c2b-234a-438f-b869-b92bedaa5d6f', 'Objeto: ', 2)
insert into search_constants(pk_search_constant, fk_organ, constant, type) values (NEWID(), '8af68c2b-234a-438f-b869-b92bedaa5d6f', 'Valor Orçado: ', 3)
insert into search_constants(pk_search_constant, fk_organ, constant, type) values (NEWID(), '8af68c2b-234a-438f-b869-b92bedaa5d6f', 'Valor Orçado: R$ ', 0)
insert into search_constants(pk_search_constant, fk_organ, constant, type) values (NEWID(), '8af68c2b-234a-438f-b869-b92bedaa5d6f', 'Data Abertura: ', 1)
insert into search_constants(pk_search_constant, fk_organ, constant, type) values (NEWID(), '8af68c2b-234a-438f-b869-b92bedaa5d6f', 'Data Abertura: ', 4)
insert into search_constants(pk_search_constant, fk_organ, constant, type) values (NEWID(), '8af68c2b-234a-438f-b869-b92bedaa5d6f', ' às', 5)
insert into search_constants(pk_search_constant, fk_organ, constant, type) values (NEWID(), '8af68c2b-234a-438f-b869-b92bedaa5d6f', '//article', 8)
insert into search_constants(pk_search_constant, fk_organ, constant, type) values (NEWID(), '8af68c2b-234a-438f-b869-b92bedaa5d6f', 'An error occurred on the server when processing the URL.', 9)
insert into search_constants(pk_search_constant, fk_organ, constant, type) values (NEWID(), '8af68c2b-234a-438f-b869-b92bedaa5d6f', 'http://www.der.mg.gov.br/', 10)