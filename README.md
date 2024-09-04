# Sistema de Gestão de Fretes

O **Sistema de Gestão de Fretes** é uma aplicação que permite a postagem e gerenciamento de fretes para uma empresa de logística. Neste sistema, empresas podem cadastrar fretes e entregadores (terceiros) podem visualizar e solicitar esses fretes para realizar as entregas.

## Funcionamento

1. **Cadastro de Fretes**:
   - A empresa cadastra os detalhes dos fretes, incluindo informações como origem, destino, peso da carga e tipo de veículo necessário.
   - O sistema calcula automaticamente o valor do frete com base na distância, peso e tipo de veículo.
   - Uma taxa é aplicada de acordo com a distância percorrida (conforme as regras definidas).

2. **Visualização de Fretes Disponíveis**:
   - Os entregadores podem acessar o sistema e visualizar os fretes disponíveis para entrega.
   - Eles podem filtrar por distância, tipo de veículo ou outros critérios relevantes.

3. **Solicitação de Fretes**:
   - Um entregador interessado em um frete pode solicitar a entrega.
   - O sistema verifica se o entregador atende aos requisitos (por exemplo, possui o tipo de veículo necessário).
   - Se tudo estiver correto, o frete é atribuído ao entregador.

4. **Status do Frete**:
   - O sistema permite que o entregador atualize o status do frete:
     - "Aceito": O entregador aceitou o frete.
     - "Rota de Entrega": O entregador está a caminho do destino.
     - "Finalizado": O frete foi entregue com sucesso.

5. **Histórico de Fretes**:
   - Os entregadores podem consultar seu histórico de fretes realizados.
   - O sistema mantém um registro de todos os fretes concluídos.

## Tecnologias Utilizadas

- C# (ASP.NET Core)
- SQL Server
- Entity Framework

## Como Executar o Projeto

1. **Pré-requisitos**:
   - Instale o [.NET Core SDK](https://dotnet.microsoft.com/download) na sua máquina.
   - Configure a conexão com o banco de dados no arquivo `appsettings.json`.

2. **Clonar o Repositório**:
   ```
   git clone https://github.com/RogerioSousaM/SistemaFrete.git

3. **Executar o Projeto**:
   - Abra o terminal na pasta do projeto e execute no vs code:
     ```
     dotnet watch run
     ```

Espero que esse README ajude a entender o funcionamento do sistema. Se tiver alguma dúvida ou precisar de mais informações, estou à disposição! 🚚📦
