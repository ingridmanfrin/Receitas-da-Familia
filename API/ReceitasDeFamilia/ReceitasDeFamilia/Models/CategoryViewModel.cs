using Microsoft.AspNetCore.Mvc.ModelBinding;
using ReceitasDeFamilia.Services;
using System.Text.Json.Serialization;

namespace ReceitasDeFamilia.Models
{
    public class CategoryViewModel : ModelBase
    {
        [JsonPropertyName("Id")]
        [BindRequired]
        public int CategoryId { get; set; } 

        [BindRequired]
        public int UserId { get; set; } 

        [JsonRequired]
        public string Name { get; set; }

        [JsonRequired]
        public double TargetPercentage { get; set; }
    }
}
