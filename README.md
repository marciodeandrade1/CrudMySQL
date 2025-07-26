# CrudMySQL

 Vantagens desta abordagem com Models

1. Separação de responsabilidades:
   - Models representam os dados
   - Repository lida com o acesso ao banco de dados
   - Formulário cuida apenas da interface do usuário

2. Reutilização de código:
   - O mesmo repository pode ser usado em diferentes forms
   - O model pode ser usado em toda a aplicação

3. Manutenibilidade:
   - Mudanças no banco de dados afetam apenas o repository
   - Alterações na interface não afetam a lógica de negócios

4. Testabilidade:
   - Facilita a criação de testes unitários
   - Permite mockar o repository para testar o form
