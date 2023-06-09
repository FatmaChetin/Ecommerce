﻿using Bogus.DataSets;
using Project.COMMON.Tools;
using Project.DAL.Context;
using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Project.DAL.Init
{
    public class MyInit:CreateDatabaseIfNotExists<MyContext>
    {
        protected override void Seed(MyContext context)
        {

            #region Admin
            
            AppUser au= new AppUser();
            au.UserName = "Fatma";
            au.Password = DantexCrypt.Crypt("1234");
            au.Email = "fatmachetin88@gmail.com";
            au.UserRole = ENTITIES.Enums.UserRole.Admin;
            au.Active = true;
            context.AppUsers.Add(au);
            context.SaveChanges();

            #endregion
            // memberlar bogus kütüphanesi kullanılarak yapılmıştır o yüzden burada yaratılmıştır ki test edilebilmesi için.
            #region NormalUsers
            for (int i = 0; i < 10; i++)
            {
                AppUser ap = new AppUser();
                ap.UserName = new Internet("tr").UserName();
                ap.Password=new Internet("tr").Password();
                ap.Email = new Internet("tr").Email();
                context.AppUsers.Add(ap);
            }
            context.SaveChanges();

            // appuserprofile için sonradan for döngüsü oluşturuldu çünkü idlerinin birbirine denk gelmesini istiyoruz bire bir ilişkide önce id kullandığımız yerin döngüsü arkasından iste diğeri oluşturulur.
            for (int i = 2; i < 12; i++)
            {
                AppUserProfile apu = new AppUserProfile();
                apu.ID = 1;
                apu.FirstName = new Name("tr").FirstName();
                apu.LastName = new Name("tr").LastName();
                context.AppUserProfiles.Add(apu);
            }
            context.SaveChanges();
            #endregion

            #region CategoryAndProductsInformations
            for (int i = 0; i < 10; i++)
            {
                Category c = new Category();
                c.CategoryName = new Commerce("tr").Categories(1)[0];
                c.Description = new Lorem("tr").Sentence(10);

                for (int j =0; j < 300; j++)
                {
                    Product p= new Product();
                    p.ProductName = new Commerce("tr").ProductName();
                    p.UnitInStock = 100;
                    p.UnitPrice = Convert.ToDecimal(new Commerce("tr").Price());
                    p.ImagePath = new Images().Nightlife();
                    c.Products.Add(p);

                }
                context.Categories.Add(c);
                context.SaveChanges();
            }
            #endregion
        }
    }
}
