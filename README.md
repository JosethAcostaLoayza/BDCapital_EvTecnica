# 📊 Evaluación Técnica - Gestión de Préstamos (ASP.NET WebForms + Ext.NET + Crystal Reports + Oracle)

**👤 Autor:** Joseth David Acosta Loayza
**📅 Fecha:** 08 Setiembre 2025

**Tipo de proyecto:** Aplicación web ASP.NET (.NET Framework 4.7.2)  
**Frontend:** Ext.NET (puede reemplazarse por alternativa)  
**Reportes:** Crystal Reports  
**Base de Datos:** Oracle 11g XE o superior (desarrollado con Oracle SQL Developer)  

---

## 📄 Descripción

Este proyecto implementa una **aplicacion web ASP.NET WebForms** con arquitectura en capas para la gestion de prestamos.  
Incluye la administracion de prestamos, cuotas y reportes utilizando **Oracle Database**.

---

## 🏗�?Arquitectura del Proyecto

La solución está organizada en 3 capas:

1. **EvaluacionTecnica.Datos**  
   - Contiene la lógica de acceso a datos con Oracle.  
   - Repositorios (`CuotaRepository`, `PrestamoRepository`).  
   - Clase `ConexionDB` para manejar la conexión.

2. **EvaluacionTecnica.Negocio**  
   - Implementa la lógica de negocio.  
   - Clases `CuotaService`, `PrestamoService`.

3. **EvaluacionTecnica.Web**  
   - Proyecto ASP.NET WebForms.  
   - Formularios (`RegistroPrestamo.aspx`, `CuotasPendientes.aspx`).  
   - Reportes (`ReportePrestamos.rpt`).  
   - Configuración (`Web.config`).

---

## 🧱 Requisitos Previos

- Visual Studio 2019/2022.  
- .NET Framework 4.7.2.  
- Crystal Reports Developer para VS (runtime y extensiones instaladas).  
- Oracle 11g XE (o superior) y Oracle SQL Developer.  
- Paquetes NuGet: `Oracle.ManagedDataAccess`.

---

## 🗄�?Base de Datos

Dentro de la carpeta **Database/** se encuentra el script:

- `ScriptBD.sql` �?Contiene:
  - Creación de la base de datos.  
  - Tablas (`Prestamos`, `Cuotas`, etc.).  
  - Stored Procedures para operaciones principales.  

---

## ⚙️ Instalación y Ejecución

1. Clonar el repositorio:
   ```bash
   git clone https://github.com/tuusuario/BDCapital_EvTecnica.git

2. Restaurar BD ejecutando Database/ScriptBD.sql.

   - Abrir Oracle SQL Developer o tu cliente preferido.
   - Ejecutar el script Database/ScriptBD.sql.

3. Configurar la cadena de conexión en Web.config:
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
   
## 🧪 Funcionalidades Clave (Demo)

- Registro de Cliente y Prestamo: formulario + calculo de cronograma (grilla Ext.NET).
- Consulta de Cuotas Pendientes/Vencidas: filtro por cliente/préstamo, botón Pagar.
- Reporte PDF de Prestamos: boton Imprimir �?Crystal Reports �?visor PDF.

## 🧠 Decisiones Tecnicas

- Ext.NET para UI rápida con grillas y formularios.
- Crystal Reports para salida a PDF sin dependencias externas.
- Stored Procedures para aislar la lógica SQL y cumplir el requisito sin SQL embebido.
- Capas (Datos/Negocio/Web) para separación de responsabilidades.
   
## 📬 Contacto

- ✉️ Email: joseth.acosta92@gmail.com
- 🔗 LinkedIn: https://www.linkedin.com/in/joseth-acosta/
- 🐳 Docker: https://hub.docker.com/u/jacosta92