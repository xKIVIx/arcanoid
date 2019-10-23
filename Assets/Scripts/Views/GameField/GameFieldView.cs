using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arcanoid.Views
{
    public class GameFieldView : MonoBehaviour, IGameFieldView
    {

        public IUserSlideView UserSlideView => throw new System.NotImplementedException();

        public IBallView BallView => throw new System.NotImplementedException();

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
