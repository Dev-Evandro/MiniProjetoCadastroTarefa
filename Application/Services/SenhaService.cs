using Core.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class SenhaService : ISenhaService
    {
        private readonly PasswordHasher<USUARIO> _passwordHasher = new();

        public string GerarHash(string senha)
        {
            if (string.IsNullOrWhiteSpace(senha))
                throw new ArgumentException("Informe a senha.");

            return _passwordHasher.HashPassword(
                new USUARIO(),
                senha);
        }

        public bool VerificarSenha(
            string senha,
            string senhaHash)
        {
            if (string.IsNullOrWhiteSpace(senha) ||
                string.IsNullOrWhiteSpace(senhaHash))
            {
                return false;
            }

            var resultado = _passwordHasher.VerifyHashedPassword(
                new USUARIO(),
                senhaHash,
                senha);

            return resultado is
                PasswordVerificationResult.Success or
                PasswordVerificationResult.SuccessRehashNeeded;
        }
    }
}
