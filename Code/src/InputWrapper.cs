using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace gamecore { 

    public class InputWrapper : MonoBehaviour
    {
        // Start is called before the first frame update

        public RectTransform rectTransform;

        public GameObject inputbox;

        public void Start()
        {
            RectTransform r = inputbox.GetComponent<RectTransform>();
            r.rect.Set(0, 0, 300, 50);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}
