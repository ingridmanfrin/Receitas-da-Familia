using System;
using System.Collections.Generic;

namespace ReceitasDeFamilia.Repositories.Entities;

public partial class Receita
{
    public int IdReceita { get; set; }

    public int IdCategoria { get; set; }

    public int IdFamilia { get; set; }

    public string Nome { get; set; } = null!;

    public string? CriadorReceita { get; set; }

    public int? TempoPreparoMin { get; set; }

    public string? Rendimento { get; set; }

    public string Ingredientes { get; set; } = null!;

    public string ModoPreparo { get; set; } = null!;

    public string? InformacoesAdicionais { get; set; }

    public DateTime DataCriacao { get; set; }

    public DateTime DataAlteracao { get; set; }

    public string UsuarioCriacao { get; set; } = null!;

    public string UsuarioAlteracao { get; set; } = null!;

    public bool FoiDeletado { get; set; }

    public virtual CategoriasReceitum IdCategoriaNavigation { get; set; } = null!;

    public virtual Familia IdFamiliaNavigation { get; set; } = null!;
}
