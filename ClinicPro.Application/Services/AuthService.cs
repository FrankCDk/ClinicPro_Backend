using AutoMapper;
using ClinicPro.Application.Dtos.Auth;
using ClinicPro.Application.Dtos.Login;
using ClinicPro.Application.Interfaces;
using ClinicPro.Application.Validations.Auth;
using ClinicPro.Core.Entities;
using ClinicPro.Core.Interfaces;
using ClinicPro.Utils;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ClinicPro.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public AuthService(IMapper mapper, IUserRepository userRepository, IConfiguration configuration)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _configuration = configuration;
        }

        #region Login de usuario
        public async Task<LoginResponse> Login(LoginRequest request)
        {
            CreateLoginValidator validationRules = new CreateLoginValidator();
            ValidationResult validationResult = validationRules.Validate(request);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            User? usuario = await _userRepository.Login(_mapper.Map<User>(request));

            if (usuario == null)
            {
                throw new Exception("Usuario no encontrado.");
            }

            LoginResponse response = _mapper.Map<LoginResponse>(usuario);

            if (!response.IsActive)
            {
                throw new Exception("El usuario no se encuentra activado.");
            }

            if (!PasswordHasher.VerifyPassword(request.Password, usuario.Usr_password_hash))
            {
                throw new Exception("La contraseña es incorrecta.");
            }

            response.Token = GenerateJwtToken(usuario);
            return response;
        }
        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = _configuration["Jwt:Key"];
            var time = _configuration["Jwt:ExpirationMinutes"];
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];

            if (string.IsNullOrEmpty(key))
            {
                throw new Exception("JWT key no esta configurado.");
            }

            if (!int.TryParse(time, out int expirationMinutes) || expirationMinutes <= 0)
            {
                throw new Exception("JWT tiempo de expiración no esta configurado correctamente.");
            }

            if (string.IsNullOrEmpty(issuer))
            {
                throw new Exception("JWT issuer no esta configurado.");
            }

            if (string.IsNullOrEmpty(audience))
            {
                throw new Exception("JWT audience no esta configurado.");
            }

            var keyBytes = Encoding.ASCII.GetBytes(key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                [
                    new Claim(ClaimTypes.NameIdentifier, user.Usr_id.ToString()),
                    new Claim(ClaimTypes.Name, user.Usr_first_name),
                    new Claim(ClaimTypes.Email, user.Usr_email),
                    new Claim(ClaimTypes.Role, user.Usr_rol),
                ]),
                Expires = DateTime.Now.AddMinutes(expirationMinutes),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature),
                NotBefore = DateTime.Now
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        #endregion

        #region Registro de usuario
        public async Task Register(RegisterRequest request)
        {

            CreateRegisterValidator validationRules = new CreateRegisterValidator();
            ValidationResult validationResult = validationRules.Validate(request);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            if(request.Password != request.ConfirmPassword)
            {
                throw new Exception("Las contraseñas no coinciden.");
            }

            request.Password = PasswordHasher.HashPassword(request.Password);

            User usuario = _mapper.Map<User>(request);

            bool execute = await _userRepository.Register(usuario);

            if(!execute)
            {
                throw new Exception("No se pudo registrar el usuario.");
            }

        }
        #endregion

    }
}
