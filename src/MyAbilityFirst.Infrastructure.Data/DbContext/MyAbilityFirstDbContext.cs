using MyAbilityFirst.Domain;
using MyAbilityFirst.Domain.Shared.Models.ValueObject;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyAbilityFirst.Infrastructure.Data
{
	public sealed class MyAbilityFirstDbContext : DbContext, IWriteEntities
	{

		#region DbSets

		public DbSet<User> Users { get; set; }
		public DbSet<Client> Clients { get; set; }
		public DbSet<CareWorker> CareWorkers { get; set; }
		public DbSet<Patient> Patients { get; set; }
		public DbSet<Suburb> Suburbs { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Subcategory> Subcategories { get; set; }
		public DbSet<UserSubcategory> UserSubcategories { get; set; }
		public DbSet<Job> Jobs { get; set; }
		public DbSet<Contact> Contacts { get; set; }
		public DbSet<Shortlist> Shortlists { get; set; }
		public DbSet<UserAttachment> UserAttachments { get; set; }

		#endregion

		#region Ctor

		public MyAbilityFirstDbContext() : base("MyAbilityFirstDbContext")
		{
		}

		public MyAbilityFirstDbContext(IDatabaseInitializer<MyAbilityFirstDbContext> initializer) : base("MyAbilityFirstDbContext")
		{
			if (initializer == null)
				throw new ArgumentNullException("initializer");

			Database.SetInitializer(initializer);
		}

		#endregion

		#region DbContext methods

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

			modelBuilder.Entity<User>().ToTable("User");
			modelBuilder.Entity<Client>().ToTable("Client");
			modelBuilder.Entity<CareWorker>().ToTable("CareWorker");
			modelBuilder.Entity<Suburb>().ToTable("Suburb");
			modelBuilder.Entity<Category>().ToTable("Category");
			modelBuilder.Entity<Subcategory>().ToTable("Subcategory");
			modelBuilder.Entity<UserSubcategory>().ToTable("UserSubcategory");
			modelBuilder.Entity<Patient>().ToTable("Patient");
			modelBuilder.Entity<Job>().ToTable("Job");
			modelBuilder.Entity<Contact>().ToTable("Contact");
			modelBuilder.Entity<Shortlist>().ToTable("Shortlist");
			modelBuilder.Entity<UserAttachment>().ToTable("UserAttachment");
	}

		#endregion

		#region IWriteEntities methods

		public TEntity GetById<TEntity>(object id) where TEntity : class
		{
			if (id == null)
				throw new ArgumentNullException("id");

			return this.Set<TEntity>().Find(id);
		}

		public async Task<TEntity> GetByIdAsync<TEntity>(object id) where TEntity : class
		{
			if (id == null)
				throw new ArgumentNullException("id");

			return await this.Set<TEntity>().FindAsync(id);
		}

		public IEnumerable<TEntity> Get<TEntity>(
			Expression<Func<TEntity, bool>> filter = null,
			Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
			Expression<Func<TEntity, object>>[] includeProperties = null,
			int? skip = null,
			int? take = null)
			where TEntity : class
		{
			return this.GetQueryable(filter, orderBy, includeProperties, skip, take).ToList();
		}

		public async Task<IEnumerable<TEntity>> GetAsync<TEntity>(
			Expression<Func<TEntity, bool>> filter = null,
			Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
			Expression<Func<TEntity, object>>[] includeProperties = null,
			int? skip = null,
			int? take = null)
			where TEntity : class
		{
			return await this.GetQueryable(filter, orderBy, includeProperties, skip, take).ToListAsync();
		}

		public TEntity Single<TEntity>(
			Expression<Func<TEntity, bool>> filter = null,
			Expression<Func<TEntity, object>>[] includeProperties = null)
			where TEntity : class
		{
			return this.GetQueryable(filter, null, includeProperties).SingleOrDefault();
		}

		public async Task<TEntity> SingleAsync<TEntity>(
			Expression<Func<TEntity, bool>> filter = null,
			Expression<Func<TEntity, object>>[] includeProperties = null)
			where TEntity : class
		{
			return await this.GetQueryable(filter, null, includeProperties).SingleOrDefaultAsync();
		}

		public TEntity First<TEntity>(
			Expression<Func<TEntity, bool>> filter = null,
			Expression<Func<TEntity, object>>[] includeProperties = null)
			where TEntity : class
		{
			return this.GetQueryable(filter, null, includeProperties).FirstOrDefault();
		}

		public async Task<TEntity> FirstAsync<TEntity>(
			Expression<Func<TEntity, bool>> filter = null,
			Expression<Func<TEntity, object>>[] includeProperties = null)
			where TEntity : class
		{
			return await this.GetQueryable(filter, null, includeProperties).FirstOrDefaultAsync();
		}

		public int Count<TEntity>(Expression<Func<TEntity, bool>> filter = null) where TEntity : class
		{
			return this.GetQueryable(filter).Count();
		}

		public async Task<int> CountAsync<TEntity>(Expression<Func<TEntity, bool>> filter = null) where TEntity : class
		{
			return await this.GetQueryable(filter).CountAsync();
		}

		public bool Exists<TEntity>(Expression<Func<TEntity, bool>> filter = null) where TEntity : class
		{
			return this.GetQueryable(filter).Any();
		}

		public async Task<bool> ExistsAsync<TEntity>(Expression<Func<TEntity, bool>> filter = null) where TEntity : class
		{
			return await this.GetQueryable(filter).AnyAsync();
		}

		public void Create<TEntity>(TEntity entity) where TEntity : class
		{
			if (this.Entry(entity).State == EntityState.Detached)
				this.Set<TEntity>().Add(entity);
		}

		public void Update<TEntity>(TEntity entity) where TEntity : class
		{
			if (this.Entry(entity).State == EntityState.Detached)
				this.Set<TEntity>().Attach(entity);

			this.Entry(entity).State = EntityState.Modified;
		}

		public void Delete<TEntity>(TEntity entity) where TEntity : class
		{
			if (this.Entry(entity).State == EntityState.Detached)
				this.Set<TEntity>().Attach(entity);

			this.Set<TEntity>().Remove(entity);
		}

		public void Save()
		{
			try
			{
				this.SaveChanges();
			}
			catch (DbEntityValidationException e)
			{
				ThrowEnhancedValidationException(e);
			}
		}

		public Task SaveAsync()
		{
			try
			{
				return this.SaveChangesAsync();
			}
			catch (DbEntityValidationException e)
			{
				ThrowEnhancedValidationException(e);
			}

			return Task.CompletedTask;
		}

		#endregion

		#region Private methods

		private IQueryable<TEntity> GetQueryable<TEntity>(
			Expression<Func<TEntity, bool>> filter = null,
			Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
			Expression<Func<TEntity, object>>[] includeProperties = null,
			int? skip = null,
			int? take = null)
			where TEntity : class
		{
			IQueryable<TEntity> query = this.Set<TEntity>();

			if (filter != null)
				query = query.Where(filter);

			if (orderBy != null)
				query = orderBy(query);

			if (includeProperties != null)
			{
				foreach (var prop in includeProperties)
					if (prop != null)
						query.Include(prop);
			}

			if (skip.HasValue)
				query = query.Skip(skip.Value);

			if (take.HasValue)
				query = query.Take(take.Value);

			return query;
		}

		private void ThrowEnhancedValidationException(DbEntityValidationException e)
		{
			var errorMessages = e.EntityValidationErrors
														.SelectMany(x => x.ValidationErrors)
														.Select(x => x.ErrorMessage);

			var fullErrorMessage = string.Join("; ", errorMessages);
			var exceptionMessage = string.Concat(e.Message, " The validation errors are: ", fullErrorMessage);

			throw new DbEntityValidationException(exceptionMessage, e.EntityValidationErrors);
		}

		#endregion

	}
}