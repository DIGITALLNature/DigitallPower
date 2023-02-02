namespace dgt.power.common.Logic;

public class Identity
{
    public required string ConnectionString { get; init; }
    public string SecurityProtocol { get; init; } = "Tls12";
    public bool Insecure { get; init; }
}
