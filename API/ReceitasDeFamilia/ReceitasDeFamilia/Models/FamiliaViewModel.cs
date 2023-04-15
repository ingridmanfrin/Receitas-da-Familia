using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json.Serialization;

namespace ReceitasDeFamilia.Models
{
    public class FamiliaViewModel : ModelBase
    {
        [JsonPropertyName("Id")]
        [BindRequired]
        public int IdFamilia { get; set; }

        [JsonRequired]
        public string Nome { get; set; }
        
        public string? Descricao{ get; set; }

        public string? Foto{ get; set; }
    }
}
