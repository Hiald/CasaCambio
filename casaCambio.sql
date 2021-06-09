

/* ---------------------------------------------- */

/* MYSQL: mysql5046.site4now.net
Username: a66376_dbcdc
nombre db: db_a66376_dbcdc
pass: CasaCambio21.
*/

/*PROCESO

estado:
1: cotizado
2: abonado
3: validando (administrador)
4: en curso de abono
5: finalizado
6: reembolsado
7: error
*/

/* se guarda caracteristicas generales tales como: estado del abono, etc */
CREATE TABLE t_maestro (
  `idmaestro` INT NOT NULL AUTO_INCREMENT,
  `v_nombre` VARCHAR(50) NOT NULL,
  `v_descripcion` VARCHAR(50) NOT NULL,
  `b_estado` BIT(1) NOT NULL,
  `dt_fecharegistro` DATETIME NOT NULL,
  PRIMARY KEY (`idmaestro`)
);

/* detalle de la tabla maestro tales como: cotizado,abonado,finalizado, etc */
CREATE TABLE t_maestro_parametro (
  `idparametro` INT NOT NULL,
  `idmaestro` INT NOT NULL,
  `v_nombre` VARCHAR(50) NOT NULL,
  `v_descripcion` VARCHAR(200) DEFAULT NULL,
  `b_estado` BIT(1) NOT NULL,
  `dt_fecharegistro` DATETIME NOT NULL,
  FOREIGN KEY (`idmaestro`) REFERENCES t_maestro(`idmaestro`),
  PRIMARY KEY (`idparametro`)
);


/* informacion del usuario, o tambien de la persona a cargo de administrar */
CREATE TABLE t_usuario (
  `idusuario` INT NOT NULL AUTO_INCREMENT,
  `idprovincia` INT NULL,
  `idciudad` INT NULL,
  `iddistrito` INT NULL,
  `v_direccion` VARCHAR(100) DEFAULT NULL,
  `v_nombres` VARCHAR(50) NOT NULL,
  `v_apellidos` VARCHAR(45) NOT NULL,
  `i_genero` CHAR(1) DEFAULT NULL,
  `v_imagen` MEDIUMBLOB DEFAULT NULL,
  `v_imagen_ruta` VARCHAR(500) DEFAULT NULL,
  `v_imagen_dni_ruta_1` VARCHAR(500) DEFAULT NULL,
  `v_imagen_dni_ruta_2` VARCHAR(500) DEFAULT NULL,
  `v_celular1` VARCHAR(20) DEFAULT NULL,
  `v_celular2` VARCHAR(20) DEFAULT NULL,
  `v_telefono` VARCHAR(10) DEFAULT NULL,
  `i_tipodocumento` INT NULL,
  `v_documento` VARCHAR(20) NULL,
  `v_ubigeo` VARCHAR(20) NULL,
  `d_fechanac` DATE DEFAULT NULL,
  `b_estado` BIT(1) NOT NULL,
  `dt_fecharegistro` DATETIME NOT NULL,
  `v_horaregistro` VARCHAR(25) NULL,
  `dt_fechamodificacion` DATETIME NULL,
  `v_horamodificacion` VARCHAR(25) NULL,
  `idusuario_Modificacion` INT NULL,
  PRIMARY KEY (`idusuario`)
);

/* informacion de los accesos, correo,clave del usuario final y administrador */
CREATE TABLE t_acceso (
  `idacceso` INT NOT NULL AUTO_INCREMENT,
  `idusuario` INT NOT NULL,
  `i_tipo_usuario` INT NOT NULL,
  `v_correo` VARCHAR(100) NOT NULL,
  `v_clave` VARCHAR(500) NOT NULL,
  `v_token` VARCHAR(500) DEFAULT NULL,
  `b_estado` BIT(1) NOT NULL,
  `dt_fecharegistro` DATETIME NOT NULL,
  `v_horaregistro` VARCHAR(25) NULL,
  `dt_fechamodificacion` DATETIME NULL,
  `v_horamodificacion` VARCHAR(25) NULL,
  `idusuario_Modificacion` INT NULL,
  FOREIGN KEY (`idusuario`) REFERENCES t_usuario(`idusuario`),
  PRIMARY KEY (`idacceso`)
);

