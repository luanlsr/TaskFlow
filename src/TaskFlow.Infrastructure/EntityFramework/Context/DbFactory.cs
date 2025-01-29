using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskFlow.Infrastructure.Data.EntityFramework.Context
{
    public interface IDbFactory : IDisposable
    {
        AppDbContext Init();
    }

    public class DbFactory : Disposable, IDbFactory
    {
        private AppDbContext _context;
        private readonly DbContextOptions<AppDbContext> _options;

        public DbFactory(DbContextOptions<AppDbContext> options)
        {
            _options = options;
        }

        public AppDbContext Init()
        {
            return _context ?? (_context = new AppDbContext(_options));
        }

        protected override void DisposeCore()
        {
            _context?.Dispose();
        }
    }
}
