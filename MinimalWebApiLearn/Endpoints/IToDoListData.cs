using MinimalWebApiLearn.Models;

namespace MinimalWebApiLearn.Endpoints;

public interface IToDoListData
{
    Task<IEnumerable<Assignment>> GetAllToDoListS();
    Task <Assignment?> GetToDoList(int id);
    Task InsertToDoList(AssignmentDto assignment);
    Task UpdateTask(int id, AssignmentDto assignment);
    Task DeleteTask(int id);
}