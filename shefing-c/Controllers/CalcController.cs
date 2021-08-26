using System;
using Microsoft.AspNetCore.Mvc;
using shefing_c.Calculators;
using shefing_c.Entities;

namespace shefing_c.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalcController : ControllerBase
    {
        // POST api/values
        [HttpPost]
        public String Post([FromBody] CalcModel entity)
        {
            ICalculate c = Program.myCalculator;
            return c.process(entity);
        }

    }
}
