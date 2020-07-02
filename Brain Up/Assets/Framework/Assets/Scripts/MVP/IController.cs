using Assets.Scripts.Framework.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Screens
{
    public abstract class IController<A,B, C> : SingleInstanceObject<C> where C: MonoBehaviour
        where A : IModel
        where B : IView
    {
        public A _model;
        public B _view;

        public bool IsActive { get; internal set; } = false;
    }
}
