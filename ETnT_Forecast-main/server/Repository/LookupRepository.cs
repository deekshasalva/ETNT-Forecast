using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Models;
using DataAccess.Abstractions;
using DataAccess.DbSets;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class LookupRepository : ILookupRepository
    {
        private readonly ForecastContext _context;

        public LookupRepository(ForecastContext context)
        {
            _context = context;
        }

        public async Task<Business> GetBusinessByNameAsync(string name)
        {
            return await _context.Businesses.FirstOrDefaultAsync(x => x.Value == name).ConfigureAwait(false);
        }

        public async Task<Capability> GetCapabilityByNameAsync(string name)
        {
            return await _context.Capabilities.FirstOrDefaultAsync(x => x.Value == name).ConfigureAwait(false);
        }

        public async Task<Category> GetCategoryByNameAsync(string name)
        {
            return await _context.Categories.FirstOrDefaultAsync(x => x.Value == name).ConfigureAwait(false);
        }

        public async Task<Org> GetOrgByNameAsync(string name)
        {
            return await _context.Orgs.FirstOrDefaultAsync(x => x.Value == name).ConfigureAwait(false);
        }

        public async Task<Project> GetProjectByNameAsync(string name)
        {
            return await _context.Projects.FirstOrDefaultAsync(x => x.Value == name).ConfigureAwait(false);
        }

        public async Task<Skill> GetSkillByNameAsync(string name)
        {
            return await _context.Skills.FirstOrDefaultAsync(x => x.Value == name).ConfigureAwait(false);
        }

        public async Task<User> GetUserByNameAsync(string name)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.FirstName + " " + x.LastName == name)
                .ConfigureAwait(false);
        }

        public async Task<EventData> GetEventByIdAsync(Guid id)
        {
            return await _context.Task.FindAsync(id);
        }

        public async Task AddTaskAsync(EventData @event)
        {
            await _context.Task.AddAsync(@event);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTaskStatusAsync(Guid id, EventStatus status, string error = null)
        {
            var @event = await _context.Task.FirstOrDefaultAsync(x => x.Id == id);
            @event.Status = status.ToString();
            if (error != null) @event.Errors = error;
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAll<T>() where T : class
        {
            return await _context.Set<T>().ToListAsync();
        }
    }
}