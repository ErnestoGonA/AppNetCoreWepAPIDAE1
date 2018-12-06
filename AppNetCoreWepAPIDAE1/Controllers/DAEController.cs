using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using AppNetCoreWepAPIDAE1.Models;
using AppNetCoreWepAPIDAE1.Data;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;

namespace AppNetCoreWepAPIDAE1.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    //[Produces("application/json")]
    public class DAEController : Controller
    {

        //Declare local variable of context (Mongoose)
        private readonly DBContext dBlContext;

        //Constructor
        public DAEController(DBContext dBpContext)
        {
            dBlContext = dBpContext;
        }

        //GET
        [HttpGet]
        [Route("api/Edificios")]
        public ContentResult GetEdificios()
        {
            var res = from CE in dBlContext.eva_cat_edificios
                       select CE;
            string result = JsonConvert.SerializeObject(res);
            return Content(result, "application/json");

        }

        //GET
        [HttpGet]
        [Route("api/Edificios/{id}")]
        public ContentResult GetEdificio(short id)
        {
            var edificio = dBlContext.eva_cat_edificios.Find(id);
            string result = JsonConvert.SerializeObject(edificio);
            return Content(result, "application/json");

        }          

        [HttpGet]
        [Route("api/competencias")]
        public ContentResult GetCompetencias()
        {
            var res = from TCOM in dBlContext.eva_cat_tipo_competencias
                      select new
                      {
                          TCOM.IdTipoCompetencia,
                          TCOM.DesTipoCompetencia,
                          TCOM.Detalle,
                          TCOM.FechaReg,
                          TCOM.FechaUltMod,
                          TCOM.UsuarioReg,
                          TCOM.UsuarioMod,
                          competencias = from COM in dBlContext.eva_cat_competencias
                                         where TCOM.IdTipoCompetencia == COM.IdTipoCompetencia
                                         select new
                                         {
                                             COM.IdCompetencia,
                                             COM.IdTipoCompetencia,
                                             COM.DesCompetencia,
                                             COM.Detalle,
                                             COM.FechaReg,
                                             COM.FechaUltMod,
                                             COM.UsuarioReg,
                                             COM.UsuarioMod,
                                             conocimientos = from CON in dBlContext.eva_cat_conocimientos
                                                             where COM.IdCompetencia == CON.IdCompetencia
                                                             select new
                                                             {
                                                                 CON.IdConocimiento,
                                                                 CON.IdCompetencia,
                                                                 CON.DesConocimiento,
                                                                 CON.Detalle,
                                                                 CON.FechaReg,
                                                                 CON.FechaUltMod,
                                                                 CON.UsuarioReg,
                                                                 CON.UsuarioMod,
                                                             }
                                         }
                      };

            string result = JsonConvert.SerializeObject(res);
            return Content(result, "application/json");
        }

        [HttpGet]
        [Route("api/conocimientos")]
        public ContentResult GetTipCompTodo()
        {
            var resultado = from CTC in dBlContext.eva_cat_tipo_competencias
                            join CC in dBlContext.eva_cat_competencias on CTC.IdTipoCompetencia equals CC.IdTipoCompetencia
                            join CCO in dBlContext.eva_cat_conocimientos on CC.IdCompetencia equals CCO.IdCompetencia
                            select new
                            {
                                CTC.IdTipoCompetencia,
                                CTC.DesTipoCompetencia,
                                CTC.Detalle,
                                CC.IdCompetencia,
                                CC.DesCompetencia,
                                CCO.IdConocimiento,
                                CCO.DesConocimiento
                            };
            var resultadoGroup = resultado.GroupBy(CTC => CTC.IdTipoCompetencia)
                .Select(group => new {
                    group.First().IdTipoCompetencia,
                    group.First().DesTipoCompetencia,
                    group.First().Detalle,
                    Competencias = group.GroupBy(CC => CC.IdCompetencia).Select(groupp => new {
                        groupp.First().IdCompetencia,
                        groupp.First().DesCompetencia,
                        Conocimientos = groupp.Select(CCO => new
                        {
                            CCO.IdConocimiento,
                            CCO.DesConocimiento
                        })
                    })

                });
            string json = JsonConvert.SerializeObject(resultadoGroup);
            return Content(json, "application/json");
        }


        // POST:
        [HttpPost]
        [Route("api/Edificios")]
        public IActionResult Post([FromBody]eva_cat_edificios edificio)
        {
            if (ModelState.IsValid)
            {
                dBlContext.eva_cat_edificios.Add(edificio);
                dBlContext.SaveChanges();
                return new ObjectResult("Edificio agregado satisfactoriamente");
            }
            return BadRequest();
        }

        [HttpPut]
        [Route("api/Edificios")]
        public IActionResult Put(int id, [FromBody] List<eva_cat_edificios> list)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    foreach (eva_cat_edificios edificio in list)
                    {                        
                        eva_cat_edificios ed = (from inv in dBlContext.eva_cat_edificios where inv.IdEdificio == edificio.IdEdificio select inv).SingleOrDefault();

                        if (ed != null)
                        {
                            try
                            {
                                dBlContext.Entry(ed).State = EntityState.Detached;
                                dBlContext.Entry(edificio).State = EntityState.Modified;
                                dBlContext.SaveChanges();
                                dBlContext.Entry(edificio).State = EntityState.Detached;
                            }
                            catch(Exception ex1)
                            {
                                System.Diagnostics.Debug.WriteLine("Excepcion1/n" + ex1.Message.ToString());
                                return new ObjectResult(ex1.Message.ToString());
                            }
                        }
                        else
                        {
                            try
                            {
                                System.Diagnostics.Debug.WriteLine("edificionote" + edificio.Alias);
                                dBlContext.Add(edificio);
                                dBlContext.SaveChanges();
                                dBlContext.Entry(edificio).State = EntityState.Detached;
                            }
                            catch(Exception ex2)
                            {
                                System.Diagnostics.Debug.WriteLine("Excepcion2/n" + ex2.Message.ToString());
                                return new ObjectResult(ex2.Message.ToString());
                            }
                            
                        }
                    }
                    return new OkResult();
                }
                catch(Exception e)
                {
                    System.Diagnostics.Debug.WriteLine("Excepcion/n" + e.Message.ToString());
                    return new ObjectResult(e.Message.ToString());
                }
            }
            return BadRequest();
        }

        // PUT: api/DAE/5
        [HttpPut]
        [Route("api/Edificios/{id}")]
        public IActionResult Put(int id, [FromBody] eva_cat_edificios edificio)
        {
            if (ModelState.IsValid)
            {
                dBlContext.Entry<eva_cat_edificios>(edificio).State = EntityState.Modified;
                dBlContext.SaveChanges();
                return new ObjectResult("Actualizado correctamente");
            }
            return BadRequest();
        }

        // DELETE
        [HttpDelete]
        [Route("api/Edificios/{id}")]
        public IActionResult Delete(short id)
        {
            if (ModelState.IsValid)
            {
                dBlContext.eva_cat_edificios.Remove(dBlContext.eva_cat_edificios.Find(id));
                dBlContext.SaveChanges();
                return new ObjectResult("Borrado correctamente");
            }
            return BadRequest();
            
        }
    }
}
