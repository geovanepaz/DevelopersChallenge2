using API.ViewQuery;
using Application.Interfaces;
using Application.ViewModels.Transaction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Linq;

namespace API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/transaction")]
    [Authorize]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionAppService appService;
        public TransactionController(ITransactionAppService appService)
        {
            this.appService = appService;
        }

        [HttpPost]
        [Route("upload")]
        [ProducesResponseType(typeof(UploadResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Upload([FromQuery] UploadQuery query)
        {
            return Ok(await appService.InsertBulkAsync(query.GetStreamReader()));
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<TransactionResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetAll()
        {
            var result = await appService.GetAll();
            if (!result.Any()) return NotFound();

            return Ok(result);
        }
    }
}
