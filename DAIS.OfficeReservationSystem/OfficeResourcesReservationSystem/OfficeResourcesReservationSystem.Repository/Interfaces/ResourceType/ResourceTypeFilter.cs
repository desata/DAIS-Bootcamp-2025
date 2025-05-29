using System.Data.SqlTypes;

namespace OfficeResourcesReservationSystem.Repository.Interfaces.ResourceType
{
    public class ResourceTypeFilter
    {
        public SqlString? Name { get; set; }
    }
}