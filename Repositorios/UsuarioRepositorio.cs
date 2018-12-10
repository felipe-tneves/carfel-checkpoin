using System.Collections.Generic;
using System.IO;
using BackFront.Senai.MVC.Interfaces;
using BackFront.Senai.MVC.Models;

namespace BackFront.Senai.MVC.Repositorios
{
    public class UsuarioRepositorio 
    {
        // Salvando serializado
        //  /// <summary>
        // /// Lista que armazena todos os usuários cadastrados no sistema
        // /// </summary>
        // private List<UsuarioModel> UsuariosSalvos {get; set;} 

        // public UsuarioRepositorioSerializacao()
        // {
            
        //     //Verificando se ja existe um arquivo serializado...
        //     if (File.Exists("usuarios.dat"))
        //     {
        //         //Ler o arquivo
        //         UsuariosSalvos = LerArquivoSerializado();
        //     }
        //     else
        //     {
        //         UsuariosSalvos = new List<UsuarioModel>();
        //     }


        // Salvando no CSV
        private List<UsuarioModel> CarregarCSV(){
            List<UsuarioModel> lsUsuario = new List<UsuarioModel>();
            string[] linhas = File.ReadAllLines("usuario.csv");

            foreach (string linha in linhas)
            {
                string[] dadosDaLinha = linha.Split(';');
                UsuarioModel usuario = new UsuarioModel{
                    Id = int.Parse(dadosDaLinha[0]),
                    Nome = dadosDaLinha[1],
                    Email = dadosDaLinha[2],
                    Senha = dadosDaLinha[3],
                    Administrador = bool.Parse (dadosDaLinha[4])
                };
                lsUsuario.Add(usuario);
            }
            return lsUsuario;
        }

        public UsuarioModel Cadastro(UsuarioModel usuario){
             
            if (System.IO.File.Exists("usarios.csv"))
            {
                string[] lines = System.IO.File.ReadAllLines("usarios.csv");
                usuario.Id = lines.Length + 1;
            }
            else
            {
                usuario.Id=1;
            }

            using(StreamWriter sw = new StreamWriter("usuario.csv", true)){
                sw.WriteLine ($"{usuario.Id};{usuario.Nome};{usuario.Email};{usuario.Senha};{usuario.Administrador}");

            };

            return usuario;
        }

        public UsuarioModel BuscarPorEmailSenha (string email, string senha){
            List<UsuarioModel> usuariosCadastrados = CarregarCSV ();
            foreach (UsuarioModel usuario in usuariosCadastrados )
            {
                if (usuario.Email == email && usuario.Senha == senha)
                {
                    return usuario;
                }
            }
            return null;
        }
         public List<UsuarioModel> Listar() => CarregarCSV();
    }
}