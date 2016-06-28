using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersoManagerLibrary
{
    public class UserModel
    {
        public long ID { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
        public List<FunctionModel> Function { get; set; }
        public BranchModel Branch { get; set; }
        public string Response { get; set; }
    }
}
