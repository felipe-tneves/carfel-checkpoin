using System;

namespace BackFront.Senai.MVC.Models
{
    [Serializable] // atribuindo serialização 
    public class ComentarioModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string ComentarioPost { get; set; }
        public DateTime DataPost { get; set;}
        public bool Status {get; set;}



    //Construindo Metodo contrutor
        public ComentarioModel( string nome, string comentarioPost, DateTime dataPost)
        {
          
            this.Nome = nome;
            this.ComentarioPost = comentarioPost;
            this.DataPost = dataPost;
            this.Status = Status;
        }
    }

}