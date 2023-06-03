using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json.Serialization;

namespace ReceitasDeFamilia.Models
{
    public class ReceitaViewModel : ModelBase
    {
        [JsonPropertyName("Id")]
        [BindRequired]
        public int IdReceita { get; set; }

        [JsonRequired]
        public int IdFamilia { get; set; }

        [JsonRequired]
        public int IdCategoria { get; set; }

        [JsonRequired]
        public string Nome { get; set; }

        public string? CriadorReceita { get; set; }

        public int? TempoPreparoMin { get; set; }

        public string? Rendimento { get; set; }

        [JsonRequired]
        public string Ingredientes { get; set; } = null!;

        [JsonRequired]
        public string ModoPreparo { get; set; } = null!;

        public string? InformacoesAdicionais { get; set; }

        public bool Favorito { get; set; }
    }
}
