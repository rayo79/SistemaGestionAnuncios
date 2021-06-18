using Npgsql;
using SistemaGestionAnuncios.Models.Request;
using SistemaGestionAnuncios.Models.Response;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SistemaGestionAnuncios.Controllers
{
    public class AnuncioController : ApiController
    {
        [HttpPost]
        public IHttpActionResult Get(AnuncioRequest anuncio)
        {
            List<AnuncioResponse> anuncios = new List<AnuncioResponse>();            
            var dato = ConfigurationManager.AppSettings["ConexionBD"]; ; //"Host=saea98.com;Port=5433;Username=ca_12;Password=4i5r25u8ca12;Database=ca_12";

            var sql = @"SELECT ""Titulo"", ""Precio"", ""Imagen"", ""Tipo"", ""ID"" FROM public.""Anuncio"" WHERE 1=1 ";
            if (!string.IsNullOrEmpty(anuncio.Titulo))
                sql += @" AND ""Titulo"" like '%" + anuncio.Titulo + "%' ";
            if (!string.IsNullOrEmpty(anuncio.Precio))
                sql += @" AND ""Precio"" = " + anuncio.Precio ;
            if (anuncio.Tipo!="-1")
                sql += @" AND ""Tipo"" = " + anuncio.Tipo;

            using (var conn = new NpgsqlConnection(dato))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    using (NpgsqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            AnuncioResponse anun = new AnuncioResponse();
                            anun.Titulo = dr.GetString(0);
                            anun.Precio = Convert.ToString(dr.GetDecimal(1));
                            anun.Imagen = dr.GetString(2);
                            anun.Tipo = Convert.ToString(dr.GetInt16(3));
                            anun.Id = dr.GetInt32(4);
                            anuncios.Add(anun);
                        }
                    }
                }
            }
            

            return Ok(anuncios);
        }

        
    }
}
