//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PersoManagerLibrary.ModelLibrary.EntityFrameworkLib
{
    using System;
    using System.Collections.Generic;
    
    public partial class Role
    {
        public Role()
        {
            this.RoleFunctions = new HashSet<RoleFunction>();
            this.Users = new HashSet<User>();
        }
    
        public long ID { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
    
        public virtual ICollection<RoleFunction> RoleFunctions { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
