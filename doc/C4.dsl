workspace "Name" "Description" {

    !identifiers hierarchical

    model {
        admin = person "Administrador"  "Um Usuário não cadastrado responsável por cadastrar Livros, Categoria e Autores"
        client = person "Cliente" "Usuário não cadastrado que deseja realizar a compra de um livro"
        system = softwareSystem "Alexandria" "Lida com cadastro de livros e suas vendas"{
            wa = container "Web API" "Aplicação web quer fornece serviços de cadastros e compra de livros"
            db = container "Banco de Dados" "Livros, categorias, autores, cidades, etc" {
                tags "Database"
            }
        }
        systemPayment = softwareSystem "Sistema de Pagamentos"
        admin -> system.wa "Cadastra Livros" "Software que realiza venda se livros"
        system.wa -> systemPayment "Realiza pagamento"
        system.wa -> system.db "Lê e escreve informações no"
        client -> system.wa "Compra Livros"
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

