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

            using (var efr = new EFHelper<XEContext>())
            {
                var userinfo = new UserInfo
                {
                    ID = 1,
                    ACCOUNT = "ywx236582",
                    CREATEDATE = DateTime.Now,
                    NAME = "Yaojie",
                    STATUS = "A"
                };

                var (state, msg) = efr.BulkInsert(new List<UserInfo> { userinfo });
                if (state == Commmon.Utility.Enums.EnumMethodState.Success)
                {
                    Console.WriteLine("Success");
                }else
                {
                    Console.WriteLine("Fail");
                }
            }
        }
    }
}
