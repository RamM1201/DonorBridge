# DonorBridge

A comprehensive donor management platform that facilitates secure donation processing, user registration, and administrative oversight with enterprise-grade security features.

## Features

### Core Functionality
#### Secure Authentication - JWT-based authentication with role-based authorization
#### Donation Processing - Integrated payment gateway (Razorpay) for secure transactions
#### User Management - Complete user registration and profile management 
#### Admin Dashboard - Comprehensive reporting and monitoring capabilities
#### Transaction Tracking - Real-time donation and payment status monitoring 

### Technical Features
#### JWT Authentication - Stateless token-based authentication 
#### Structured Logging - Serilog integration for comprehensive audit trails
#### Global Exception Handling - Centralized error management with standardized responses
#### RESTful API - Clean, well-documented REST endpoints 
#### Stored Procedures - Optimized database operations with pre-compiled execution plans 

## Technology Stack

Component              Technology              Version
----------------------------------------------------------
Backend Framework      ASP.NET Core Web API    .NET 10.0    
Frontend Framework     ASP.NET Core MVC        .NET 10.0
Database               Microsoft SQL Server    2022
ORM                    Dapper(Micro ORM)       2.1.66
Authentication         JWT                     Built-in
Logging                Serilog                 Latest
Payment Gateway        Razorpay                3.3.2
API Documentation      Swagger/OpenAPI         10.1.0
Exception Handling     Custom Middleware       Built-in

