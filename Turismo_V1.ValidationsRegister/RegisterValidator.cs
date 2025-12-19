/**
* Trabalho de POO
* @author José Sousa - 31552
* @version 1.0
* Autenticação e validação de registo de utilizadores. 
*/
namespace Turismo_V1.ValidationsRegister
{

    public static class RegisterValidator
    {
        /// <summary>
        /// Valida se o número de telefone é válido (9 dígitos numéricos).
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        public static bool IsValidPhoneNumber(string phoneNumber)
        {
          
            if (string.IsNullOrEmpty(phoneNumber) || phoneNumber.Length != 9)
                return false;

           
            bool conseguiuConverter = long.TryParse(phoneNumber, out _);

            return conseguiuConverter;
        }
        /// <summary>
        /// Valida se o email é válido. 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrEmpty(email)) return false;

            if (email.Contains("@") && email.Contains(".")) 
            {
                return true;
            }
            return false;
           
        }


    }
















}
