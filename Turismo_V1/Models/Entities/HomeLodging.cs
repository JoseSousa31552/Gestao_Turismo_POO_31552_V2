/**
* Trabalho de POO
* @author José Sousa - 31552
* @version 1.0
* Classe HomeLodging que representa um alojamento local.
*/
namespace Turismo_V1.Models.Entities
{
    public class HomeLodging : IdClass
    {
        #region Properties
        /// <summary>
        /// Nome do Alojamento Local.
        /// </summary>
        public string NameHomeLodging { get; set; }

        /// <summary>
        /// Endereço do Alojamento Local.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Preço por noite do Alojamento Local.
        /// </summary>
        public double PricePerNight { get; set; }

        /// <summary>
        /// Data de início da disponibilidade do Alojamento Local.
        /// </summary>
        public DateTime DateStart { get; set; }

        /// <summary>
        /// Data de término da disponibilidade do Alojamento Local.
        /// </summary>
        public DateTime DateEnd { get; set; }

        


        public HomeLodging(string nameHomeLodging, string address, double pricePerNight, DateTime dateStart, DateTime dateEnd)
        {
           
            NameHomeLodging = nameHomeLodging;
            Address = address;
            PricePerNight = pricePerNight;
            DateStart = dateStart;
            DateEnd = dateEnd;
        }
        #endregion Properties
        
    }
}
