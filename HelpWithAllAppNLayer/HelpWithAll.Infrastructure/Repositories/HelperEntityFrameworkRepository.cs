using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelpWithAll.Core.Models;
using HelpWithAll.Core.Repositories.Base;
using HelpWithAll.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace HelpWithAll.Infrastructure.Repositories
{
    public class HelperEntityFrameworkRepository : IHelperRepository
    {
        private readonly HelperDbContext _context;

        public HelperEntityFrameworkRepository(HelperDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<bool> DeleteHelperAsync(int id)
        {
            var helper = await _context.Helpers.FindAsync(id);
            if (helper == null)
            {
                return false;
            }

            _context.Helpers.Remove(helper);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> InsertHelperAsync(Helper helper)
        {
            await _context.Helpers.AddAsync(helper);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Helper>> SelectAllHelpersAsync()
        {
            return await _context.Helpers.ToListAsync();
        }

        public async Task<Helper> SelectHelperByIdAsync(int id)
        {
            return await _context.Helpers.FindAsync(id);
        }

        public async Task<bool> UpdateHelperAsync(Helper updatedHelper)
        {
            _context.Helpers.Update(updatedHelper);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}