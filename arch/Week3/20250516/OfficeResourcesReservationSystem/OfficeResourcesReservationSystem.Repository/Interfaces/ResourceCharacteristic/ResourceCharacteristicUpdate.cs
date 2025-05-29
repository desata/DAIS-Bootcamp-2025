using System.Data.SqlTypes;

namespace OfficeResourcesReservationSystem.Repository.Interfaces.ResourceCharacteristic
{
    public class ResourceCharacteristicUpdate
    {
        public SqlString? Name { get; set; }
        public SqlString? Value { get; set; }
    }
}
