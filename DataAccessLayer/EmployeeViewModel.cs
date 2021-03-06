﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
   public class EmployeeViewModel
    {
        public int EmpId { get; set; }
        public string Name { get; set; }
        public int Gender { get; set; }
        public System.DateTime DOB { get; set; }
        public string Address { get; set; }
        public int CityId { get; set; }
        public int DepartmentId { get; set; }
        public bool IsActive { get; set; }

        public string CityName { get; set; }

        public string DepartmentName { get; set; }
    }
}
