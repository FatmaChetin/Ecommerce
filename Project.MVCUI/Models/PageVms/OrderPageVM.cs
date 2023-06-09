﻿using Project.ENTITIES.Models;
using Project.MVCUI.OuterRequestModel;
using Project.VM.PureVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.MVCUI.Models.PageVms
{
    public class OrderPageVM
    {
        public Order Order { get; set; }
        public List<OrderVM> Orders { get; set; }
        public PaymentRequestModel PaymentRM { get; set; }
    }
}