using System;
using System.Text;

namespace Smokey.Demo.Features.ValuesCollecting
{
    public static class Key
    {
        public static string Create(
            string propertyName, 
            string location = null, 
            string root = null,
            bool unique = true)
        {
            var sb = new StringBuilder();

            if (string.IsNullOrWhiteSpace(root) is false)
            {
                sb.Append($"{root}.");
            }
            
            if (string.IsNullOrWhiteSpace(location) is false)
            {
                sb.Append($"{location}.");
            }

            sb.Append($"{propertyName}");

            if (unique)
            {
                sb.Append($".{Guid.NewGuid().ToString()}");
            }

            return sb.ToString();
        }
    }
}
