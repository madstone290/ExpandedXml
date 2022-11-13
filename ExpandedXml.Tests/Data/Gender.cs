using System.ComponentModel.DataAnnotations;

namespace ExpandedXml.Tests.Data
{
    public enum Gender
    {
        [Display(Name ="남자")]
        Male,
        [Display(Name = "여자")]
        Female
    }
}
