# Harmoniq API

## Sumário
- [Introdução](#introdução)
- [Features](#features)
- [Pré Requisitos](#pré-requisitos)
- [Instalação](#instalação)
- [Uso](#uso)
- [API Endpoints](#api-endpoints)
- [Recursos Adicionais](#recursos-adicionais)
---------------------------------------------------------------------------
## Introdução
Bem-vindo à Harmoniq API! Essa API foi projetada para oferecer uma experiência de e-commerce robusta e intuitiva, permitindo que você gerencie usuários, produtos, e em breve, Merchandise de forma eficiente. Focada em simplicidade, segurança e alto desempenho, a Harmoniq API proporciona uma integração fluida para desenvolvedores, ajudando a criar soluções escaláveis que garantem uma navegação sem interrupções para os usuários finais. Explore nossas funcionalidades e descubra como facilitar a gestão de conteúdo digital com facilidade e confiança!

---------------------------------------------------------------------------
## Features
- Autenticação e autorização de usuários
- Gerenciar um perfil de Consumidor de Conteúdo 
- Gerenciar um perfil de Criador de Conteúdo
- Cadastro e adminisitração de álbuns 
- Cadastro e adminisitração de músicas 
- Personalização de Álbuns favoritos 
- Personalização de Lista de desejos
- Cadastro e administração de carrinho de compras 
- Compra de carrinhos de compras
- Compra de álbuns individuais
- Download da discrogafia adquiriada pelo usuário
- Visualização de estatísticas de vendas mensais
- Sistema de seguidores
- Personalização e gerenciamento de Rewiews
---------------------------------------------------------------------------
## Pré-requisitos
Antes de começar, certifique-se de ter as seguintes ferramentas instaladas:
- [.NET SDK 8.0+](https://dotnet.microsoft.com/download/dotnet)
- [SQL Server](https://www.microsoft.com/sql-server)
- [Stripe Account](https://stripe.com) para configurações de pagamento
- [AWS CLI](https://aws.amazon.com/pt/cli/) configurado para gerenciamento de buckets
---------------------------------------------------------------------------

## Instalação

1. Clone o repositório:
```bash
git clone https://github.com/AngeloVidor/Harmoniq
```

2. Navegue até o diretório do projeto
```bash
cd Harmoniq/Harmoniq.API
```
3. Crie o arquivo `appsettings.json` com as seguintes seções:

   - `ConnectionString`: Configuração para conectar ao SQL Server.
    - `JWT Token`: Configuração para autenticação de usuários.
    - `Stripe`: Chaves para integração com o Stripe para processar pagamentos.
    - `AWS`: Informações para armazenamento de mídia.


4. Após configurar seu `appsettings.json`, você pode executar os seguintes comandos:
```bash
dotnet ef migrations add MyMigration --project Harmoniq.DAL --startup-project Harmoniq.API

dotnet ef database update --project Harmoniq.DAL --startup-project Harmoniq.API

##Nota: Após adicionar uma nova migração, edite o arquivo de design da migração para definir todas as propriedades de onDelete para NoAction, evitando exclusões em cascata indesejadas.

##Você pode facilmente alterar todas as propriedades pressionando CTRL + H na sua migration e alterando de {onDelete: ReferentialAction.Cascade} para {onDelete: ReferentialAction.NoAction}
```
---------------------------------------------------------------------------


## Uso

Para rodar o servidor, utilize o seguinte comando:
```bash
cd Harmoniq/Harmoniq.API
```
```bash
dotnet run
```

O servidor irá iniciar na porta padrão: `http://localhost:5029/`

---------------------------------------------------------------------------
## API Endpoints

### Albums
- **POST** `/api/Albums/album` - Cadastra um novo álbum no sistema.
- **PUT** `/api/Albums/album` - Edita o álbum especificado.
- **GET** `/api/Albums/albums` - Lista todos os álbuns cadastrados.
- **GET** `/api/Albums/{albumId}` - Lista o álbum específico.
- **POST** `/api/Albums/delete-album/{albumId}` - Inativa a visualização de um álbum.
- **GET** `/api/Albums/albums/{contentCreatorId}` - Lista todos os álbuns do criador específico.

### Auth
- **POST** `/api/Auth/register` - Registra um novo usuário no sistema.
- **POST** `/api/Auth/login` - Autenticação do usuário.
- **GET** `/api/Auth/me` - Retorna o usuário logado.

### Cart
- **POST** `/api/Cart/cart` - Cadastra um novo carrinho.
- **POST** `/api/Cart/album` - Adiciona álbuns ao carrinho.
- **PUT** `/api/Cart/albums-in-cart` - Edita os álbuns no carrinho.
- **DELETE** `/api/Cart/remove-album-from-cart` - Remove um álbum do carrinho.
- **GET** `/api/Cart/ConsumerCart/{consumerId}` - Retorna o carrinho do usuário.

### CartCheckout
- **POST** `/api/CartCheckout/create-checkout-session` - Cria uma sessão do carrinho na Stripe.
- **GET** `/api/CartCheckout/success` - Retorna sucesso caso o pagamento seja efetuado com sucesso.
- **GET** `/api/CartCheckout/cancel` - Retorna cancelamento caso o pagamento não seja efetuado.

### Checkout
- **POST** `/api/Checkout/create-checkout-session` - Cria uma sessão na Stripe para álbuns singulares.
- **GET** `/api/Checkout/success` - Retorna sucesso caso o pagamento seja efetuado com sucesso.
- **GET** `/api/Checkout/cancel` - Retorna cancelamento caso o pagamento não seja efetuado.

### Favorites
- **POST** `/api/Favorites/favorite-album` - Favorita um álbum.
- **GET** `/api/Favorites/favorite-albums` - Retorna uma lista de álbuns favoritados pelo usuário.

### Profiles
- **POST** `/api/Profiles/contentConsumer` - A conta do usuário passa a ser consumidor de conteúdo.
- **POST** `/api/Profiles/contentCreator` - A conta do usuário passa a ser Criador de conteúdo.
- **PUT** `/api/Profiles/contentConsumer` - Edita o perfil do Consumidor de conteúdo.
- **PUT** `/api/Profiles/contentCreator` - Edita o perfil do Criador de conteúdo.
- **GET** `/api/Profiles/contentCreator/{contentCreatorId}` - Retorna o perfil do Criador de conteúdo.
- **GET** `/api/Profiles/contentConsumer/{contentConsumerId}` - Retorna o perfil do Consumidor de conteúdo.

### Follows
- **POST** `/api/Follows/follow` - Segue um criador de conteúdo.
- **DELETE** `/api/Follows/stop-following/{contentCreatorId}` - Deixa de seguir um criador de conteúdo.

### Purchases
- **GET** `/api/Purchases/{consumerId}` - Retorna uma lista de todos os álbuns comprados pelo usuário.
- **GET** `/api/Purchases/download-discography/{albumId}` - Baixa na máquina do cliente o álbum específico.

### Reviews
- **POST** `/api/Reviews/album/{albumId}` - Adiciona um review a um álbum.
- **GET** `/api/Reviews/my-reviews/{contentConsumerId}` - Retorna os reviews do usuário.
- **DELETE** `/api/Reviews/{reviewId}` - Remove um review de um álbum.
- **PUT** `/api/Reviews/{reviewId}` - Edita o review de um álbum.

### Songs
- **POST** `/api/Songs/song` - Adiciona faixas de áudio aos álbuns.
- **PUT** `/api/Songs/song` - Edita detalhes e a faixa de áudio do álbum.
- **DELETE** `/api/Songs/albums/{albumId}/songs/{songId}` - Deleta uma música do álbum.

### Statistics
- **GET** `/api/Statistics/stats` - Retorna às estátísticas mensais de venda do criador de conteúdo.

### StripeWebhook
- **POST** `/api/StripeWebhook/hook` - Processa uma compra singular do usuário na Stripe.
- **POST** `/api/StripeWebhook/cart` - Processa a compra de um carrinho na Stripe.

### Wishlist
- **POST** `/api/Wishlist/albums` - Adiciona um álbum à lista de desejos.
- **GET** `/api/Wishlist/{consumerId}` - Retorna uma lista de lista de desejos do usuário.
- **DELETE** `/api/Wishlist/wishlist/{wishlistId/album/{albumId}` - Adiciona um álbum à lista de desejos.
---------------------------------------------------------------------------
## Recursos Adicionais
- [Documentação .NET](https://docs.microsoft.com/dotnet/)
- [Documentação Stripe](https://stripe.com/docs)
- [Documentação AWS S3](https://docs.aws.amazon.com/s3/)


