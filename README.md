# 馃搳 Evaluaci贸n T茅cnica - Gesti贸n de Pr茅stamos (ASP.NET WebForms + Ext.NET + Crystal Reports + Oracle)

**馃懁 Autor:** Joseth David Acosta Loayza
**馃搮 Fecha:** 08 Setiembre 2025

**Tipo de proyecto:** Aplicaci贸n web ASP.NET (.NET Framework 4.7.2)  
**Frontend:** Ext.NET (puede reemplazarse por alternativa)  
**Reportes:** Crystal Reports  
**Base de Datos:** Oracle 11g XE o superior (desarrollado con Oracle SQL Developer)  

---

## 馃搫 Descripci贸n

Este proyecto implementa una **aplicacion web ASP.NET WebForms** con arquitectura en capas para la gestion de prestamos.  
Incluye la administracion de prestamos, cuotas y reportes utilizando **Oracle Database**.

---

## 馃彈锔?Arquitectura del Proyecto

La soluci贸n est谩 organizada en 3 capas:

1. **EvaluacionTecnica.Datos**  
   - Contiene la l贸gica de acceso a datos con Oracle.  
   - Repositorios (`CuotaRepository`, `PrestamoRepository`).  
   - Clase `ConexionDB` para manejar la conexi贸n.

2. **EvaluacionTecnica.Negocio**  
   - Implementa la l贸gica de negocio.  
   - Clases `CuotaService`, `PrestamoService`.

3. **EvaluacionTecnica.Web**  
   - Proyecto ASP.NET WebForms.  
   - Formularios (`RegistroPrestamo.aspx`, `CuotasPendientes.aspx`).  
   - Reportes (`ReportePrestamos.rpt`).  
   - Configuraci贸n (`Web.config`).

---

## 馃П Requisitos Previos

- Visual Studio 2019/2022.  
- .NET Framework 4.7.2.  
- Crystal Reports Developer para VS (runtime y extensiones instaladas).  
- Oracle 11g XE (o superior) y Oracle SQL Developer.  
- Paquetes NuGet: `Oracle.ManagedDataAccess`.

---

## 馃梽锔?Base de Datos

Dentro de la carpeta **Database/** se encuentra el script:

- `ScriptBD.sql` 鈫?Contiene:
  - Creaci贸n de la base de datos.  
  - Tablas (`Prestamos`, `Cuotas`, etc.).  
  - Stored Procedures para operaciones principales.  

---

## 鈿欙笍 Instalaci贸n y Ejecuci贸n

1. Clonar el repositorio:
   ```bash
   git clone https://github.com/tuusuario/BDCapital_EvTecnica.git

2. Restaurar BD ejecutando Database/ScriptBD.sql.

   - Abrir Oracle SQL Developer o tu cliente preferido.
   - Ejecutar el script Database/ScriptBD.sql.

3. Configurar la cadena de conexi贸n en Web.config:
```json
<connectionStrings>
  <add name="OracleDbContext"
       connectionString="User Id=USUARIO;Password=CLAVE;Data Source=SERVIDOR:1521/XE;"
       providerName="Oracle.ManagedDataAccess.Client" />
</connectionStrings>
```

4. Ejecutar el proyecto desde Visual Studio:

   - Establecer EvaluacionTecnica.Web como proyecto de inicio.
   - Presionar F5.
   
## 馃И Funcionalidades Clave (Demo)

- Registro de Cliente y Prestamo: formulario + calculo de cronograma (grilla Ext.NET).
- Consulta de Cuotas Pendientes/Vencidas: filtro por cliente/pr茅stamo, bot贸n Pagar.
- Reporte PDF de Prestamos: boton Imprimir 鈫?Crystal Reports 鈫?visor PDF.

## 馃 Decisiones Tecnicas

- Ext.NET para UI r谩pida con grillas y formularios.
- Crystal Reports para salida a PDF sin dependencias externas.
- Stored Procedures para aislar la l贸gica SQL y cumplir el requisito sin SQL embebido.
- Capas (Datos/Negocio/Web) para separaci贸n de responsabilidades.
   
## 馃摤 Contacto

- 鉁夛笍 Email: joseth.acosta92@gmail.com
- 馃敆 LinkedIn: https://www.linkedin.com/in/joseth-acosta/
- 馃惓 Docker: https://hub.docker.com/u/jacosta92