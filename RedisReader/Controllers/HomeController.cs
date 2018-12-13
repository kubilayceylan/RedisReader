using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RedisReader.Models;
using StackExchange.Redis;


namespace RedisReader.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {

            return View();

        }


        [HttpPost]
        public IActionResult Index([FromBody]string message)
        {
            var result = "";
            try
            {
                var redisHost = Environment.GetEnvironmentVariable("REDIS_HOSTNAME");
                Console.Out.WriteLine("REDIS_HOSTNAME = {0}", redisHost);
                var redisPort = Environment.GetEnvironmentVariable("REDIS_PORT");
                Console.Out.WriteLine("REDIS_PORT = {0}", redisPort);

                if (redisHost == null)
                {
                    throw new Exception("REDIS_HOSTNAME Environment variable is not defined!");
                }
                if (redisPort == null)
                {
                    throw new Exception("REDIS_PORT Environment variable is not defined!");
                }

                // Redis Write
                ConnectionMultiplexer redis = Redis.GetInstance(redisHost, Int32.Parse(redisPort));
                Console.Out.WriteLine("Got redis connection multiplexer.");
                IDatabase db = redis.GetDatabase(0);
                Console.Out.WriteLine("Connected to redis database 0.");
                result = db.StringGet(message);
                Console.Out.WriteLine("Read from redis {0}:{1}", message, result);

            }
            catch (Exception e)
            {
                Console.Out.WriteLine("{0} \n\nException caught.", e);

            }
            return Json(result);

        }

        public IActionResult Result()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
