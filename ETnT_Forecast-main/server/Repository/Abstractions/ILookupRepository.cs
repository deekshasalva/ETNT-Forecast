using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Models;
using DataAccess.DbSets;

namespace DataAccess.Abstractions
{
    public interface ILookupRepository
    {
        Task<Business> GetBusinessByNameAsync(string name);
        Task<Capability> GetCapabilityByNameAsync(string name);
        Task<Category> GetCategoryByNameAsync(string name);
        Task<Org> GetOrgByNameAsync(string name);
        Task<Project> GetProjectByNameAsync(string name);
        Task<Skill> GetSkillByNameAsync(string name);
        Task<User> GetUserByNameAsync(string name);
        Task<EventData> GetEventByIdAsync(Guid id);
        Task AddTaskAsync(EventData @event);
        Task UpdateTaskStatusAsync(Guid id, EventStatus status, string error = null);

        Task<IEnumerable<T>> GetAll<T>() where T : class;
    }
}