using System.ComponentModel.DataAnnotations;

namespace RestaurantApi.ModelsDto.User
{
    public class AddUserDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required] 
        public string LastName { get; set; }
    }
}
