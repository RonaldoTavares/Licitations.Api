namespace Repositories.SqlStatements
{
	public static class LicitationStatements
    {
        public const string CREATE_LICITATION = @"
        INSERT INTO licitations (
			pk_licitation,
			organ_name,
			organ_document,
			notice,
			object,
			value,
			status,
			opening_date,
			link,
			create_date
		)
		OUTPUT INSERTED.pk_licitation as PkLicitation
		VALUES (
			@PkLicitation,
			@OrganName,
			@OrganDocument,
			@Notice,
			@Object,
			@Value,
			@Status,
			@OpeningDate,
			@Link,
			@CreateDate
			)
		";
    }
}