/* guarda los numeros de cuenta (CCI) de los diferentes bancos del usuario final  */
CREATE TABLE t_cuenta_bancaria (
  `idcuentabancaria` INT NOT NULL AUTO_INCREMENT,
  `idusuario` INT NOT NULL,
  `i_tipo_cuenta` INT NOT NULL,
  `i_moneda` INT NOT NULL,
  `i_banco` INT NOT NULL, /* maestro_parametro */
  `v_banco` VARCHAR(150) NOT NULL, /* si es otro banco */
  `v_numero_cuenta` VARCHAR(150) NOT NULL, /* 123132 */
  `v_nombre_cuenta` VARCHAR(150) NOT NULL, /* cuenta mia 2 */
  `i_tipo_declaracion` INT NOT NULL, /* confirmo que la cuenta es mia */
  `v_titular` VARCHAR(150) NOT NULL, /* jonathan eric... */
  `b_estado` BIT(1) NOT NULL,
  `dt_fecharegistro` DATETIME NOT NULL,
  `v_horaregistro` VARCHAR(25) NULL,
  `dt_fechamodificacion` DATETIME NOT NULL,
  `v_horamodificacion` VARCHAR(25) NULL,
  `idusuario_Modificacion` INT NULL,
  FOREIGN KEY (`idusuario`) REFERENCES t_usuario(`idusuario`),
  PRIMARY KEY (`idcuentabancaria`)
);

/* consulta a una API para guardar los montos por cada día, y guarda en la tabla el monto de dicho dia, tambien pueden ser puestos manualmente */
CREATE TABLE t_divisa (
  `iddivisa` INT NOT NULL AUTO_INCREMENT,
  `idusuario` INT NOT NULL,
  `i_tipo_busqueda` INT NULL, /* si es buscado por api o manual en dicho dia */
  `d_monto` DECIMAL(10,3) NULL, /* S/ 3.47 */
  `d_soles_venta` DECIMAL(10,3) NULL, /* S/ 3.47 */
  `d_soles_compra` DECIMAL(10,3) NULL, /* S/ 3.47 */
  `d_dolares_venta` DECIMAL(10,3) NULL, /* S/ 3.47 */
  `d_dolares_compra` DECIMAL(10,3) NULL, /* S/ 3.47 */
  `i_tipo_promocion` INT NULL, /* S/ cupon, aniversario, etc */
  `dt_fecha` DATE NULL,
  `v_hora` VARCHAR(25) NULL,
  `b_estado` BIT(1) NOT NULL,
  `dt_fecharegistro` DATETIME NOT NULL,
  `dt_fechamodificacion` DATETIME NOT NULL,
  `v_horamodificacion` VARCHAR(25) NULL,
  FOREIGN KEY (`idusuario`) REFERENCES t_usuario(`idusuario`),
  PRIMARY KEY (`iddivisa`)
);

/* guarda la transaccion del usuario final: cambiar 50 soles a dolares */
CREATE TABLE t_transaccion (
  `idtransaccion` INT NOT NULL AUTO_INCREMENT,
  `idusuario` INT NOT NULL,
  `iddivisa` INT NOT NULL,
  `idpromocion` INT NULL, /* opcional */
  `idcuentabancaria` INT NOT NULL,
  `idusuario_administrador` INT NULL, /* usuario que hizo la transaccion */
  `dt_fecha` DATE NULL,
  `i_tipo_divisa` INT NOT NULL, /* si se uso algun cupon o descuento general */
  `d_dolares_venta` DECIMAL(10,3) NULL, /* S/ 3.47 (solo en el caso de cupon o descuento por alguna fecha aniversario) */
  `d_dolares_compra` DECIMAL(10,3) NULL, /* S/ 3.47 (solo en el caso de cupon o descuento por alguna fecha aniversario) */
  `v_hora` VARCHAR(25) NULL,
  `v_imagen` MEDIUMBLOB DEFAULT NULL,
  `v_imagen_ruta` VARCHAR(500) DEFAULT NULL,  
  `i_tipo_envio` INT NULL, /* si desea ahora o mas tarde*/
  `i_estado` INT NOT NULL, /* abonado, en transcurso, etc */
  `v_operacion` VARCHAR(150) NULL, /* 123132 */
  `i_origen_fondo` INT NULL, /* ahorros, cuenta corriente */
  `d_envio` DECIMAL(10,3) NULL, /* $25 */
  `d_recibo` DECIMAL(10,3) NULL, /* S/.75 */
  `i_tipo_cambio` INT NULL, /* es compra o venta */
  `i_tipo_trasaccion` INT NULL, /* si es de soles a dolares o viceversa */
  `d_igv` DECIMAL(10,3) NULL, /* 18% opcional */
  `v_banco_receptor` VARCHAR(150) NOT NULL, /* bbva, etc */
  `b_estado` BIT(1) NOT NULL,
  `dt_fecharegistro` DATETIME NOT NULL,
  `dt_fechamodificacion` DATETIME NOT NULL,
  `idusuario_Modificacion` INT NULL,
  FOREIGN KEY (`idusuario`) REFERENCES t_usuario(`idusuario`),
  FOREIGN KEY (`iddivisa`) REFERENCES t_divisa(`iddivisa`),
  FOREIGN KEY (`idcuentabancaria`) REFERENCES t_cuenta_bancaria(`idcuentabancaria`),
  PRIMARY KEY (`idtransaccion`)
);

