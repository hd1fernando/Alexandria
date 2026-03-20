# Alexandria - Arquitetura

## Visão Geral

Alexandria segue uma arquitetura em camadas com responsabilidade única, organizada para facilitar manutenção, testabilidade e escalabilidade.

```
┌──────────────────────────────────────────────────────────────┐
│                        Controllers                             │
│            Recebe requisições HTTP e retorna responses        │
└─────────────────────────────┬────────────────────────────────┘
                              │
┌─────────────────────────────▼────────────────────────────────┐
│                          Dtos                                  │
│            Data Transfer Objects (Request/Response)            │
└─────────────────────────────┬────────────────────────────────┘
                              │
┌─────────────────────────────▼────────────────────────────────┐
│                          Domain                               │
│                 Entidades e Value Objects                       │
│              (Regras de negócio puras)                         │
└─────────────────────────────┬────────────────────────────────┘
                              │
┌─────────────────────────────▼────────────────────────────────┐
│                       Repositories                            │
│              Persistência e mapeamentos EF Core               │
└──────────────────────────────────────────────────────────────┘
```

## Estrutura de Pastas

```
src/Alexandria.Api/
├── Controllers/
│   └── Dtos/
├── Domain/
│   ├── Entities/
│   └── ValueObjects/
└── Repositories/
    ├── Authors/
    ├── Books/
    ├── Categories/
    └── Mappings/
```

## Camadas

### Controllers

Responsabilidade: Receber requisições HTTP, validar DTOs de entrada, chamar services, retornar responses.

- Um controller por recurso (Author, Book, Category)
- Herdam de `MainController` (base com configurações comuns)
- Métodos públicos para cada endpoint (Get, Post, Put, Delete)

### Dtos

Responsabilidade: Transferência de dados entre a API e as demais camadas.

- **Request DTOs**: Validação de entrada (`CreateAuthorRequest`, `UpdateAuthorRequest`)
- **Response DTOs**: Formato das respostas (`AuthorResponse`, `AuthorListResponse`)
- Localização: `Controllers/Dtos/`

### Domain

Responsabilidade: Regras de negócio puras, sem dependências externas.

#### Entities

Objetos com identidade própria e ciclo de vida.

- Possuem ID único
- São rastreáveis no banco de dados
- Exemplos: `Author`, `Book`, `Category`

#### Value Objects

Objetos imutáveis sem identidade, definidos por seus atributos.

- Não possuem ID próprio
- São comparados pelos valores dos atributos
- Exemplos: `Address`, `Money`, `CPF`

### Repositories

Responsabilidade: Persistência e acesso a dados utilizando Entity Framework Core.

#### Interfaces

Contratos que definem as operações de acesso a dados.

- Uma interface por Entity
- Define métodos como: `GetByIdAsync`, `AddAsync`, `UpdateAsync`, `DeleteAsync`

#### Implementações

Implementações concretas das interfaces de repositório.

#### Mapeamentos

Configurações de mapeamento objeto-relacional (ORM) utilizando Fluent API do EF Core.

- Definem: nome de tabelas, colunas, relacionamentos, restrições
- Um arquivo de mapeamento por Entity

## Convenções de Código

### Entities

```csharp
public class Author : BaseEntity
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Description { get; set; }
    
    // Relacionamentos
    public ICollection<Book> Books { get; set; }
}
```

### Value Objects

```csharp
public class Address : ValueObject
{
    public string Street { get; }
    public string City { get; }
    public string State { get; }
    public string ZipCode { get; }
    
    // Construtor privado, criação via factory method
    private Address(string street, string city, string state, string zipCode)
    {
        Street = street;
        City = city;
        State = state;
        ZipCode = zipCode;
    }
}
```

### Repositories

```csharp
public interface IAuthorRepository
{
    Task<Author?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<IEnumerable<Author>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Author author, CancellationToken cancellationToken);
    Task UpdateAsync(Author author, CancellationToken cancellationToken);
    Task DeleteAsync(int id, CancellationToken cancellationToken);
}
```

### Mapeamentos EF Core

```csharp
public class AuthorMapping : IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> builder)
    {
        builder.ToTable("Authors");
        
        builder.HasKey(a => a.Id);
        
        builder.Property(a => a.Name)
            .IsRequired()
            .HasMaxLength(200);
            
        builder.Property(a => a.Email)
            .IsRequired()
            .HasMaxLength(100);
    }
}
```

## Fluxo de Dados

```
┌──────────┐    ┌────────────┐    ┌────────┐    ┌───────────┐    ┌────────────┐    ┌──────────┐
│  Client   │───▶│ Controller │───▶│  Dto   │───▶│  Service  │───▶│ Repository │───▶│ Database │
└──────────┘    └────────────┘    └────────┘    └───────────┘    └────────────┘    └──────────┘
                  ▲                                                                   │
                  │                         Domain                                     │
                  └──────────────────── (Entities & Value Objects) ◀───────────────────┘
```

## Como Criar um Novo Recurso

### 1. Criar Domain/Entity

Criar a classe em `Domain/Entities/`

### 2. Criar Value Objects (se necessário)

Criar em `Domain/ValueObjects/`

### 3. Criar Repository Interface

Criar interface em `Repositories/{Resource}/`

### 4. Criar Repository Implementation

Criar classe em `Repositories/{Resource}/`

### 5. Criar Mapping EF Core

Criar classe em `Repositories/Mappings/`

### 6. Criar DTOs

Criar em `Controllers/Dtos/`

### 7. Criar/Atualizar Controller

Criar ou atualizar em `Controllers/`

### 8. Registrar Dependências

Configurar injeção de dependências no `Program.cs`
