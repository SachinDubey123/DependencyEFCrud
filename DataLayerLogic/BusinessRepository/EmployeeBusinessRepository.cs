﻿using DataLayerLogic.Model;
using DomainLayer.Model;
using DomainLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DataLayerLogic.BusinessRepository
{
    public class EmployeeBusinessRepository : IEmployeeRepository
    {
        //public List<EmployeeViewModel> GetAllEmployee()
        //{
        //    throw new NotImplementedException();
        //}
        public EmployeeViewModel GetAllEmployees(int currentPage)
        {
            int maxRows = 10;
            List<EmployeeViewModel> List = new List<EmployeeViewModel>();
            using (CrudDemoDBEntities entities = new CrudDemoDBEntities())
            {
                EmployeeViewModel1 customerModel1 = new EmployeeViewModel1();
                EmployeeViewModel customerModel = new EmployeeViewModel();
                // CustomerModel customerModel = new CustomerModel();

                var name = (from customer in entities.Employees
                                            where customer.IsActive != false
                                            select customer)
                            .OrderBy(customer => customer.EmpId)
                            .Skip((currentPage - 1) * maxRows)
                            .Take(maxRows).ToList();

                double pageCount = (double)((decimal)entities.Employees.Count() / Convert.ToDecimal(maxRows));
                customerModel1.PageCount = (int)Math.Ceiling(pageCount);

                customerModel1.CurrentPageIndex = currentPage;


                //List = db.Employees
                //  .OrderBy(customer => customer.EmpId)
                //            .Skip((currentPage - 1) * maxRows)
                //            .Take(maxRows)
                //   .Select(x => new EmployeeViewModel
                //   {
                //       Name = x.Name,
                //       EmpId = x.EmpId,
                //       DepartmentId = x.DepartmentId,
                //       DepartmentName = x.Department.DepartmentName,
                //       Address = x.Address,
                //       IsActive = x.IsActive
                //   }).ToList();

                //double pageCount = (double)((decimal)db.Employees.Count() / Convert.ToDecimal(maxRows));
                //customerModel.PageCount = (int)Math.Ceiling(pageCount);

                //customerModel.CurrentPageIndex = currentPage;

                return customerModel;
            }
        }

        public EmployeeViewModel GetEmployee(int EmployeeId)
        {
            CrudDemoDBEntities db = new CrudDemoDBEntities();

            List<SelectListItem> listOfDeparts = new List<SelectListItem>();
            foreach (Department dept in db.Departments)
            {
                SelectListItem selectList = new SelectListItem()
                {
                    Text = dept.DepartmentName,
                    Value = dept.DepartmentId.ToString(),
                    Selected = dept.IsActive
                };
                listOfDeparts.Add(selectList);
            }
            EmployeeViewModel customerModel = new EmployeeViewModel();
            customerModel.Departs = listOfDeparts;



            if (EmployeeId > 0)
            {
                //Employee emp = from x in db.Employees
                //               join y in db.Ci on x.EmpId equals y.Countryid into ps
                //               from p in ps
                //               select new { x.is, x.name, x.countryid, p.countryname }

                Employee emp = db.Employees.SingleOrDefault(x => x.EmpId == EmployeeId && x.IsActive == true);
                customerModel.EmpId = emp.EmpId;
                customerModel.DepartmentId = emp.DepartmentId;
                customerModel.Name = emp.Name;
                customerModel.Address = emp.Address;

                customerModel.Gender = emp.Gender;
                customerModel.DOB = emp.DOB;
                customerModel.CityId = emp.CityId;
                customerModel.IsActive = emp.IsActive;

            }
            return customerModel;

        }

        public EmployeeViewModel CreateEmp(EmployeeViewModel model)
        {
            CrudDemoDBEntities db = new CrudDemoDBEntities();

            if (model.EmpId > 0)
            {
                //update
                Employee emp = db.Employees.SingleOrDefault(x => x.EmpId == model.EmpId && x.IsActive == true);
                emp.Name = model.Name;
                emp.Gender = model.Gender;
                emp.DOB = model.DOB;
                emp.Address = model.Address;
                emp.CityId = model.CityId;
                emp.DepartmentId = model.DepartmentId;
                emp.IsActive = model.IsActive;
                db.SaveChanges();


                //Employee emp = new Employee();
                //emp.Address = model.Address;
                //emp.Name = model.Name;
                //emp.DepartmentId = model.DepartmentId;

                //db.Employees.Add(emp);
                //db.SaveChanges();

                //int latestEmpId = emp.EmployeeId;


                //Site site = new Site();
                //site.SiteName = model.SiteName;
                //site.EmployeeId = latestEmpId;

                //db.Sites.Add(site);
                //db.SaveChanges();

            }
            else
            {
                //Insert
                Employee emp = new Employee();
                emp.Name = model.Name;
                emp.Gender = model.Gender;
                emp.DOB = model.DOB;
                emp.Address = model.Address;
                emp.CityId = model.CityId;

                emp.DepartmentId = model.DepartmentId;
                emp.IsActive = model.IsActive;
                db.Employees.Add(emp);
                db.SaveChanges();

            }
            return model;

        }

        public void DeleteEmp(int EmployeeId)
        {
            CrudDemoDBEntities db = new CrudDemoDBEntities();

            Employee emp = db.Employees.SingleOrDefault(x => x.IsActive == true && x.EmpId == EmployeeId);
            if (emp != null)
            {
                emp.IsActive = false;
                db.SaveChanges();
            }

            //  return RedirectToAction("Index");
        }
    }
}