/* tabla auditoria, en el caso que el usuario quiera cambiar alguna informacion del usuario/transaccion, etc */
 CREATE TABLE t_transaccion_cambio (
  `idtransaccioncambio` INT NOT NULL AUTO_INCREMENT,
  `idusuario` INT NOT NULL,
  `dt_fecha` DATE NULL,
  `v_hora` VARCHAR(25) NULL,
  `v_tabla` VARCHAR(25) NULL, /* nombre de la tabla */
  `v_campo` VARCHAR(150) NULL, /* columna nombre */
  `v_valorAntiguo` VARCHAR(250) NULL, /* mario */
  `v_valorNuevo` VARCHAR(250) NULL, /* jonathan */
  `dt_fecharegistro` DATETIME NOT NULL,
  FOREIGN KEY (`idusuario`) REFERENCES t_usuario(`idusuario`),
  PRIMARY KEY (`idtransaccioncambio`)
);

/* Por ahora stand by, serian los cupones de descuento de la persona al hacer transacciones */
CREATE TABLE t_promocion (
  `idpromocion` INT NOT NULL AUTO_INCREMENT,
  `v_cupon` INT NULL, /* EL CODIGO DEL CUPON */
  `d_monto` DECIMAL(10,3) NULL, /* S/ 3.47 */
  `i_tipo_cupon` INT NULL, /* si es por montos, por porcentaje, etc */
  `d_porcentaje` DECIMAL(10,3) NULL, /* 15 % */
  `d_monto_promocion` DECIMAL(10,3) NULL, /* 50 soles de descuento */
  `d_restriccion` DECIMAL(10,3) NULL, /* por montos mayores a 5000 % */
  `i_limite` INT NULL, /* solo 500 personas, 10000, etc */
  `dt_fecha` DATE NULL,
  `v_hora` VARCHAR(25) NULL,
  `b_estado` BIT(1) NOT NULL,
  `dt_fecharegistro` DATETIME NOT NULL,
  `dt_fechamodificacion` DATETIME NOT NULL,
  `idusuario_Modificacion` INT NULL,
  PRIMARY KEY (`idpromocion`)
);


-- --------------------------------------------------------------------------

/*DROP procedure IF EXISTS `sp_obtener_usuario`;*/
DELIMITER $$
CREATE PROCEDURE `sp_obtener_usuario`(IN _usuario VARCHAR(100),IN _clave VARCHAR(500) )
BEGIN

  IF EXISTS(SELECT idusuario FROM T_ACCESO WHERE v_correo = _usuario AND v_clave = _clave) THEN
  BEGIN
    
  SELECT 
        u.idusuario
        ,u.v_nombres
        ,u.v_apellidos
        ,u.v_imagen
        ,u.v_imagen_ruta
        ,u.v_celular1
        ,u.v_telefono
        ,u.i_tipodocumento
        ,u.v_documento
        ,a.v_correo
        ,a.i_tipo_usuario
        ,1 as '_resultado'
  FROM t_usuario u
    LEFT JOIN t_acceso a ON a.idusuario = u.idusuario
    WHERE u.idusuario = (SELECT idusuario FROM t_acceso WHERE v_correo = _usuario AND v_clave = _clave)
      AND u.b_estado = 1;

    END;
    ELSE 
    BEGIN

    SELECT 
         0 as 'idusuario'
        ,'0' as 'v_nombres'
        ,'0' as 'v_apellidos'
        ,'0' as 'v_imagen'
        ,'0' as 'v_imagen_ruta'
        ,'0' as 'v_celular1'
        ,'0' as 'v_telefono'
        ,0 as 'i_tipodocumento'
        ,'0' as 'v_documento'
        ,'0' as 'v_correo'
        ,0 as 'i_tipo_usuario'
        ,1 as '_resultado';

    END;
    END IF;

