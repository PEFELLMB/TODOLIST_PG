API REST 'ToDoList_PG' para controle de tarefas diárias.
----------------------------------------------------
Instruções para configurar e executar o projeto:
------------------------------------------------------
1.Clone o repositório do projeto no GitHub.
2. Abra o projeto no Visual Studio ou em sua IDE favorita.
3. Instale as dependências do projeto:
Microsoft.EntityFrameworkCore.Design 6.0.1
 - Microsoft.EntityFrameworkCore.Sqlite 6.0.1
 - Microsoft.EntityFrameworkCore.InMemory 6.0.1
 - Microsoft.EntityFrameworkCore.Tools 6.0.1
 - Pomelo.EntityFrameworkCore.MySql 6.0.1

4. Para a integração do banco de dados, o projeto pode ser utilizado SQLite ou MySQL [opcional]: 
4.1.1. SQLite: 
- No arquivo "appsettings.json", adicionar abaixo de "AllowedHosts", a aba 'ConnectionStrings', a qual faremos o direcionamento
a qual banco de dados ele será integrado, evidenciado na linha 11 por: 
- "ConexaoSQLite": "Data Source=devevents.db;"

4.1.2. .Antes de inicializar o programa, deve se realizar a primeira migração para o banco de dados para estabelecer o esquemático do banco de dados por meio
do comando 'dotnet ef migrations add NOME_DA_MIGRACAO' -o  Persistence/Migrations', o qual gerará o arquivo do esquemático, em uma pasta
chamada Migrations, dentro da pasta Persistence.

4.1.2. Após o esquemático ser gerado, realize o comando 'dotnet ef migrations update' para realizar as devidas atualizações, desta forma, gerará o arquivo
do banco de dados local para integrar com a API.

4.2.MySQL [opcional]:
- No arquivo "appsettings.json", adicionar abaixo de "AllowedHosts", a aba 'ConnectionStrings', a qual faremos o direcionamento
a qual banco de dados ele será integrado, evidenciado no comentário da linha 10 por: 
- "ConexãoMySQL":"Server=SEU_SERVIDOR; Port=3306;Database=NOME_DA_DATABASE ;User=root;Password=SUA_SENHA-; Persist Security Info=False; Connect Timeout= 300"

- No arquivo 'Program.cs', utilize a função estabelecida entre a linha 10 e 13 que estabelece a conexão com o dado servidor direcionado:
builder.Services.AddDbContextPool<ToDoListDbContext>(options =>
options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

4.2.2. Instale o software MySQL, de sistema de gerenciamento de banco de dados, e crie uma database com o nome referenciado em ConectionStrings.

4.2.3.Antes de inicializar o programa, deve se realizar a primeira migração para o banco de dados para estabelecer o esquemático do banco de dados por meio
do comando 'dotnet ef migrations add NOME_DA_MIGRACAO -o  Persistence/Migrations', o qual gerará o arquivo do esquemático, em uma pasta
chamada Migrations, dentro da pasta Persistence.

4.2.4. Após o esquemático ser gerado, realize o comando 'dotnet ef migrations update' para realizar as devidas atualizações. Com isto, será 
realizado a passagem das devidas informações do esquemático para o banco de dados criado no servidor do MySQL.



6. Execute o projeto.

EXEMPLO DE USO DE ENDPOINTS:
1. Listar todas as tarefas(GetAll):
GET /api/todolist

2. Criar uma nova tarefa (Post):
POST /api/todolist

Segue um exemplo de Request Body (dados de entrada JSON) que arquivo deve seguir:
{
  "Task_Title": "Comprar pão",
  "Task_Description": "Comprar pão francês, integral e de leite",
  "Start_Date": "2023-07-20",
  "End_Date": "2023-07-21"
}


3. Atualizar uma tarefa(Update):
PUT /api/todolist/[title]      //Sendo uma string 'title' um dado de inserção para localização a tarefa via 'Task_Title'
Segue um exemplo de Request Body (dados de entrada JSON) que o arquivo deve seguir:
{
  "Task_Title": "Comprar pão e leite",
  "Task_Description": "Comprar pão francês, integral e de leite",
  "Start_Date": "2023-07-20",
  "End_Date": "2023-07-21"
}

4. Marcar uma tarefa como concluída(TaskConcluded):
- Nesta função, iremos fornecer o título da Tarefa e ela atualizará seu status de ItsConcluded para true.
-  Sendo uma string 'title' um dado de inserção para localização a tarefa via 'Task_Title'

Dados de entrada:
PUT /api/todolist/[title]/TaskConcluded  

Dados de saída (retornará a tarefa atualizada):
GET /api/todolist/[title]

5. Excluir uma tarefa(Delete):
- Nesta função você irá fornecer uma string 'title' com o título da tarefa, ela irá deletar a tarefa e não retornará nenhum dado.

Dados de entrada:
DELETE /api/todolist/[title]

6. Listar todas as tarefas concluídas:
- Neste endpoint, ele irá realizar um GET e nos trazes todas as tarefas que foram marcadas como concluídas.

Dados de saída:
GET /api/todolist/GetAllConcludedTasks

7. Listar todas as tarefas vencidas(GetAllExpiredTasks):
- Neste endpoint, ele irá realizar um GET e nos trazer todas as tarefas vencidas, ou seja, já passaram da data atual.

Dados de saída:
GET /api/todolist/GetAllExpiredTasks


 
