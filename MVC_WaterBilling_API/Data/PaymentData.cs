using Microsoft.EntityFrameworkCore;
using MVC_WaterBilling_API.Model.Meter_Reading;
using MVC_WaterBilling_API.Model.Payments;
using MVC_WaterBilling_API.Services;

namespace MVC_WaterBilling_API.Data
{
    public class PaymentData
    {
        private readonly ApplicationDBContext _db;

        public PaymentData(ApplicationDBContext db)
        {
            _db = db;
        }

        public async Task<List<PaymentsWithUserConsumer>> GetPaymentsAsync()
        {
            var result = await (from p in _db.Payments
                                where p.Remarks == "Pending"
                                join b in _db.Bills on p.BillID equals b.BillID into billGroup
                                from bill in billGroup.DefaultIfEmpty() 

                                join r in _db.Meters on bill.ReadingID equals r.ReadingID into readingGroup
                                from reading in readingGroup.DefaultIfEmpty()

                                join c in _db.Consumers on reading.Meter_Number equals c.Meter_Number into consumerGroup
                                from consumer in consumerGroup.DefaultIfEmpty()
                                join u in _db.Users on consumer.UserID equals u.UserID.ToString() into userGroup
                                from user in userGroup.DefaultIfEmpty()

                                select new PaymentsWithUserConsumer
                                {
                                    Payments = p,                      // Using correct capitalization
                                    Bills = bill,                       // Bill might be null
                                    MeterReading = reading,             // MeterReading might be null
                                    Consumer = consumer,                // Consumer might be null
                                    User = user                          // User might be null
                                }).ToListAsync();

            return result;
        }

        public async Task<List<PaymentsWithUserConsumer>> GetConsumersPaymentAsync(int UserID)
        {
            var result = await (from p in _db.Payments
                                join b in _db.Bills on p.BillID equals b.BillID into billGroup
                                from bill in billGroup.DefaultIfEmpty()

                                join r in _db.Meters on bill.ReadingID equals r.ReadingID into readingGroup
                                from reading in readingGroup.DefaultIfEmpty()

                                join c in _db.Consumers on reading.Meter_Number equals c.Meter_Number into consumerGroup
                                from consumer in consumerGroup.DefaultIfEmpty()
                                join u in _db.Users on consumer.UserID equals u.UserID.ToString() into userGroup
                                from user in userGroup.DefaultIfEmpty()

                                where user != null && user.UserID == UserID

                                select new PaymentsWithUserConsumer
                                {
                                    Payments = p,                      // Using correct capitalization
                                    Bills = bill,                       // Bill might be null
                                    MeterReading = reading,             // MeterReading might be null
                                    Consumer = consumer,                // Consumer might be null
                                    User = user                          // User might be null
                                }).ToListAsync();

            return result;
        }

        public async Task<List<PaymentsWithUserConsumer>> GetCashierHistory(string CashierID)
        {
            var result = await (from p in _db.Payments
                                where p.CashierID == CashierID
                                join b in _db.Bills on p.BillID equals b.BillID into billGroup
                                from bill in billGroup.DefaultIfEmpty()

                                join r in _db.Meters on bill.ReadingID equals r.ReadingID into readingGroup
                                from reading in readingGroup.DefaultIfEmpty()

                                join c in _db.Consumers on reading.Meter_Number equals c.Meter_Number into consumerGroup
                                from consumer in consumerGroup.DefaultIfEmpty()
                                join u in _db.Users on consumer.UserID equals u.UserID.ToString() into userGroup
                                from user in userGroup.DefaultIfEmpty()

                                select new PaymentsWithUserConsumer
                                {
                                    Payments = p,                      // Using correct capitalization
                                    Bills = bill,                       // Bill might be null
                                    MeterReading = reading,             // MeterReading might be null
                                    Consumer = consumer,                // Consumer might be null
                                    User = user                          // User might be null
                                }).ToListAsync();

            return result;
        }

        public async Task<PaymentsWithUserConsumer?> GetPaymentAsync(int id)
        {
            var result = await (from p in _db.Payments
                                where p.PaymentID == id
                                join b in _db.Bills on p.BillID equals b.BillID into billGroup
                                from bill in billGroup.DefaultIfEmpty()

                                join r in _db.Meters on bill.ReadingID equals r.ReadingID into readingGroup
                                from reading in readingGroup.DefaultIfEmpty()

                                join c in _db.Consumers on reading.Meter_Number equals c.Meter_Number into consumerGroup
                                from consumer in consumerGroup.DefaultIfEmpty()

                                join u in _db.Users on consumer.UserID equals u.UserID.ToString() into userGroup
                                from user in userGroup.DefaultIfEmpty()

                                select new PaymentsWithUserConsumer
                                {
                                    Payments = p,     
                                    Bills = bill,       
                                    MeterReading = reading,  
                                    Consumer = consumer,  
                                    User = user          
                                }).FirstOrDefaultAsync(); 

            return result;
        }

