using System.Security.Cryptography;

namespace kafka_for_web.DataAccess;

public static class HashFunction
{
   private static readonly HashAlgorithm Sha = SHA256.Create();

   public static int Hash<T> (T key, int mod = 1)
   {
      // hash over the generic type T
      if (key == null)
      {
         return -1; 
      }
      // * return a key from 0 to 2^32 - 1
      var data = Sha.ComputeHash(System.Text.Encoding.UTF8.GetBytes(key.ToString() ?? throw new InvalidOperationException()));
      
      return BitConverter.ToInt32(data) % mod; 
   }
   
   public static int ConsistentHashing<T>(int hashSpace, T key)
   {
      return -1;
   }
}