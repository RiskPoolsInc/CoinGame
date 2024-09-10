using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SilentNotary.Cqrs.Domain.Interfaces;

namespace TS.Infrastructure.Dal
{
    public class ValueObjectProvider : IValueObjectProvider
    {
        private readonly DbContext _context;

        public ValueObjectProvider(DbContext context)
        {
            _context = context;
        }

        public T PopulateObject<T>(T entity)
            where T : class, IPersistableValueObject
        {
            return entity;
            // todo
            //            return _context.ReAttach(entity);
        }

        public ICollection<T> PopulateCollection<T>(ICollection<T> source)
            where T : class, IPersistableValueObject
        {
            return source;
            // todo
            //return _context.ReAttachCollection(source);
        }
    }
}