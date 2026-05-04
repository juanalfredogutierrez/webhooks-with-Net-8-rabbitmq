# Webhooks Processing con .NET 8 y RabbitMQ

## 📌 Descripción
Sistema distribuido basado en eventos que permite el procesamiento de webhooks mediante comunicación asíncrona entre servicios utilizando RabbitMQ.

## 🧱 Arquitectura
El sistema está compuesto por múltiples servicios independientes que se comunican a través de colas:

- Servicio receptor de webhooks
- Servicio procesador de eventos
- Servicio consumidor

## 🚀 Tecnologías
- .NET 8
- RabbitMQ
- Web API

## ⚙️ Características
- Procesamiento asíncrono de eventos
- Desacoplamiento entre servicios
- Escalabilidad mediante colas
- Manejo de eventos tipo webhook

## ▶️ Cómo ejecutar
1. Levantar RabbitMQ
2. Ejecutar los proyectos por separado
3. Enviar webhook al endpoint
4. Ver procesamiento en cola

## 💡 Objetivo
Demostrar arquitectura basada en eventos y comunicación entre servicios utilizando mensajería.

## 👨‍💻 Autor
Juan Gutierrez
