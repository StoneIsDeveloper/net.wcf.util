using MagicEightBallServiceLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ServiceHost  serviceHost = new ServiceHost(typeof(MagicEightBallService) ))
            {
                serviceHost.Open();
                foreach (System.ServiceModel.Description.ServiceEndpoint se in serviceHost.Description.Endpoints)
                {
                    Console.WriteLine("Address:{0}", se.Address);
                    Console.WriteLine("Binding.Name:{0}", se.Binding.Name);
                    Console.WriteLine("Binding.Name:{0}", se.Binding.Name);

                }
                Console.ReadLine();



            }
        }
    }
}
