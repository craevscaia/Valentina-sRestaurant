namespace Client.ClientService;

public interface IClientService
{
    Task ExecuteCode(CancellationToken cancellationToken);
}