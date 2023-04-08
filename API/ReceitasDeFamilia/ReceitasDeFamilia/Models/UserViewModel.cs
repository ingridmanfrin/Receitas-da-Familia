using ReceitasDeFamilia.Services;
using System.Text.Json.Serialization;

namespace ReceitasDeFamilia.Models
{
    public class UserViewModel : ModelBase
    {
        [JsonPropertyName("Id")]
        //[JsonRequired]
        public int UserId { get; set; }

        public string Nome { get; set; }

        public string? Email { get; set; }

        public bool? IsEmailValidated { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string? Senha { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public string? PasswordSalt { get; set; }

    }
}
