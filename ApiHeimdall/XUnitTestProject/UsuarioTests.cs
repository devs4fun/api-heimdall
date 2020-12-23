using ApiHeimdall.Controllers;
using ApiHeimdall.Interfaces;
using ApiHeimdall.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace XUnitTestProject
{
    public class UsuarioTests
    {
        [Trait("UsuarioController", "CadastrarUsuario")]
        [Fact(DisplayName = "01-DeveriaSalvarUsuario")]
        public void DeveriaSalvarUsuario()
        {
            //Arrange
            var UsuarioRepositoryMock = new Mock<IUsuarioRepository>();
            var TokenRepositoryMock = new Mock<ITokenRepository>();
            var sutUsuarioController = new UsuarioController(UsuarioRepositoryMock.Object, TokenRepositoryMock.Object);
            var sutTokenController = new TokenController(TokenRepositoryMock.Object, UsuarioRepositoryMock.Object);

            Usuario usuarioFake = new Usuario()
            {
                Email = "robsonjunior1994@gmail.com",
                Ativo = false,
                NomeCompleto = "Eneas Martins",
                Senha = "eneas12345",
                UserName = "eneasmartins"
            };

            Usuario userDoBanco = null;
            UsuarioRepositoryMock.Setup(u => u.Cadastrar(usuarioFake));
            UsuarioRepositoryMock.Setup(u => u.BuscarPorUserName(usuarioFake.UserName)).Returns(userDoBanco);
            UsuarioRepositoryMock.Setup(u => u.BuscarPorEmail(usuarioFake.Email)).Returns(usuarioFake);

            //Act
            var retorno = sutUsuarioController.Post(usuarioFake) as OkObjectResult;
            

            //Assert
            Assert.True(retorno.StatusCode == 200);
        }

        [Trait("UsuarioController", "CadastrarUsuario")]
        [Theory(DisplayName = "03-DeveriaNaoSalvarQuandoUsuarioInvalido")]
        [InlineData("", "Eneas Martins", "eneas12345", "eneasmartins")]
        [InlineData("robsonjunior1994@gmail.com", "", "eneas12345", "eneasmartins")]
        [InlineData("robsonjunior1994@gmail.com", "Eneas Martins", "", "eneasmartins")]
        [InlineData("robsonjunior1994@gmail.com", "Eneas Martins", "eneas12345", "")]
        public void DeveriaNaoSalvarSeUsuarioInvalido(string email, string nome, string senha, string user)
        {
            //Arrange
            var UsuarioRepositoryMock = new Mock<IUsuarioRepository>();
            var TokenRepositoryMock = new Mock<ITokenRepository>();
            var sutUsuarioController = new UsuarioController(UsuarioRepositoryMock.Object, TokenRepositoryMock.Object);
            var sutTokenController = new TokenController(TokenRepositoryMock.Object, UsuarioRepositoryMock.Object);

            Usuario usuarioFake = new Usuario()
            {
                Email = email,
                Ativo = false,
                NomeCompleto = nome,
                Senha = senha,
                UserName = user
            };
            UsuarioRepositoryMock.Setup(u => u.Cadastrar(usuarioFake));

            //Act
            var retorno = sutUsuarioController.Post(usuarioFake) as BadRequestObjectResult;

            //Assert
            Assert.True(retorno.StatusCode == 400);
        }

        [Trait("UsuarioController", "CadastrarUsuario")]
        [Fact(DisplayName = "02-DeveriaNaoSalvarQuandoUsuarioExiste")]
        public void DeveriaNaoSalvarQuandoUsuarioExiste()
        {
            //Arrange
            var UsuarioRepositoryMock = new Mock<IUsuarioRepository>();
            var TokenRepositoryMock = new Mock<ITokenRepository>();
            var sutUsuarioController = new UsuarioController(UsuarioRepositoryMock.Object, TokenRepositoryMock.Object);
            var sutTokenController = new TokenController(TokenRepositoryMock.Object, UsuarioRepositoryMock.Object);

            Usuario usuarioFake = new Usuario()
            {
                Id = 1,
                Email = "robsonjunior1994@gmail.com",
                Ativo = false,
                NomeCompleto = "Eneas Martins",
                Senha = "eneas12345",
                UserName = "eneasmartins"
            };
            UsuarioRepositoryMock.Setup(u => u.Cadastrar(usuarioFake));
            UsuarioRepositoryMock.Setup(u => u.BuscarPorUserName(usuarioFake.UserName)).Returns(usuarioFake);

            //Act
            var retorno = sutUsuarioController.Post(usuarioFake) as BadRequestObjectResult;

            //Assert
            Assert.True(retorno.StatusCode == 400);
        }
      
        [Trait("UsuarioController", "ValidarChave")]
        [Theory(DisplayName = "04-DeveriaFalharAoTentarValidarChave")]
        [InlineData("         ")]
        [InlineData("")]
        [InlineData("ChaveQueNaoExiste")]
        public void DeveriaFalharAoTentarValidarChave(string chave)
        {
            //Arrange
            var UsuarioRepositoryMock = new Mock<IUsuarioRepository>();
            var TokenRepositoryMock = new Mock<ITokenRepository>();
            var sutUsuarioController = new UsuarioController(UsuarioRepositoryMock.Object, TokenRepositoryMock.Object);
            Usuario usuario = null;
            UsuarioRepositoryMock.Setup(u => u.BuscarChave(chave)).Returns(usuario);

            //Act
            var retorno = sutUsuarioController.Post(chave) as BadRequestObjectResult;

            //Assert
            Assert.True(retorno.StatusCode == 400);
        }
      
        [Trait("UsuarioController", "Login")]
        [Theory(DisplayName = "05-DeveriaLogarQuandoDadosCorretos")]
        [InlineData("", "eneasmartins", "eneas123")]
        [InlineData("eneasmartins.socialrj@gmail.com", "", "eneas123")]
        public void DeveriaLogarQuandoDadosCorretos(string email, string username, string senha)
        {
            //Arrange
            var UsuarioRepositoryMock = new Mock<IUsuarioRepository>();
            var TokenRepositoryMock = new Mock<ITokenRepository>();
            var sutUsuarioController = new UsuarioController(UsuarioRepositoryMock.Object, TokenRepositoryMock.Object);
            var sutTokenController = new TokenController(TokenRepositoryMock.Object, UsuarioRepositoryMock.Object);

            Usuario usuarioFake = new Usuario()
            {
                Email = email,
                Senha = senha,
                UserName = username
            };
            Usuario usuarioBancoFake = new Usuario()
            {
                Id = 1,
                Email = "eneasmartins.socialrj@gmail.com",
                Ativo = true,
                NomeCompleto = "Eneas Martins",
                Senha = "eneas12345",
                UserName = "eneasmartins"
            };
            UsuarioRepositoryMock.Setup(u => u.BuscarUsuario(usuarioFake)).Returns(usuarioBancoFake);
            //Act
            var retorno = sutUsuarioController.Login(usuarioFake) as OkObjectResult;

            //Assert
            Assert.True(retorno.StatusCode == 200);
        }

        [Trait("UsuarioController", "Login")]
        [Theory(DisplayName = "06-DeveriaNaoLogarQuandoDadosIncorretos")]
        [InlineData("", "eneasmartins", "eneas123")]
        [InlineData("eneasmartins.socialrj@gmail.com", "", "eneas123")]
        [InlineData("eneasmartins.socialrj@gmail.com","","")]
        public void DeveriaNaoLogarQuandoDadosIncorretos(string email, string username, string senha)
        {
            //Arrange
            var UsuarioRepositoryMock = new Mock<IUsuarioRepository>();
            var TokenRepositoryMock = new Mock<ITokenRepository>();
            var sutUsuarioController = new UsuarioController(UsuarioRepositoryMock.Object, TokenRepositoryMock.Object);
            var sutTokenController = new TokenController(TokenRepositoryMock.Object, UsuarioRepositoryMock.Object);

            Usuario usuarioFake = new Usuario()
            {
                Email = email,
                Senha = senha,
                UserName = username
            };
            Usuario usuarioBancoFake = new Usuario()
            {
                Id = 1,
                Email = "eneasmartins.socialrj@gmail.com",
                Ativo = false,
                Senha = "eneas123",
                UserName = "eneasmartins"
            };
            Token tokenFake = new Token()
            {
                DataLimite = DateTime.Now.AddDays(3),
                Email = "eneasmartins.socialrj@gmail.com",
                Id = 1,
                Valor = "AUSDIAJSDiJAIFJASKDJAOISSDJ"
            };
            UsuarioRepositoryMock.Setup(u => u.BuscarUsuario(usuarioFake)).Returns(usuarioBancoFake);
            UsuarioRepositoryMock.Setup(u => u.BuscarPorEmail("eneasmartins.socialrj@gmail.com")).Returns(usuarioBancoFake);
            //Act
            var retorno = sutUsuarioController.Login(usuarioFake) as BadRequestObjectResult;

            //Assert
            Assert.True(retorno.StatusCode == 400);
        }

        [Trait("UsuarioController", "Deveria falhar o cadastro de usuario com o Username que ja existe")]
        [Fact(DisplayName = "07-Deveriafalharocadastrodeusuariocomousernamequejaexiste")]
        public void Deveriafalharocadastrodeusuariocomousernamequejaexiste()
        {
            //Arrange
            var MockIUsuarioRepository = new Mock<IUsuarioRepository>();
            var MockITokenRepository = new Mock<ITokenRepository>();
            var sutUsuarioController = new UsuarioController(MockIUsuarioRepository.Object, MockITokenRepository.Object);

            var UsuarioFake = new Usuario()
            {
                Id = 0,
                 Ativo = false,
                  Chave = null,
                   Email = "robsonjunior1994@hotmail.com",
                    NomeCompleto = "Robson Ribeiro",
                     Senha = "123456789",
                      UserName = "robsonjunior1994"
            };

            var UsuarioDoBanco = new Usuario()
            {
                Id = 1,
                Ativo = true,
                Chave = null,
                Email = "robsonjunior1994@gmail.com",
                NomeCompleto = "Robson Ribeiro",
                Senha = "25f9e794323b453885f5181f1b624d0b",
                UserName = "robsonjunior1994"
            };

            MockIUsuarioRepository.Setup(u => u.BuscarPorUserName(UsuarioFake.UserName)).Returns(UsuarioDoBanco);

            //Act
            var resultado = sutUsuarioController.Post(UsuarioFake) as BadRequestObjectResult;

            //Assert
            MockIUsuarioRepository.Verify(u => u.Cadastrar(UsuarioFake), Times.Never);
            Assert.True(resultado.StatusCode == 400);
        }
    }
}
    