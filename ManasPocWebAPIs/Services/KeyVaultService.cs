using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Azure.Core;


namespace ManasPocWebAPIs.Services
{
    public class KeyVaultService
    {
        private static KeyVaultService instance;
        private KeyVaultService() { }

        public static KeyVaultService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new KeyVaultService();
                }
                return instance;
            }
        }


        public string GetKeyVaultSecretValue(string secretKey)
        {
            string secretValue = string.Empty;


            return "My project name is :: " + secretValue;
        }

        public string GetKeyVaultSecretValue(string secretUri, bool isSecure)
        {
            string secretValue = string.Empty;
            secretValue = getSecretByUri(secretUri, isSecure);

            return "My project name is :: " + secretValue;
        }
        private string getSecretByUri(string secreateUri, bool isSecure)
        {
            string secretKey = string.Empty;
            string keyVaultName = string.Empty;
            string keyVaultUri = string.Empty;
            string secretValue = string.Empty;
            //TO DO, Manipulate the secret uri and get the ke and keyvault name out of it

            keyVaultUri = GetKeyVaultUri(secreateUri);
            secretKey = GetSecretKey(secreateUri);
            

            if (isSecure)
            {
                //get from keyvault
                SecretClientOptions options = new SecretClientOptions()
                {
                    Retry =
                {
                  Delay= TimeSpan.FromSeconds(2),
                  MaxDelay = TimeSpan.FromSeconds(16),
                  MaxRetries = 5,
                  Mode = RetryMode.Exponential
                }
                };

                var credential = new ClientSecretCredential("", "", "");

                var client = new SecretClient(new Uri(keyVaultUri), new DefaultAzureCredential(), options);

                KeyVaultSecret secret = client.GetSecret(secretKey);

                secretValue = secret.Value;


            }
            else
            {
                //get it from Sql or from App setting
            }

            return secretValue;

        }

        private string GetSecretByKey(string keyVaultUrl, string secretKey, bool isSecure)
        {
            string newSecretValue = string.Empty;
            if (isSecure)
            {
                //get from keyvault
                SecretClientOptions options = new SecretClientOptions()
                {
                    Retry =
                {
                  Delay= TimeSpan.FromSeconds(2),
                  MaxDelay = TimeSpan.FromSeconds(16),
                  MaxRetries = 5,
                  Mode = RetryMode.Exponential
                }
                };
                var client = new SecretClient(new Uri(keyVaultUrl), new DefaultAzureCredential(), options);

                KeyVaultSecret secret = client.GetSecret(secretKey);

                newSecretValue = secret.Value;
            }
            else
            {
                // Get it from app setting / sql
            }

            return newSecretValue;
        }

        /// <summary>
        /// Get Secure key :https://{vault-name}.vault.azure.net/{object-type}/{object-name}/{object-version}
        /// </summary>
        /// <param name="secureUrl"></param>
        /// <returns>secureKey</returns>
        private string GetSecretKey(string secretUrl)
        {
            string secureKey = string.Empty;

            if (secretUrl.Contains("/secrets/"))
            {
                int startIndex = secretUrl.IndexOf("/secrets/");
                // int endIndex =
                secureKey = secretUrl.Substring(startIndex+9).Replace('/', ' ').Trim();
            }

            return secureKey;
        }

        private string GetKeyVaultName(string secretUrl)
        {

            string keyVaultName = string.Empty;

            if (secretUrl.Contains("https://"))
            {
                keyVaultName = secretUrl.Substring(secretUrl.IndexOf("://") + 3);
                keyVaultName = keyVaultName.Substring(0, keyVaultName.IndexOf('.'));

            }

            return keyVaultName;
        }

        private string GetKeyVaultUri(string secretUrl)
        {
            string keyVaultUri = string.Empty;
            if (secretUrl.Contains("secrets/"))
            {
                keyVaultUri = secretUrl.Substring(0, secretUrl.IndexOf("secrets/")).Trim();
            }


            return keyVaultUri;
        }

        private string GenerateKeyVaultUri(string keyVaultName)
        {
            string secureUrl = string.Empty;

            var kvUri = $"https://{keyVaultName}.vault.azure.net";

            return secureUrl;
        }


    }
}
