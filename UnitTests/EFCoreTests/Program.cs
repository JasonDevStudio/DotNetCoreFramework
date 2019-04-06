using System;
using System.Collections.Generic;
using EFCore.DataAccess;
using EFCore.Entities;
using Newtonsoft.Json;

namespace EFCoreTests
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            //Z.EntityFramework.Extensions.LicenseManager.AddLicense("1638;100-Huawei", "77248321-6103-24d6-9a64-cbd2c71f7077");

            var eft = new EFHelperTest();
            var result = eft.InsertTest();
            var json = JsonConvert.SerializeObject(result);
            Console.WriteLine(json);
            
        }
    }
}
