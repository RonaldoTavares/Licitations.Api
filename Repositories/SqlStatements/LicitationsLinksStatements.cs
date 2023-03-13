namespace Repositories.SqlStatements
{
	public class LicitationsLinksStatements
    {
        public const string GET_BY_STATUS = @"
		select 
            pk_licitations_links as PkLicitationLink, 
            link as Link, 
            status as Status
        from 
            licitations_links 
        where 
            status = {0}
        ";
    }
}
