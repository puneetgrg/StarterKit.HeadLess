using EPiServer.Validation;
using StarterKit.HeadLess.CMS.Implemenation.Models.Blocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterKit.HeadLess.CMS.Implemenation.Infrastructure.Validators
{
    public class PageBannerBlockValidator : IValidate<PageBannerBlock>
    {
        //example for the block validators
        IEnumerable<ValidationError> IValidate<PageBannerBlock>.Validate(PageBannerBlock block)
        {
            var validationErrors = new List<ValidationError>();

            //if (!ContentReference.IsNullOrEmpty(block.Svg) && string.IsNullOrEmpty(block.SvgColor))
            //
            //    validationErrors.Add(new ValidationError
            //    
            //        ErrorMessage =
            //            "Icon color is required, please choose a color using the dropdown menu.",
            //        Severity = ValidationErrorSeverity.Error,
            //        ValidationType = ValidationErrorType.PropertyValidation
            //    )
            //

            return validationErrors;
        }
    }
}
