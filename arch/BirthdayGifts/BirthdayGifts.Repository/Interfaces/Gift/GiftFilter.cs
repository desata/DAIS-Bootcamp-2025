using System.Data.SqlTypes;

namespace BirthdayGifts.Repository.Interfaces.Gift
{
    public class GiftFilter
    {
        public SqlString? Name { get; set; }
    }
}