namespace SallesWebMVC.Services.Exceptions
{
    public class DbConcurrencyException: ApplicationException
    {
        public DbConcurrencyException(string exception) : base(exception)
        {

        }
    }
}