        public async Task<List<PaymentsWithUserConsumer>> GetOnlinePaymentsAsync()
        {
            var result = await (from p in _db.Payments
                                where p.Remarks == "Pending"
                                join b in _db.Bills on p.BillID equals b.BillID into billGroup
                                from bill in billGroup.DefaultIfEmpty() // Left Join Bills

                                join r in _db.Meters on bill.ReadingID equals r.ReadingID into readingGroup
                                from reading in readingGroup.DefaultIfEmpty() // Left Join MeterReadings

                                join c in _db.Consumers on reading.Meter_Number equals c.Meter_Number into consumerGroup
                                from consumer in consumerGroup.DefaultIfEmpty() // Left Join Consumers

                                join u in _db.Users on consumer.UserID equals u.UserID.ToString() into userGroup
                                from user in userGroup.DefaultIfEmpty() // Left Join Users

                                select new PaymentsWithUserConsumer
                                {
                                    Payments = p,                      // Using correct capitalization
                                    Bills = bill,                       // Bill might be null
                                    MeterReading = reading,             // MeterReading might be null
                                    Consumer = consumer,                // Consumer might be null
                                    User = user                          // User might be null
                                }).ToListAsync();

            return result;
        }





        public async Task<Payments?> GetPaymentByPaymentIDAsync(int id)
        {
            return await _db.Payments.FindAsync(id);
        }

        public async Task<Payments?> GetPaymentByBillIDAsync(int id)
        {
            return await _db.Payments.FirstOrDefaultAsync(p => p.BillID == id);
        }

        public async Task<double> GetTodaytotalAmount()
        {
            var today = DateOnly.FromDateTime(DateTime.Now);

            var totalAmount = await _db.Payments
                                        .Where(p => p.PaymentDate == today)
                                        .SumAsync(p => p.Amount_Paid);

            return totalAmount;
        }

        public async Task<bool> isBilledAlreadyAsync(int BillID)
        {
            return await _db.Payments.AnyAsync(p => p.BillID == BillID);
        }

        public async Task CreatePaymentAsync(Payments payments)
        {
            _db.Payments.Add(payments);
            await _db.SaveChangesAsync();
        }

        public async Task<List<PaymentsWithUserConsumer>> SearchAsync(string searchQuery)
        {
            var query = from p in _db.Payments
                        join b in _db.Bills on p.BillID equals b.BillID into billGroup
                        from bill in billGroup.DefaultIfEmpty()
                        join mr in _db.Meters on bill.ReadingID equals mr.ReadingID into meterReadingGroup
                        from meterReading in meterReadingGroup.DefaultIfEmpty()
                        join c in _db.Consumers on meterReading.Meter_Number equals c.Meter_Number into consumerGroup
                        from consumer in consumerGroup.DefaultIfEmpty()
                        join u in _db.Users on consumer.UserID equals u.UserID.ToString() into userGroup
                        from user in userGroup.DefaultIfEmpty()
                        where user == null || user.Status != "Deleted" // Exclude Deleted Users
                        select new PaymentsWithUserConsumer
                        {
                            Payments = p,
                            Bills = bill,
                            MeterReading = meterReading,
                            Consumer = consumer,
                            User = user
                        };

            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                searchQuery = searchQuery.ToLower().Trim();

                // Try parsing the searchQuery as a DateTime
                DateTime searchDate;
                bool isDate = DateTime.TryParse(searchQuery, out searchDate);

                query = query.Where(puc =>
                    puc.User.Firstname.ToLower().Contains(searchQuery) ||
                    puc.User.Middlename.ToLower().Contains(searchQuery) ||
                    puc.User.Lastname.ToLower().Contains(searchQuery) ||
                    puc.User.Gender.ToLower().Contains(searchQuery) ||
                    puc.User.PhoneNumber.Contains(searchQuery) ||
                    puc.User.Email.ToLower().Contains(searchQuery) ||
                    puc.User.Role.ToLower().Contains(searchQuery) ||
                    puc.Consumer.Address.ToLower().Contains(searchQuery) ||
                    puc.Consumer.ConnectionType.ToLower().Contains(searchQuery) ||
                    puc.Consumer.Meter_Number.Contains(searchQuery) ||
                    puc.Consumer.Consumer_Status.ToLower().Contains(searchQuery) ||
                    puc.MeterReading.ReaderID.Contains(searchQuery) ||
                    puc.MeterReading.Previous_Reading.ToString().Contains(searchQuery) ||
                    puc.MeterReading.Current_Reading.ToString().Contains(searchQuery) ||
                    puc.MeterReading.Usage.ToString().Contains(searchQuery) ||
                    puc.MeterReading.Status.ToLower().Contains(searchQuery) ||
                    (puc.MeterReading.MonthOf != null && puc.MeterReading.MonthOf.ToLower().Contains(searchQuery)) ||
                    (isDate && puc.MeterReading.Reading_Date.Date == searchDate.Date) ||
                    puc.Bills.ReferenceNumber.ToLower().Contains(searchQuery) ||
                    puc.Bills.Consumed_Amount.ToString().Contains(searchQuery) ||
                    puc.Bills.BillStatus.ToLower().Contains(searchQuery) ||
                    (isDate && (puc.Bills.BillDate.Date == searchDate.Date ||
                                puc.Bills.From == DateOnly.FromDateTime(searchDate) ||
                                puc.Bills.To == DateOnly.FromDateTime(searchDate) ||
                                puc.Bills.DueDate == DateOnly.FromDateTime(searchDate))) ||
                    puc.Payments.Amount_Paid.ToString().Contains(searchQuery) ||
                    puc.Payments.PaymentMethod.ToLower().Contains(searchQuery) ||
                    (isDate && puc.Payments.PaymentDate == DateOnly.FromDateTime(searchDate))
                );
            }

