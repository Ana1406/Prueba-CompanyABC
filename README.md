# Company-ABC
Prueba Técnica – Desarrollador Fullstack  
Tecnologías: .NET 8/9, Angular 21, MongoDB / PostgreSQL, Docker, Docker Compose

##  Diseño de Arquitectura Propuesto
La siguiente arquitectura representa el **diseño ideal planteado para la solución**, siguiendo principios de microservicios desacoplados y separación de responsabilidades.
 <img src="docs/architecture.png" width="800" />
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
