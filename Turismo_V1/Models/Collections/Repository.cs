
using System.IO;
using Turismo_V1.Models.Collections.Interfaces;
using System.Text.Json;

/**
* Trabalho de POO
* @author José Sousa - 31552
* @version 1.0
* Classe Repository que representa um repositório genérico para armazenar e gerenciar dados. 
*/
namespace Turismo_V1.Models.Collections
{
    
        public abstract class Repository<T> : IRepository<T>
        {
        #region Fields

            private readonly string _filePath;
            private readonly List<T> _info = new();

            #endregion

        #region Constructor

            /// <summary>
            /// Creates a generic repository for a specific file.
            /// </summary>
            protected Repository(string fileName)
            {
                string basePath = Path.Combine("." + Path.DirectorySeparatorChar, "Data");

                if (!Directory.Exists(basePath))
                    Directory.CreateDirectory(basePath);

                _filePath = Path.Combine(basePath, fileName);

                if (!File.Exists(_filePath))
                    File.WriteAllText(_filePath, "[]");

                LoadFromFile();
            }

        #endregion

        #region List_Methods

        /// <summary>
        /// Adds a new info to the repository.
        /// </summary>
        /// <param name="item"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public void Add(T item)
            {
                if (item == null)
                    throw new ArgumentNullException();

                _info.Add(item);
                SaveToFile();
            }

        /// <summary>
        /// Removes an item from the repository.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public bool Remove(T item)
            {
                if (item == null)
                    throw new ArgumentNullException();

                bool removed = _info.Remove(item);

                if (removed)
                    SaveToFile();

                return removed;
            }

        #endregion

        #region File Operations


        /// <summary>
        /// Loads the repository data from the associated file.
        /// </summary>
        private void LoadFromFile()
            {
               
                    string json = File.ReadAllText(_filePath);
                    var data = JsonSerializer.Deserialize<List<T>>(json);

                    _info.Clear();
                    if (data != null)
                        _info.AddRange(data);
            }

        /// <summary>
        /// Saves the current repository data to the associated file.
        /// </summary>
        private void SaveToFile()
            {
                var options = new JsonSerializerOptions { WriteIndented = true };
                string json = JsonSerializer.Serialize(_info, options);
                File.WriteAllText(_filePath, json);
            }

        #endregion

        #region Query

        /// <summary>
        /// Gets all items from the repository.
        /// </summary>
        /// <returns></returns>
        public List<T> GetAll()
            {
                return new List<T>(_info);
            }

        /// <summary>
        /// Finds an item in the repository based on a predicate.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public T? Find(Func<T, bool> predicate)
            {
                return _info.FirstOrDefault(predicate);
            }

            #endregion
        }
  
    }




    



