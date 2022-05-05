using Microsoft.AspNetCore.Mvc;
using Shop.Bll.Abstract.IManager;
using System.Net;

namespace Shop.RESTfulWebApi.Controller
{
    [ApiController]
    public class HomeController : ControllerBase
    {
        #region Veriables
        private readonly IInvoiceManager _invoiveManager;
        #endregion
        public HomeController(IInvoiceManager invoiveManager)
        {
            _invoiveManager = invoiveManager;
        }

        [Route(""), HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult RedirectToSwaggerUi() => Redirect("/swagger");


        [HttpGet]
        [Route("GetInvoices")]
        public async Task<IActionResult> GetInvoices()
        {
            try
            {
                var model = await _invoiveManager.GetAllInvoicesAsync();
                if (model.Count == 0)
                    return NoContent();

                return Ok(model);
            }
            catch
            {
                return NotFound("Server error, error occurred while processing request!");
            }
        }

        [HttpGet]
        [Route("GetInvoicesById/{id?}")]
        public async Task<IActionResult> GetInvoicesById(int id)
        {
            try
            {
                var model = await _invoiveManager.GetInvoiceById(id);
                if (model == null)
                    return NoContent();

                return Ok(model);
            }
            catch
            {
                return NotFound("Server error, error occurred while processing request!");
            }
        }
    }
}