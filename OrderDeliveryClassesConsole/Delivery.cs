using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderDeliveryClassesConsole
{
    public enum Status
    {
        Complite,
        Underway,
        Cancelled
    }
    public abstract class Transport
    {

    }
    public class Truck:Transport
    {

    }
    public class Car:Transport
    {

    }
    public class Bike:Transport
    {

    }
    public class AvailableTransprorts
    {
        public Transport[] transports;
        public Transport GetTransport(int i)
        {
            return transports[i];
        }
    }
    public static class StringExtension
    {
        public static string RemoveComas(this string str)
        {
            return str.Replace(",", string.Empty);
        }
    }
    public abstract class Delivery
    {
        public string address;
        public string Address
        {
            get { return address; }
            set { address = value.RemoveComas(); }
        }

        protected DateTime deliveryDate;
        public DateTime DeliveryDate
        {
            get { return deliveryDate; }
        }
        public Status Status { get; set; }
        public void UpdateStatus(Status newStatus)
        {
            Status = newStatus;
        }
        public abstract void UpdateDeliveryDate();
    }
    /// <summary>
    /// доставка на дом. Этот тип будет подразумевать наличие курьера или передачу курьерской компании, 
    /// в нем будет располагаться своя, отдельная от прочих типов доставки логика.
    /// </summary>
    public abstract class HomeDelivery : Delivery
    {
        internal AvailableTransprorts availableTransprortsRef;
        public HomeDelivery(ref AvailableTransprorts availableTransprortsRef)
        {
            this.availableTransprortsRef = availableTransprortsRef;
        }
    }
    public abstract class OurHomeDelivery<TTransport> : HomeDelivery where TTransport : Transport
    {
        public OurHomeDelivery(ref AvailableTransprorts availableTransprortsRef) : base(ref availableTransprortsRef)
        {
        }

        internal abstract DateTime CalculationTimeDelivery(TTransport transport);

        
    }
    public class Incity<TTransport> : OurHomeDelivery<TTransport> where TTransport : Transport
    {
        public Incity(ref AvailableTransprorts availableTransprortsRef) : base(ref availableTransprortsRef)
        {
        }
        public override void UpdateDeliveryDate()
        {
            deliveryDate = CalculationTimeDelivery((TTransport)availableTransprortsRef.GetTransport(5));
        }

        internal override DateTime CalculationTimeDelivery(TTransport transport)
        {
            return DateTime.Now;
        }
    }
    public class InRegion : OurHomeDelivery<Truck>
    {
        public InRegion(ref AvailableTransprorts availableTransprortsRef) : base(ref availableTransprortsRef)
        {
        }
        public override void UpdateDeliveryDate()
        {
            deliveryDate = CalculationTimeDelivery((Truck)availableTransprortsRef.GetTransport(5));
        }

        internal override DateTime CalculationTimeDelivery(Truck transport)
        {
            return DateTime.Now;
        }
    }
    /// <summary>
    /// доставка в пункт выдачи. Здесь будет храниться какая-то ещё логика, 
    /// необходимая для процесса доставки в пункт выдачи, например, 
    /// хранение компании и точки выдачи, а также какой-то ещё информации.
    /// </summary>
    class PickPointDelivery : Delivery
    {
        /* ... */
        public override void UpdateDeliveryDate()
        {
        }
    }
    /// <summary>
    ///  доставка в розничный магазин. 
    ///  Эта доставка может выполняться внутренними средствами компании
    ///  и совсем не требует работы с <внешними> элементами.
    /// </summary>
    class ShopDelivery : Delivery
    {
        /* ... */
        public override void UpdateDeliveryDate()
        {
        }
    }

    public class Product
    {
        private int id;
        public string Title;
        public string Discripsion;
        public int Id
        {
            get { return id; }
            set
            {
                if (value >= 0)
                {
                    id = value;
                }
                else
                {
                    Console.WriteLine("Id can't be under zero!");
                }
            }
        }

        public Product(int id, string title, string discripsion)
        {
            this.id = id;
            Title = title;
            Discripsion = discripsion;
        }
        public Product(int id, string title)
        {
            this.id = id;
            Title = title;
            Discripsion = null;
        }
    }


    class Order<TDelivery, TStruct> where TDelivery : Delivery
    {
        public TDelivery Delivery;
        public class ProductCount
        {
            public Product product;
            public int count;
            public ProductCount(Product product, int count)
            {
                this.product = product;
                this.count = count;
            }
        }

        private ProductCount[] productCount;
        public static int maxProductCount = 10;

        public int id;
        public string CustumerName;

        public Order(string[] order)
        {
            if (order.Length > maxProductCount)
            {
                Console.WriteLine("Too many orders!");
            }
            else
            {
                productCount = new ProductCount[order.Length];
                for (int i = 0; i < order.Length; i++)
                {
                    //реализовать парсинг,

                }
            }
        }

        public void DisplayAddress()
        {
            Console.WriteLine(Delivery.Address);
        }

        public (int, Product) this[int index]//Переделать на картеж
        {
            get { return (productCount[index].count, productCount[index].product); }
            set
            { productCount[index] = new ProductCount(value.Item2, value.Item1); }
        }
        public static Order<TDelivery, TStruct> operator +(Order<TDelivery, TStruct> order, ProductCount productCount)
        {
            ProductCount[] newProductCount= new ProductCount[order.productCount.Length];
            for(int i = 0;i < order.productCount.Length;i++)
            {
                newProductCount[i] = order.productCount[i];
            }
            newProductCount[order.productCount.Length] = productCount;

            return order;
        }

        // ... Другие поля
    }
}