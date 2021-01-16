using System.ComponentModel.DataAnnotations;

namespace chatApi.InputModels
{
    public class SignInUserInputModel
    {
        [MaxLength(100)]
        public string UserName { get; set; }

        public string Password { get; set; }
    }
}