using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterKit.HeadLess.CMS.Implemenation.Models
{
    public static class Constant
    {
        public static class ButtonStyle
        {
            public const string PrimaryButton = "Primary";
            public const string SecondaryButton = "Secondary";
            public const string TertiaryButton = "Tertiary";
        }

        public static class ButtonStyleClasses
        {
            public const string PrimaryButton = "btn-primary";
            public const string SecondaryButton = "btn-secondary";
            public const string TertiaryButton = "btn-tertiary";
        }

        public static class Alignment
        {
            public const string LeftAlignment = "Left";
            public const string RightAlignment = "Right";
        }

        public static class Height
        {
            public const string ExtraLarge = "XL";
            public const string Large = "L";
            public const string Meduim = "M";
        }

        public static class HeadlineHeading
        {
            public const string H1 = "h1";
            public const string H2 = "h2";
            public const string H3 = "h3";
        }

        public static class VideoAlignment
        {
            public const string Large = "Large";
            public const string Left = "Left";
            public const string Right = "Right";
            public const string FullWidth = "Full";
        }

        public static class CardTheme
        {
            public const string Grey = "Grey";
            public const string Light = "Light";
            public const string Dark = "Dark";
            public const string White = "White";
        }

        public static class ListItemIcon
        {
            public const string RedCircleCheck = "RedCircleCheck";
            public const string RedCirclePlus = "RedCirclePlus";
            public const string GradientCircleCheck = "GradientCircleCheck";
            public const string GradientCirclePlus = "GradientCirclePlus";
        }

        public static class CardAlignment
        {
            public const string LeftAlignment = "Left";
            public const string CenterAlignment = "Center";
        }

        public static class Badges
        {
            public const string New = "New"; //Corresponding key -> Localization key Headless.Badges.New 
            public const string ComingSoon = "ComingSoon"; //Corresponding key -> Localization key Headless.Badges.ComingSoon
            public const string Featured = "Featured"; //Corresponding key -> Localization key Headless.Badges.Featured
        }

        public static class LanguagesConstants
        {
            public const string DefaultCulture = "en";
        }

        public static class PropertyGroupNames
        {
            public const string SEO = "SEO";
            public const string SectionTab_1 = "Section 1";
            public const string SectionTab_2 = "Section 2";
            public const string SectionTab_3 = "Section 3";
            public const string SectionTab_4 = "Section 4";

            public const string Header = "Header";
            public const string Footer = "Footer";
            public const string QuickLinks = "Quick Links";
            public const string Forms = "Forms";
        }

        public static class SearchSortOptions
        {
            public const string Popularity = "Popularity";
            public const string AlphabeticalAscending = "Alphabetical: A-Z";
            public const string AlphabeticalDescending = "Alphabetical: Z-A";
            public const string Newest = "Newest";
            public const string Oldest = "Oldest";
        }

        public static class GenericValidation
        {
            public const string EnglishValidationMessage = "Please limit your {0} input to {1} characters in English. Your current input exceeds the character limit.";
            public const string NonEnglishValidationMessage = "Please limit your {0} input to {1} characters in non-English. Your current input exceeds the character limit.";
            public const string GenericValidationMessage = "Please limit your input {0} to a maximum {1} characters. Your current input exceeds the character limit.";
            public const string SvgImageSelectionValidation = "Please limit your {0} selection to an {1} format. The recommendation is to use only a \"thin\" icon.";

            public const string NewEnglishValidationMessage = "Your ideal {0} input is {1} characters in English. Your current input exceeds the character limit.";
        }
    }
}
