using MongoDB.Driver;
using ApiTarea.Models;
using System.Collections.Generic;

namespace ApiTarea.Services
{
    public class BookService
    {
        private readonly IMongoCollection<User> _books;

        public BookService(IBookstoreDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _books = database.GetCollection<User>(settings.BooksCollectionName);
        }

        public List<User> Get() =>
            _books.Find(book => true).ToList();

        public User Get(string id) =>
            _books.Find<User>(book => book.Id == id).FirstOrDefault();

        public User GetByName(string id) =>
            _books.Find<User>(book => book.Username == id).FirstOrDefault();

        public User Create(User book)
        {
            _books.InsertOne(book);
            return book;
        }

        public void Update(string id, User bookIn) =>
            _books.ReplaceOne(book => book.Id == id, bookIn);

        public void Remove(User bookIn) =>
            _books.DeleteOne(book => book.Id == bookIn.Id);

        public void Remove(string id) =>
            _books.DeleteOne(book => book.Id == id);
    }
}
