﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wa_liec
{
    public class cn
    {
        private static string con_sql = @"Data Source=192.168.1.253;Initial Catalog=dd_liec;User ID=u_liec;Password=dev_liec";
        private static string con_postgreSQL = @"Server=localhost;Port=5432; User Id = development; Password=dev_.0;Database = im";

        public static string cn_SQL
        {
            get
            {
                return con_sql;
            }
        }

        public static string cn_postgreSQL
        {
            get
            {
                return con_postgreSQL;
            }
        }
    }
}