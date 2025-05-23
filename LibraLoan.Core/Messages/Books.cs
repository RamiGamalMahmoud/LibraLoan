namespace LibraLoan.Core.Messages
{
    public static class Books
    {
        public record BookLoanedMessage(int Id);
        public record BookReturnedMessage(int Id);
    }
}
