using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderDeliveryClassesConsole
{
    
    internal abstract class Delivery
    {
        public string Address;
    }
    /// <summary>
    /// доставка на дом. Этот тип будет подразумевать наличие курьера или передачу курьерской компании, 
    /// в нем будет располагаться своя, отдельная от прочих типов доставки логика.
    /// </summary>
    class HomeDelivery : Delivery
    {
        /* ... */
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

    class Order<TDelivery,TStruct> where TDelivery : Delivery
    {
        public TDelivery Delivery;

        public int Number;

        public string Description;

        public void DisplayAddress()
        {
            Console.WriteLine(Delivery.Address);
        }

        // ... Другие поля
    }
}
