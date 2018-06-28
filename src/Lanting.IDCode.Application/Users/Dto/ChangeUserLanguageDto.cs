using System.ComponentModel.DataAnnotations;

namespace Lanting.IDCode.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}