/**
* Trabalho de POO
* @author José Sousa - 31552
* @version 1.0
* Repository Interface genérica.
*/
namespace Turismo_V1.Models.Collections.Interfaces
{
    public interface IRepository<T>
    {
        #region Methods

        /// <summary>
        /// Adds a new info to the repository.
        /// </summary>
        /// <param name="info"></param>
        public void Add(T info);

        /// <summary>
        /// Removes an item from the repository.
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public bool Remove(T info);

        /// <summary>
        /// Finds an item in the repository based on a predicate.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        T? Find(Func<T, bool> predicate);

        /// <summary>
        /// Gets all items from the repository.
        /// </summary>
        /// <returns></returns>
        public List<T> GetAll();


        #endregion



    }
}

