using System.Data.SqlTypes;

namespace BirthdayGifts.Repository.Interfaces.Gift
{
    public class GiftUpdate
    {
        public SqlString? Name { get; set; }
        public SqlString? Description { get; set; }
        public SqlDecimal? Price { get; set; }
    }
}