            return await query.OrderByDescending(puc => puc.Payments.PaymentDate).ToListAsync();
        }

        public async Task<List<PaymentsWithUserConsumer>> SearchFromConsumersAsync(string searchQuery)
        {
            var query = from p in _db.Payments
                        join b in _db.Bills on p.BillID equals b.BillID into billGroup
                        from bill in billGroup.DefaultIfEmpty()
                        join mr in _db.Meters on bill.ReadingID equals mr.ReadingID into meterReadingGroup
                        from meterReading in meterReadingGroup.DefaultIfEmpty()
                        join c in _db.Consumers on meterReading.Meter_Number equals c.Meter_Number into consumerGroup
                        from consumer in consumerGroup.DefaultIfEmpty()
                        join u in _db.Users on consumer.UserID equals u.UserID.ToString() into userGroup
                        from user in userGroup.DefaultIfEmpty()
                        where user == null || user.Status != "Deleted" // Exclude Deleted Users
                        select new PaymentsWithUserConsumer
                        {
                            Payments = p,
                            Bills = bill,
                            MeterReading = meterReading,
                            Consumer = consumer,
                            User = user
                        };

            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                searchQuery = searchQuery.ToLower().Trim();

                // Try parsing the searchQuery as a DateTime
                DateTime searchDate;
                bool isDate = DateTime.TryParse(searchQuery, out searchDate);

                query = query.Where(puc =>
                    puc.User.Firstname.ToLower().Contains(searchQuery) ||
                    puc.User.Middlename.ToLower().Contains(searchQuery) ||
                    puc.User.Lastname.ToLower().Contains(searchQuery) ||
                    puc.User.Gender.ToLower().Contains(searchQuery) ||
                    puc.User.PhoneNumber.Contains(searchQuery) ||
                    puc.User.Email.ToLower().Contains(searchQuery) ||
                    puc.User.Role.ToLower().Contains(searchQuery) ||
                    puc.Consumer.Address.ToLower().Contains(searchQuery) ||
                    puc.Consumer.ConnectionType.ToLower().Contains(searchQuery) ||
                    puc.Consumer.Meter_Number.Contains(searchQuery) ||
                    puc.Consumer.Consumer_Status.ToLower().Contains(searchQuery) ||
                    puc.MeterReading.ReaderID.Contains(searchQuery) ||
                    puc.MeterReading.Previous_Reading.ToString().Contains(searchQuery) ||
                    puc.MeterReading.Current_Reading.ToString().Contains(searchQuery) ||
                    puc.MeterReading.Usage.ToString().Contains(searchQuery) ||
                    puc.MeterReading.Status.ToLower().Contains(searchQuery) ||
                    (puc.MeterReading.MonthOf != null && puc.MeterReading.MonthOf.ToLower().Contains(searchQuery)) ||
                    (isDate && puc.MeterReading.Reading_Date.Date == searchDate.Date) ||
                    puc.Bills.ReferenceNumber.ToLower().Contains(searchQuery) ||
                    puc.Bills.Consumed_Amount.ToString().Contains(searchQuery) ||
                    puc.Bills.BillStatus.ToLower().Contains(searchQuery) ||
                    (isDate && (puc.Bills.BillDate.Date == searchDate.Date ||
                                puc.Bills.From == DateOnly.FromDateTime(searchDate) ||
                                puc.Bills.To == DateOnly.FromDateTime(searchDate) ||
                                puc.Bills.DueDate == DateOnly.FromDateTime(searchDate))) ||
                    puc.Payments.Amount_Paid.ToString().Contains(searchQuery) ||
                    puc.Payments.PaymentMethod.ToLower().Contains(searchQuery) ||
                    (isDate && puc.Payments.PaymentDate == DateOnly.FromDateTime(searchDate))
                );
            }

