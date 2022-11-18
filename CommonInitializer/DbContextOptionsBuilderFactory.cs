﻿using Microsoft.EntityFrameworkCore;

namespace CommonInitializer
{
    public static class DbContextOptionsBuilderFactory
    {
        public static DbContextOptionsBuilder<TDbContext> Create<TDbContext>()
            where TDbContext : DbContext
        {
            var connStr = Environment.GetEnvironmentVariable("DefaultDBConnStr");
            var optionsBuilder = new DbContextOptionsBuilder<TDbContext>();
            Console.WriteLine(connStr);
            if (string.IsNullOrEmpty(connStr))
                throw new Exception("未找到环境变量DefaultDBConnStr对应值");
            optionsBuilder.UseSqlServer(connStr);
            return optionsBuilder;
        }
    }
}
