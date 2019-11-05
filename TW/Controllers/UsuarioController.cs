using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TW.Models;
using TW.Repositorios;

namespace TW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]

    public class UsuarioController : ControllerBase
    {
        UsuarioRepositorio repositorio = new UsuarioRepositorio();

        [HttpGet]
        public async Task<ActionResult<List<Usuario>>> Get() //definição do tipo de retorno
        {
            try
            {
                return await repositorio.Get();
                //await vai esperar traser a lista para armazenar em Categoria
            }
            catch (System.Exception)
            {
                throw;
            } 
            
        }       

        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetAction(int id)
        {
            Usuario usuarioRetornado = await repositorio.Get(id);
            if(usuarioRetornado == null)
            {
                return NotFound();
            }
            return usuarioRetornado;
        }

        [HttpPost]
        public async Task<ActionResult<Usuario>> Post(Usuario usuario) //tipo do objeto que está sendo enviado (Categoria) - nome que você determina pro objeto
        {
            if (await repositorio.ValidaEmail (usuario)) {
                return BadRequest ();
            } else {
                try {

                    var usr = await repositorio.Post (usuario);

                    SendMail (usuario.Email);
;
                    return usr;

                } catch (System.Exception) {
                    throw;
                }
            }
        }

        public bool SendMail (string email) {
            try {
                // Estancia da Classe de Mensagem
                MailMessage _mailMessage = new MailMessage ();
                // Remetente
                _mailMessage.From = new MailAddress ("lxpvnwr7@gmail.com");

                // Destinatario seta no metodo abaixo

                //Contrói o MailMessage
                _mailMessage.CC.Add (email);
                _mailMessage.Subject = "TESTELIGHT CODE XP";
                _mailMessage.IsBodyHtml = true;
                _mailMessage.Body = "<b>Olá Tudo bem ??</b><p>Teste Parágrafo</p>";

                //CONFIGURAÇÃO COM PORTA
                SmtpClient _smtpClient = new SmtpClient ("smtp.gmail.com", Convert.ToInt32 ("587"));

                //CONFIGURAÇÃO SEM PORTA

                // SmtpClient _smtpClient = new SmtpClient(UtilRsource.ConfigSmtp);

                // Credencial para envio por SMTP Seguro (Quando o servidor exige autenticação);

                _smtpClient.UseDefaultCredentials = false;

                _smtpClient.Credentials = new NetworkCredential ("lxpvnwr7@gmail.com", "0736867444@");

                _smtpClient.EnableSsl = true;

                _smtpClient.Send (_mailMessage);

                return true;

            } catch (Exception ex) {
                throw ex;
            }
        }

        [HttpPost("{nome_usuario}")]

        public async Task<ActionResult<Usuario>> PostUserName(Usuario usuario)
        {
            try
            {
                await repositorio.PostUserName(usuario);
            }
            catch (System.Exception)
            {
                
                throw;
            }
            return usuario;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Usuario>> Put(int id, Usuario usuario)
        {
            if(id != usuario.IdUsuario)
            {
                return BadRequest();
            }
            try
            {
               return await repositorio.Put(usuario);
                
            }
            catch (DbUpdateConcurrencyException)
            {
                var usuarioValido = await repositorio.Get(id);
                if(usuarioValido == null)
                {
                    return NotFound();
                }else{
                    throw;
                }
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Usuario>> Delete(int id)
        {
            Usuario usuarioRetornado = await repositorio.Get(id);
            if(usuarioRetornado == null)
            {
                return NotFound();
            }
            await repositorio.Delete(usuarioRetornado);
            return usuarioRetornado;
        }
    }
}