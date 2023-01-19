using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ManasPocWebAPIs.Services;
namespace ManasPocWebAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KeyVaultController : ControllerBase
    {
        [HttpGet("getKeyVaultSecretByUri")]
        public ActionResult<string> KeyVaultSecretByUri(string SecretUri,bool isSecure)
        {
            try
            {

                string secretValue = KeyVaultService.Instance.GetKeyVaultSecretValue("https://manaspockeyvault.vault.azure.net/secrets/Project/", true);
                return secretValue;
            }
            catch (Exception ex)
            {
                // TIEPLogger.Instance.Error(ex);
                return BadRequest();
            }
        }

        [HttpGet("getKeyVaultSecretByKey")]
        public ActionResult<string> KeyVaultSecretByKey(string keyVaultName,string SecretKey,bool isSecure)
        {
            try
            {

                string secretValue = KeyVaultService.Instance.GetKeyVaultSecretValue("https://manaspockeyvault.vault.azure.net/secrets/Project/", true);
                return secretValue;
            }
            catch (Exception ex)
            {
                // TIEPLogger.Instance.Error(ex);
                return BadRequest();
            }
        }

    }
}
