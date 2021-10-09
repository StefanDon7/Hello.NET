namespace Hello.NET.Repository {
    using System.Collections;
    namespace Repository.Demo.DAL {
        public interface IRepository<T> where T : class {
            ///  
            /// Get All objects  
            ///  
            ///  
            IEnumerable GetAll();
            ///  
            /// Get Object by Id  
            ///  
            ///  
            T GetById(int id);
            ///  
            /// Create the object  
            ///  
            ///  
            void Create(T entity);
            ///  
            /// Delete object  
            ///  
            ///  
            void Delete(T entity);
            ///  
            ///  
            ///  
            ///  
            void Delete(int id);
            ///  
            /// Update the object  
            ///  
            ///  
            void Update(T entity);
        }
    }
}
