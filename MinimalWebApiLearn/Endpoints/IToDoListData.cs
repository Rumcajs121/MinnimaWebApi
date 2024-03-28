using MinimalWebApiLearn.Models;

namespace MinimalWebApiLearn.Endpoints;

public interface IToDoListData
{
    Task<IEnumerable<Assignment>> GetAllToDoListS();
    Task <Assignment?> GetToDoList(int id);
    Task InsertToDoList(string description, DateTime endDate);
    Task UpdateTask(Assignment assignment);
    Task DeleteTask(int id);
}