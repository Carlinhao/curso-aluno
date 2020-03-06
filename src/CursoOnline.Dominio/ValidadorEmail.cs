using System.Text.RegularExpressions;

namespace CurosOnline.Dominio
{
    public class ValidadorEmail
    {
        private static Regex _regex;

        public static bool IsEmail(string email)
        {
            _regex = new Regex(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$");

            if(string.IsNullOrEmpty(email))
            {
                return false;
            }
            else if(_regex.IsMatch(email))
            {
                return true;
            }
            return false;
        }
    }
}