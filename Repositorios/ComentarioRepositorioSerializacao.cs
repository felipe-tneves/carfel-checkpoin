using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using BackFront.Senai.MVC.Interfaces;
using BackFront.Senai.MVC.Models;

namespace BackFront.Senai.MVC.Repositorios {
    public class ComentarioRepositorioSerializacao : IComentario {

        /// <summary>
        /// Lista que armazena todos os usuários cadastrados no sistema
        /// </summary>
        private List<ComentarioModel> ComentarioSalvos { get; set; }

        public ComentarioRepositorioSerializacao () {

            //Verificando se ja existe um arquivo serializado...
            if (File.Exists ("comentario.dat")) {
                //Ler o arquivo
                ComentarioSalvos = LerArquivoSerializado ();
            } else {
                ComentarioSalvos = new List<ComentarioModel> ();
            }

        }

        public ComentarioModel BuscarPorId (int id) {
            //Percorre todos os comentario buscando pelo id...
            foreach (ComentarioModel comentario in ComentarioSalvos) {
                if (id == comentario.Id) {
                    return comentario;
                }
            }

            //Caso não encontre o comentario pelo id
            return null;
        }

        public ComentarioModel Cadastro (ComentarioModel comentario) {
            //Adiciona o comenatrio na lista
            comentario.Id = ComentarioSalvos.Count + 1;
            ComentarioSalvos.Add (comentario);

            //Serializando a lista com todos os comentario cadastrados

            EscreverNoArquivo ();

            return comentario;
        }

        public void EscreverNoArquivo () {
            //MemoryStream: vai guardar os bytes da serialização
            MemoryStream memoria = new MemoryStream ();
            //BinaryFormatter: Objeto que fará a serialização
            BinaryFormatter serializadora = new BinaryFormatter ();

            serializadora.Serialize (memoria, ComentarioSalvos);

            //Pegando os bytes salvos na memória
            byte[] bytes = memoria.ToArray ();

            File.WriteAllBytes ("comentario.dat", bytes);
        }

        public List<ComentarioModel> Listar () {
            return ComentarioSalvos;
        }

        private List<ComentarioModel> LerArquivoSerializado () {
            //Lê os bytes do arquivo
            byte[] bytesSerializados = File.ReadAllBytes ("comentario.dat");

            //Cria o fluxo de memória com os bytes do arquivo serializado
            MemoryStream memoria = new MemoryStream (bytesSerializados);

            BinaryFormatter serializador = new BinaryFormatter ();

            //ClassCastException
            return (List<ComentarioModel>) serializador.Deserialize (memoria);
        }

        //Receber o id no método
        //Ler a lista do arquivo .dat
        //Procurar da lista lida o comentario com o id passado no parametro do metodo
        //Caso o id seja igual a um determinado comentario da lista, voc~e deve alterar seu status
        //Gravar o arquivo novamente para persisitir suas alterações
        public void AprovarComentario (int id) {
            LerArquivoSerializado ();
            foreach (ComentarioModel comentario in ComentarioSalvos) {
                if (id == comentario.Id) {
                    comentario.Status = true;
                    // return comentario;
                    EscreverNoArquivo ();
                }
            }
        }
        public void RecusarComentario (int id) {
            LerArquivoSerializado ();
            foreach (ComentarioModel comentario in ComentarioSalvos) {
                if (id == comentario.Id) {
                    comentario.Status = false;
                    EscreverNoArquivo ();
                }
            }
        }

        //Lista = Desserializar();
        //Percorre a Lista;
        //Se o item.ID for igual ao id
        //Lista.Remove(item);


         public void DeletarComentario (int Id) {

            ComentarioModel comentario = BuscarPorId(Id);
                
            if(comentario != null){
                ComentarioSalvos.Remove(comentario);    

                EscreverNoArquivo();
            }
        }
            
        //     private List<ComentarioModel> CarregarCSVComentario(){
        //         List<ComentarioModel> lsComentario = new List<ComentarioModel> ();
        //         string[] linhas = File.ReadAllLines ("Comentario.csv");

        //         foreach (string linha in linhas)
        //         {
        //             string[] dadosDaLinha = linha.Split (';');
        //             if (string.IsNullOrEmpty (linha))
        //             {
        //                 continue;
        //             }

        //             ComentarioModel comentario = new ComentarioModel {
        //                 IdUsuario = int.Parse(dadosDaLinha[0]),
        //                 Nome = dadosDaLinha[1],
        //                 ComentarioPost = dadosDaLinha[2],
        //                 DataPost = DateTime.Parse(dadosDaLinha[3]),
        //                 Status = bool.Parse(dadosDaLinha[4])
        //             };

        //             lsComentario.Add( comentario);
        //         }
        //         return lsComentario;
        //     }

        //     public List<ComentarioModel> Listar(){
        //         return CarregarCSVComentario ();
        //     }

        //     public ComentarioModel BuscarPorId (int id){
        //         string[] linhas = System.IO.File.ReadAllLines ("Comentario.csv");
        //         for (int i = 0; i < linhas.Length; i++)
        //         {
        //             if (string.IsNullOrEmpty (linhas[i]))
        //             {
        //                 continue;
        //             }

        //             string[] dados = linhas[i].Split(';');

        //             if (dados[0] == id.ToString())
        //             {
        //                 ComentarioModel  comentarioModel = new ComentarioModel();

        //                 id = int.Parse(dados[0]);
        //                 return comentarioModel;

        //             }
        //         }

        //         return null;
        //     }

        //     public void RecusarComentario (int id){
        //         string[] linhas = System.IO.File.ReadAllLines ("Comentario.csv");
        //         for (int i = 0; i < linhas.Length; i++)
        //         {
        //             string[] dadosDaLinha = linhas[i].Split(';');
        //             if (id.ToString () == dadosDaLinha[0])
        //             {
        //                 linhas[i] = ($"{dadosDaLinha[0]};{dadosDaLinha[1]};{dadosDaLinha[2]};{dadosDaLinha[3]};{false}");
        //             }
        //         }
        //     }

        //     public void AprovarComentario (int id){
        //         string[] linhas = System.IO.File.ReadAllLines ("Comentario.csv");
        //         for (int i = 0; i < linhas.Length; i++)
        //         {
        //             string[] dadosDaLinha = linhas[i].Split(';');
        //             if (id.ToString () == dadosDaLinha[0])
        //             {
        //                 linhas[i] = ($"{dadosDaLinha[0]};{dadosDaLinha[1]};{dadosDaLinha[2]};{dadosDaLinha[3]};{true}");
        //             }
        //         }
        //     }
    }
}