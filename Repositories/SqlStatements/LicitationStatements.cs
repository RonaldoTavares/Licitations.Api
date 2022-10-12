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
			create_date,
			update_date
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
			@CreateDate,
			GETDATE()
			)
		";

		public const string GET_LICITATION_BY_STATUS = @"
		SELECT
			pk_licitation as PkLicitation,
			notice as Notice,
			object as Object,
			organ_name as OrganName,
			organ_document as OrganDocument,
			value as Value,
			opening_date as OpeningDate,
			status as Status,
			link as Link,
			create_date as CreateDate
		FROM 
			licitations 
		WHERE 
			status = @Status
		";

		public const string UPDATE_LICITATION_STATUS_BY_ID = @"
			UPDATE 
				licitations 
			SET 
				status = @Status,
				update_date = GETDATE()
			WHERE 
				pk_licitation = @PkLicitation
		";
	}
}
