using System;

namespace DevQuiz.Admin.Core.Models.Entities
{
    /// <summary>
    /// Interface for models which need audit information
    /// </summary>
    public interface IAuditEntity
    {
        /// <summary>
        /// Created Date
        /// </summary>
        DateTime CreatedDate { get; set; }   
        /// <summary>
        /// Who create entity
        /// </summary>
        string CreatedBy { get; set; }   
        /// <summary>
        /// Update Date
        /// </summary>
        DateTime? UpdatedDate { get; set; }  
    }
}