namespace Repositories.SqlStatements
{
	public class OrgansStatements
    {
        public const string GET_ACTIVE_ORGANS = @"
		SELECT
			pk_organ as PkOrgan,
			organ_name as OrganName,
			organ_document as OrganDocument,
			active as Active,
			last_licitation as LastLicitation
		FROM 
			organs 
		WHERE 
			active = 1
        ";

		public const string UPDATE_LAST_LICITATION = @"
		UPDATE 
			organs 
		SET 
			last_licitation = @LastLicitation
		WHERE 
			organ_document = @OrganDocument
		";
    }
}
