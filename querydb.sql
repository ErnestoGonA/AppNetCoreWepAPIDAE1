USE DB_DAE
GO 

CREATE TABLE cat_estatus
( 
	IdTipoEstatus        smallint  NOT NULL ,
	IdEstatus            smallint  NOT NULL ,
	Clave                varchar(50)  NULL ,
	DesEstatus           varchar(30)  NULL ,
	Activo               char(1)  NULL ,
	FechaReg             datetime  NULL ,
	FechaUltMod          datetime  NULL ,
	UsuarioReg           varchar(20)  NULL ,
	UsuarioMod           varchar(20)  NULL ,
	Borrado              char(1)  NULL 
)
go



ALTER TABLE cat_estatus
	ADD CONSTRAINT XPKcat_estatus PRIMARY KEY (IdTipoEstatus ASC,IdEstatus ASC)
go



CREATE INDEX XIF1cat_estatus ON cat_estatus
( 
	IdTipoEstatus         ASC
)
go



CREATE TABLE cat_tipos_estatus
( 
	IdTipoEstatus        smallint  NOT NULL ,
	DesTipoEstatus       varchar(30)  NULL ,
	Activo               char(1)  NULL ,
	FechaReg             datetime  NULL ,
	UsuarioReg           varchar(20)  NULL ,
	FechaUltMod          datetime  NULL ,
	UsuarioMod           varchar(20)  NULL ,
	Borrado              char(1)  NULL 
)
go



ALTER TABLE cat_tipos_estatus
	ADD CONSTRAINT XPKcat_tipos_estatus PRIMARY KEY (IdTipoEstatus ASC)
go



CREATE TABLE eva_cat_edificios
( 
	IdEdificio           smallint  NOT NULL ,
	Alias                varchar(10)  NULL ,
	DesEdificio          varchar(50)  NULL ,
	Prioridad            smallint  NULL ,
	Clave                varchar(20)  NULL ,
	FechaReg             datetime  NULL ,
	FechaUltMod          datetime  NULL ,
	UsuarioReg           varchar(20)  NULL ,
	UsuarioMod           varchar(20)  NULL ,
	Activo               char(1)  NULL ,
	Borrado              char(1)  NULL 
)
go



ALTER TABLE eva_cat_edificios
	ADD CONSTRAINT XPKeva_cat_edificios PRIMARY KEY (IdEdificio ASC)
go



CREATE TABLE eva_cat_espacios
( 
	IdEdificio           smallint  NOT NULL ,
	IdEspacio            smallint  NOT NULL ,
	Clave                varchar(20)  NULL ,
	DesEspacio           varchar(50)  NULL ,
	Prioridad            smallint  NULL ,
	Alias                char(10)  NULL ,
	RangoTiempoReserva   smallint  NULL ,
	Capacidad            smallint  NULL ,
	IdTipoEstatus        smallint  NULL ,
	IdEstatus            smallint  NULL ,
	RefeUbicacion        varchar(255)  NULL ,
	PermiteCruce         char(1)  NULL ,
	Observacion          varchar(20)  NULL ,
	FechaReg             datetime  NULL ,
	FechaUltMod          datetime  NULL ,
	UsuarioReg           varchar(20)  NULL ,
	UsuarioMod           varchar(20)  NULL ,
	Activo               char(1)  NULL ,
	Borrado              char(1)  NULL 
)
go



ALTER TABLE eva_cat_espacios
	ADD CONSTRAINT XPKeva_cat_espacios PRIMARY KEY (IdEdificio ASC,IdEspacio ASC)
go



CREATE INDEX XIF1eva_cat_espacios ON eva_cat_espacios
( 
	IdEdificio            ASC
)
go



CREATE INDEX XIF2eva_cat_espacios ON eva_cat_espacios
( 
	IdTipoEstatus         ASC,
	IdEstatus             ASC
)
go




ALTER TABLE cat_estatus
	ADD CONSTRAINT R_8 FOREIGN KEY (IdTipoEstatus) REFERENCES cat_tipos_estatus(IdTipoEstatus)
		ON DELETE NO ACTION
		ON UPDATE NO ACTION
go




ALTER TABLE eva_cat_espacios
	ADD CONSTRAINT R_164 FOREIGN KEY (IdEdificio) REFERENCES eva_cat_edificios(IdEdificio)
		ON DELETE NO ACTION
		ON UPDATE NO ACTION
go




ALTER TABLE eva_cat_espacios
	ADD CONSTRAINT R_811 FOREIGN KEY (IdTipoEstatus,IdEstatus) REFERENCES cat_estatus(IdTipoEstatus,IdEstatus)
		ON DELETE NO ACTION
		ON UPDATE NO ACTION
go