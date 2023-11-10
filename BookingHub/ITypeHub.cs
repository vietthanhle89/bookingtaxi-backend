namespace bookingtaxi_backend.BookingHub
{
    public interface ITypeHub
    {
        Task ReceivedMessage(string username, string message);
        Task ReceivedMessage(string message);

    }
}
