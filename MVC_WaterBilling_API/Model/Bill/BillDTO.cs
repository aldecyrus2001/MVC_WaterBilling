﻿using MVC_WaterBilling_API.Model.Consumer;
using MVC_WaterBilling_API.Model.Meter_Reading;
using MVC_WaterBilling_API.Model.User;
using System.ComponentModel.DataAnnotations;

namespace MVC_WaterBilling_API.Model.Bill
{
    public class BillDTO
    {
        [Required(ErrorMessage = "Reading ID is required!")]
        public int ReadingID { get; set; }

        [Required(ErrorMessage = "Reference Number is required! Meter")]
        public string ReferenceNumber { get; set; }

        [Required(ErrorMessage = "Consumed Amount is required!")]
        public double Consumed_Amount { get; set; } //The total amount consumed by the consumers

        [Required(ErrorMessage = "From is required!")]
        public DateOnly From { get; set; }

        [Required(ErrorMessage = "To is required!")]
        public DateOnly To { get; set; }

        [Required(ErrorMessage = "Due Date is required!")]
        public DateOnly DueDate { get; set; }
    }

    public class BillWithReadingConsumerUser
    {
        public MeterReading MeterReading { get; set; }
        public Consumers Consumers { get; set; }
        public Users Users { get; set; }
        public Bills Billing { get; set; }
    }
}
