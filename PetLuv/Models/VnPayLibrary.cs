using System.Net;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace PetLuv.Models
{
    public class VnPayLibrary
    {
        private readonly SortedVariables _requestData = new SortedVariables();
        private readonly SortedVariables _responseData = new SortedVariables();

        public void AddRequestData(string key, string value) => _requestData.Add(key, value);
        public void AddResponseData(string key, string value) => _responseData.Add(key, value);

        public string GetResponseData(string key) => _responseData.Get(key) ?? string.Empty;

        public string CreateRequestUrl(string baseUrl, string vnpHashSecret)
        {
            var queryString = _requestData.GetQueryString();
            var rawData = queryString.Replace("?", "");
            var vnpSecureHash = HmacSha512(vnpHashSecret, rawData);
            return $"{baseUrl}{queryString}&vnp_SecureHash={vnpSecureHash}";
        }

        public bool ValidateSignature(string inputHash, string secretKey)
        {
            var rawData = _responseData.GetQueryString().Replace("?", "");
            var myChecksum = HmacSha512(secretKey, rawData);
            return myChecksum.Equals(inputHash, StringComparison.InvariantCultureIgnoreCase);
        }

        private string HmacSha512(string key, string inputData)
        {
            var hash = new StringBuilder();
            var keyBytes = Encoding.UTF8.GetBytes(key);
            var inputBytes = Encoding.UTF8.GetBytes(inputData);
            using (var hmac = new HMACSHA512(keyBytes))
            {
                var hashValue = hmac.ComputeHash(inputBytes);
                foreach (var theByte in hashValue)
                {
                    hash.Append(theByte.ToString("x2"));
                }
            }
            return hash.ToString();
        }
    }

    public class SortedVariables
    {
        private readonly SortedDictionary<string, string> _data = new SortedDictionary<string, string>(StringComparer.Ordinal);

        public void Add(string key, string value)
        {
            if (!string.IsNullOrEmpty(value)) _data.Add(key, value);
        }

        public string? Get(string key) => _data.TryGetValue(key, out var value) ? value : null;

        public string GetQueryString()
        {
            var sb = new StringBuilder();
            foreach (var kv in _data)
            {
                sb.Append($"&{WebUtility.UrlEncode(kv.Key)}={WebUtility.UrlEncode(kv.Value)}");
            }
            if (sb.Length > 0) sb.Remove(0, 1).Insert(0, "?");
            return sb.ToString();
        }
    }
}