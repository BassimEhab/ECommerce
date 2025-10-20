# 🚀 E-COMMERCE API: BACKEND MASTERPIECE 🚀

**A robust and scalable Web API built for a modern e-commerce platform, leveraging .NET 9 and Onion Architecture principles.**

[![Language Badge](https://img.shields.io/badge/Language-C%23-blue.svg)]()
[![Framework Badge](https://img.shields.io/badge/Framework-ASP.NET%20Core%209-purple.svg)]()
[![License Badge](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE)
[![Database Badge](https://img.shields.io/badge/Database-SQL%20Server-red.svg)]()

---

## 📖 Table of Contents

- [Project Overview & Goals](#project-overview--goals)
- [Key Features](#key-features)
- [Tech Stack & Architecture](#tech-stack--architecture)
- [Prerequisites](#prerequisites)
- [Getting Started](#getting-started)

---

## Project Overview & Goals

This project serves as a comprehensive **E-commerce Web API** backend built using **ASP.NET Core** and **Entity Framework Core**. It was primarily developed as a training project to strengthen **backend development skills** and demonstrate practical experience in building a scalable, maintainable, and well-structured application.

The core focus is providing a structured system for managing users, products, and a complete order lifecycle within an online shopping environment.

### Core Objectives:

* Implement the **Onion Architecture** for clean separation of concerns and maintainability.
* Provide a full **Order Management System** (from basket creation through payment).
* Showcase expertise in advanced **.NET** patterns and performance optimization (e.g., **Redis Caching**, **Specification Pattern**).

---

## Key Features

This API implements several advanced patterns and functionalities to ensure a high-quality, high-performance system:

| Category | Feature | Technical Implementation |
| :--- | :--- | :--- |
| **Architecture** | **Onion Architecture** | Ensures clean separation between core domain, application, and infrastructure layers. |
| **Performance** | **Redis Caching** ⚡ | Reduces database load and lowers latency for frequently accessed product data. |
| **Transactions** | **Full Order Management System** | Handles the entire lifecycle: Basket $\rightarrow$ Checkout $\rightarrow$ Payment via **Stripe Integration**. |
| **Security** | **JWT Authentication & Authorization** | Robust security measures, including role-based access control (Admin, Buyer). |
| **Data Management** | **CRUD Operations** | Complete functionality for managing Products, Brands, and Types. |
| **Querying** | **Specification Pattern** | Used for flexible, reusable, and testable filtering, sorting, and pagination logic. |
| **Design Patterns** | **Repository + Unit of Work** | Decouples data access logic and ensures control over database transactions. |
| **Utilities** | **AutoMapper & Exception Handling** | **AutoMapper** for clean DTO mapping and **Centralized Exception Handling** for consistent error responses. |

---

## Tech Stack & Architecture

This section highlights the technologies and design choices underpinning the project.

### 🛠️ Tech Stack

* **Backend Framework:** **.NET 9** (ASP.NET Core)
* **Database:** **MS SQL Server** via **Entity Framework Core**
* **Caching:** **Redis**
* **Payments:** **Stripe**
* **Authentication:** **JWT (JSON Web Tokens)**
* **Mapping:** **AutoMapper**

### 🧱 Design Patterns

* **Onion Architecture**
* **Repository Pattern**
* **Unit of Work**
* **Specification Pattern**

---

## Prerequisites

To set up and run the project locally, you will need the following installed:

1.  **[.NET 9 SDK]**
2.  **[SQL Server]** (LocalDB / Express or a full instance)
3.  **[Redis Server]** 
4.  **[Git]**
