using System.IO;
using BackFront.Senai.MVC.Models;
using BackFront.Senai.MVC.Repositorios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackFront.Senai.MVC.Controllers
{
    public class UsuarioController : Controller // pega a biblioteca 
    {
        [HttpGet]
        public ActionResult Cadastro(){
            ViewData["Comentarios"] = new ComentarioRepositorioSerializacao().Listar();
            return View();
        }

        [HttpPost]
        //criando um formulario
        public ActionResult Cadastro (IFormCollection form)
        {
            //criando um objeto 
            UsuarioModel usuarioModel = new UsuarioModel();

            int id = 0;

            bool admin = false;

            if (System.IO.File.Exists("usuario.csv"))
            {
                string[] lines = System.IO.File.ReadAllLines("usuario.csv");
                id = lines.Length + 1;
            }
            else
            {
                id=1;
            }

            usuarioModel.Id = id;
            usuarioModel.Nome = form["nome"];
            usuarioModel.Email = form["email"];
            usuarioModel.Senha = form["senha"];
            usuarioModel.Administrador = admin;

            using(StreamWriter sw = new StreamWriter("usuario.csv", true)){
                sw.WriteLine ($"{usuarioModel.Id};{usuarioModel.Nome};{usuarioModel.Email};{usuarioModel.Senha};{usuarioModel.Administrador}");

            };

            ViewBag.Mensagem = "Usuário Cadastrado";
          
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login (IFormCollection form){
            UsuarioModel usuario = new UsuarioModel{
                Email= form["email"],
                Senha = form["senha"]
            };
            
            UsuarioRepositorio usuarioRepositorio = new UsuarioRepositorio();
            UsuarioModel usuarioModel = usuarioRepositorio.BuscarPorEmailSenha (usuario.Email, usuario.Senha);
            if(usuario.Email == "admin@gmail.com" && usuario.Senha == "admin123" )
            {
                HttpContext.Session.SetString("nomeUsuario","Admin");
                HttpContext.Session.SetString("tipoUsuario","Admin");
                ViewBag.Mensagem = "Login Realizado";               
                return RedirectToAction("ComentarioAdm","Comentario");
            }
            
            if(usuarioModel !=null){
                ViewBag.Mensagem = "Login Realizado";
                HttpContext.Session.SetString("nomeUsuario", usuarioModel.Nome);
                HttpContext.Session.SetString("tipoUsuario","Cliente");
                return RedirectToAction("Cadastro", "Comentario");
            }else
            {
                ViewBag.Mensagem = "Acesso Negado";
            }
            

            return View();


        }
    
    }
}