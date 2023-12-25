using GenelProje.Models;
using GenelProje.Service;
using Hangfire;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GenelProje.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly NorthwndContext _context;
        public ValuesController(NorthwndContext context)
        {
            _context = context;
        }

        [HttpPost("AddCustomer")]
        public IActionResult AddCustomer(Customer customer)
        {
            _context.Add(customer);
            _context.SaveChanges();

            BackgroundJob.Enqueue<IServiceManagement>(x => x.SendMessage(customer.CustomerId));

            return Ok();
        }

        [HttpPost("AddCustomer2")]
        public IActionResult AddCustomer2(Customer customer)
        {
            _context.Add(customer);
            _context.SaveChanges();


            BackgroundJob.Schedule<IServiceManagement>(x => x.SendCoupon(), TimeSpan.FromMinutes(5));//Schedule dönüş türü task TimeSpan.FromMinutes(5) yaknızca bir kere çalışacak görevlerde kullanılıyor.

            //BackgroundJob.Schedule<IServiceManagement>(x => x.SendCoupon(), Cron.MinuteInterval(5));

            return Ok();
        }
        [HttpPost("Confirm")]//BackgroundJob.ContinueJobWith metodu kullanılarak, ikinci iş birinci iş tamamlandıktan sonra çalışır. 
        public IActionResult Confirm()
        {
            int timeInSeconds = 30;
            var parentJobId = BackgroundJob.Schedule(() => Console.WriteLine("birinci iş!" + DateTime.Now), TimeSpan.FromSeconds(timeInSeconds));
            BackgroundJob.ContinueJobWith(parentJobId, () => Console.WriteLine("ikinci iş!"));
            return Ok("Confirmation job created!");
        }

        [HttpPost("start")]
        public IActionResult StartPeriodicTask()
        {

            RecurringJob.AddOrUpdate<IServiceManagement>(x => x.PeriodicTask(), Cron.MinuteInterval(1));

            //RecurringJob.AddOrUpdate("periodic-task", () => PeriodicTask(), Cron.MinuteInterval(1)); görev ismi çalışacak fonksiyon ve periyod aralığı

            return Ok();
        }


        [HttpPost("stop")]
        public IActionResult StopPeriodicTask()
        {
            // Hangfire'da tanımlanan belirli bir görevi durdur
            RecurringJob.RemoveIfExists("IServiceManagement.PeriodicTask");
            // belirtilen isimde bir görev varsa onu durdurur.

            return Ok();
        }

    }
}
