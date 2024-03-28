using MinimalWebApiLearn.Models;

namespace MinimalWebApiLearn.Endpoints
{
    public class ToDoListData
    {
        private readonly ISqlDataAcces _db;

        public ToDoListData(ISqlDataAcces db)
        {
            _db = db;
        }

        public Task<IEnumerable<Assignment>> GetAllToDoListS() => _db.LoadData<Assignment, dynamic>("dbo.spToDoListsDb_GettAll", new { });
        public async Task <Assignment?> GetToDoList(int id)
        {
            var results=await _db.LoadData<Assignment,dynamic>("Xyz", new {Id=id});
            return results.FirstOrDefault();
        }
    }
}
