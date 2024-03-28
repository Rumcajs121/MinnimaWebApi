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
            var results=await _db.LoadData<Assignment,dynamic>("dbo.spToDoListsDb_GettOneTask", new {Id=id});
            return results.FirstOrDefault();
        }
        public Task InsertToDoList(Assignment assignment) =>_db.SaveData("dbo.spToDoListsDb_CreateNewTask", new{ assignment.Description, assignment.EndDate});
        public Task UpdateTask(Assignment assignment) =>_db.SaveData("dbo.spToDoListsDb_EditTask", new{ assignment.Description, assignment.EndDate});
        public Task DeleteTask(int id) =>_db.SaveData("dbo.spToDoListsDb_DeleteTaskId",new{ Id=id});
    }
}
