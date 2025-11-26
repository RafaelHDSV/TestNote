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
                var company = new Company { Name = "Headquarters", Owner = "System", Sector = "IT", NumberOfEmployees = 10 };
                await _database.InsertAsync(company);

                var adminUser = new User { Email = "admin@sistema.com", Password = "123", Role = 1 };
                await _database.InsertAsync(adminUser);
            }
        }

        public async Task<User?> LoginAsync(string email, string password)
        {
            await InitializeAsync();
            return await _database.Table<User>()
                                  .Where(u => u.Email == email && u.Password == password)
                                  .FirstOrDefaultAsync();
        }

        public async Task CreateManagerAsync(string name, string email, string password, int companyId)
        {
            await InitializeAsync();

            var newUser = new User { Email = email, Password = password, Role = 2 };
            await _database.InsertAsync(newUser);

            var newManager = new Manager
            {
                Name = name,
                UserId = newUser.Id,
                CompanyId = companyId
            };
            await _database.InsertAsync(newManager);
        }

        public async Task CreateEmployeeAsync(string name, string jobTitle, string email, string password, int companyId)
        {
            await InitializeAsync();

            var newUser = new User { Email = email, Password = password, Role = 3 }; 
            await _database.InsertAsync(newUser);

            var newEmployee = new Employee
            {
                Name = name,
                JobTitle = jobTitle,
                UserId = newUser.Id,
                CompanyId = companyId
            };
            await _database.InsertAsync(newEmployee);
        }

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

        public async Task<List<Employee>> GetEmployeesByCompanyAsync(int companyId)
        {
            await InitializeAsync();

            var employees = await _database.Table<Employee>()
                                           .Where(e => e.CompanyId == companyId)
                                           .ToListAsync();

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
            var pkProp = typeof(T).GetProperty("Id");

            if (pkProp != null)
            {
                int id = (int)pkProp.GetValue(item);

                if (id != 0)
                {
                    return await _database.UpdateAsync(item);
                }
            }
            return await _database.InsertAsync(item);
        }

        public async Task<int> DeleteItemAsync<T>(T item) where T : new()
        {
            await InitializeAsync();
            return await _database.DeleteAsync(item);
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            await InitializeAsync();
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

        public async Task CreateTestAsync(Test test)
        {
            await InitializeAsync();
            await SaveItemAsync(test);
        }

        public async Task FixMissingDataAsync()
        {
            await InitializeAsync();

            // 1. Busca todos os testes que estão com a Seção vazia ou nula
            var testsToFix = await _database.Table<Test>()
                                            .Where(t => t.Section == null || t.Section == "")
                                            .ToListAsync();

            if (testsToFix.Count > 0)
            {
                // 2. Atualiza um por um
                foreach (var test in testsToFix)
                {
                    test.Section = "Geral"; // Define uma seção padrão
                    await _database.UpdateAsync(test);
                }

                // Opcional: Avisa no console
                System.Diagnostics.Debug.WriteLine($"CORREÇÃO: {testsToFix.Count} testes foram atualizados com a seção 'Geral'.");
            }
        }
    }
}
