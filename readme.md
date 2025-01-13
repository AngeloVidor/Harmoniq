# Harmoniq API

## Sumário
- [Introdução](#introdução)
- [Features](#features)
- [Instalação](#instalação)
- [Uso](#uso)
- [API Endpoints](#api-endpoints)

## Introdução
Bem vindo à API da Harmoniq! Esta aplicação oferece diversos endpoints para gerenciar usuários, produtos e futuramente Merchandise. Desenvolvida com simplicidade, segurança e eficiência em mente, esta API convida você a explorar suas funcionalidades principais que proporcionam uma experiência de e-commerce fluida e sem interrupções!

## Features
- Autenticação e autorização de usuários
- Cadastro de álbuns (Autorizado para ContentCreator)
- Cadastro de músicas (Autorizado para ContentCreator)
- Personalização de Álbuns favoritos (Autorizado para ContentConsumer)
- Personalização de Lista de desejos (Autorizado para ContentConsumer)
- Cadastro de carrinho de compras e pagamento (Autorizado para ContentConsumer)
- Compra de álbuns individuais (Autorizado para ContentConsumer)
- Registrar um perfil de Consumidor de Conteúdo (Autorizado para ContentConsumer)
- Registrar um perfil de Criador de Conteúdo (Autorizado para ContentCreator)

## Instalação

Clone o repositório:
```bash
git clone https://github.com/AngeloVidor/Harmoniq
```

Para rodar a API localmente, você precisará criar um arquivo `appsettings.json` em `Harmoniq/Harmoniq.API` e nele incluir:
- `ConnectionString` para conexão com SQL Server.
- Configurar a seção do JWT token fornecendo uma `secret key`, `issuer`, e duração do token.
- Uma seção para a Stripe com `SecretKey`, `PublishableKey`, `SuccessUrl`, `CancelUrl`, e chaves secretas para os webhooks que interagem com o Checkout.
- Também é necessário adicionar informações do Bucket na AWS como `bucketname`, `accesskey`, `secretkey`, `region`.

Após configurar seu `appsettings.json`, você pode executar os seguintes comandos:

```bash
dotnet ef migrations add MyMigration --project Harmoniq.DAL --startup-project Harmoniq.API
dotnet ef database update --project Harmoniq.DAL --startup-project Harmoniq.API
```

## Uso

Para rodar o servidor, utilize o seguinte comando:
```bash
cd Harmoniq/Harmoniq.API
```
```bash
dotnet run
```

O servidor irá iniciar na porta padrão: `http://localhost:5029/`

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
- **POST** `/api/Profiles/ContentConsumer` - A conta do usuário passa a ser consumidor de conteúdo.
- **POST** `/api/Profiles/ContentCreator` - A conta do usuário passa a ser criador de conteúdo.
- **PUT** `/api/Profiles/contentConsumer` - Edita o perfil do consumidor de conteúdo.

### Purchases
- **GET** `/api/Purchases/{consumerId}` - Retorna uma lista de todos os álbuns comprados pelo usuário.
- **GET** `/api/Purchases/download-discography/{albumId}` - Baixa na máquina do cliente o álbum específico.

### Songs
- **POST** `/api/Songs/song` - Adiciona faixas de áudio aos álbuns.
- **PUT** `/api/Songs/song` - Edita detalhes e a faixa de áudio do álbum.

### StripeWebhook
- **POST** `/api/StripeWebhook/hook` - Processa uma compra singular do usuário na Stripe.
- **POST** `/api/StripeWebhook/cart` - Processa a compra de um carrinho na Stripe.

### Wishlist
- **POST** `/api/Wishlist/albums` - Adiciona um álbum à lista de desejos.
- **GET** `/api/Wishlist/{consumerId}` - Retorna uma lista de lista de desejos do usuário.

