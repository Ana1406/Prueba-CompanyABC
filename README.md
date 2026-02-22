# Company-ABC
Prueba Técnica – Desarrollador Fullstack  
Tecnologías: .NET 8/9, Angular 21, MongoDB / PostgreSQL, Docker, Docker Compose
El objetivo del proyecto es demostrar la capacidad de diseñar una arquitectura escalable, mantenible y preparada para entornos productivos, aplicando buenas prácticas modernas de desarrollo Fullstack.

##  Diseño de Arquitectura Propuesto
La siguiente arquitectura representa el **diseño ideal planteado para la solución**, siguiendo principios de microservicios desacoplados y separación de responsabilidades.
 <img src="docs/Diagrama de arquitectura.png" width="800" />
###  Capas Arquitectónicas

 **Capa de Presentación**
- Frontend desarrollado en Angular 21
- Comunicación vía REST
- Consume los endpoints expuestos por los microservicios

 **Capa de Entrada (Diseño Ideal)**
- En el diseño arquitectónico ideal se contempla un API Gateway para centralizar:
  - Autenticación
  - Enrutamiento
  - Logging
  - Control de acceso

 **Nota Importante:**  
En la implementación actual de la prueba técnica **NO se incluye API Gateway**, y el frontend consume directamente los microservicios.

 **Capa de Servicios**
- Microservicios desarrollados en .NET 8/9
- Arquitectura interna en capas:
  - `Backend.Api` → Controladores
  - `Backend.Core` → Lógica de negocio
  - `Backend.Domain` → Entidades, DTOs
  - `Backend.Database` → Acceso a datos

Cada microservicio es independiente y desacoplado.

 **Capa de Datos**
- Se implementa el patrón **Database per Microservice**
- Motor utilizado: **MongoDB**
- Cada microservicio gestiona su propia colección

---
# Justificación Tecnológica

##  Backend – .NET 8/9

Se seleccionó .NET como tecnología backend por las siguientes razones:

###  Alto rendimiento
.NET es uno de los frameworks con mejor desempeño en benchmarks de APIs REST, ideal para arquitecturas basadas en microservicios.

###  Soporte multiplataforma
Gracias a .NET Core/.NET 8+, las aplicaciones pueden ejecutarse tanto en:

- Servidores Windows
- Servidores Linux
- Contenedores Docker

Esto permite despliegue en entornos cloud modernos (AWS, Azure, GCP).

###  Integración con Docker
.NET tiene soporte nativo para contenedores, facilitando despliegue y escalabilidad horizontal.

###  Entity Framework Core
Permite:
- Mapeo objeto-relacional (ORM)
- Migraciones de base de datos
- Abstracción del motor de base de datos
- Inyección de dependencias
- Consultas LINQ tipadas

Esto reduce acoplamiento a nivel de infraestructura.

###  Arquitectura en Capas
.NET facilita una clara separación entre:
- API
- Core (Lógica de negocio)
- Domain
- Database

Lo que permite aplicar Clean Architecture correctamente.

---

##  Frontend – Angular 21

Angular fue seleccionado por:

###  Arquitectura robusta y estructurada
Angular promueve:

- Modularidad
- Separación de responsabilidades
- Tipado fuerte con TypeScript
- Inyección de dependencias

###  Escalabilidad
Es ideal para aplicaciones empresariales donde:

- Existen múltiples módulos
- Se requiere mantenibilidad
- Se manejan estados complejos, usando signals y ngrx o RxJS

###  Integración REST
Angular se integra fácilmente con APIs RESTful mediante los providers de HttpClient.

---

#  Justificación del Uso de PostgreSQL (Arquitectura Ideal)

Aunque en la implementación de la prueba se utiliza MongoDB, la arquitectura ideal contempla PostgreSQL como motor principal por las siguientes razones:

##  Motor robusto y empresarial

PostgreSQL es:

- Open Source
- Altamente confiable
- Compatible con estándares SQL
- Utilizado ampliamente en entornos productivos

##  Compatibilidad con Entity Framework Core

PostgreSQL se integra perfectamente con:
- Migraciones automáticas
- Modelado relacional
- Control de versiones de base de datos
##  Soporte multiplataforma

PostgreSQL es compatible con:
- Linux
- Windows
- MacOS
- Contenedores Docker
---

#  Ejecución del Proyecto con Docker
## Requisitos Previos

Antes de ejecutar el proyecto asegúrese de tener instalado:

- Docker Desktop
- Docker Compose
- WSL2 habilitado (en caso de Windows)

Verificar instalación:

```bash
docker --version
docker compose version
```
Ubíquese en la carpeta donde se encuentra el archivo docker-compose.yml
```bash
cd Company-ABC
```
Verifique que el archivo exista:
```bash
dir
```
Debe visualizar:docker-compose.yml

---
## Construir y levantar los contenedores
Ejecutar el siguiente comando:
```bash
docker compose up --build
```
Este comando realizará:
- Construcción de imágenes
- Creación de contenedores
- Configuración de red interna
- Inicio de MongoDB
- Inicio de los microservicios
- Inicio del frontend

---
## Acceso a los servicios
Acceso a los servicios

- Frontend -->http://localhost:4200
- Users API -->http://localhost:5001
- Payments API -->http://localhost:5002
- Users API-->http://localhost:5003

---
## Probar endpoint de Health y Status

```bash
http://localhost:5001/health
http://localhost:5001/status
```
