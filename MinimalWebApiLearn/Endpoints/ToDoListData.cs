using MinimalWebApiLearn.Models;

namespace MinimalWebApiLearn.Endpoints
{
    public class ToDoListData : IToDoListData
    {
        private readonly ISqlDataAcces _db;

        public ToDoListData(ISqlDataAcces db)
        {
            _db = db;
        }

        public Task<IEnumerable<Assignment>> GetAllToDoListS() => _db.LoadData<Assignment, dynamic>("dbo.spToDoListsDb_GettAll", new { });
        public async Task <Assignment?> GetToDoList(int id)
        {
            var results=await _db.LoadData<Assignment,dynamic>("dbo.spToDoListsDb_GettOneTask", new {Id=id});
            return results.FirstOrDefault();
        }
        public Task InsertToDoList(string description, DateTime endDate) =>_db.SaveData("dbo.spToDoListsDb_CreateNewTask", new{Description=description, EndDate=endDate});
        public Task UpdateTask(int id, string description, DateTime endDate) =>_db.SaveData("dbo.spToDoListsDb_EditTask", new{ Id=id, Description = description, EndDate=endDate});
        public Task DeleteTask(int id) =>_db.SaveData("dbo.spToDoListsDb_DeleteTaskId",new{ Id=id});
    }
}
