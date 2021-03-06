//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AudioView.Common.DataAccess
{
    using System;
    using System.Collections.Generic;
    
    public partial class Project
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Project()
        {
            this.Readings = new HashSet<Reading>();
        }
    
        public System.Guid Id { get; set; }
        public long MinorInterval { get; set; }
        public long MajorInterval { get; set; }
        public string Name { get; set; }
        public System.DateTime Created { get; set; }
        public int MinorDBLimit { get; set; }
        public int MajorDBLimit { get; set; }
        public string Number { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Reading> Readings { get; set; }
    }
}
