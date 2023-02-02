using dgt.power.common.Logic;

namespace dgt.power.common;

public interface IProfileManager
{
    string Current { get; }
    Identity? CurrentIdentity { get; }
    string CurrentConnectionString { get; }
    IIdentities GetIdentities();
    void Save();
    void Purge();
}
