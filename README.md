# FashionTrend API - Supplier and Service Order Management WebAPI

This repository contains a .NET WebAPI for managing suppliers and service orders. The application utilizes the Kafka messaging system for asynchronous communication among the various components of the system.

## Features

1. **Registration of Materials, Products, and Suppliers:**
   - The API allows the registration of materials, products, and suppliers, crucial for effective service order management.

2. **Service Order Submission and Acceptance:**
   - Clients (FashionTrend) can post service orders.
   - Suppliers can accept service orders that match the materials in the products they offer.

3. **Contract Management:**
   - Upon successfully accepting a service order, the system checks for an active contract for the supplier.
   - If one exists, the service order is associated with the existing contract. Otherwise, a new contract is automatically created.
   - Contracts have a default validity period of 1 year.

4. **Service Completion and Payment:**
   - Suppliers can notify the completion of a service through the system.
   - FashionTrend initiates total or partial payment for the service order when the service is successfully completed.

## API Routes

The API is documented using Swagger. Access `/swagger` to explore and interact with the available routes.

1. **Registration:**
   - `/api/Materials`: CRUD for materials.
   - `/api/Products`: CRUD for products.
   - `/api/Suppliers`: CRUD for suppliers.

2. **Service Orders:**
   - `/api/Orders`: Creation and listing of service orders.
   - `/api/Orders/{orderId}/accept`: Acceptance of a service order by a supplier.

3. **Contracts:**
   - `/api/Contracts/status/{contractStatus}`: Listing of contracts by status.
   - `/api/Contracts/{contractNumber}`: Retrieve a contract by contract number.

4. **Service Completion and Payment:**
   - `/api/Orders/{orderId}/complete`: Informs the completion of a service by the supplier.
   - `/api/Payments/`: Initiates the payment process by FashionTrend for a service order.

## Kafka Configuration

Ensure the correct configuration of Kafka parameters in the `appsettings.json` file to ensure proper functionality of asynchronous communication between components.

```json
"Kafka": {
  "BootstrapServers": "localhost:9092",
  "GroupId": "your-group-id",
  "AutoOffsetReset": "earliest",
  "EnableAutoCommit": "false"
}
```

## Prerequisites

- [ ] Installed .NET SDK
- [ ] Kafka settings in the `appsettings.json` file configured for your environment

## How to Run

1. Clone the repository: `git clone https://github.com/your-username/your-repository.git`
2. Navigate to the directory: `cd your-repository`
3. Run the application: `dotnet run`

Access Swagger at [http://localhost:5000/swagger](http://localhost:5000/swagger) to start exploring the API.

**Note:** Make sure to have Kafka running or adjust the Kafka configuration for your specific environment.
