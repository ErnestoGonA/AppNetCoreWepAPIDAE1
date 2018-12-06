﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppNetCoreWepAPIDAE1.Models
{
    public class eva_cat_edificios
    {
        [Key]
        [Required]
        public Int16 IdEdificio { get; set; }
        public string Alias { get; set; }
        public string DesEdificio { get; set; }
        public Int16 Prioridad { get; set; }
        public string Clave { get; set; }
        public DateTime FechaReg { get; set; } 
        public DateTime FechaUltMod { get; set; }
        public string UsuarioReg { get; set; }
        public string UsuarioMod { get; set; }
        public string Activo { get; set; }
        public string Borrado { get; set; }
        public List<eva_cat_espacios> espacios { get; set; }
    }

    public class eva_cat_espacios
    {
        [Key]
        [Required]
        public Int16 IdEdificio { get; set; }
        public Int16 IdEspacio { get; set; }
        public string Clave { get; set; }
        public string DesEspacio { get; set; }
        public Int16 Prioridad { get; set; }
        public string Alias { get; set; }
        public Int16 RangoTiempoReserva { get; set; }
        public Int16 Capacidad { get; set; }
        public Int16 IdTipoEstatus { get; set; }
        public Int16 IdEstatus { get; set; }
        public string RefUbicacion { get; set; }
        public string PermiteCruce { get; set; }
        public string Observacion { get; set; }
        public DateTime FechaReg { get; set; }
        public DateTime FechaUltMod { get; set; }
        public  string UsuarioReg { get; set; }
        public string UsuarioMod { get; set; }
        public string Activo { get; set; }
        public string Borrado { get; set; }
    }

    public class eva_cat_tipo_competencias
    {
        [Key]
        [Required]
        public Int16 IdTipoCompetencia { get; set; }
        public string DesTipoCompetencia { get; set; }
        public string Detalle { get; set; }
        public DateTime FechaReg { get; set; }
        public string UsuarioReg { get; set; }
        public DateTime FechaUltMod { get; set; }
        public string UsuarioMod { get; set; }
        public string Activo { get; set; }
        public string Borrado { get; set; }

        public List<eva_cat_competencias> competencias { get; set; }

    }

    public class eva_cat_competencias
    {
        [Key]
        [Required]
        public int IdCompetencia { get; set; }
        public Int16 IdTipoCompetencia { get; set; }
        public string DesCompetencia { get; set; }
        public string Detalle { get; set; }
        public DateTime FechaReg { get; set; }
        public string UsuarioReg { get; set; }
        public DateTime FechaUltMod { get; set; }
        public string UsuarioMod { get; set; }
        public string Activo { get; set; }
        public string Borrado { get; set; }

        public List<eva_cat_conocimientos> conocimientos { get; set; }

    }

    public class eva_cat_conocimientos
    {
        [Key]
        [Required]
        public int IdConocimiento { get; set; }
        public int IdCompetencia { get; set; }
        public string DesConocimiento { get; set; }
        public string Detalle { get; set; }
        public DateTime FechaReg { get; set; }
        public string UsuarioReg { get; set; }
        public DateTime FechaUltMod { get; set; }
        public string UsuarioMod { get; set; }
        public string Activo { get; set; }
        public string Borrado { get; set; }
    }

}
