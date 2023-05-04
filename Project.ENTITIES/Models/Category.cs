using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public class Category:BaseEntity
    {
        public Category()
        {
            Products = new List<Product>();// bu ifadeyi MyInit cllasında henüz ef tetiklenmeden yani işlemlerimizi saf bir şekilde Ram'de başladığında bu categoryde produts iaimli propertysi null gelmesin diye yaptık. 
        }
        public string CategoryName { get; set; }
        public string Description { get; set; }

        //Relational properties
        public virtual List<Product> Products { get; set; }
    }
}