END$$
DELIMITER ;

-- --------------------------------------------------------------------------

/*DROP procedure IF EXISTS `sp_insertar_usuario`;*/
DELIMITER $$
CREATE PROCEDURE `sp_insertar_usuario`
  (IN _v_nombres VARCHAR(50)
  ,IN _v_apellidos VARCHAR(45)
  ,IN _i_tipodocumento INT
  ,IN _v_documento VARCHAR(20)
  ,IN _dt_fecharegistro VARCHAR(25)
  ,IN _v_horaregistro VARCHAR(25)
  ,IN _i_tipo_usuario INT
  ,IN _v_correo VARCHAR(100)
  ,IN _v_clave VARCHAR(500)
  ,IN _v_token VARCHAR(500))
BEGIN

IF EXISTS(SELECT idusuario FROM t_acceso WHERE v_correo = _v_correo) THEN
BEGIN
  SELECT 0 as '_resultado';
END;
ELSE
BEGIN
    INSERT INTO t_usuario(
       v_nombres
      ,v_apellidos
      ,i_tipodocumento
      ,v_documento
      ,b_estado
      ,dt_fecharegistro
      ,v_horaregistro)
    VALUES (
       _v_nombres
      ,_v_apellidos
      ,_i_tipodocumento
      ,_v_documento
      ,1
      ,STR_TO_DATE(_dt_fecharegistro, '%Y-%m-%d')
      ,_v_horaregistro);

    INSERT INTO t_acceso(
       idusuario
      ,i_tipo_usuario
      ,v_correo
      ,v_clave
      ,v_token
      ,b_estado
      ,dt_fecharegistro
      ,v_horaregistro)
  values (
      LAST_INSERT_ID()
      ,_i_tipo_usuario
      ,_v_correo
      ,_v_clave
      ,_v_token
      ,1
      ,STR_TO_DATE(_dt_fecharegistro, '%Y-%m-%d')
      ,_v_horaregistro);
        
    SELECT LAST_INSERT_ID() as '_resultado';
END;
END IF;

END$$
DELIMITER ;

-- --------------------------------------------------------------------------

/*DROP procedure IF EXISTS `sp_actualizar_acceso`;*/
DELIMITER $$
CREATE PROCEDURE `sp_actualizar_acceso`(
   IN _tipoactualizar INT
  ,IN _idusuario INT
  ,IN _v_correo VARCHAR(100)
  ,IN _v_clave VARCHAR(500))
BEGIN

  IF (_tipoactualizar = 1) THEN
  BEGIN
    
    UPDATE t_acceso
      SET  
      v_correo = _v_correo,
     v_clave = _v_clave
    WHERE idusuario = _idusuario;

  END;
  ELSE 
  BEGIN

    UPDATE t_acceso
      SET b_estado = 0 
    WHERE idusuario = _idusuario;
    
  END;
  END IF;

END$$
DELIMITER ;

-- --------------------------------------------------------------------------

/*DROP procedure IF EXISTS `sp_actualizar_clave`;*/
DELIMITER $$
CREATE PROCEDURE `sp_actualizar_clave` (IN _idusuario INT, IN _nuevaclave VARCHAR(500))
BEGIN

UPDATE t_acceso SET v_clave = _nuevaclave WHERE idusuario = _idusuario;

END$$
DELIMITER ;

-- --------------------------------------------------------------------------

/*DROP procedure IF EXISTS `sp_actualizar_usuario`;*/
DELIMITER $$
CREATE PROCEDURE `sp_actualizar_usuario`( 
   IN _idusuario INT
  ,IN _idprovincia INT
  ,IN _idciudad INT
  ,IN _iddistrito INT
  ,IN _v_direccion VARCHAR(100)
  ,IN _v_nombres VARCHAR(50)
  ,IN _v_apellidos VARCHAR(45)
  ,IN _i_genero CHAR(1)
  ,IN _v_imagen_ruta VARCHAR(500)
  ,IN _v_imagen_dni_ruta_1 VARCHAR(500)
  ,IN _v_imagen_dni_ruta_2 VARCHAR(500)
  ,IN _v_celular1 VARCHAR(20)
  ,IN _v_celular2 VARCHAR(20)
  ,IN _v_telefono VARCHAR(10)
  ,IN _i_tipodocumento INT
  ,IN _v_documento VARCHAR(20)
  ,IN _v_ubigeo VARCHAR(20)
  ,IN _d_fechanac VARCHAR(20)
  ,IN _dt_fechamodificacion VARCHAR(20)
  ,IN _v_horamodificacion VARCHAR(25)
  ,IN _idusuario_Modificacion INT)
