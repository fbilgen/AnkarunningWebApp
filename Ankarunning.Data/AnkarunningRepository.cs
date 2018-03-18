using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ankarunning.Data {
    public class AnkarunningRepository<T> : IAnkarunningRepository<T> where T : BaseEntity {

        private readonly AnkarunningContext _context;
        private DbSet<T> _entities;

        //DI of context
        public AnkarunningRepository(AnkarunningContext context) {
            _context = context;
            _entities = context.Set<T>();
        }
        public IQueryable<T> GetAll() {
            return _entities.AsQueryable();
        }
        public IQueryable<T> Get(long id) {
            return _entities.Where(s => s.Id == id);
        }

        public long Insert(T entity) {
            if (entity == null) {
                throw new ArgumentNullException("entity");
            }
            _context.Add(entity);
            return entity.Id;
        }

        public void Update(T entity) {
            if (entity == null) {
                throw new ArgumentNullException("entity");
            }
            _entities.Update(entity);
        }
        public void Delete(T entity) {
            if (entity == null) {
                throw new ArgumentNullException("entity");
            }
            _entities.Remove(entity);
        }

        public void SaveChanges() {
            _context.SaveChanges();
        }
    }
}
