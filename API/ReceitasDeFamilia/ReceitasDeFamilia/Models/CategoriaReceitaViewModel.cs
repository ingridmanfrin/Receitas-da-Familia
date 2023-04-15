using Microsoft.AspNetCore.Mvc.ModelBinding;
using ReceitasDeFamilia.Services;
using System.Text.Json.Serialization;

namespace ReceitasDeFamilia.Models
{
    public class CategoriaReceitaViewModel : ModelBase
    {
        [JsonPropertyName("Id")]
        [BindRequired]
        public int CategoryId { get; set; } 

        [JsonRequired]
        public string Nome { get; set; }
    }
}
