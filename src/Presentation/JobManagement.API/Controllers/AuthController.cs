using JobManagement.Application.DTOs.Auth;
using JobManagement.Application.Interfaces;
using JobManagement.Domain.Entities.Test;
using JobManagement.Domain.Enums;
using JobManagement.Persistence.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System.ComponentModel;
using LicenseContext = OfficeOpenXml.LicenseContext;

namespace JobManagement.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly AppDbContext _context;

    public AuthController(IAuthService authService, AppDbContext context)
    {
        _authService = authService;
        _context = context;
    }



    //[HttpPost]
    //public async Task<IActionResult> ImportExcel(IFormFile file)
    //{
    //    if (file == null || file.Length == 0)
    //        return BadRequest("Zəhmət olmasa düzgün Excel faylı seçin.");


    //    using var stream = new MemoryStream();
    //    await file.CopyToAsync(stream);
    //    using var package = new ExcelPackage(stream);

    //    var worksheet = package.Workbook.Worksheets[0];
    //    var rowCount = worksheet.Dimension.Rows;

    //    // Keş lüğətləri (Yenilənmiş unikal açarlarla təkrarlanmanın qarşısı alınır)
    //    var customerCache = new Dictionary<string, Guid>();     // Key: DriverPhone (IdentityNo) -> Value: Customer Guid
    //    var customerCodeCache = new Dictionary<long, Guid>();   // Key: PersonAisCode (CustomerCode) -> Value: Customer Guid
    //    var saleCache = new Dictionary<int, Guid>();           // Key: OldSaleId -> Value: Sale Guid

    //    // Bazadakı aktiv ölkələrin xəritəsi (LegacyId -> Guid Id)
    //    var countryMap = await _context.Set<Country>()
    //        .AsNoTracking()
    //        .Where(c => c.IsActive)
    //        .ToDictionaryAsync(c => c.LegacyId, c => c.Id);

    //    // 💡 DEFAULT ÖLKƏ TƏYİNATI: Satış cədvəlinin boş qalmaması üçün FK qoruması
    //    var defaultCountryGuid = await _context.Set<Country>()
    //        .Select(c => c.Id)
    //        .FirstOrDefaultAsync();

    //    if (defaultCountryGuid == Guid.Empty)
    //    {
    //        return BadRequest("XƏTA: Cədvəllərin dolması üçün əvvəlcə 'Countries' cədvəlinə ən azı 1 ölkə (məs. LegacyId = 57 olan) əlavə etməlisiniz!");
    //    }

    //    // =========================================================================
    //    // MƏRHƏLƏ 1: UNİKAL MÜŞTƏRİLƏRİN (CUSTOMER) YARADILMASI
    //    // =========================================================================
    //    for (int row = 2; row <= rowCount; row++)
    //    {
    //        // ✅ DOĞRU: N sütunu = 14 (DriverPhone) -> Bu müştərinin IdentityNo-sudur!
    //        var identityNo = worksheet.Cells[row, 14].Value?.ToString()?.Trim();

    //        // ✅ DOĞRU: K sütunu = 11 (PersonAisCode) -> Bu müştərinin CustomerCode-dur!
    //        long.TryParse(worksheet.Cells[row, 11].Value?.ToString(), out long customerCode);

    //        if (string.IsNullOrEmpty(identityNo))
    //            continue;

    //        // Təkrarlanma qorumaları (Keş yoxlanışı)
    //        if (customerCache.ContainsKey(identityNo))
    //            continue;

    //        if (customerCodeCache.TryGetValue(customerCode, out Guid cachedGuidByCode))
    //        {
    //            customerCache[identityNo] = cachedGuidByCode;
    //            continue;
    //        }

    //        // Bazada dublikat yoxlanışı
    //        var existingCustomer = await _context.Set<Customer>()
    //            .FirstOrDefaultAsync(c => c.IdentityNo == identityNo || c.CustomerCode == customerCode);

    //        if (existingCustomer != null)
    //        {
    //            customerCache[identityNo] = existingCustomer.Id;
    //            customerCodeCache[customerCode] = existingCustomer.Id;
    //            continue;
    //        }

    //        var lastName = worksheet.Cells[row, 12].Value?.ToString()?.Trim();  // L sütunu = 12
    //        var firstName = worksheet.Cells[row, 13].Value?.ToString()?.Trim(); // M sütunu = 13

    //        var customerType = (identityNo.Length == 10 && long.TryParse(identityNo, out _))
    //            ? CustomerType.Legal
    //            : CustomerType.Individual;

    //        // Müştərinin ölkəsi (O sütunu = 15 -> NationalityId)
    //        Guid? customerCountryGuid = null;
    //        var excelCustCountryVal = worksheet.Cells[row, 15].Value;
    //        if (excelCustCountryVal != null && int.TryParse(excelCustCountryVal.ToString().Trim(), out int custOldCountryId))
    //        {
    //            if (countryMap.TryGetValue(custOldCountryId, out Guid matchedGuid))
    //                customerCountryGuid = matchedGuid;
    //        }

    //        var customer = new Customer
    //        {
    //            Id = Guid.NewGuid(),
    //            CustomerCode = customerCode, // ✅ PersonAisCode bura oturdu
    //            Type = customerType,
    //            IdentityNo = identityNo,     // ✅ DriverPhone bura oturdu
    //            FirstName = firstName,
    //            LastName = lastName,
    //            CompanyName = customerType == CustomerType.Legal ? $"{lastName} {firstName}".Trim() : null,
    //            CitizenshipCountryId = customerCountryGuid,
    //            CreatedAt = DateTime.UtcNow,
    //            UpdatedAt = DateTime.UtcNow
    //        };

    //        await _context.Set<Customer>().AddAsync(customer);

    //        customerCache[identityNo] = customer.Id;
    //        customerCodeCache[customerCode] = customer.Id;
    //    }
    //    await _context.SaveChangesAsync();


    //    // =========================================================================
    //    // MƏRHƏLƏ 2: SATIŞLARIN (SALE) YARADILMASI
    //    // =========================================================================
    //    for (int row = 2; row <= rowCount; row++)
    //    {
    //        // A sütunu = 1 (OldSaleId)
    //        if (!int.TryParse(worksheet.Cells[row, 1].Value?.ToString(), out int oldSaleId))
    //            continue;

    //        // Əgər bu satış ID-si yaddaşda artıq varsa, təkrar master satış obyekti yaratmırıq
    //        if (saleCache.ContainsKey(oldSaleId))
    //            continue;

    //        long.TryParse(worksheet.Cells[row, 2].Value?.ToString(), out long saleCode);         // B sütunu = 2 (SaleAisCode)
    //        DateTime.TryParse(worksheet.Cells[row, 3].Value?.ToString(), out DateTime saleDate); // C sütunu = 3 (SaleDate)
    //        DateTime.TryParse(worksheet.Cells[row, 4].Value?.ToString(), out DateTime payDate);   // D sütunu = 4 (PayDate)
    //        decimal.TryParse(worksheet.Cells[row, 5].Value?.ToString(), out decimal saleMainAmount); // E sütunu = 5 (SaleMainAmount)
    //        var cargoType = worksheet.Cells[row, 6].Value?.ToString();                           // F sütunu = 6 (CargoType)
    //        Enum.TryParse(worksheet.Cells[row, 7].Value?.ToString(), out SaleType saleType);     // G sütunu = 7 (SaleType)

    //        // Müştəri əlaqələndirilməsi (N sütunundakı telefona görə keşdən Guid-i tapırıq)
    //        var identityNo = worksheet.Cells[row, 14].Value?.ToString()?.Trim() ?? "";
    //        var customerId = customerCache.GetValueOrDefault(identityNo);

    //        if (customerId == Guid.Empty)
    //            continue;

    //        // Sürücü vətəndaşlıq ID-si (O sütunu = 15 -> NationalityId)
    //        Guid finalDriverCountryGuid = defaultCountryGuid;
    //        var excelSaleCountryValue = worksheet.Cells[row, 15].Value;
    //        if (excelSaleCountryValue != null && int.TryParse(excelSaleCountryValue.ToString().Trim(), out int saleOldCountryId))
    //        {
    //            if (countryMap.TryGetValue(saleOldCountryId, out Guid realCountryGuidId))
    //            {
    //                finalDriverCountryGuid = realCountryGuidId;
    //            }
    //        }

    //        var sale = new Sale
    //        {
    //            Id = Guid.NewGuid(),
    //            SaleCode = saleCode,
    //            SaleType = saleType,
    //            Status = SaleStatus.Paid,
    //            CustomerId = customerId,

    //            CustomerNameSnapshot = (worksheet.Cells[row, 12].Value?.ToString() + " " + worksheet.Cells[row, 13].Value?.ToString()).Trim(),
    //            CustomerIdentitySnapshot = identityNo,
    //            CustomerTypeSnapshot = (identityNo.Length == 10) ? CustomerType.Legal : CustomerType.Individual,

    //            Phone = worksheet.Cells[row, 14].Value?.ToString() ?? "N/A",        // N sütunu = 14 (DriverPhone)
    //            CargoType = cargoType,
    //            VehiclePlate = worksheet.Cells[row, 10].Value?.ToString() ?? "N/A", // J sütunu = 10 (VehiclePlate)
    //            TotalAmount = saleMainAmount,
    //            CreatedAt = saleDate == DateTime.MinValue ? DateTime.UtcNow : saleDate,
    //            PaidAt = payDate == DateTime.MinValue ? null : payDate,

    //            DriverCitizenshipId = finalDriverCountryGuid
    //        };

    //        await _context.Set<Sale>().AddAsync(sale);
    //        saleCache[oldSaleId] = sale.Id;
    //    }
    //    await _context.SaveChangesAsync(); // Sales cədvəli artıq tam dolacaq!


    //    // =========================================================================
    //    // MƏRHƏLƏ 3: SATIŞ DETALLARININ (SALEITEM) YARADILMASI
    //    // =========================================================================
    //    for (int row = 2; row <= rowCount; row++)
    //    {
    //        // Q sütunu = 17 (DetailSaleId) -> Master satışın Excel-dəki köhnə ID-si
    //        if (!int.TryParse(worksheet.Cells[row, 17].Value?.ToString(), out int detailSaleId))
    //            continue;

    //        if (!saleCache.TryGetValue(detailSaleId, out Guid originalSaleGuid))
    //            continue;

    //        int.TryParse(worksheet.Cells[row, 18].Value?.ToString(), out int serviceId);       // R sütunu = 18 (ServiceId)
    //        decimal.TryParse(worksheet.Cells[row, 19].Value?.ToString(), out decimal price);    // S sütunu = 19 (DetailPrice)
    //        int.TryParse(worksheet.Cells[row, 20].Value?.ToString(), out int quantity);     // T sütunu = 20 (DetailQuantity)
    //        decimal.TryParse(worksheet.Cells[row, 21].Value?.ToString(), out decimal total);    // U sütunu = 21 (DetailTotalAmount)

    //        var saleItem = new SaleItem
    //        {
    //            Id = Guid.NewGuid(),
    //            SaleId = originalSaleGuid,
    //            Quantity = quantity <= 0 ? 1 : quantity,
    //            UnitPrice = price,
    //            Total = total,
    //            ServiceName = serviceId == 1 ? "Terminal (MS)" : $"Xidmət #{serviceId}",
    //            Unit = "ədəd"
    //        };

    //        await _context.Set<SaleItem>().AddAsync(saleItem);
    //    }

    //    await _context.SaveChangesAsync();

    //    return Ok(new { Message = "Sinxronizasiya uğurla tamamlandı. Sales və Customer tam doldu!", TotalRows = rowCount - 1 });
    //}


    [HttpPost]
    public async Task<IActionResult> ImportExcel(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("Zəhmət olmasa düzgün Excel faylı seçin.");

        using var stream = new MemoryStream();
        await file.CopyToAsync(stream);
        using var package = new ExcelPackage(stream);

        var worksheet = package.Workbook.Worksheets[0];
        var rowCount = worksheet.Dimension.Rows;

        var customerCache = new Dictionary<string, Guid>();
        var saleCache = new Dictionary<int, Guid>();

        // =========================================================================
        // 💡 XÜSUSİ SABİT ID-LƏR
        // =========================================================================
        var defaultDepartmentGuid = Guid.Parse("E62C057C-C249-41AC-A55B-D8D33F2F70BE");
        var defaultOperatorGuid = Guid.Parse("8C5E59A7-A441-413F-A917-AB7F3DBCDC6E");

        var countryMap = await _context.Set<Country>()
            .AsNoTracking()
            .Where(c => c.IsActive)
            .ToDictionaryAsync(c => c.LegacyId, c => c.Id);

        var defaultCountryGuid = await _context.Set<Country>()
            .Select(c => c.Id)
            .FirstOrDefaultAsync();

        if (defaultCountryGuid == Guid.Empty)
        {
            return BadRequest("XƏTA: Cədvəllərin dolması üçün əvvəlcə 'Countries' cədvəlinə ən azı 1 ölkə əlavə etməlisiniz!");
        }

        // 💡 Xanadakı dəyəri oxuyub "NULL" stringi olub-olmadığını yoxlayan daxili köməkçi funksiya
        string GetCleanValue(int r, int c)
        {
            var val = worksheet.Cells[r, c].Value?.ToString()?.Trim();
            if (string.IsNullOrEmpty(val) || val.Equals("NULL", StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }
            return val;
        }

        // =========================================================================
        // MƏRHƏLƏ 1: UNİKAL MÜŞTƏRİLƏRİN (CUSTOMER) YARADILMASI
        // =========================================================================
        for (int row = 2; row <= rowCount; row++)
        {
            // M sütunu = 13 (VoenOrPassport)
            var identityNo = GetCleanValue(row, 13);
            long.TryParse(identityNo, out long customerCode);

            // Əgər unikal kod boşdursa və ya "NULL" idisə (artıq funksiya null qaytarır) keçirik
            if (string.IsNullOrEmpty(identityNo))
                continue;

            if (customerCache.ContainsKey(identityNo))
                continue;

            var existingCustomer = await _context.Set<Customer>()
                .FirstOrDefaultAsync(c => c.IdentityNo == identityNo || c.CustomerCode == customerCode);

            if (existingCustomer != null)
            {
                customerCache[identityNo] = existingCustomer.Id;
                continue;
            }

            // K=11 (LastName), L=12 (FirstName)
            var lastName = GetCleanValue(row, 11);
            var firstName = GetCleanValue(row, 12);

            var customerType = (identityNo.Length == 10 && long.TryParse(identityNo, out _))
                ? CustomerType.Legal
                : CustomerType.Individual;

            // O sütunu = 15 (NationalityId)
            Guid? customerCountryGuid = null;
            var excelCustCountryVal = GetCleanValue(row, 15);
            if (excelCustCountryVal != null && int.TryParse(excelCustCountryVal, out int custOldCountryId))
            {
                if (countryMap.TryGetValue(custOldCountryId, out Guid matchedGuid))
                    customerCountryGuid = matchedGuid;
            }

            var customer = new Customer
            {
                Id = Guid.NewGuid(),
                Type = customerType,
                IdentityNo = identityNo,
                FirstName = firstName,
                LastName = lastName,
                CompanyName = customerType == CustomerType.Legal ? $"{lastName} {firstName}".Trim() : null,
                CitizenshipCountryId = customerCountryGuid,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _context.Set<Customer>().AddAsync(customer);
            customerCache[identityNo] = customer.Id;
        }
        await _context.SaveChangesAsync();


        // =========================================================================
        // MƏRHƏLƏ 2: SATIŞLARIN (SALE) YARADILMASI
        // =========================================================================
        for (int row = 2; row <= rowCount; row++)
        {
            // A sütunu = 1 (OldSaleId)
            var oldSaleIdStr = GetCleanValue(row, 1);
            if (string.IsNullOrEmpty(oldSaleIdStr) || !int.TryParse(oldSaleIdStr, out int oldSaleId))
                continue;

            if (saleCache.ContainsKey(oldSaleId))
                continue;

            // B=2 (SaleDate), C=3 (PayDate), E=5 (CargoType)
            DateTime.TryParse(GetCleanValue(row, 2), out DateTime saleDate);
            DateTime.TryParse(GetCleanValue(row, 3), out DateTime payDate);
            var cargoType = GetCleanValue(row, 5);

            // U sütunu = 21 (DetailTotalAmount)
            decimal.TryParse(GetCleanValue(row, 21), out decimal detailTotalAmount);

            // F sütunu = 6 (SaleType)
            int.TryParse(GetCleanValue(row, 6), out int oldSaleTypeValue);
            JobManagement.Domain.Enums.SaleType finalSaleType = oldSaleTypeValue switch
            {
                1 => JobManagement.Domain.Enums.SaleType.Import,
                2 => JobManagement.Domain.Enums.SaleType.Import,
                4 => JobManagement.Domain.Enums.SaleType.Import,
                5 => JobManagement.Domain.Enums.SaleType.Import,
                7 => JobManagement.Domain.Enums.SaleType.Import,

                3 => JobManagement.Domain.Enums.SaleType.Export,
                6 => JobManagement.Domain.Enums.SaleType.Export,

                _ => JobManagement.Domain.Enums.SaleType.Import
            };

            // G sütunu = 7 (SalePayStatus)
            int.TryParse(GetCleanValue(row, 7), out int oldStatusValue);
            SaleStatus finalSaleStatus = oldStatusValue switch
            {
                0 => SaleStatus.Paid,
                1 => SaleStatus.Unpaid,
                2 => SaleStatus.Cancelled,
                _ => SaleStatus.Unpaid
            };

            var identityNo = GetCleanValue(row, 13) ?? "";
            var customerId = customerCache.GetValueOrDefault(identityNo);

            if (customerId == Guid.Empty)
                continue;

            // N sütunu = 14 (DriverPhone)
            var driverPhone = GetCleanValue(row, 14) ?? "";

            // O sütunu = 15 (NationalityId)
            Guid finalDriverCountryGuid = defaultCountryGuid;
            var excelSaleCountryValue = GetCleanValue(row, 15);
            if (excelSaleCountryValue != null && int.TryParse(excelSaleCountryValue, out int saleOldCountryId))
            {
                if (countryMap.TryGetValue(saleOldCountryId, out Guid realCountryGuidId))
                {
                    finalDriverCountryGuid = realCountryGuidId;
                }
            }

            var sale = new Sale
            {
                Id = Guid.NewGuid(),
                SaleType = finalSaleType,
                Status = finalSaleStatus,
                CustomerId = customerId,
                DepartmentId = defaultDepartmentGuid,
                OperatorId = defaultOperatorGuid,

                CustomerNameSnapshot = ($"{GetCleanValue(row, 11)} {GetCleanValue(row, 12)}").Trim(),
                CustomerIdentitySnapshot = identityNo,
                CustomerTypeSnapshot = (identityNo.Length == 10) ? CustomerType.Legal : CustomerType.Individual,

                Phone = driverPhone,
                CargoType = cargoType,
                VehiclePlate = GetCleanValue(row, 9) ?? "N/A", // I sütunu = 9
                TotalAmount = detailTotalAmount,

                CreatedAt = saleDate == DateTime.MinValue ? DateTime.UtcNow : saleDate,
                PaidAt = payDate == DateTime.MinValue ? null : payDate,

                DriverCitizenshipId = finalDriverCountryGuid
            };

            await _context.Set<Sale>().AddAsync(sale);
            saleCache[oldSaleId] = sale.Id;
        }
        await _context.SaveChangesAsync();


        // =========================================================================
        // MƏRHƏLƏ 3: SATIŞ DETALLARININ (SALEITEM) YARADILMASI
        // =========================================================================
        for (int row = 2; row <= rowCount; row++)
        {
            // Q sütunu = 17 (DetailSaleId)
            var detailSaleIdStr = GetCleanValue(row, 17);
            if (string.IsNullOrEmpty(detailSaleIdStr) || !int.TryParse(detailSaleIdStr, out int detailSaleId))
                continue;

            if (!saleCache.TryGetValue(detailSaleId, out Guid originalSaleGuid))
                continue;

            // R=18 (ServiceId), S=19 (DetailPrice), T=20 (DetailQuantity), U=21 (DetailTotalAmount)
            int.TryParse(GetCleanValue(row, 18), out int serviceId);
            decimal.TryParse(GetCleanValue(row, 19), out decimal price);
            int.TryParse(GetCleanValue(row, 20), out int quantity);
            decimal.TryParse(GetCleanValue(row, 21), out decimal total);

            var saleItem = new SaleItem
            {
                Id = Guid.NewGuid(),
                SaleId = originalSaleGuid,
                Quantity = quantity <= 0 ? 1 : quantity,
                UnitPrice = price,
                Total = total,
                ServiceName = serviceId == 1 ? "Terminal (MS)" : $"Xidmət #{serviceId}",
                Unit = "ədəd"
            };

            await _context.Set<SaleItem>().AddAsync(saleItem);
        }

        await _context.SaveChangesAsync();

        return Ok(new { Message = "Sinxronizasiya tekst 'NULL' qoruması və tam düzgün sütunlarla uğurla tamamlandı!", TotalRows = rowCount - 1 });
    }
}