using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Arc.Database.Models;

namespace Arc.Database
{
    public interface IDatabase : IDisposable
    {
        DbSet<User> Users { get; }
    }
}
