﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactsAPI.Models
{
    public class MessageRespones
    {
        /// <summary>
        /// 状态码
        /// </summary>
        public int Stat { get; set; }
        /// <summary>
        /// 消息
        /// </summary>
        public string Mes { get; set; }

        public  void Suceess()
        {
            Stat = 1;
            Mes = "更新成功";
        }

        public void Fail()
        {
            Stat = -1;
            Mes = "更新失败";
        }
    }
}
