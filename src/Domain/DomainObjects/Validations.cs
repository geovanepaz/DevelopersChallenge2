using System.Text.RegularExpressions;

namespace Domain.DomainObjects
{
    public class Validations
    {
        public static void IsEqual(object object1, object object2, string message)
        {
            if (object1.Equals(object2))
            {
                throw new DomainException(message);
            }
        }

        public static void IsEmpty(string value, string message)
        {
            if (value == null || value.Trim().Length == 0)
            {
                throw new DomainException(message);
            }
        }
    }
}