BEGIN
    UPDATE t_usuario
    SET
       idprovincia = _idprovincia
      ,idciudad = _idciudad
      ,iddistrito = _iddistrito
      ,v_direccion = _v_direccion
      ,v_nombres = _v_nombres
      ,v_apellidos = _v_apellidos
      ,i_genero = _i_genero
      ,v_imagen_ruta = _v_imagen_ruta
      ,v_imagen_dni_ruta_1 = _v_imagen_dni_ruta_1
      ,v_imagen_dni_ruta_2 = _v_imagen_dni_ruta_2
      ,v_celular1 = _v_celular1
      ,v_celular2 = _v_celular2
      ,v_telefono = _v_telefono
      ,i_tipodocumento = _i_tipodocumento
      ,v_documento = _v_documento
      ,v_ubigeo = _v_ubigeo
      ,d_fechanac = STR_TO_DATE(_d_fechanac, '%Y-%m-%d')
      ,dt_fechamodificacion = STR_TO_DATE(_dt_fechamodificacion, '%Y-%m-%d')
      ,v_horamodificacion = _v_horamodificacion
      ,idusuario_Modificacion = _idusuario_Modificacion
    WHERE idusuario = _idusuario;   
    SELECT _idusuario as '_resultado';      

END$$
DELIMITER ;

-- --------------------------------------------------------------------------

/*DROP procedure IF EXISTS `sp_filtrar_usuario`;*/
DELIMITER $$
CREATE PROCEDURE `sp_filtrar_usuario`(IN _idusuario INT )
BEGIN

  SELECT 
   u.idusuario
  ,u.idprovincia
  ,u.idciudad
  ,u.iddistrito
  ,u.v_direccion
  ,u.v_nombres
  ,u.v_apellidos
  ,u.i_genero
  ,u.v_imagen_ruta
  ,u.v_imagen_dni_ruta_1
  ,u.v_imagen_dni_ruta_2
  ,u.v_celular1
  ,u.v_celular2
  ,u.v_telefono
  ,u.i_tipodocumento
  ,u.v_documento
  ,DATE_FORMAT(u.d_fechanac, '%d-%m-%Y') as 'd_fechanac'
  ,u.b_estado
  ,DATE_FORMAT(u.dt_fecharegistro, '%d-%m-%Y') as 'dt_fecharegistro'
  ,u.v_horaregistro
  FROM t_usuario u
    WHERE u.idusuario = _idusuario AND u.b_estado = 1;

END$$
DELIMITER ;

-- --------------------------------------------------------------------------

/*DROP procedure IF EXISTS `sp_listar_maestro`;*/
DELIMITER $$
CREATE PROCEDURE `sp_listar_maestro` (IN _idmaestro INT)
BEGIN

SELECT
   sm.idparametro
  ,sm.idmaestro
  ,sm.v_nombre
  ,sm.v_descripcion
  ,sm.b_estado
  ,DATE_FORMAT(sm.dt_fecharegistro, '%d-%m-%Y') as 'dt_fecharegistro'
FROM t_maestro as m 
  INNER JOIN t_maestro_parametro as sm WHERE m.idmaestro = sm.idmaestro
    AND sm.b_estado = 1 
    AND m.b_estado = 1 
    AND m.idmaestro = _idmaestro;

END$$
DELIMITER ;

-- --------------------------------------------------------------------------

/*DROP procedure IF EXISTS `sp_operacion_cuentabancaria`;*/
DELIMITER $$
CREATE PROCEDURE `sp_operacion_cuentabancaria`
  (IN _itipo_operacion INT
  ,IN _idcuentabancaria INT
  ,IN _idusuario INT
  ,IN _i_tipo_cuenta INT
  ,IN _i_moneda INT
  ,IN _i_banco INT
  ,IN _v_banco VARCHAR(150)
  ,IN _v_numero_cuenta VARCHAR(150)
  ,IN _v_nombre_cuenta VARCHAR(150)
  ,IN _i_tipo_declaracion INT
  ,IN _v_titular VARCHAR(150)
  ,IN _dt_fecharegistro VARCHAR(25)
  ,IN _v_horaregistro VARCHAR(25))
