using GenelProje.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GenelProje.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class denemeController : ControllerBase
    {

        private readonly NorthwndContext _northwndContext;
        public denemeController(NorthwndContext northwndContext)
        {
            _northwndContext = northwndContext;
        }

        [HttpGet("deneme")]
        public IActionResult Index()
        {
            return Ok();
        }



        //[HttpPost("start")]
        //public IActionResult StartPeriodicTask()
        //{

        //    //RecurringJob.AddOrUpdate<IServiceManagement>(x => x.PeriodicTask(), Cron.MinuteInterval(1));
        //    //RecurringJob.AddOrUpdate("periodic-task", () => PeriodicTask(), "*/5 * * * *");
        //    RecurringJob.AddOrUpdate("periodic-task", () => PeriodicTask(), Cron.MinuteInterval(1));


        //    return Ok();
        //}


        //[HttpPost("stop")]
        //public IActionResult StopPeriodicTask()
        //{
        //    // Hangfire'da tanımlanan belirli bir görevi durdur
        //    RecurringJob.RemoveIfExists("periodic-task");// belirtilen isimde bir görev varsa onu durdurur.

        //    return Ok();
        //}

        //public void PeriodicTask()
        //{

        //    //Hangfire tarafından belirli aralıklarla çalıştırılacak olan işlemler

        //    // güncelleme işlemi
        //    var recordsToUpdate = _northwndContext.Products.ToList();

        //    foreach (var record in recordsToUpdate)
        //    {
        //        record.UnitPrice++;
        //    }

        //    _northwndContext.SaveChanges();
        //}
    }
}
