﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace WebApplication3.DBContext
{
    public partial class testapiContext : DbContext
    {
        public testapiContext()
        {
        }

        public testapiContext(DbContextOptions<testapiContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Actor> Actors { get; set; }
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Film> Films { get; set; }
        public virtual DbSet<FilmActor> FilmActors { get; set; }
        public virtual DbSet<FilmCategory> FilmCategories { get; set; }
        public virtual DbSet<FilmText> FilmTexts { get; set; }
        public virtual DbSet<Inventory> Inventories { get; set; }
        public virtual DbSet<Language> Languages { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<Rental> Rentals { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<staff> staff { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySQL("server=localhost;userid=root;password='';database=testapi;Port=3306");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Actor>(entity =>
            {
                entity.ToTable("actor");

                entity.HasIndex(e => e.LastName, "idx_actor_last_name");

                entity.Property(e => e.ActorId)
                    .HasColumnType("smallint(5) unsigned")
                    .HasColumnName("actor_id");

                entity.Property(e => e.Dob)
                    .HasColumnType("date")
                    .HasColumnName("dob")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .HasColumnName("email")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("first_name");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("last_name");
            });

            modelBuilder.Entity<Address>(entity =>
            {
                entity.ToTable("address");

                entity.HasIndex(e => e.CityId, "idx_fk_city_id");

                entity.Property(e => e.AddressId)
                    .HasColumnType("smallint(5) unsigned")
                    .HasColumnName("address_id");

                entity.Property(e => e.Address1)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("address");

                entity.Property(e => e.Address2)
                    .HasMaxLength(50)
                    .HasColumnName("address2")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.CityId)
                    .HasColumnType("smallint(5) unsigned")
                    .HasColumnName("city_id");

                entity.Property(e => e.District)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("district");

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("phone");

                entity.Property(e => e.PostalCode)
                    .HasMaxLength(10)
                    .HasColumnName("postal_code")
                    .HasDefaultValueSql("'NULL'");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Addresses)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_address_city");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("category");

                entity.Property(e => e.CategoryId)
                    .HasColumnType("tinyint(3) unsigned")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("category_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(25)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.ToTable("city");

                entity.HasIndex(e => e.CountryId, "idx_fk_country_id");

                entity.Property(e => e.CityId)
                    .HasColumnType("smallint(5) unsigned")
                    .HasColumnName("city_id");

                entity.Property(e => e.City1)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("city");

                entity.Property(e => e.CountryId)
                    .HasColumnType("smallint(5) unsigned")
                    .HasColumnName("country_id");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Cities)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_city_country");
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.ToTable("country");

                entity.Property(e => e.CountryId)
                    .HasColumnType("smallint(5) unsigned")
                    .HasColumnName("country_id");

                entity.Property(e => e.Country1)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("country");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("customer");

                entity.HasIndex(e => e.AddressId, "idx_fk_address_id");

                entity.HasIndex(e => e.StoreId, "idx_fk_store_id");

                entity.HasIndex(e => e.LastName, "idx_last_name");

                entity.Property(e => e.CustomerId)
                    .HasColumnType("smallint(5) unsigned")
                    .HasColumnName("customer_id");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasColumnName("active")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.AddressId)
                    .HasColumnType("smallint(5) unsigned")
                    .HasColumnName("address_id");

                entity.Property(e => e.CreateDate).HasColumnName("create_date");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .HasColumnName("email")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("first_name");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("last_name");

                entity.Property(e => e.StoreId)
                    .HasColumnType("tinyint(3) unsigned")
                    .HasColumnName("store_id");

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.AddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_customer_address");
            });

            modelBuilder.Entity<Film>(entity =>
            {
                entity.ToTable("film");

                entity.HasIndex(e => e.LanguageId, "idx_fk_language_id");

                entity.HasIndex(e => e.OriginalLanguageId, "idx_fk_original_language_id");

                entity.HasIndex(e => e.Title, "idx_title");

                entity.Property(e => e.FilmId)
                    .HasColumnType("smallint(5) unsigned")
                    .HasColumnName("film_id");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.LanguageId)
                    .HasColumnType("tinyint(3) unsigned")
                    .HasColumnName("language_id");

                entity.Property(e => e.Length)
                    .HasColumnType("smallint(5) unsigned")
                    .HasColumnName("length")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.OriginalLanguageId)
                    .HasColumnType("tinyint(3) unsigned")
                    .HasColumnName("original_language_id")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Rating)
                    .HasColumnType("enum('G','PG','PG-13','R','NC-17')")
                    .HasColumnName("rating")
                    .HasDefaultValueSql("'''G'''");

                entity.Property(e => e.ReleaseYear)
                    .HasColumnType("year(4)")
                    .HasColumnName("release_year")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.RentalDuration)
                    .HasColumnType("tinyint(3) unsigned")
                    .HasColumnName("rental_duration")
                    .HasDefaultValueSql("'3'");

                entity.Property(e => e.RentalRate)
                    .HasColumnType("decimal(4,2)")
                    .HasColumnName("rental_rate")
                    .HasDefaultValueSql("'4.99'");

                entity.Property(e => e.ReplacementCost)
                    .HasColumnType("decimal(5,2)")
                    .HasColumnName("replacement_cost")
                    .HasDefaultValueSql("'19.99'");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("title");

                entity.HasOne(d => d.Language)
                    .WithMany(p => p.FilmLanguages)
                    .HasForeignKey(d => d.LanguageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_film_language");

                entity.HasOne(d => d.OriginalLanguage)
                    .WithMany(p => p.FilmOriginalLanguages)
                    .HasForeignKey(d => d.OriginalLanguageId)
                    .HasConstraintName("fk_film_language_original");
            });

            modelBuilder.Entity<FilmActor>(entity =>
            {
                entity.HasKey(e => new { e.ActorId, e.FilmId })
                    .HasName("PRIMARY");

                entity.ToTable("film_actor");

                entity.HasIndex(e => e.FilmId, "idx_fk_film_id");

                entity.Property(e => e.ActorId)
                    .HasColumnType("smallint(5) unsigned")
                    .HasColumnName("actor_id");

                entity.Property(e => e.FilmId)
                    .HasColumnType("smallint(5) unsigned")
                    .HasColumnName("film_id");

                entity.HasOne(d => d.Actor)
                    .WithMany(p => p.FilmActors)
                    .HasForeignKey(d => d.ActorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_film_actor_actor");

                entity.HasOne(d => d.Film)
                    .WithMany(p => p.FilmActors)
                    .HasForeignKey(d => d.FilmId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_film_actor_film");
            });

            modelBuilder.Entity<FilmCategory>(entity =>
            {
                entity.HasKey(e => new { e.FilmId, e.CategoryId })
                    .HasName("PRIMARY");

                entity.ToTable("film_category");

                entity.HasIndex(e => e.CategoryId, "fk_film_category_category");

                entity.Property(e => e.FilmId)
                    .HasColumnType("smallint(5) unsigned")
                    .HasColumnName("film_id");

                entity.Property(e => e.CategoryId)
                    .HasColumnType("tinyint(3) unsigned")
                    .HasColumnName("category_id");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.FilmCategories)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_film_category_category");

                entity.HasOne(d => d.Film)
                    .WithMany(p => p.FilmCategories)
                    .HasForeignKey(d => d.FilmId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_film_category_film");
            });

            modelBuilder.Entity<FilmText>(entity =>
            {
                entity.HasKey(e => e.FilmId)
                    .HasName("PRIMARY");

                entity.ToTable("film_text");

                entity.HasIndex(e => new { e.Title, e.Description }, "idx_title_description");

                entity.Property(e => e.FilmId)
                    .HasColumnType("smallint(6)")
                    .HasColumnName("film_id");

                entity.Property(e => e.Description)
                    .HasColumnType("text")
                    .HasColumnName("description")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("title");
            });

            modelBuilder.Entity<Inventory>(entity =>
            {
                entity.ToTable("inventory");

                entity.HasIndex(e => e.FilmId, "idx_fk_film_id");

                entity.HasIndex(e => new { e.StoreId, e.FilmId }, "idx_store_id_film_id");

                entity.Property(e => e.InventoryId)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasColumnName("inventory_id");

                entity.Property(e => e.FilmId)
                    .HasColumnType("smallint(5) unsigned")
                    .HasColumnName("film_id");

                entity.Property(e => e.StoreId)
                    .HasColumnType("tinyint(3) unsigned")
                    .HasColumnName("store_id");

                entity.HasOne(d => d.Film)
                    .WithMany(p => p.Inventories)
                    .HasForeignKey(d => d.FilmId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_inventory_film");
            });

            modelBuilder.Entity<Language>(entity =>
            {
                entity.ToTable("language");

                entity.Property(e => e.LanguageId)
                    .HasColumnType("tinyint(3) unsigned")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("language_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("name")
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.ToTable("payment");

                entity.HasIndex(e => e.RentalId, "fk_payment_rental");

                entity.HasIndex(e => e.CustomerId, "idx_fk_customer_id");

                entity.HasIndex(e => e.StaffId, "idx_fk_staff_id");

                entity.Property(e => e.PaymentId)
                    .HasColumnType("smallint(5) unsigned")
                    .HasColumnName("payment_id");

                entity.Property(e => e.Amount)
                    .HasColumnType("decimal(5,2)")
                    .HasColumnName("amount");

                entity.Property(e => e.CustomerId)
                    .HasColumnType("smallint(5) unsigned")
                    .HasColumnName("customer_id");

                entity.Property(e => e.PaymentDate).HasColumnName("payment_date");

                entity.Property(e => e.RentalId)
                    .HasColumnType("int(11)")
                    .HasColumnName("rental_id")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.StaffId)
                    .HasColumnType("tinyint(3) unsigned")
                    .HasColumnName("staff_id");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_payment_customer");

                entity.HasOne(d => d.Rental)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.RentalId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("fk_payment_rental");

                entity.HasOne(d => d.Staff)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.StaffId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_payment_staff");
            });

            modelBuilder.Entity<Rental>(entity =>
            {
                entity.ToTable("rental");

                entity.HasIndex(e => e.CustomerId, "idx_fk_customer_id");

                entity.HasIndex(e => e.InventoryId, "idx_fk_inventory_id");

                entity.HasIndex(e => e.StaffId, "idx_fk_staff_id");

                entity.HasIndex(e => new { e.RentalDate, e.InventoryId, e.CustomerId }, "rental_date")
                    .IsUnique();

                entity.Property(e => e.RentalId)
                    .HasColumnType("int(11)")
                    .HasColumnName("rental_id");

                entity.Property(e => e.CustomerId)
                    .HasColumnType("smallint(5) unsigned")
                    .HasColumnName("customer_id");

                entity.Property(e => e.InventoryId)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasColumnName("inventory_id");

                entity.Property(e => e.RentalDate).HasColumnName("rental_date");

                entity.Property(e => e.ReturnDate)
                    .HasColumnName("return_date")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.StaffId)
                    .HasColumnType("tinyint(3) unsigned")
                    .HasColumnName("staff_id");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Rentals)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_rental_customer");

                entity.HasOne(d => d.Inventory)
                    .WithMany(p => p.Rentals)
                    .HasForeignKey(d => d.InventoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_rental_inventory");

                entity.HasOne(d => d.Staff)
                    .WithMany(p => p.Rentals)
                    .HasForeignKey(d => d.StaffId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_rental_staff");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.Property(e => e.Id)
                    .HasColumnType("int(10) unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("password");

                entity.Property(e => e.Refreshtoken)
                    .HasMaxLength(50)
                    .HasColumnName("refreshtoken")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Refreshtokenexpirytime)
                    .HasColumnName("refreshtokenexpirytime")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("username");
            });

            modelBuilder.Entity<staff>(entity =>
            {
                entity.HasIndex(e => e.AddressId, "idx_fk_address_id");

                entity.HasIndex(e => e.StoreId, "idx_fk_store_id");

                entity.Property(e => e.StaffId)
                    .HasColumnType("tinyint(3) unsigned")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("staff_id");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasColumnName("active")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.AddressId)
                    .HasColumnType("smallint(5) unsigned")
                    .HasColumnName("address_id");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .HasColumnName("email")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("first_name");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("last_name");

                entity.Property(e => e.Password)
                    .HasMaxLength(40)
                    .HasColumnName("password")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Picture)
                    .HasColumnType("blob")
                    .HasColumnName("picture")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.StoreId)
                    .HasColumnType("tinyint(3) unsigned")
                    .HasColumnName("store_id");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(16)
                    .HasColumnName("username");

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.staff)
                    .HasForeignKey(d => d.AddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_staff_address");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
