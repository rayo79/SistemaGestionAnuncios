using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestionAnuncios.Models.Response
{
    public class AnuncioResponse
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Tipo { get; set; }
        public string Precio { get; set; }
        public string Imagen { get; set; }
    }
}