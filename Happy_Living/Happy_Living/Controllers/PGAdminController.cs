using HL.BAL.Interface;
using HL.DAL.DomainModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Happy_Living.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PGAdminController : ControllerBase
    {
        private readonly IPGAdmin _PGAdmin;

        PGAdminController(IPGAdmin PGAdmin)
        {
            _PGAdmin = PGAdmin;
        }
        [HttpPost]
        [Route("PGUserAdd")]
        IActionResult AddPGUser(PGAdminDomain pGAdminDomain)
        {
            return _PGAdmin.AddPGUser(pGAdminDomain);
        }

    }
}
