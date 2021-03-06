﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using proj1_OnlineStore.Models;

namespace proj1_OnlineStore.Data
{
	public class OnlineStoreDbContext : DbContext
	{
		public DbSet<Customer> Customers { get; set; }
		public DbSet<Location> Locations { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderLineItem> LineItems { get; set; }
		public DbSet<Product> Products { get; set; }

		public OnlineStoreDbContext() { }

		public OnlineStoreDbContext(DbContextOptions<OnlineStoreDbContext> options)
			: base(options) { }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			//  User's login must be a unique value
			modelBuilder.Entity<Customer>().HasIndex(u => u.Login).IsUnique();
		}
	}
}
