namespace ServiceContracts;

public interface IHashingService
{
    string HashUrl(string urlLong, long number);
}