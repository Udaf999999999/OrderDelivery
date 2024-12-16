﻿using System;
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

    public abstract class Delivery
    {
        public string Address;
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
        public virtual void UpdateDeliveryDate()
        {

        }
    }
    /// <summary>
    /// доставка на дом. Этот тип будет подразумевать наличие курьера или передачу курьерской компании, 
    /// в нем будет располагаться своя, отдельная от прочих типов доставки логика.
    /// </summary>
    public abstract class HomeDelivery : Delivery
    {

        //public HomeDelivery()
        //{


        //}
        /* ... */

    }
    public class OurHomeDelivery : HomeDelivery
    {
        private DateTime CalculationTimeDelivery()//Реализовать данный метод
        {
            return DateTime.Now;
        }
        public override void UpdateDeliveryDate()
        {
            deliveryDate = CalculationTimeDelivery();
        }
    }
    public class Incity : OurHomeDelivery
    {

    }
    /// <summary>
    /// доставка в пункт выдачи. Здесь будет храниться какая-то ещё логика, 
    /// необходимая для процесса доставки в пункт выдачи, например, 
    /// хранение компании и точки выдачи, а также какой-то ещё информации.
    /// </summary>
    class PickPointDelivery : Delivery
    {
        /* ... */
    }
    /// <summary>
    ///  доставка в розничный магазин. 
    ///  Эта доставка может выполняться внутренними средствами компании
    ///  и совсем не требует работы с <внешними> элементами.
    /// </summary>
    class ShopDelivery : Delivery
    {
        /* ... */
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
            get { return (productCount[index].count, productCount[index].product);   }
            set 
            { productCount[index] = new ProductCount(value.Item2,value.Item1); }
        }

        // ... Другие поля
    }
}