using Moq;
using System;
using Xunit;
using ApiHeimdall.Interfaces;
using ApiHeimdall.Models;
using ApiHeimdall.Controllers;
using ApiHeimdall.Services;
using Microsoft.AspNetCore.Mvc;

namespace XUnitTestProject1
{
    public class UnitTest1
    {
        [Trait("","")]
        [Fact(DisplayName = "01-DeveriaEnviarUmEmailDeconfirmacao")]
        public void DeveriaEnviarUmEmailDeconfirmacao()
        {
            //Arrange
            var UsuarioRepositoryMock = new Mock<IUsuarioRepository>();
            var TokenRepositoryMock = new Mock<ITokenRepository>();
            var EmailSenderMock = new Mock<IEmailSender>();
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
            var sutTokenController = new TokenController(TokenRepositoryMock.Object, UsuarioRepositoryMock.Object , EmailSenderMock.Object);

            //Act
            var retorno = sutTokenController.Post("robsonjunior1994@gmail.com") as OkObjectResult;

            //Assert
            Assert.True(retorno.StatusCode == 200);

        }
    }
}
