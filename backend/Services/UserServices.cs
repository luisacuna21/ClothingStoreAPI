using Microsoft.Extensions.Options;
using Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Services;

public class UserService
{
    private readonly IMongoCollection<User> _usersCollection;

    public UserService(IOptions<ClothingStoreDBSettings> clothingStoreDBSettings)
    {
        var mongoClient = new MongoClient(clothingStoreDBSettings.Value.ConnectionString);
        var mongoDb = mongoClient.GetDatabase(clothingStoreDBSettings.Value.DBName);

        _usersCollection = mongoDb.GetCollection<User>(clothingStoreDBSettings.Value.UsersCollectionName);
    }

    #region CRUD

    public async Task<List<User>> GetAsync() => await _usersCollection.FindAsync(new BsonDocument()).Result.ToListAsync();
    public async Task<User> GetAsync(string id) => await _usersCollection.Find(u => u.Id == id).FirstOrDefaultAsync();
    public async Task InsertAsync(User user) => await _usersCollection.InsertOneAsync(user);
    public async Task UpdateAsync(string id, User user) => await _usersCollection.ReplaceOneAsync(u => u.Id == id, user);
    public async Task DeleteAsync(string id) => await _usersCollection.DeleteOneAsync(u => u.Id == id);
    public async Task<bool> ValidateUserAsync(string username, string password)
    {
        bool valid = false;

        User user = await _usersCollection.Find(u => u.Username == username && u.Password == password).FirstOrDefaultAsync();

        if (user is not null)
            valid = true;

        return valid;
    }

    #endregion
}