using EPiServer.DataAnnotations;
using EPiServer.DataAbstraction;
using System.ComponentModel.DataAnnotations;

namespace StarterKit.HeadLess.CMS.Implemenation.Models.Categories
{
    [ContentType(DisplayName = "Starter Kit Category", GUID = "4BBFBEAD-E095-4864-A7C0-1E92BE5D84DC",
     Description = "Use this type to create the Blog Categories for Blog Category Pages")]
    public class starterKitCategory : HeadlessBaseCategoryData, IDisplayable
    {
        [CultureSpecific]
        [Required]
        [Display(Name = "Display Name",
           GroupName = SystemTabNames.Content,
           Order = 10)]
        public override string DisplayName { get; set; }
    }
}
