using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Czytnik_Czujników
{
    public class DbConnectionData
    {
        public string server = "192.168.0.20";
        public string database = "czujniki";
        public string user = "czujnik";
        public string _password = "kontroler123";
        
        public string GetConnectionString()
        {
            return $"server={server};database={database};user={user};password={_password}";
        }
    }
}
