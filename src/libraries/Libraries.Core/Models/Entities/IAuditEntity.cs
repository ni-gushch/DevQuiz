using System;

namespace DevQuiz.Libraries.Core.Models.Entities
{
    /// <summary>
    /// Interface for models whitch need audit information
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
    
    /// <summary>
    /// Interface for models whitch need audit information
    /// </summary>
    public interface IAuditEntity<TKey> : IHasKey<TKey>
        where TKey :IEquatable<TKey>
    {    }
}