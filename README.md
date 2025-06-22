
# 🛒 Talabat E-Commerce API

A complete and scalable e-commerce backend built with **ASP.NET Core**, designed using clean architecture principles and integrated with modern tools like **Stripe**, **Redis**, and **JWT Authentication**.

---

## 📖 Project Summary

This project is a backend simulation of an e-commerce platform like Talabat. It provides a RESTful API with features covering user authentication, product management, order processing, basket functionality, and online payments.

---

## ✨ Key Features

- ✅ **JWT Authentication & Authorization**
- 📦 **Product Catalog** with filtering, sorting, and pagination
- 🛒 **Shopping Cart / Basket** system
- 📃 **Order Management** with delivery tracking
- 💳 **Stripe Payment Integration**
- 🔐 **Role-based Access Control**
- ⚡ **Redis Caching** for improved performance
- 🧩 **Repository & Unit of Work Patterns**
- 📂 **Clean Architecture (Onion Architecture)**
- 📘 **Swagger UI** for API testing

---

## 🧰 Technologies Used

| Layer        | Technology                          |
|--------------|-------------------------------------|
| Framework    | ASP.NET Core Web API                |
| Database     | SQL Server, Entity Framework Core   |
| Caching      | Redis                               |
| Auth         | JWT                                 |
| Payment      | Stripe API                          |
| Mapping      | AutoMapper                          |
| Documentation| Swagger (Swashbuckle)               |

---

## 📁 Project Structure



Talabat-ECommerce/
│
├── Talabat.APIs        → API Layer (Controllers, Middleware)
├── Talabat.Core        → Domain Entities, Interfaces, Specifications
├── Talabat.Repository  → EF Core Repositories, UoW Implementation
├── Talabat.Service     → Business Logic & External Integrations
└── Talabat.Domain      → Custom Models, Enums, Value Objects

````

---

## ⚙️ Getting Started

### Prerequisites

- [.NET 6 SDK](https://dotnet.microsoft.com/download)
- SQL Server
- Redis (local or cloud-based)
- Stripe account (for payments)

### Setup Instructions

1. **Clone the repository**

```bash
git clone https://github.com/MahmoudAmin5/Talabat-ECommerce.git
cd Talabat-ECommerce
````

2. **Configure `appsettings.json`**

Update the following:

```json
"ConnectionStrings": {
  "DefaultConnection": "Your_SQL_Server_Connection_String"
},
"StripeSettings": {
  "SecretKey": "your_secret_key",
  "PublishableKey": "your_publishable_key"
}
```

3. **Run migrations and seed database**

```bash
dotnet ef database update --project Talabat.Repository
```

4. **Start the API**

```bash
dotnet run --project Talabat.APIs
```

5. **Open Swagger UI**

Visit: `https://localhost:5001/swagger`

---

## 🧪 Testing

Run all test projects:

```bash
dotnet test
```

---

## 📬 Contact

**Author:** Mahmoud Amin
**GitHub:** [@MahmoudAmin5](https://github.com/MahmoudAmin5)
**Email:** [mahmoud886.amin@gmail.com](mahmoud886.amin@gmail.com)

---

## 🙌 Contributions

Contributions, bug reports, and feature requests are welcome!
Feel free to open an issue or submit a pull request.

