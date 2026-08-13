# SalesOps — Proje Dokümantasyonu

## 📌 Proje Özeti

SalesOps, **multi-tenant** bir **prim hesaplama** sistemidir.  
Firmalar ve şubeleri bazında satış verilerini takip eder, personellerin primlerini otomatik hesaplar.

- Her **firma (Company)** birden fazla **şubeye (Branch)** sahip olabilir.
- Her şubede **personeller (Employee)** çalışır.
- Personeller **ürün (Catalog)** satar ve satışa göre **prim** kazanır.
- Prim hesabı ürünün `CommissionAmount` değeri × satış adedi üzerinden yapılır.

---

## 🏗️ Mimari

```
Client
  ↓
[Identity]         ← JWT token üretir / doğrular
  ↓
[Ocelot Gateway]   ← Token doğrular, servislere yönlendirir
  ↓         ↓         ↓
Sales     Catalog   Employee
  ↓
[RabbitMQ / MassTransit]   ← Async event bus
  ↑              ↑
Catalog       Employee      ← Eventleri dinler
```

---

## 🧩 Servisler

| Servis | Port | Açıklama |
|---|---|---|
| **SalesOps.DuendeIdentityServer** | 5054 | Duende IdentityServer — OpenID Connect / OAuth 2.0 ✅ |
| **SalesOps.Sales.WebApi** | 5058 | Satış kayıtları, prim hesabı + JWT Auth ✅ |
| **SalesOps.Catalog.WebApi** | 5042 | Ürünler, kategori, fiyat, komisyon oranı + JWT Auth ✅ |
| **SalesOps.Employee.WebApi** | 5001 | Personel bilgileri, şube atamaları + JWT Auth ✅ |
| **SalesOps.Company.WebApi** | 5002 | Firma ve şube yönetimi + JWT Auth ✅ |
| **SalesOps.Gateway** | 5010 | Ocelot API Gateway *(sırada)* |

---

## 🗄️ Veri Kaynakları

| Servis | Veritabanı |
|---|---|
| Sales | **Elasticsearch** |
| Catalog | **MongoDB** |
| Employee | **MSSQL** (EF Core) |
| Company | **MSSQL** (EF Core) |

---

## 📦 Teknoloji Stack

| Kategori | Teknoloji |
|---|---|
| Framework | .NET 9 |
| API Gateway | **Ocelot** |
| Identity / Auth | **Duende IdentityServer** (OpenID Connect + OAuth 2.0) |
| Mesajlaşma | **RabbitMQ + MassTransit** |
| Search / NoSQL | **Elasticsearch** |
| NoSQL | **MongoDB** |
| İlişkisel DB | **MSSQL** |
| ORM | **Entity Framework Core** |
| API Docs | **Swagger (Swashbuckle)** |
| Container | **Docker** |

---

## 🔄 Servisler Arası İletişim

### Sync (HttpClient)
- `Sales → Catalog` : Satış oluştururken ürün bilgisi (fiyat, komisyon, barkod) çekilir
- `Sales → Employee` : Satış oluştururken personel bilgisi çekilir *(TODO)*

### Async (RabbitMQ Events)
| Event | Publisher | Subscriber | Açıklama |
|---|---|---|---|
| `SaleCreated` | Sales | Employee | Prim hesabı tetiklenir |
| `SaleCreated` | Sales | Catalog | Stok düşürülür *(opsiyonel)* |
| `ProductUpdated` | Catalog | Sales | Fiyat/komisyon cache güncellenir |
| `StockDepleted` | Catalog | Sales | Stok bitti bildirimi |

---

## ✅ Tamamlananlar

### Mikroservisler
- [x] Sales — Repository (Elasticsearch CRUD + query)
- [x] Sales — Service katmanı
- [x] Sales — Controller (API endpointleri)
- [x] Sales — Swagger entegrasyonu + JWT Authorize butonu
- [x] Sales — JWT Authentication (JwtBearer + [Authorize])
- [x] Sales — Catalog entegrasyonu (HttpClient)
- [x] Catalog — Servis yapısı (MongoDB + Clean Architecture)
- [x] Catalog — JWT Authentication + Swagger Authorize
- [x] Employee — Build başarılı (MSSQL + CQRS/MediatR)
- [x] Employee — JWT Authentication + Swagger Authorize
- [x] Company — JWT Authentication + Swagger Authorize

