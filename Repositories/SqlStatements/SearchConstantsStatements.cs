namespace Repositories.SqlStatements
{
    public static class SearchConstantsStatements
    {
        public const string GET_CONSTANTS_BY_DOCUMENT_ORGAN = @"
            SELECT
                constants.pk_search_constant as PkSearchConstant,
                constants.fk_organ as FkOrgan,
                constants.constant as Constant,
                    constants.type as Type
            FROM
            
                search_constants constants
            JOIN
                organs organ on constants.fk_organ = organ.pk_organ
            WHERE
                organ.organ_document = @DocumentOrgan
            ";
    }
}
