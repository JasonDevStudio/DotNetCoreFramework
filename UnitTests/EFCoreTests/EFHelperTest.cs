using Commmon.Utility.Enums;
using EFCore.DataAccess;
using EFCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EFCoreTests
{
    public class EFHelperTest
    {
        public EFHelperTest()
        { }

        public List<UserInfo> QueryTest()
        {
            using (var efr = new EFHelper<OracleContext>())
            {
                var list = efr.QueryAll<UserInfo>().ToList();
                return list;
            }
        }

        public (EnumMethodState state,string msg) InsertTest()
        {
            using (var efr = new EFHelper<OracleContext>())
            {
                var userinfo = new UserInfo
                {   
                    ID = DateTime.Now.Millisecond,
                    ACCOUNT = "zhangsan",
                    CREATEDATE = DateTime.Now,
                    NAME = "Yaojie",
                    STATUS = "A"
                };

               return efr.BulkInsert(new List<UserInfo> { userinfo },seq: "USERINFO_SEQ");                 
            }
        }

    }
}
