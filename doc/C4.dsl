workspace "Name" "Description" {

    !identifiers hierarchical

    model {
        admin = person "Administrador"  "Um Usuário não cadastrado responsável por cadastrar Livros, Categoria e Autores"
        client = person "Cliente" "Usuário não cadastrado que deseja realizar a compra de um livro"
        system = softwareSystem "Alexandria" {
            wa = container "Web Application"
            db = container "Database Schema" {
                tags "Database"
            }
        }
        admin -> system.wa "Cadastra Livros" "Software que realiza venda se livros"
        system.wa -> system.db "Reads from and writes to"
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