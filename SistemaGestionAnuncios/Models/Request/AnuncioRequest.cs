using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaGestionAnuncios.Models.Request
{
    public class AnuncioRequest
    {
        public string Titulo { get; set; }
        public string Tipo { get; set; }
        public string Precio { get; set; }        
    }
}