BEGIN

  IF (_itipo_operacion = 1) THEN
  BEGIN
    
  INSERT INTO t_cuenta_bancaria(
       idusuario
      ,i_tipo_cuenta
      ,i_moneda
      ,i_banco
      ,v_banco
      ,v_numero_cuenta
      ,v_nombre_cuenta
      ,i_tipo_declaracion
      ,v_titular
      ,b_estado
      ,dt_fecharegistro
      ,v_horaregistro)
    VALUES (
       _idusuario
      ,_i_tipo_cuenta
      ,_i_moneda
      ,_i_banco
      ,_v_banco
      ,_v_numero_cuenta
      ,_v_nombre_cuenta
      ,_i_tipo_declaracion
      ,_v_titular
      ,1
      ,STR_TO_DATE(_dt_fecharegistro, '%Y-%m-%d')
      ,_v_horaregistro);

  END;
  ELSE 
  BEGIN

    UPDATE t_cuenta_bancaria SET
      i_tipo_cuenta = _i_tipo_cuenta,
      i_moneda = _i_moneda,
      i_banco = _i_banco,
      v_banco = _v_banco,
      v_numero_cuenta = _v_numero_cuenta,
      v_nombre_cuenta = _v_nombre_cuenta,
      i_tipo_declaracion = _i_tipo_declaracion,
      v_titular = _v_titular,
      dt_fechamodificacion = STR_TO_DATE(_dt_fecharegistro, '%Y-%m-%d'),
      v_horamodificacion = _v_horaregistro,
      idusuario_Modificacion = _idusuario
      WHERE idusuario = idcuentabancaria;

  END;
  END IF;

END$$
DELIMITER ;

-- --------------------------------------------------------------------------

/*DROP procedure IF EXISTS `sp_listar_cuentabancaria`;*/
DELIMITER $$
CREATE PROCEDURE `sp_listar_cuentabancaria`(IN _idusuario INT, IN _nombres VARCHAR(50))
BEGIN

  SELECT
     c.idcuentabancaria
    ,c.idusuario
    ,u.v_nombres
    ,u.v_apellidos
    ,c.i_tipo_cuenta
    ,c.i_moneda
    ,c.i_banco
    ,c.v_banco
    ,c.v_numero_cuenta
    ,c.v_nombre_cuenta
    ,c.i_tipo_declaracion
    ,c.v_titular
    ,c.b_estado
    ,DATE_FORMAT(c.dt_fecharegistro, '%d-%m-%Y') as 'dt_fecharegistro'
    ,c.v_horaregistro
    ,c.idusuario_Modificacion
  FROM t_cuenta_bancaria c
    LEFT JOIN t_usuario u ON u.idusuario = c.idusuario
    WHERE ((c.idusuario = _idusuario) OR (_idusuario = 0))
     AND (CONCAT_WS(" ", u.v_nombres,  u.v_apellidos) LIKE CONCAT('%', _nombres, '%') OR _nombres = "vacio")
     AND u.b_estado = 1;

END$$
DELIMITER ;

-- --------------------------------------------------------------------------

/*DROP procedure IF EXISTS `sp_operacion_divisa`;*/
DELIMITER $$
CREATE PROCEDURE `sp_operacion_divisa`
  (IN _itipo_operacion INT
  ,IN _iddivisa INT
  ,IN _idusuario INT
  ,IN _i_tipo_busqueda INT
  ,IN _d_monto DECIMAL(10,3)
  ,IN _d_soles_venta DECIMAL(10,3)
  ,IN _d_soles_compra DECIMAL(10,3)
  ,IN _d_dolares_venta DECIMAL(10,3)
  ,IN _d_dolares_compra DECIMAL(10,3)
  ,IN _i_tipo_promocion INT
  ,IN _dt_fecha VARCHAR(25)
  ,IN _v_hora VARCHAR(25)
  ,IN _dt_fecharegistro VARCHAR(25))
