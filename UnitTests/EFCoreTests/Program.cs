using System;
using System.Collections.Generic;
using EFCore.DataAccess;
using EFCore.Entities;

namespace EFCoreTests
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            Z.EntityFramework.Extensions.LicenseManager.AddLicense("1638;100-Huawei","77248321-6103-24d6-9a64-cbd2c71f7077");

            using (var efr = new EFHelper<XEContext>())
            {
                var userinfo = new UserInfo
                {
                    ID = 1,
                    ACCOUNT = "ywx236582",
                    CREATEDATE = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss:ffff}",
                    NAME = "Yaojie",
                    STATUS = "A"
                };
 
                var (state, msg) = efr.BulkInsert(new List<UserInfo> { userinfo });
                if (state == Commmon.Utility.Enums.EnumMethodState.Success)
                {
                    Console.WriteLine("Success");
                }else
                {
                    Console.WriteLine($"Fail,{msg}");
                }
            }
        }
    }
}
