using System.Text.Json.Serialization;

namespace ReceitasDeFamilia.Models
{
    public class LoginViewModel
    {
        public string? Nome { get; set; }

        public string Email { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string? Senha { get; set; }
    }
}
