using System.Data.SqlTypes;

namespace DAIS.WikiSystem.Repository.Interfaces.User
{
    public class UserFilter
    {
        public SqlString? Username { get; set; }
    }
}
