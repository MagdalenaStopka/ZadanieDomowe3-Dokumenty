using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentProject.Managers
{

    public class OurIOCCenter
    {


        private Dictionary<Type, Type> Container;

        public void AddToContainer(Type type, Type type2)
        {
            if (Container.ContainsKey(type))
                return;

            Container.Add(type, type2);

        }

        public Type Get(Type type)
        {
            return Container[type];
        }


    }
}
