﻿using System.Numerics;

namespace MVC_WaterBilling_API.Services
{
    public class ApiResponse
    {
        public string Message { get; set; }
        public int? ReadingID { get; set; }
        public int? BillID { get; set; }
    }
}
