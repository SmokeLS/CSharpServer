using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcoSelf_Server.Models
{
    public class ProductRepository
    {

        private readonly ServerDBContex serverDBContex;

        public ProductRepository(ServerDBContex serverDBContex)
        {
            this.serverDBContex = serverDBContex;
        }

        public IEnumerable<Product> Products(string BarCode) => serverDBContex.Products.Where(c => c.BarCode == BarCode);
        
        //Андрей по аналогии можешь допилить остальные методы, если нужно
        //Я делаю это в 6 утра и могу, что то упустить


    }
}
