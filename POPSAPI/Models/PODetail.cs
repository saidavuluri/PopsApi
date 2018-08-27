﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POPS.Models
{
    using System;
    using System.Collections.Generic;

    public partial class PoDetailModel
    {
        public string PONO { get; set; }
        public string ITCODE { get; set; }
        public Nullable<int> QTY { get; set; }
        public string ITNAME { get; set; }
        public DateTime? PODATE { get; set; }

        public string SUPLNAME { get; set; }
        public string SUPLNO { get; set; }
    }
}