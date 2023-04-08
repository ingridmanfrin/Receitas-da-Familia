using System;
using System.Collections.Generic;

namespace ReceitasDeFamilia.Repositories.Entities;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string Nome { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Senha { get; set; } = null!;

    public string Salt { get; set; } = null!;

    public bool EmailValidado { get; set; }

    public int? CodigoValidacaoEmail { get; set; }

    public DateTime DataCriacao { get; set; }

    public DateTime DataAlteracao { get; set; }

    public string UsuarioCriacao { get; set; } = null!;

    public string UsuarioAlteracao { get; set; } = null!;

    public bool FoiDeletado { get; set; }
}
