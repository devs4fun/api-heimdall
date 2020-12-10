using Moq;
using System;
using Xunit;
using ApiHeimdall.Interfaces;
using ApiHeimdall.Models;
using ApiHeimdall.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace XUnitTestProject1
{
    public class TokenTests
    {
        [Trait("TokenController", "Enviar e-mail para validação do cadastro")]
        [Fact(DisplayName = "01-Deveria Enviar Um Email De Validacao De Cadastro")]
        public void DeveriaEnviarUmEmailDeconfirmacao()
        {
            //Arrange
            var UsuarioRepositoryMock = new Mock<IUsuarioRepository>();
            var TokenRepositoryMock = new Mock<ITokenRepository>();
            Usuario usuarioFake = new Usuario()
            {
                Id = 1,
                Email = "robsonjunior1994@gmail.com",
                Ativo = false,
                NomeCompleto = "Robson Junior",
                Senha = "123",
                UserName = "robsonjunior1994"
            };
            UsuarioRepositoryMock.Setup(u => u.BuscarPorEmail("robsonjunior1994@gmail.com")).Returns(usuarioFake);
            var sutTokenController = new TokenController(TokenRepositoryMock.Object, UsuarioRepositoryMock.Object);

            //Act
            var retorno = sutTokenController.Post("robsonjunior1994@gmail.com") as OkObjectResult;

            //Assert
            Assert.True(retorno.StatusCode == 200);

        }

        [Trait("TokenController", "Deveria Falhar Se A DataLimite Ja Estiver Expirado")]
        [Fact(DisplayName = "02-Deveria Falhar Se A DataLimite Ja Estiver Expirado")]
        public void DeveriaFalharSeADataLimiteJaEstiverExpirado()
        {
            //Arrange
            var UsuarioRepositoryMock = new Mock<IUsuarioRepository>();
            var TokenRepositoryMock = new Mock<ITokenRepository>();
            var sutTokenController = new TokenController(TokenRepositoryMock.Object, UsuarioRepositoryMock.Object);
            var token = new Token()
            {
                Id = 1,
                DataLimite = DateTime.Now.AddDays(-5),
                Email = "robsonjunior1994@gmail.com",
                Valor = "59585b3f39c184361a7a5d11a8423da8"
            };

            Usuario usuarioFake = new Usuario()
            {
                Id = 1,
                Email = "robsonjunior1994@gmail.com",
                Ativo = false,
                NomeCompleto = "Robson Junior",
                Senha = "123",
                UserName = "robsonjunior1994"
            };
            

            //Act
            TokenRepositoryMock.Setup(t => t.BuscarToken("59585b3f39c184361a7a5d11a8423da8")).Returns(token);
            UsuarioRepositoryMock.Setup(u => u.BuscarPorEmail("robsonjunior1994@gmail.com")).Returns(usuarioFake);
            var retorno = sutTokenController.Get("59585b3f39c184361a7a5d11a8423da8") as BadRequestResult;

            //Assert
            UsuarioRepositoryMock.Verify(u => u.Atualizar(usuarioFake), Times.Never);

        }
    }
}
