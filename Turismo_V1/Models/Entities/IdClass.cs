/**
* Trabalho de POO
* @author José Sousa - 31552
* @version 1.0
* Classe IdClass que serve de base para outras classes, fornecendo um ID único.
*/
namespace Turismo_V1.Models.Entities
{
    /// <summary>
    /// Represents a base class with a unique globally unique identifier (ID).
    /// </summary>

    public abstract class IdClass
    {
        #region Properties

        /// <summary>
        /// Gets the unique ID of the instance.
        /// </summary>
        public Guid ID { get;  set; }


        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the class and generates a unique ID.
        /// </summary>
        protected IdClass()
        {
            GenerateID();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Generates a new globally unique ID for the instance.
        /// </summary>
        private void GenerateID()
        {
            ID = Guid.NewGuid();
        }

        #endregion
    }
}
