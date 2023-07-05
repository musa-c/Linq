using System.Linq.Expressions;

namespace LinqProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Category> categories = new List<Category>
            {
                new Category {CategoryId = 1, CategoryName = "Bilgisayar"},
                new Category {CategoryId = 2, CategoryName = "Telefon"},
            };

            List<Product> products = new List<Product>
            {
                new Product {ProductId = 1, CategoryId = 1, ProductName = "Acer Laptop", QuatityPerUnit = "32 GB Ram", UnitPrice = 10000, UnitsInStok = 5},
                new Product {ProductId = 2, CategoryId = 1, ProductName = "Asus Laptop", QuatityPerUnit = "16 GB Ram", UnitPrice = 18000, UnitsInStok = 3},
                new Product {ProductId = 3, CategoryId = 1, ProductName = "Hp Laptop", QuatityPerUnit = "8 GB Ram", UnitPrice = 18000, UnitsInStok = 2},
                new Product {ProductId = 4, CategoryId = 2, ProductName = "Samsung Telefon", QuatityPerUnit = "4 GB Ram", UnitPrice = 5000, UnitsInStok = 15},
                new Product {ProductId = 5, CategoryId = 2, ProductName = "Apple Telefon", QuatityPerUnit = "4 GB Ram", UnitPrice = 8000, UnitsInStok = 0}
            };

            //Test(products);

            //GetProducts(products);
            //GetProductsLinq(products);

            //AnyTest(products);

            //FindTest(products);

            //FindAllTest(products);

            //AscDescTest(products);

            ClassicLinqTest(products);

        }

        private static void ClassicLinqTest(List<Product> products)
        {
            var result = from p in products
                         where p.UnitPrice > 6000
                         orderby p.UnitPrice descending, p.ProductName ascending
                         select new ProductDto { ProductId = p.ProductId, ProductName = p.ProductName, UnitPrice = p.UnitPrice };

            foreach (var product in result)
            {
                Console.WriteLine(product.ProductName);
            }
        }

        private static void AscDescTest(List<Product> products)
        {
            // Single line query
            var result = products.Where(p => p.ProductName.Contains("top")).OrderByDescending(p => p.UnitPrice).ThenByDescending(p => p.ProductName);
            // orderBy ascending(artan) ile sıralama demektir. descending(azalan) için orderByDescending kullanılır.
            // a-z alfabeye göre sıralama için ThenBy kullanılır. (z-a)tersi için ThenByDescending sıralama yapılır.
            // yukarıdaki kodda önce OrderByDescending'i uygular sonra ThenByDescending'i uygular.

            foreach (var product in result)
            {
                Console.WriteLine(product.ProductName);
            }
        }

        private static void FindAllTest(List<Product> products)
        {
            var result = products.FindAll(p => p.ProductName.Contains("top")); // uyan tüm elemanları getirir.
            Console.WriteLine(result);
        }

        private static void FindTest(List<Product> products)
        {
            // fonksiyon paramteresinde predicate var ie lambda expression ile yazılabilir.
            var result = products.Find(p => p.ProductId == 10); // bulursa nesne döner bulmazsa null döner.
            Console.WriteLine(result.ProductName);
        }

        private static void AnyTest(List<Product> products)
        {
            var result = products.Any(p => p.ProductName == "Acer Laptop"); // var ise true yok ise false döner.
            Console.WriteLine(result);
        }

        private static void Test(List<Product> products)
        {
            Console.WriteLine("Algoritmik-------------");

            foreach (var product in products)
            {
                if (product.UnitPrice > 5000 && product.UnitsInStok > 3)
                {
                    Console.WriteLine(product.ProductName);
                }
            }

            Console.WriteLine("Linq-----------------");

            var result = products.Where(p => p.UnitPrice > 5000 && p.UnitsInStok > 3);

            foreach (var product in result)
            {
                Console.WriteLine(product.ProductName);
            }
        }

        static List<Product> GetProducts(List<Product> products)
        {
            List<Product> filteredProducts = new List<Product>();

            foreach (var product in products)
            {
                if(product.UnitPrice > 5000 && product.UnitsInStok > 3)
                {
                    filteredProducts.Add(product);
                }
            }

            return filteredProducts;
        }

        static List<Product> GetProductsLinq(List<Product> products)
        {
            // where hepsini base'si olan IEnumerable döndürür. ToList() ile listeye çeviririz.
            return products.Where(p => p.UnitPrice > 5000 && p.UnitsInStok > 3).ToList();
        }
    }

    // Dto: Data transformation object(Veri dönüştürme nesnesi)
    class ProductDto
    {
        public int ProductId { get; set; }
        public string CategoryName { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
    }

    class Product
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public string ProductName { get; set; }
        public string QuatityPerUnit { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal UnitsInStok { get; set; }
    }

    class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}