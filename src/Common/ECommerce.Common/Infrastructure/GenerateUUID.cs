using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Common.Infrastructure
{
    public class GenerateUUID
    {
        public string GenerateOrderNumber()
        {
            string uuid = Convert.ToBase64String(Guid.NewGuid().ToByteArray()) // GUID’i Base64’e çevir
                     .Replace("=", "") // "=" karakterlerini kaldır
                     .Replace("+", "") // "+" karakterlerini kaldır
                     .Replace("/", "") // "/" karakterlerini kaldır
                     .Substring(0, 12) // İlk 12 karakteri al
                     .ToUpper(); // Büyük harfe çevir

            return uuid;
        }

    }
}
