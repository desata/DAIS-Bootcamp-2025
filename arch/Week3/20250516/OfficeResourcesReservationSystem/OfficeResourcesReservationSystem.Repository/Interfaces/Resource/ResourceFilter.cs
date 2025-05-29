using System.Data.SqlTypes;

namespace OfficeResourcesReservationSystem.Repository.Interfaces.Resource
{
    public class ResourceFilter
    {
        public SqlInt32? ResourceId { get; set; }
        
        public SqlString? Name { get; set; }

        public SqlInt32? ResourceTypeId { get; set; }
        
    }
}
