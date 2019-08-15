using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace gamecore
{

    public class GuiPrefabs
    {
        public static GameObject InputField { get { return GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/InputFieldWithCanvas3")); } }

        public static GameObject BasicText { get { return GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/BasicText2")); } }
    }


}
