workspace "Alexandria" "Description" {

    !identifiers hierarchical

    model {
        admin = person "Administrador"  "Um Usuário não cadastrado responsável por cadastrar Livros, Categoria e Autores"
        client = person "Cliente" "Usuário não cadastrado que deseja realizar a compra de um livro"
        system = softwareSystem "Alexandria" "Lida com cadastro de livros e suas vendas"{
            wa = container "Web API" "Aplicação web quer fornece serviços de cadastros e compra de livros" {
                authorController = component "AuthorController"
                categoryController = component "CategoryController"
                bookController = component "BookController"
                addressController = component "AddressController"
                paymentController = component "PaymentController"
                
                authorService = component "AuthorService"
                categoryService = component "CategoryService"
                bookService = component "BookService"
                addressService = component "AddressService"
                paymentService = component "PaymentService"
                
                authorRepository = component "AuthorRepository"
                categoryRepository = component "CategoryRepository"
                bookRepository = component "BootRepository"
                addressRepository = component "AddressRepository"
                paymentRepository = component "PaymentRepository"
            }
            db = container "Banco de Dados" "Livros, categorias, autores, cidades, etc" {
                tags "Database"
            }
        }
        systemPayment = softwareSystem "Sistema de Pagamentos"
        admin -> system.wa "Cadastra Livros" "Software que realiza venda se livros"
        system.wa -> systemPayment "Realiza pagamento"

        system.wa.authorController -> system.wa.authorService "Uses"
        system.wa.authorService -> system.wa.authorRepository "Uses"
        system.wa.categoryController -> system.wa.categoryService "Uses"
        system.wa.categoryService -> system.wa.categoryRepository "Uses"
        system.wa.bookController -> system.wa.bookService "Uses"
        system.wa.bookService -> system.wa.bookRepository "Uses"
        system.wa.bookService -> system.wa.categoryService "Uses"
        system.wa.bookService -> system.wa.authorService "Uses"
        system.wa.addressController -> system.wa.addressService "Uses"
        system.wa.addressService -> system.wa.addressRepository "Uses"
        system.wa.paymentController -> system.wa.paymentService "Uses"
        system.wa.paymentService -> system.wa.addressService "Uses"
        system.wa.paymentService -> system.wa.paymentRepository "Uses"

        system.wa.categoryRepository -> system.db "Lê e escreve informações no"
        system.wa.bookRepository -> system.db "Lê e escreve informações no"
        system.wa.addressRepository -> system.db "Lê e escreve informações no"
        system.wa.paymentRepository -> system.db "Lê e escreve informações no"
        system.wa.authorRepository -> system.db "Escreve informações no"
        
        system.wa.paymentService -> systemPayment "Realiza pagamento no"
    }

    views {
        systemContext system "Alexandria" {
            include *
            autolayout lr
        }

        container system "Diagram2" {
            include *
            autolayout lr
        }
        
        component system.wa "WebApp" {
            include *
            autolayout lr
        }

        styles {
            element "Element" {
                color #f8289c
                stroke #f8289c
                strokeWidth 7
                shape roundedbox
            }
            element "Person" {
                shape person
            }
            element "Database" {
                shape cylinder
            }
            element "Boundary" {
                strokeWidth 5
            }
            relationship "Relationship" {
                thickness 4
            }
        }
    }

    configuration {
        scope softwaresystem
    }

}
