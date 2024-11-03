namespace Barbearia.Application.DTOs.Usuario
{
    public class UsuarioTokenDTO
    {
        public string Token { get; set; }
        public DateTime Expiracao { get; set; }
        public bool Sucesso { get; set; }
        public List<string> Erros { get; set; }
    }
}
