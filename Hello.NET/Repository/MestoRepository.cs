using Hello.NET.Domain.Models;
using Hello.NET.Repository.Repository.Demo.DAL;
using Hello.NET.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hello.NET.Repository {
    public class MestoRepository : IRepository<Mesto> {
        public void Create(Mesto entity) {
            using (AppDbContext context = new AppDbContext()) {
                context.MestoDb.Add(entity);
                context.SaveChanges();
            }
        }

        public void Delete(Mesto entity) {
            throw new NotImplementedException();
        }

        public void Delete(int id) {
            throw new NotImplementedException();
        }

        public IEnumerable GetAll() {
            return null; 
        }

        public Mesto GetById(int id) {
            Mesto m = new Mesto();
            m.Naziv = "blabla";
            return m;
        }

        public void Update(Mesto entity) {
            throw new NotImplementedException();
        }
    }
}
