using System.ComponentModel.DataAnnotations;

namespace StarterKit.HeadLess.CMS.Implemenation.Models.Properties
{
    public class JumpLink
    {
        [Required]
        [Display(
            Name = "Title",
            Description = "Title",
            Order = 10)]
        public virtual string Title { get; set; }

        [Required]
        [Display(
            Name = "Anchor Id",
            Description = "Anchor Id",
            Order = 20)]
        public virtual string AnchorId { get; set; }
    }
}
