using System.Data.SqlTypes;

namespace OfficeResourcesReservationSystem.Repository.Interfaces.Resource
{
    public class ResourceUpdate
    {
        public SqlString? Name { get; set; }
        public SqlString? Description { get; set; }


    }
}
