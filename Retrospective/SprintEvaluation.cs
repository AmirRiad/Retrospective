//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Retrospective
{
    using System;
    using System.Collections.Generic;
    
    public partial class SprintEvaluation
    {
        public long Id { get; set; }
        public int MemberId { get; set; }
        public int SprintId { get; set; }
        public int Value { get; set; }
    
        public virtual AgileMember AgileMember { get; set; }
        public virtual Sprint Sprint { get; set; }
    }
}
