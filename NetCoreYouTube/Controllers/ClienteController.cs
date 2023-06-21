﻿using Microsoft.AspNetCore.Mvc;
using NetCoreYouTube.Models;
using System.Security.Claims;

namespace NetCoreYouTube.Controllers
{
    [ApiController]
    [Route("cliente")]
    public class ClienteController : ControllerBase
    {
        [HttpGet]
        [Route("listar")]
        public dynamic listarCliente()
        {
            //Todo el codigo

            List<Cliente> clientes = new List<Cliente>
            {
                new Cliente
                {
                    id = "1",
                    correo = "google@gmail.com",
                    edad = "19",
                    nombre = "Bernardo Peña"
                },
                new Cliente
                {
                    id = "2",
                    correo = "miguelgoogle@gmail.com",
                    edad = "23",
                    nombre = "Miguel Mantilla"
                }
            };

            return clientes;
        }

        [HttpGet]
        [Route("listarxid")]
        public dynamic listarClientexid(int codigo)
        {
            //obtienes el cliente de la db

            return new Cliente
            {
                id = codigo.ToString(),
                correo = "google@gmail.com",
                edad = "19",
                nombre = "Bernardo Peña"
            };
        }

        [HttpPost]
        [Route("guardar")]
        public dynamic guardarCliente(Cliente cliente)
        {
            //Guardar en la db y le asignas un id
            cliente.id = "3";

            return new
            {
                success = true,
                message = "cliente registrado",
                result = cliente
            };
        }

        [HttpPost]
        [Route("eliminar")]
        public dynamic eliminarCliente(Cliente cliente)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;


            var rToken = Jwt.validarToken(identity);

            if (!rToken.success) return rToken;

            Usuario usuario = rToken.result;

            if(usuario.Rol != "Administrador")
            {
                return new
                {
                    success = false,
                    message = "No tienes permiso de administrador",
                    result = ""
                };
            }

            return new
            {
                success = true,
                message = "cliente eliminado",
                result = cliente
            };
        }
    }
}
