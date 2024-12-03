using System.ComponentModel.DataAnnotations;

namespace MediPlusMVC.DTO.UserDTO
{
    public class LoginUserDto
    {
        [Required]
        [Display(Prompt = "Email or Username")]
        public string EmailOrUserName { get; set; }

        [Required]
        [Display(Prompt = "Password")]
        public int Password { get; set; }
    }
}
