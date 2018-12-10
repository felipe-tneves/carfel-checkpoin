namespace BackFront.Senai.MVC.Models
{
    //Logo abaixo esta acontecendo a declaração das variaveis 
    public class UsuarioModel
    {
        public int Id { get; set; }//get pega as informaçoes / set entrada 
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public bool Administrador { get; set; }

    }
}