### Identity & Authentication
- [x] **Duende IdentityServer** kurulumu (port 5054)
  - [x] ApplicationUser (CompanyId, BranchId custom properties)
  - [x] ApplicationDbContext (ASP.NET Core Identity)
  - [x] Config.cs (Clients, Scopes, ApiResources)
  - [x] CustomProfileService (company_id, branch_id, role claims)
  - [x] DatabaseInitializer (seed data: Admin/Manager/Staff rolleri, admin kullanıcısı)
  - [x] 3 EF Migration oluşturuldu ve uygulandı
  - [x] Token endpoint test edildi ✅
  - [x] Discovery endpoint (`/.well-known/openid-configuration`) çalışıyor
  
### JWT Authentication (Tüm API'ler)
- [x] Sales API — `Microsoft.AspNetCore.Authentication.JwtBearer` eklendi
- [x] Catalog API — JWT middleware + Swagger authorize
- [x] Employee API — JWT middleware + Swagger authorize  
- [x] Company API — JWT middleware + Swagger authorize
- [x] Tüm API'lerde token validation test edildi ✅

### Seed Data
- [x] Identity Server'da: Admin/Manager/Staff rolleri
- [x] Admin kullanıcısı: `admin / Admin1234!` (CompanyId: 1, Role: Admin)
- [x] Client tanımları: `salesops-ro` (ResourceOwnerPassword), `salesops-service` (ClientCredentials)

---

## 🔧 Yapılacaklar

- [x] ~~Identity servisi kur~~ ✅ **TAMAMLANDI**
- [ ] **Ocelot Gateway** kur (port 5010)
  - [ ] Ocelot.JwtAuthorize paketi
  - [ ] ocelot.json (downstream routing: Sales, Catalog, Employee, Company)
  - [ ] JWT validation Gateway'de
  - [ ] Rate limiting (opsiyonel)
- [ ] **Employee entegrasyonu** — Sales'de personel bilgisi çekme (TODO)
- [ ] **RabbitMQ + MassTransit** kur
- [ ] `SaleCreated` eventi yayınla → Employee prim hesabı
- [ ] Update işleminde ProductId/StaffId değişince API'den yeni bilgi çek
- [ ] Uçtan uca test (Gateway → Sales → Catalog/Employee entegrasyonu)

---

## 🗂️ Proje Klasör Yapısı

```
SalesOps/
├── Gateway/
│   └── SalesOps.Gateway/              ← Ocelot (sırada)
├── IdentityServer/
│   └── SalesOps.DuendeIdentityServer/ ← Duende IdentityServer ✅
├── Shared/
│   └── SalesOps.Shared.Messaging/     ← MassTransit events (yapılacak)
├── Services/
│   ├── Sales/
│   │   └── SalesOps.Sales.WebApi/     ← JWT ✅
│   ├── Catalog/
│   │   ├── Core/
│   │   ├── Infrastructure/
│   │   └── Presentation/              ← JWT ✅
│   ├── Employee/
│   │   ├── Core/
│   │   ├── Infrastructure/
│   │   ├── Presentation/              ← JWT ✅
│   │   └── Tests/
│   └── Company/
│       ├── Core/
│       ├── Infrastructure/
│       └── Presentation/              ← JWT ✅
└── PROJECT.md                         ← Bu dosya
```

---

## 🔐 Multi-Tenant Yapısı

- Her istek `CompanyId` (zorunlu) ve `BranchId` (opsiyonel) ile tenant'ı belirler.
- Elasticsearch sorgularında tüm filtrelemeler `CompanyId` + `BranchId` bazında yapılır.
- Identity servisinde kullanıcıya `CompanyId`, `BranchId` ve `Role` claim olarak atanacak.
- Gateway, token içindeki claim'leri downstream servislere header olarak iletecek.
- Duende IdentityServer kullanıcı verilerini **MSSQL**'de ASP.NET Core Identity tabloları ile saklar.
