using ApiHeimdall.Interfaces;
using ApiHeimdall.RequestResponse;
using ApiHeimdall.Service;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace XUnitTestProject
{
    public class ServiceUsuarioTests
    {
        [Trait("UsuarioService", "Cadastrar")]
        [Fact(DisplayName = "Deveria Cadastrar Usuario")]
        public void DeveriaCadasstrarUsuario()
        {
            //Arrange
            var usuarioRepositoryMock = new Mock<IUsuarioRepository>();
            var usuarioService = new UsuarioService(usuarioRepositoryMock.Object);
            var usuarioRequest = new UsuarioRequest() 
            { NomeCompleto = "Robson Junior", Email = "robson@mail.com", Senha = "12345678", UserName = "robsonjunior1994" };

            //Act
            var retorno = usuarioService.Cadastrar(usuarioRequest);

            //Assert
            Assert.True(retorno);
        }
    }
}
