using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;
using Core.Data;
using Core.Data.Stats;
using Core.Modules;

namespace Core.Modules
{
    public class IntegrityModule : MonoBehaviour
    {
        #region Cache
        private CoreModule core;
        #endregion

        public void SetCoreModule(CoreModule mod)
        {
            core = mod;
        }
    }
}