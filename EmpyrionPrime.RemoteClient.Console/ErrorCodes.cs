namespace EmpyrionPrime.RemoteClient.Console;

public enum ErrorCodes : int
{
    None = 0,
    Unknown = 1,
    ServerConnection = 20,
    RequestError = 21,
    CommandSettings = 30,
    CommandPayload = 31,
}
