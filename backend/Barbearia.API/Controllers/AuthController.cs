using Barbearia.API.Configurations;
using Barbearia.Application.DTOs.Usuario;
using Barbearia.Domain.Entities.Identity;
using Barbearia.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Asp.Versioning;

namespace Barbearia.API.Controllers
{
    [ApiVersion(1.0)]
    [Route("api/v{version:apiVersion}/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly JwtConfig _jwtConfig;

        public AuthController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration, IOptions<JwtConfig> jwtConfig)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _jwtConfig = jwtConfig.Value;
        }

        [HttpPost("registro-cliente")]
        public async Task<ActionResult> RegistroCliente(RegistroClienteDTO modelo)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.SelectMany(e => e.Errors));

            var usuario = new ApplicationUser
            {
                UserName = modelo.Email,
                Email = modelo.Email,
                NomeCompleto = modelo.NomeCompleto,
                PhoneNumber = modelo.Telefone,
                TipoUsuario = TipoUsuario.Cliente,
                Status = true,
                DataCadastro = DateTime.Now
            };

            var resultado = await _userManager.CreateAsync(usuario, modelo.Senha);
            if (resultado.Succeeded)
            {
                await _userManager.AddToRoleAsync(usuario, "Cliente");
                return Ok(await GerarJwt(usuario.Email));
            }

            return BadRequest(resultado.Errors);

        }

        [HttpPost("registro-admin")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> RegistroAdmin(RegistroAdminDTO modelo)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.SelectMany(e => e.Errors));

            var usuario = new ApplicationUser
            {
                UserName = modelo.Email,
                Email = modelo.Email,
                NomeCompleto = modelo.NomeCompleto,
                PhoneNumber = modelo.Telefone,
                TipoUsuario = TipoUsuario.Administrador,
                Status = true,
                DataCadastro = DateTime.Now
            };

            var resultado = await _userManager.CreateAsync(usuario, modelo.Senha);
            if (resultado.Succeeded)
            {
                await _userManager.AddToRoleAsync(usuario, "Admin");
                return Ok(await GerarJwt(usuario.Email));
            }

            return BadRequest(resultado.Errors);
        }


        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginDTO modelo)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.SelectMany(e => e.Errors));

            var resultado = await _signInManager.PasswordSignInAsync(modelo.Email, modelo.Senha, false, true);

            if (resultado.Succeeded)
            {
                return Ok(await GerarJwt(modelo.Email));
            }

            if (resultado.IsLockedOut)
            {
                return BadRequest("Usuário temporariamente bloqueado por tentativas inválidas");
            }

            return BadRequest("Usuário ou Senha incorretos");
        }
           
        [HttpPost("primeiro-admin")]
        public async Task<ActionResult> CriarPrimeiroAdmin([FromBody] RegistroAdminDTO modelo, [FromQuery] string setupKey)
        {
            // Verificar se já existe algum admin
            if (await _userManager.Users.AnyAsync(u => u.TipoUsuario == TipoUsuario.Administrador))
                return BadRequest("Já existe um administrador no sistema");

            // Verificar a chave de setup (defina no appsettings.json)
            if (setupKey != _configuration["SetupKey"])
                return Unauthorized();

            var admin = new ApplicationUser
            {
                UserName = modelo.Email,
                Email = modelo.Email,
                NomeCompleto = modelo.NomeCompleto,
                PhoneNumber = modelo.Telefone,
                TipoUsuario = TipoUsuario.Administrador,
                Status = true,
                DataCadastro = DateTime.Now
            };

            var resultado = await _userManager.CreateAsync(admin, modelo.Senha);
            if (resultado.Succeeded)
            {
                await _userManager.AddToRoleAsync(admin, "Admin");
                return Ok("Administrador inicial criado com sucesso");
            }

            return BadRequest(resultado.Errors);
        }
        

        private async Task<UsuarioTokenDTO> GerarJwt(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.UserData, user.TipoUsuario.ToString())
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Issuer = _jwtConfig.Emissor,
                Audience = _jwtConfig.ValidoEm,
                Expires = DateTime.UtcNow.AddHours(_jwtConfig.ExpiracaoHoras),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            });

            var encodedToken = tokenHandler.WriteToken(token);

            return new UsuarioTokenDTO
            {
                Token = encodedToken,
                Expiracao = DateTime.UtcNow.AddHours(_jwtConfig.ExpiracaoHoras),
                Sucesso = true
            };
        }

    }
}
