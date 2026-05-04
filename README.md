# 🚀 Webhooks Processing con .NET 8, RabbitMQ y Docker

![.NET](https://img.shields.io/badge/.NET-8-blue)
![RabbitMQ](https://img.shields.io/badge/RabbitMQ-Message%20Broker-orange)
![Docker](https://img.shields.io/badge/Docker-Containerized-blue)
![Architecture](https://img.shields.io/badge/Architecture-Event--Driven-green)

---

## 📌 Descripción

Sistema distribuido basado en eventos que implementa el procesamiento de webhooks utilizando **.NET 8** y **RabbitMQ**.

La solución demuestra cómo construir aplicaciones desacopladas mediante mensajería, permitiendo el procesamiento asíncrono de eventos entre múltiples servicios.

---

## 🧱 Arquitectura

### 🔹 High-Level Architecture
![High-Level Architecture](./docs/high-level.png)

### 🔹 Solution Architecture
![Solution Architecture](./docs/solution-architecture.png)

---

## ⚙️ Flujo del sistema

```mermaid
sequenceDiagram
    participant Client
    participant WebhooksAPI
    participant RabbitMQ
    participant SendAgent
    participant ExternalSystem

    Client->>WebhooksAPI: Registrar webhook
    WebhooksAPI->>RabbitMQ: Publicar evento
    RabbitMQ->>SendAgent: Consumir mensaje
    SendAgent->>ExternalSystem: HTTP POST webhook