            return await query.OrderByDescending(puc => puc.Payments.PaymentDate).ToListAsync();
        }

        public async Task<List<PaymentsWithUserConsumer>> SearchOnlinePaymentAsync(string searchQuery)
        {
            var query = from p in _db.Payments
                        where p.Remarks == "Pending"
                        join b in _db.Bills on p.BillID equals b.BillID into billGroup
                        from bill in billGroup.DefaultIfEmpty()
                        join mr in _db.Meters on bill.ReadingID equals mr.ReadingID into meterReadingGroup
                        from meterReading in meterReadingGroup.DefaultIfEmpty()
                        join c in _db.Consumers on meterReading.Meter_Number equals c.Meter_Number into consumerGroup
                        from consumer in consumerGroup.DefaultIfEmpty()
                        join u in _db.Users on consumer.UserID equals u.UserID.ToString() into userGroup
                        from user in userGroup.DefaultIfEmpty()
                        where user == null || user.Status != "Deleted" // Exclude Deleted Users
                        select new PaymentsWithUserConsumer
                        {
                            Payments = p,
                            Bills = bill,
                            MeterReading = meterReading,
                            Consumer = consumer,
                            User = user
                        };

            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                searchQuery = searchQuery.ToLower().Trim();

                // Try parsing the searchQuery as a DateTime
                DateTime searchDate;
                bool isDate = DateTime.TryParse(searchQuery, out searchDate);

                query = query.Where(puc =>
                    puc.User.Firstname.ToLower().Contains(searchQuery) ||
                    puc.User.Middlename.ToLower().Contains(searchQuery) ||
                    puc.User.Lastname.ToLower().Contains(searchQuery) ||
                    puc.User.Gender.ToLower().Contains(searchQuery) ||
                    puc.User.PhoneNumber.Contains(searchQuery) ||
                    puc.User.Email.ToLower().Contains(searchQuery) ||
                    puc.User.Role.ToLower().Contains(searchQuery) ||
                    puc.Consumer.Address.ToLower().Contains(searchQuery) ||
                    puc.Consumer.ConnectionType.ToLower().Contains(searchQuery) ||
                    puc.Consumer.Meter_Number.Contains(searchQuery) ||
                    puc.Consumer.Consumer_Status.ToLower().Contains(searchQuery) ||
                    puc.MeterReading.ReaderID.Contains(searchQuery) ||
                    puc.MeterReading.Previous_Reading.ToString().Contains(searchQuery) ||
                    puc.MeterReading.Current_Reading.ToString().Contains(searchQuery) ||
                    puc.MeterReading.Usage.ToString().Contains(searchQuery) ||
                    puc.MeterReading.Status.ToLower().Contains(searchQuery) ||
                    (puc.MeterReading.MonthOf != null && puc.MeterReading.MonthOf.ToLower().Contains(searchQuery)) ||
                    (isDate && puc.MeterReading.Reading_Date.Date == searchDate.Date) ||
                    puc.Bills.ReferenceNumber.ToLower().Contains(searchQuery) ||
                    puc.Bills.Consumed_Amount.ToString().Contains(searchQuery) ||
                    puc.Bills.BillStatus.ToLower().Contains(searchQuery) ||
                    (isDate && (puc.Bills.BillDate.Date == searchDate.Date ||
                                puc.Bills.From == DateOnly.FromDateTime(searchDate) ||
                                puc.Bills.To == DateOnly.FromDateTime(searchDate) ||
                                puc.Bills.DueDate == DateOnly.FromDateTime(searchDate))) ||
                    puc.Payments.Amount_Paid.ToString().Contains(searchQuery) ||
                    puc.Payments.PaymentMethod.ToLower().Contains(searchQuery) ||
                    (isDate && puc.Payments.PaymentDate == DateOnly.FromDateTime(searchDate))
                );
            }

            return await query.OrderByDescending(puc => puc.Payments.PaymentDate).ToListAsync();
        }

        public async Task UpdatePaymentStatus(int id,string CashierID)
        {
            var payment = await _db.Payments.FindAsync(id);
            if (payment != null)
            {
                payment.Remarks = "Paid";
                payment.PaymentMethod = "Online Bank";
                payment.CashierID = CashierID;
                await _db.SaveChangesAsync();
            }
        }
        public async Task UpdatePaymentRemarks(Payments payment)
        {
            _db.Payments.Attach(payment);
            _db.Entry(payment).Property(x => x.Remarks).IsModified = true;
            await _db.SaveChangesAsync();
        }
    }

}
