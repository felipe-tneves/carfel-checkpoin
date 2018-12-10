using System.Collections.Generic;
using BackFront.Senai.MVC.Models;

namespace BackFront.Senai.MVC.Interfaces
{
    public interface IComentario
    {
        
         ComentarioModel Cadastro(ComentarioModel comentario);
         List<ComentarioModel> Listar();
         void AprovarComentario(int id);
         void RecusarComentario(int id);
         void DeletarComentario(int id);
         ComentarioModel BuscarPorId(int id);
         void EscreverNoArquivo ();
         
    }
}