BEGIN

  IF (_itipo_operacion = 1) THEN
  BEGIN
    
  INSERT INTO t_divisa(
     idusuario
    ,i_tipo_busqueda
    ,d_monto
    ,d_soles_venta
    ,d_soles_compra
    ,d_dolares_venta
    ,d_dolares_compra
    ,i_tipo_promocion
    ,dt_fecha
    ,v_hora
    ,b_estado
    ,dt_fecharegistro)
  VALUES (
     _idusuario
    ,_i_tipo_busqueda
    ,_d_monto
    ,_d_soles_venta
    ,_d_soles_compra
    ,_d_dolares_venta
    ,_d_dolares_compra
    ,_i_tipo_promocion
    ,STR_TO_DATE(_dt_fecha, '%Y-%m-%d')
    ,_v_horaregistro
    ,1
    ,STR_TO_DATE(_dt_fecharegistro, '%Y-%m-%d'));

  END;
  ELSE 
  BEGIN

    UPDATE t_divisa SET
      i_tipo_busqueda = _i_tipo_busqueda,
      d_monto = _d_monto,
      d_soles_venta = _d_soles_venta,
      d_soles_compra = _d_soles_compra,
      d_dolares_venta = _d_dolares_venta,
      d_dolares_compra = _d_dolares_compra,
      i_tipo_promocion = _i_tipo_promocion,
      dt_fecha = STR_TO_DATE(_dt_fecha, '%Y-%m-%d'),
      dt_fechamodificacion = STR_TO_DATE(_dt_fecharegistro, '%Y-%m-%d'),
      v_horamodificacion = _v_horaregistro
    WHERE iddivisa = _iddivisa;
  
  END;
  END IF;

END$$
DELIMITER ;

-- --------------------------------------------------------------------------

