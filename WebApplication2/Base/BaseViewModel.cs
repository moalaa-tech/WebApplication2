using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.Base
{
    /// <summary>
    /// Base class for all ViewModels with common properties and validation
    /// </summary>
    public abstract class BaseViewModel
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.UtcNow;
        public DateTime? ModificationDate { get; set; }
        public bool IsActive { get; set; } = true;
    }

    /// <summary>
    /// Base class for Create ViewModels
    /// </summary>
    public abstract class BaseCreateViewModel
    {
        // Base properties for creation
        public DateTime CreationDate { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;
    }

    /// <summary>
    /// Base class for Update ViewModels  
    /// </summary>
    public abstract class BaseUpdateViewModel : BaseViewModel
    {
        [Required]
        public new int Id { get; set; }
        public new DateTime? ModificationDate { get; set; } = DateTime.UtcNow;
    }
}