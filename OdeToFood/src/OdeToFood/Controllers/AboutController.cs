﻿using Microsoft.AspNet.Mvc;

namespace OdeToFood.Controllers
{
    [Route("company/[controller]/[action]")]
    public class AboutController
    {
        public string Phone()
        {
            return "+359 883444444";
        }

        public string Country()
        {
            return "Bulgaria";
        }
    }
}
