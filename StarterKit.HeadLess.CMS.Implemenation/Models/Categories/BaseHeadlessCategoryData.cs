using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using Geta.Optimizely.Categories;
using System.ComponentModel.DataAnnotations;

namespace StarterKit.HeadLess.CMS.Implemenation.Models.Categories
{
    [ContentType(DisplayName = "Category", GUID = "fa84e04b-a60a-4f38-a8a4-a7aac7dafcd6",
        Description = "Use this type to create content categorizations and tagging.")]
    public class HeadlessBaseCategoryData : CategoryData, IDisplayable
    {
        [CultureSpecific]
        [Required]
        [Display(Name = "Display Name",
            GroupName = SystemTabNames.Content,
            Order = 10)]
        public virtual string DisplayName { get; set; }
    }

    public interface IDisplayable
    {
        string DisplayName { get; }
    }
}
