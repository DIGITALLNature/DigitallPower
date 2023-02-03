using System.Linq;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;

// ReSharper disable All 
namespace dgt.power.dataverse
{
   	public partial class DataContext : OrganizationServiceContext
    {
		public DataContext(IOrganizationService service) : base(service) {}
	}
}