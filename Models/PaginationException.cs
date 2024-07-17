namespace MagniseFinAPI.Models
{
    public class PaginationException : Exception
    {
        public PaginationException() { }
        public PaginationException(string message) : base(message) { }
    }
}
