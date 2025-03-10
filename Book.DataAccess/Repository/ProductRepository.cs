﻿using Book.DataAccess.Data;
using Book.DataAccess.Repository.IRepository;
using Book.Models;
using Book.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.DataAccess.Repository;

public class ProductRepository : Repository<Product>, IProductRepository
{
    private readonly AppDbContext _db;
    public ProductRepository(AppDbContext db) : base(db)
    {
        _db = db;
    }


    public void Update(Product obj)
    {
        var objFromDb = _db.Products.FirstOrDefault(u => u.Id == obj.Id);
        if(objFromDb != null)
        {
            objFromDb.Title = obj.Title;
			objFromDb.Price = obj.Price;
			objFromDb.Price50 = obj.Price50;
			objFromDb.ListPrice = obj.ListPrice;
			objFromDb.Price100 = obj.Price100;
			objFromDb.Description = obj.Description;
            objFromDb.CategoryID= obj.CategoryID;
            objFromDb.Author = obj.Author;
            if(obj.ImageUrl != null )
            {
                objFromDb.ImageUrl = obj.ImageUrl;
            }
        }
    }
}