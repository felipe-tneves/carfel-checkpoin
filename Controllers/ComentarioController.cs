using System;
using System.IO;
using BackFront.Senai.MVC.Interfaces;
using BackFront.Senai.MVC.Models;
using BackFront.Senai.MVC.Repositorios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace BackFront.Senai.MVC.Controllers
{
    public class ComentarioController : Controller
    {

    public IComentario comentarioRepositorio {get; set;}
        public IComentario ComentarioRepositorioSerializacao {get; set;}

        //Fazendo metodo contrutor 
        public ComentarioController()
        {
            ComentarioRepositorioSerializacao = new ComentarioRepositorioSerializacao();
        }

        [HttpGet]
        public ActionResult Cadastro(){
            
            if( string.IsNullOrEmpty(HttpContext.Session.GetString("nomeUsuario"))){
                return RedirectToAction("Login", "Usuario");
            }

            return View();
        }

        [HttpPost]
        public ActionResult Cadastro(IFormCollection form)
        {
            string nome = HttpContext.Session.GetString ("nomeUsuario");
            ComentarioModel comentarioModel = new ComentarioModel(nome,dataPost: DateTime.Now,comentarioPost:form["comentarioPost"]);
            ComentarioRepositorioSerializacao.Cadastro(comentarioModel);
            ViewBag.Mensagem = "Comentario em Avaliação";

            return View();
        }

         /// <summary>
        /// Lista todos os comentario cadastrados no sistema
        /// </summary>
        /// <returns>A view da listagem de comentario</returns>
        [HttpGet]
        public IActionResult Listar()
        {
            //UsuarioRepositorio rep = new UsuarioRepositorio();

            //Buscando os dados do rep. e aplicando no view bag
            //ViewBag.Usuarios = rep.Listar();
            String Email = HttpContext.Session.GetString("email");
           
            ViewData["comentario"] = ComentarioRepositorioSerializacao.Listar();

            return View();
        }

        [HttpGet]
            public ActionResult ComentarioAdm(){
               ViewData["Comentarios"] = ComentarioRepositorioSerializacao.Listar();
                return View();
            }

         [HttpGet]
            public ActionResult RecusarComentario(int id){
                ComentarioRepositorioSerializacao.RecusarComentario(id);
                TempData["Mensagem"] = "Comentario Recusado";
                return RedirectToAction ("Listar");
            }


        [HttpGet]
        public ActionResult DeletarComentario(int id){
            ComentarioRepositorioSerializacao.DeletarComentario(id);
            TempData["Mensagem"] = "Comentario Apagao";
            return RedirectToAction ("Listar");
        }
            [HttpGet]
            public ActionResult AprovarComentario (int id){
                ComentarioRepositorioSerializacao.AprovarComentario(id);
                TempData["Mensagem"] = "Comentario Aprovado";
                return RedirectToAction ("Listar");


        //Fazendo CSV
        // public ComentarioRepositorio comentarioRepositorio{get; set;}

        // public ComentarioController()
        // {
        //     comentarioRepositorio = new ComentarioRepositorio();
        // }
        // [HttpGet]
        // public ActionResult Cadastro()
        // {          

        //     if(string.IsNullOrEmpty(HttpContext.Session.GetString("nomeUsuario"))){
        //     }
        //     return View();
        // }

        // [HttpPost]
        // public IActionResult Cadastro(IFormCollection form){
        //     ComentarioModel comentario = new ComentarioModel();

        //     bool status = false;
        //     int id = 0;

        //     if (System.IO.File.Exists ("Comentario.csv")) {
        //         string[] lines = System.IO.File.ReadAllLines ("Comentario.csv");
        //         id = lines.Length + 1;
        //     } else {
        //         id = 1;
        //     }

        //     comentario.IdUsuario = id;
        //     comentario.Nome = HttpContext.Session.GetString ("nomeUsuario");
        //     comentario.ComentarioPost = form["comentarioPost"];
        //     comentario.DataPost = DateTime.Now;
        //     comentario.Status = status;

        //     using(StreamWriter sw = new StreamWriter("Comentario.csv", true)){
        //         sw.WriteLine($"{comentario.IdUsuario};{comentario.Nome};{comentario.ComentarioPost};{comentario.DataPost};{comentario.Status}");
        //     }

        //     ViewBag.Mensagem = "Comentario Cadastrado";
        //     return View();
        // }

        //     public ActionResult Listar(){
        //         ViewData["comentario"] = comentarioRepositorio.Listar();
        //         return View();
        //     }

        //     [HttpGet]
        //     public ActionResult RecusarComentario(int id){
        //         comentarioRepositorio.RecusarComentario(id);
        //         TempData["Mensagem"] = "Comentario Recusado";
        //         return RedirectToAction ("Listar");
        //     }
        //     [HttpGet]
        //     public ActionResult AprovarComentario (int id){
        //         comentarioRepositorio.AprovarComentario(id);
        //         TempData["Mensagem"] = "Comentario Aprovado";
        //         return RedirectToAction ("Listar");
        //     }
    }


    }
}