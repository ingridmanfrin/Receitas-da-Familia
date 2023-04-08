using System;
using System.Collections.Generic;

namespace ReceitasDeFamilia.Repositories.Entities;

public partial class CategoriasReceitum
{
    public int IdCategoria { get; set; }

    public string Nome { get; set; } = null!;

    public DateTime DataCriacao { get; set; }

    public DateTime DataAlteracao { get; set; }

    public string UsuarioCriacao { get; set; } = null!;

    public string UsuarioAlteracao { get; set; } = null!;

    public bool FoiDeletado { get; set; }

    public virtual ICollection<Receita> Receita { get; } = new List<Receita>();
}
