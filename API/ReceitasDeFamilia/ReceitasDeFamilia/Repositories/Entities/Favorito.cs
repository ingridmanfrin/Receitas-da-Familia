using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ReceitasDeFamilia.Repositories.Entities;

public partial class Favorito
{
    public int IdReceita { get; set; }
    [Key]
    public int IdUsuario { get; set; }

    public DateTime DataCriacao { get; set; }

    public DateTime DataAlteracao { get; set; }

    public string UsuarioCriacao { get; set; } = null!;

    public string UsuarioAlteracao { get; set; } = null!;

    public bool FoiDeletado { get; set; }

    public virtual Receita Receita { get; set; } = null!;

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
