namespace LibraLoan.Data
{
    public interface IAppDbContextFactory
    {
        AppDbContext CreateDbContext();
    }
}