/*DROP procedure IF EXISTS `sp_listar_divisa`;*/
DELIMITER $$
CREATE PROCEDURE `sp_listar_divisa`(IN _iddivisa INT,IN _dt_fecha VARCHAR(50))
BEGIN

  SELECT
     d.iddivisa
    ,d.idusuario
    ,d.i_tipo_busqueda
    ,d.d_monto
    ,d.d_soles_venta
    ,d.d_soles_compra
    ,d.d_dolares_venta
    ,d.d_dolares_compra
    ,d.i_tipo_promocion
    ,DATE_FORMAT(d.dt_fecha, '%d-%m-%Y') as 'dt_fecha',
    ,d.v_hora
    ,DATE_FORMAT(d.dt_fecharegistro, '%d-%m-%Y') as 'dt_fecharegistro'
    ,DATE_FORMAT(d.dt_fechamodificacion, '%d-%m-%Y') as 'dt_fechamodificacion'
    ,d.v_horamodificacion
  FROM t_divisa d
    WHERE
     AND (DATE_FORMAT(d.dt_fecha, '%d-%m-%Y') = STR_TO_DATE(_dt_fecha, '%Y-%m-%d') OR (_dt_fecha = 'vacio')
     AND d.b_estado = 1
     AND ((d.iddivisa = _iddivisa) OR (_iddivisa = 0));

END$$
DELIMITER ;

-- --------------------------------------------------------------------------

/*DROP procedure IF EXISTS `sp_operacion_transaccion`;*/
DELIMITER $$
CREATE PROCEDURE `sp_operacion_transaccion`
  (IN _itipo_operacion INT
  ,IN _idtransaccion INT
  ,IN _idusuario INT
  ,IN _iddivisa INT
  ,IN _idpromocion INT
  ,IN _idcuentabancaria INT
  ,IN _idusuario_administrador INT
  ,IN _dt_fecha VARCHAR(25)
  ,IN _i_tipo_divisa INT
  ,IN _d_dolares_venta DECIMAL(10,3)
  ,IN _d_dolares_compra DECIMAL(10,3)
  ,IN _v_hora VARCHAR(25)
  ,IN _v_imagen_ruta VARCHAR(500)
  ,IN _i_tipo_envio INT
  ,IN _i_estado INT
  ,IN _v_operacion VARCHAR(150)
  ,IN _i_origen_fondo INT
  ,IN _d_envio DECIMAL(10,3)
  ,IN _d_recibo DECIMAL(10,3)
  ,IN _i_tipo_cambio INT
  ,IN _i_tipo_trasaccion INT
  ,IN _d_igv DECIMAL(10,3)
  ,IN _v_banco_receptor VARCHAR(150)
  ,IN _dt_fecharegistro VARCHAR(25)
  ,IN _dt_fechamodificacion VARCHAR(25)
  ,IN _idusuario_Modificacion INT)
BEGIN

  IF (_itipo_operacion = 1) THEN
  BEGIN
    
  INSERT INTO t_transaccion(
     idusuario
    ,iddivisa
    ,idpromocion
    ,idcuentabancaria
    ,idusuario_administrador
    ,dt_fecha
    ,i_tipo_divisa
    ,d_dolares_venta
    ,d_dolares_compra
    ,v_hora
    ,v_imagen_ruta
    ,i_tipo_envio
    ,i_estado
    ,v_operacion
    ,i_origen_fondo
    ,d_envio
    ,d_recibo
    ,i_tipo_cambio
    ,i_tipo_trasaccion
    ,d_igv
    ,v_banco_receptor
    ,b_estado
    ,dt_fecharegistro)
  VALUES (
    ,_idusuario
    ,_iddivisa
    ,_idpromocion
    ,_idcuentabancaria
    ,_idusuario_administrador
    ,STR_TO_DATE(_dt_fecha, '%Y-%m-%d')
    ,_i_tipo_divisa
    ,_d_dolares_venta
    ,_d_dolares_compra
    ,_v_hora
    ,_v_imagen_ruta
    ,_i_tipo_envio
    ,_i_estado
    ,_v_operacion
    ,_i_origen_fondo
    ,_d_envio
    ,_d_recibo
    ,_i_tipo_cambio
    ,_i_tipo_trasaccion
    ,_d_igv
    ,_v_banco_receptor
    ,1
    ,STR_TO_DATE(_dt_fecharegistro, '%Y-%m-%d'));

  END;
  ELSE 
  BEGIN
  
    UPDATE t_transaccion SET
      idusuario_administrador = _idusuario_administrador,
      v_imagen_ruta = _v_imagen_ruta,
      i_estado = _i_estado,
      v_operacion = _v_operacion,
      i_origen_fondo = _i_origen_fondo,
      d_igv = _d_igv,
      v_banco_receptor = _v_banco_receptor,
      dt_fechamodificacion = STR_TO_DATE(_dt_fecharegistro, '%Y-%m-%d'),
      idusuario_Modificacion = _idusuario_Modificacion
    WHERE idtransaccion = _idtransaccion;
  
  END;
  END IF;

END$$
DELIMITER ;

-- --------------------------------------------------------------------------

/*DROP procedure IF EXISTS `sp_listar_transaccion`;*/
DELIMITER $$
CREATE PROCEDURE `sp_listar_transaccion`(IN _idusuario INT, IN _dt_fecha VARCHAR(50))
BEGIN

  SELECT 
   t.idtransaccion
  ,t.idusuario
  ,u.v_nombres
  ,u.v_apellidos
  ,t.iddivisa
  ,t.idpromocion
  ,t.idcuentabancaria
  ,t.idusuario_administrador
  ,DATE_FORMAT(t.dt_fecha, '%d-%m-%Y') as 'dt_fecha'
  ,t.i_tipo_divisa
  ,t.d_dolares_venta
  ,t.d_dolares_compra
  ,t.v_hora
  ,t.v_imagen
  ,t.v_imagen_ruta
  ,t.i_tipo_envio
  ,t.i_estado
  ,t.v_operacion
  ,t.i_origen_fondo
  ,t.d_envio
  ,t.d_recibo
  ,t.i_tipo_cambio
  ,t.i_tipo_trasaccion
  ,t.d_igv
  ,t.v_banco_receptor
  ,DATE_FORMAT(t.dt_fecharegistro, '%d-%m-%Y') as 'dt_fecharegistro'
  FROM t_transaccion t
    LEFT JOIN t_usuario u ON u.idusuario = t.idusuario
    WHERE
     AND (DATE_FORMAT(d.dt_fecha, '%d-%m-%Y') = STR_TO_DATE(_dt_fecha, '%Y-%m-%d') OR (_dt_fecha = 'vacio')
     AND ((t.idusuario = _idusuario) OR (_idusuario = 0))
     AND t.b_estado = 1 ORDER BY t.idtransaccion DESC;

END$$
DELIMITER ;

-- --------------------------------------------------------------------------


/* 
#0f803b verde
#f5c830 amarillo
*/

/* 
25MARTES: 4pm-8pm
26MIERCOLES: 9am-11am / 3pm-7pm
27JUVES: 10am-1pm
28VIERNES: 11am-1pm
31lunes: 07am-10am
01martes: 7pm-8pm
02miercoles: 6pm-8pm
03jueves:
*/
