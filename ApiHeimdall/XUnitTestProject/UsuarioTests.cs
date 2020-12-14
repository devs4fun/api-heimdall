﻿using ApiHeimdall.Controllers;
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
            UsuarioRepositoryMock.Setup(u => u.Cadastrar(usuarioFake));
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
            var retorno = sutUsuarioController.Post(usuarioFake) as BadRequestResult;

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
            UsuarioRepositoryMock.Setup(u => u.Buscar(usuarioFake.Id)).Returns(usuarioFake);

            //Act
            var retorno = sutUsuarioController.Post(usuarioFake) as BadRequestResult;

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
            var retorno = sutUsuarioController.Post(chave) as BadRequestResult;

            //Assert
            Assert.True(retorno.StatusCode == 400);
        }
    }
}
