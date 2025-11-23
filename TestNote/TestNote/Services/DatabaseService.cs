using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using TestNote.Models;

namespace TestNote.Services
{
    public class DatabaseService
    {
        private SQLiteAsyncConnection _database;
        private const string DbName = "TestNoteDB.sqlite";
        private static string DatabasePath => Path.Combine(FileSystem.AppDataDirectory, DbName);

        public DatabaseService()
        {
        }

        private async Task InitializeAsync()
        {
            if (_database != null) return;

            _database = new SQLiteAsyncConnection(DatabasePath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.SharedCache);

            await _database.CreateTableAsync<User>();
            await _database.CreateTableAsync<Manager>();
            await _database.CreateTableAsync<Employee>();
            await _database.CreateTableAsync<Company>();
            await _database.CreateTableAsync<Test>();

            // Popular dados iniciais se o banco de dados estiver vazio
            await SeedDataAsync();
        }

        private async Task SeedDataAsync()
        {
            if (await _database.Table<Company>().CountAsync() == 0)
            {
                // 1. Cria Empresa Inicial
                var company = new Company { Name = "Headquarters", Owner = "System", Sector = "IT", NumberOfEmployees = 10 };
                await _database.InsertAsync(company);

                // 2. Cria Usuário Admin
                var adminUser = new User { Email = "admin@sistema.com", Password = "123", Role = 1 };
                await _database.InsertAsync(adminUser);

                // 3. O Admin não precisa de perfil de Manager/Employee, mas se precisar, crie aqui.
            }
        }

        // --- Lógica de Login ---
        public async Task<User?> LoginAsync(string email, string password)
        {
            await InitializeAsync();
            return await _database.Table<User>()
                                  .Where(u => u.Email == email && u.Password == password)
                                  .FirstOrDefaultAsync();
        }

        // --- Métodos de Criação Específicos ---

        // Admin chama isso para criar um Gerente
        public async Task CreateManagerAsync(string name, string email, string password, int companyId)
        {
            await InitializeAsync();

            // 1. Cria o Login
            var newUser = new User { Email = email, Password = password, Role = 2 }; // Role 2 = Manager
            await _database.InsertAsync(newUser);

            // 2. Cria o Perfil vinculado
            var newManager = new Manager
            {
                Name = name,
                UserId = newUser.Id, // Vincula ao ID gerado acima
                CompanyId = companyId
            };
            await _database.InsertAsync(newManager);
        }

        // Manager chama isso para criar um Funcionário
        public async Task CreateEmployeeAsync(string name, string jobTitle, string email, string password, int companyId)
        {
            await InitializeAsync();

            // 1. Cria o Login
            var newUser = new User { Email = email, Password = password, Role = 3 }; // Role 3 = Employee
            await _database.InsertAsync(newUser);

            // 2. Cria o Perfil vinculado
            var newEmployee = new Employee
            {
                Name = name,
                JobTitle = jobTitle,
                UserId = newUser.Id,
                CompanyId = companyId
            };
            await _database.InsertAsync(newEmployee);
        }

        // --- Consultas Específicas ---

        public async Task<Manager?> GetManagerByUserIdAsync(int userId)
        {
            await InitializeAsync();
            return await _database.Table<Manager>().Where(m => m.UserId == userId).FirstOrDefaultAsync();
        }

        public async Task<Employee?> GetEmployeeByUserIdAsync(int userId)
        {
            await InitializeAsync();
            return await _database.Table<Employee>().Where(e => e.UserId == userId).FirstOrDefaultAsync();
        }

        // Busca funcionários de uma empresa (para o Manager listar)
        public async Task<List<Employee>> GetEmployeesByCompanyAsync(int companyId)
        {
            await InitializeAsync();

            // 1. Pega os perfis de funcionário
            var employees = await _database.Table<Employee>()
                                           .Where(e => e.CompanyId == companyId)
                                           .ToListAsync();

            // 2. Para cada funcionário, busca o User correspondente para pegar o Email
            // (Isso não é super performático para milhares de registros, mas ok para app local)
            foreach (var emp in employees)
            {
                var user = await _database.Table<User>()
                                          .Where(u => u.Id == emp.UserId)
                                          .FirstOrDefaultAsync();
                if (user != null)
                {
                    emp.Email = user.Email;
                    emp.AccessLevel = user.Role;
                }
            }

            return employees;
        }

        // --- Métodos Genéricos CRUD ---

        public async Task<List<T>> GetItemsAsync<T>() where T : new()
        {
            await InitializeAsync();
            return await _database.Table<T>().ToListAsync();
        }

        public async Task<T> GetItemAsync<T>(int id) where T : new()
        {
            await InitializeAsync();
            return await _database.GetAsync<T>(id);
        }

        public async Task<int> SaveItemAsync<T>(T item) where T : new()
        {
            await InitializeAsync();
            // Tenta obter a chave primária (se existir)
            var pkProp = typeof(T).GetProperty("Id");

            if (pkProp != null)
            {
                int id = (int)pkProp.GetValue(item);

                if (id != 0)
                {
                    // Item existente, atualiza
                    return await _database.UpdateAsync(item);
                }
            }
            // Novo item, insere
            return await _database.InsertAsync(item);
        }

        public async Task<int> DeleteItemAsync<T>(T item) where T : new()
        {
            await InitializeAsync();
            return await _database.DeleteAsync(item);
        }

        /// <summary>
        /// Retorna todos os registros da tabela User.
        /// </summary>
        public async Task<List<User>> GetAllUsersAsync()
        {
            await InitializeAsync();

            // Simplesmente retorna todos os itens da tabela User
            return await _database!.Table<User>().ToListAsync();
        }

        public async Task<List<Test>> GetTestsByCompanyAsync(int companyId)
        {
            await InitializeAsync();
            return await _database.Table<Test>()
                                  .Where(t => t.CompanyId == companyId)
                                  .OrderByDescending(t => t.CreatedAt)
                                  .ToListAsync();
        }

        public async Task<List<Test>> GetTestsByExecutorAsync(int executorId)
        {
            await InitializeAsync();
            return await _database.Table<Test>()
                                  .Where(t => t.ExecutorId == executorId)
                                  .ToListAsync();
        }

        // Método auxiliar para criar um teste (Gerente)
        public async Task CreateTestAsync(Test test)
        {
            await InitializeAsync();
            await SaveItemAsync(test);
        }
    }
}
