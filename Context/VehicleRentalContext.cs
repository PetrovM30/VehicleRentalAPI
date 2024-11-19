﻿using Microsoft.EntityFrameworkCore;
using VehicleRentalAPI.Entities;

namespace VehicleRentalAPI.Context
{
    public class VehicleRentalContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
    }
}
