using MinimalWebApiLearn.Models;

namespace MinimalWebApiLearn.Endpoints;

public interface IToDoListData
{
    Task<IEnumerable<Assignment>> GetAllToDoListS();
    Task <Assignment?> GetToDoList(int id);
    Task InsertToDoList(string description, DateTime endDate);
    Task UpdateTask(int id, string description, DateTime endDate);
    Task DeleteTask(int id);
}